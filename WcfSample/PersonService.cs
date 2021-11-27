using System;
using System.Collections.Generic;
using WcfSample.Models;

namespace WcfSample
{
    public class PersonService : IPersonService
    {
        public IEnumerable<Person> GetPersons()
        {
            return new Person[]
            {
                new Person()
                {
                    Name = "yim",
                    NickName = "romyim",
                    Age = 2
                }
            };
        }

        public Result<Person> CreatePerson(Person person)
        {
            Console.WriteLine("person created");
            return new Result<Person>()
            {
                Success = true,
                Message = "created",
                Data = person
            };
        }
    }
}