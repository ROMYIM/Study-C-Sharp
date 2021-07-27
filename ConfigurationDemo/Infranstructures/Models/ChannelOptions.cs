using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace ConfigurationDemo.Infranstructures.Models
{
    [Table("ChannelOptions")]
    public class ChannelOptions
    {
        [Key]
        [Required(AllowEmptyStrings = false)]
        public string PostType { get; set; }

        public string Channel { get; set; }

        private JsonElement _jsonOptions;

        [Column("Options", TypeName = "jsonb")]
        public JsonElement JsonOptions
        {
            get { return _jsonOptions; }
            set 
            { 
                _jsonOptions = value; 
            }
        }
        
    }
}