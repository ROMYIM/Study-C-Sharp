using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models
{
    [Table("ClientIdentityInfo")]
    public class ClientIdentityInfo
    {
        [Key]
        public string ClientId { get; set; }

        public string ClientName { get; set; }

        public string ClientSecret { get; set; }
    }
}