using System;

namespace ILSample
{
    public class Person
    {
        protected readonly int _age;
        protected readonly string _name;

        public Person(int age, string name)
        {
            _age = age;
            _name = name;
        }

        public virtual void Show(string name, int age)
        {
            Console.WriteLine(_age == age);
            Console.WriteLine(_name == name);
        }
    }
}