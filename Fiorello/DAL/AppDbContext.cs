using Fiorello.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
        }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderContent> SliderContents { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Description> Descriptions { get; set; }
        public DbSet<Opportunities> Opportunities { get; set; }
        public DbSet<FlowerExperts> FlowerExperts { get; set; }
        public DbSet<Experts> Experts { get; set; }
        public DbSet<OurBlog> OurBlogs { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Bio> Bios { get; set; }
    }
}
