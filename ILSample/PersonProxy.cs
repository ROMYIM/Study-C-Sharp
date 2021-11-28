using System;
using System.Threading.Tasks;

namespace ILSample
{
    public class PersonProxy
    {
        private Person _person;

        private Guid _guid;

        public PersonProxy(int age, string name, Guid guid)
        {
            _person = new Person(age, name);
            _guid = guid;
        }

        public void Show(string name, int age)
        {
            var context = new AspectContext();
            context.Instance = _person;
            context.Parameters = new object?[] {name, age};
            AspectDelegate @delegate = aspectContext =>
            {
                aspectContext.ReturnValue =
                    aspectContext.Method.Invoke(aspectContext.Instance, aspectContext.Parameters);
                return Task.CompletedTask;
            };
            @delegate.Invoke(context);
        }
    }
}