using System;

namespace ILSample
{
    public class PersonImplementaion : Person
    {

        public PersonImplementaion(int age, string name) : base(age, name)
        {
        }

        public override void Show(string name, int age)
        {
            base.Show(name, age);
        }
    }
}