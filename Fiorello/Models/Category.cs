using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required,StringLength(30)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeleteTime { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
