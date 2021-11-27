using System.Runtime.Serialization;

namespace WcfSample.Models
{
    [DataContract]
    public class Person
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string NickName { get; set; }

        [DataMember]
        public int Age { get; set; }
    }
}