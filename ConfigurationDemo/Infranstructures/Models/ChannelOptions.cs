using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace ConfigurationDemo.Infranstructures.Models
{
    [Table("ChannelOptions")]
    public class ChannelOptions<T>
    {
        [Key]
        public string PostType { get; set; }

        public string Channel { get; set; }

        private T _options;

        [NotMapped]
        public T Options 
        {
            set 
            {
                _options = value;
                _jsonOptions = JsonDocument.Parse(JsonSerializer.Serialize(_options));
            }
            get => _options;
        }

        private JsonDocument _jsonOptions;

        [Column("Options", TypeName = "jsonb")]
        public JsonDocument JsonOptions
        {
            get { return _jsonOptions; }
            set 
            { 
                _jsonOptions = value; 
                _options = JsonSerializer.Deserialize<T>(_jsonOptions.RootElement.GetRawText());
            }
        }
        
    }
}