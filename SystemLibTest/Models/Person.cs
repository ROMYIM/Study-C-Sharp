using System.ComponentModel.DataAnnotations.Schema;

namespace SystemLibTest.Models
{
    [Table("person")]
    public class Person
    {
        private Person(){}
        
        [Column("name")]
        public string Name { get; private set; }

        [Column("age")]
        public int Age { get; private set; }

        [Column("adult")]
        public bool Adult { get; private set; }
    }
}