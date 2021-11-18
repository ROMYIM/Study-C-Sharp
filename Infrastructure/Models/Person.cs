using Nest;

namespace Infrastructure.Models
{
    [ElasticsearchType(RelationName = "person")]
    public class Person
    {
        [Keyword(Name = "name")]
        public string Name { get; set; }

        [Text(Name = "nick_name", Analyzer = "ik_max_word")]
        public string NickName { get; set; }

        [Number(NumberType.Integer)]
        public int Age { get; set; }

        [Object]
        public Job Job { get; set; }
    }
}