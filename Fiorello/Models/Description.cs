using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Models
{
    public class Description
    {
        public int Id { get; set; }
        [Required,StringLength(250)]
        public string Video { get; set; }
        public string Title { get; set; }
        public string DescriptionText { get; set; }
        public ICollection<Opportunities> Opportunities { get; set; }
    }
}
