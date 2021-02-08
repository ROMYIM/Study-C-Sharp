using System.Collections;

namespace Domain.Identity.Entities
{
    public class Role
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public BitArray QueryPermissions { get; set; }

        public BitArray EditPermissions { get; set; }
    }
}