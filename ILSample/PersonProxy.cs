using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ILSample
{
    public class PersonProxy
    {
        private static readonly Func<IServiceProvider, AspectDelegate> AspectFactory = services =>
        {
            var aspectBuilder = new AspectBuilder(services);
            var aspect = aspectBuilder.Build();
            return aspect;
        };

        private static readonly MethodInfo ShowMethod =
            typeof(Person).GetTypeInfo().DeclaredMethods.First(m => m.Name == "Show");

        private static readonly MethodInfo? BaiduAsyncMethod =
            typeof(Person).GetTypeInfo().GetMethod(nameof(Person.BaiduAsync));

        private Person _person;

        private IServiceProvider _services;

        public PersonProxy(int age, string name, IServiceProvider services)
        {
            _person = new Person(age, name);
            _services = services;
        }

        public void Show(string name, int age)
        {
            var methodFeature = new MethodFeature()
            {
                MethodInfo = ShowMethod,
                OriginalInstance = _person,
                Parameters = new object[] {name, age}
            };
            
            var aspectDelegate = AspectFactory(_services);
            var contextFactory = new AspectContextFactory(_services);
            var context = contextFactory.Create(methodFeature);

            aspectDelegate(context);
        }

        public async Task<string?> BaiduAsync(string url)
        {
            var methodFeature = new MethodFeature()
            {
                MethodInfo = ShowMethod,
                OriginalInstance = _person,
                Parameters = new object?[] {url}
            };
            
            var aspectDelegate = AspectFactory(_services);
            var contextFactory = new AspectContextFactory(_services);
            var context = contextFactory.Create(methodFeature);

            await aspectDelegate(context);

            return (string?) context.ReturnValue;
        }
        
    }
}