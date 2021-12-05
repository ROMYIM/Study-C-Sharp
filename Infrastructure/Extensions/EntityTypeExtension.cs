using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Infrastructure.Cache;

namespace Infrastructure.Extensions
{
    public static class EntityTypeExtension
    {
        /// <summary>
        /// 将<see cref="DataRow"/>转换成对应的实体类。通过特性<see cref="ColumnAttribute"/>的<c>Name</c>属性映射对应的列。
        /// 如果没有则默认按属性的名称映射。
        /// </summary>
        /// <param name="dataRow">需要转换的<see cref="DataRow"/></param>
        /// <param name="throwNotFound">标识。如果没有找到映射的属性是否抛出异常。<c>true</c>抛出异常。<c>false</c>忽略。默认是<c>false</c></param>
        /// <typeparam name="T">实体对应的类型参数。必须是引用类型，且必须有无参构造函数</typeparam>
        /// <returns>返回的实体</returns>
        /// <exception cref="ArgumentNullException"><see cref="DataRow"/>为空抛出异常</exception>
        /// <exception cref="KeyNotFoundException"><c>throwNotFound</c>为<c>true</c>且没有找到对应的设置属性抛出异常</exception>
        /// <exception cref="InvalidCastException"><see cref="DataRow"/>的数据类型与实体属性的数据类型不一致抛出异常</exception>
        public static T ToEntity<T>(this DataRow dataRow, bool throwNotFound = false) where T : class
        {
            if (dataRow == null)
                throw new ArgumentNullException(nameof(dataRow));

            var instance = EntityTypeInfoCache<T>.ConstructInstance();
            foreach (DataColumn column in dataRow.Table.Columns)
            {
                if (EntityTypeInfoCache<T>.PropertySetters.TryGetValue(column.ColumnName, out var setProperty))
                {
                    setProperty(instance, dataRow[column.ColumnName]);
                }
                else if (throwNotFound)
                    throw new KeyNotFoundException($"没有列[{column.ColumnName}]对应的属性");
            }

            return instance;
        }

        /// <summary>
        /// 将<see cref="DataTable"/>转换成对应的实体集合
        /// </summary>
        /// <param name="data">要转换的<see cref="DataTable"/>对象</param>
        /// <param name="throwNotFound">标识。如果没有找到映射的属性是否抛出异常。<c>true</c>抛出异常。<c>false</c>忽略。默认是<c>false</c></param>
        /// <typeparam name="T">实体对应的类型参数。必须是引用类型，且必须有无参构造函数</typeparam>
        /// <returns>实体集合<see cref="IEnumerable{T}"/></returns>
        /// <exception cref="ArgumentNullException">data为空抛出异常</exception>
        public static IEnumerable<T> ToEntities<T>(this DataTable data, bool throwNotFound = false) where T : class
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            foreach (DataRow row in data.Rows)
            {
                yield return row.ToEntity<T>(throwNotFound);
            }
        }
    }
}