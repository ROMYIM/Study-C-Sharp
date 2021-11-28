using System;
using System.Reflection;
using System.Reflection.Emit;

namespace DynamicProxy
{
    public class ProxyTypeModule
    {
        private const string AssemblyName = "Zikey";

        private const string ModuleName = "DynamicProxy";
        
        public IServiceProvider Services { get; }
        
        public AssemblyBuilder AssemblyBuilder { get; }
        
        public ModuleBuilder ModuleBuilder { get; }

        public ProxyTypeModule(IServiceProvider serviceProvider)
        {
            Services = serviceProvider;
            
            var assemblyName = new AssemblyName(AssemblyName);
            AssemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder = AssemblyBuilder.DefineDynamicModule(ModuleName);
            
        }
    }
}
