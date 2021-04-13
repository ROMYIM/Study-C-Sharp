using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCore.Models
{
    [Table("ChannelOptions")]
    public class ChannelEsubOptions
    {
        [Key]
        [Column("PostType")]
        public string PostType { get; set; }

        [Column("Channel")]
        public string Channel { get; set; }

        [Column(TypeName = "jsonb")]
        public EsubOptions Options { get; set; }
    }
}