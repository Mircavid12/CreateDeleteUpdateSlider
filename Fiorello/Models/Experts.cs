using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fiorello.Models
{
    public class Experts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        [Required, StringLength(150)]
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public FlowerExperts FlowerExperts { get; set; }
        public int FlowerExpertsId { get; set; }
    }
}
