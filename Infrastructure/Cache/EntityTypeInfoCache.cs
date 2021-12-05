using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.Cache
{
    /// <summary>
    /// 泛型缓存。缓存实体类型的构造函数委托和属性设置为托
    /// </summary>
    /// <typeparam name="T">实体的类型参数。该实体必须是引用类型，且有无参构造函数</typeparam>
    public static class EntityTypeInfoCache<T> where T : class
    {
        private static readonly Type EntityType = typeof(T);

        private static readonly TypeInfo EntityTypeInfo = EntityType.GetTypeInfo();

        private static readonly Dictionary<string, Action<T, object>> Setters;

        public static IReadOnlyDictionary<string, Action<T, object>> PropertySetters => Setters;
        
        public static Func<T> ConstructInstance { get; }

        static EntityTypeInfoCache()
        {
            var constructorInfo =
                EntityTypeInfo
                    .GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .FirstOrDefault(c => c.GetParameters().Length == 0);
            if (constructorInfo == null)
                throw new NotSupportedException($"类型[{EntityTypeInfo.FullName}]没有无参构造函数");
            ConstructInstance = Expression.Lambda<Func<T>>(Expression.New(constructorInfo)).Compile(); 
            
            var properties = EntityTypeInfo.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (properties.Any())
            {
                Setters = new Dictionary<string, Action<T, object>>(properties.Length);
                foreach (var propertyInfo in properties)
                {
                    var setterMethod = propertyInfo.GetSetMethod(true);
                    if (setterMethod != null)
                    {
                        var name = propertyInfo.GetCustomAttribute<ColumnAttribute>()?.Name ?? propertyInfo.Name;
                        
                        var paramObj = Expression.Parameter(EntityType, "instance");
                        var paramValue = Expression.Parameter(typeof(object), "value");
                        var bodyObject = Expression.Convert(paramObj, EntityType);
                        var bodyValue = Expression.Convert(paramValue, propertyInfo.PropertyType);
                        var body = Expression.Call(paramObj, setterMethod, bodyValue);
                        var setter = Expression.Lambda<Action<T, object>>(body, paramObj, paramValue).Compile();
                        Setters[name] = setter;
                    }
                    
                }
            }
        }

        
    }
}