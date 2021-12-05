using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using DynamicProxy.Attributes;
using DynamicProxy.Extensions;
using DynamicProxy.Features;

namespace DynamicProxy
{
    public class DynamicTypeBuilder
    {
        private readonly ProxyTypeModule _module;

        private readonly IServiceProvider _services;

        private readonly TypeBuilder _typeBuilder;

        private readonly List<MethodFeature> _methodFeatures = new List<MethodFeature>();

        internal Type InstanceType { get; }

        internal IEnumerable<MethodFeature> ProxyMethodFeatures => _methodFeatures;
        
        internal ConstructorFeature InstanceConstructorFeatures { get; private set; }
        
        internal FieldFeature InstanceFieldFeature { get; private set; }
        
        internal FieldFeature ServicesFeature { get; private set; }

        public DynamicTypeBuilder(ProxyTypeModule module, Type instanceType, IServiceProvider services)
        {
            InstanceType = instanceType ?? throw new ArgumentNullException(nameof(instanceType));
            _typeBuilder = _module.ModuleBuilder.DefineType(InstanceType.Name, TypeAttributes.Public);
            _services = services;
        }

        public DynamicTypeBuilder ConfigureField(string fieldName, Type fieldType, out FieldFeature fieldFeature)
        {
            var fieldBuilder = _typeBuilder.DefineField(fieldName, fieldType, FieldAttributes.Private);
            fieldFeature = new FieldFeature()
            {
                FieldBuilder = fieldBuilder
            };

            return this;
        }

        public DynamicTypeBuilder ConfigureConstructor()
        {
            var constructors = InstanceType.GetTypeInfo().GetConstructors();
            if (constructors.Length > 1)   
            {
                Array.Sort(constructors, (ctor1, ctor2) => ctor2.GetParameters().Length - ctor1.GetParameters().Length);
                VerifyConstructorParameters();
            }

            var ctor = constructors[0];
            var parameterInfos = ctor.GetParameters();
            var originalParameterCount = parameterInfos.Length;
            var parameterTypes = new Type[originalParameterCount + 1];
            Array.Copy(parameterInfos.Select(p => p.ParameterType).ToArray(), parameterTypes, originalParameterCount);
            parameterTypes[originalParameterCount] = typeof(IServiceProvider);

            var constructorBuilder =
                _typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, parameterTypes);
            var il = constructorBuilder.GetILGenerator();
            il.Emit(OpCodes.Nop);
            il.Emit(OpCodes.Ldarg_0);
            for (var i = 1; i <= originalParameterCount; i++)
            {
                il.Emit(OpCodes.Ldarg, i);
            }
            il.Emit(OpCodes.Newobj, ctor);
            il.Emit(OpCodes.Stfld, InstanceFieldFeature.FieldBuilder);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg, originalParameterCount + 1);
            il.Emit(OpCodes.Stfld, ServicesFeature.FieldBuilder);
            il.Emit(OpCodes.Ret);

            InstanceConstructorFeatures = new ConstructorFeature()
            {
                ConstructorBuilder = constructorBuilder,
                ConstructorInfo = ctor
            };
            
            return this;
            
            void VerifyConstructorParameters()
            {
                var firstCtor = constructors[0];
                var parameterTypeSet = firstCtor.GetParameters().Select(p => p.ParameterType).ToImmutableHashSet();

                for (int i = 1; i < constructors.Length; i++)
                {
                    if (!parameterTypeSet.IsSupersetOf(constructors[i].GetParameters().Select(p => p.ParameterType)))
                        throw new InvalidOperationException("当构造函数不唯一时，必须有一个构造函数的参数是其他构造函数参数的超集");
                }
                
            }
        }

        public DynamicTypeBuilder ConfigureMethod(MethodInfo methodInfo)
        {
            var aspect = methodInfo.GetCustomAttribute<AspectAttribute>();
            Func<IServiceProvider, AspectDelegate> aspectFactory = services =>
            {
                var aspectBuilder = new AspectBuilder(services);
                if (aspect?.InterceptorTypes.Any() ?? false)
                {
                    foreach (var interceptorType in aspect.InterceptorTypes)
                    {
                        var interceptor = (IInterceptor) services.GetService(interceptorType);
                        aspectBuilder.AddInterceptor(interceptor);
                    }
                }

                AspectDelegate next = aspectBuilder.Build();
                return next;
            };
            
            

            var parameterInfos = methodInfo.GetParameters();
            var parameterTypes = parameterInfos.Select(p => p.ParameterType).ToArray();
            var methodBuilder = _typeBuilder.DefineMethod(methodInfo.Name, MethodAttributes.Public,
                CallingConventions.Standard, methodInfo.ReturnType, parameterTypes);

            var il = methodBuilder.GetILGenerator();
            il.Emit(OpCodes.Nop);
            il.Emit(OpCodes.Ldarg_0);
            
            il.Emit(OpCodes.Ldfld, ServicesFeature.FieldBuilder);
            
            
            return this;
        }
    }
}