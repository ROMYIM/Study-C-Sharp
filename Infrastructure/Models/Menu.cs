using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Models
{
    public class Menu
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public List<Menu> Children { get; set; } = new List<Menu>();

        public bool IsParent => Children.Any();
    }
}