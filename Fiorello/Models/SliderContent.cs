using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.Models
{
    public class SliderContent
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Required,StringLength(250)]
        public string Image { get; set; }


    }
}
