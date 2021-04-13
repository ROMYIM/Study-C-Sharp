using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCore.Models
{
    [Table("ChannelOptions")]
    public class ChannelOptions<T>
    {
        [Key]
        public string PostType { get; set; }

        public string Channel { get; set; }

        [Column(TypeName = "json")]
        public T Options { get; set; }
    }
}