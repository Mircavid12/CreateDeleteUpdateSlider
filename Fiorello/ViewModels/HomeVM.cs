using Fiorello.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public SliderContent SliderContent { get; set; }
        public List<Category> Categories { get; set; }
        public Description Descriptions { get; set; }
        public List<Opportunities> Opportunities { get; set; }
        public FlowerExperts FlowerExperts { get; set; }
        public List<Experts> Experts { get; set; }
    }
}
