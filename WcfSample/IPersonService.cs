using System.Collections.Generic;
using System.ServiceModel;
using WcfSample.Models;

namespace WcfSample
{
    [ServiceContract]
    public interface IPersonService
    {
        [OperationContract]
        IEnumerable<Person> GetPersons();

        Result<Person> CreatePerson(Person person);
    }
}