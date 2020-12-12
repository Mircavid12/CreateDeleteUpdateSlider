using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fiorello.DAL;
using Fiorello.Extention;
using Fiorello.Helpers;
using Fiorello.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Sliders.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (slider.Photo==null)
            {
                return View();
            }
            if (!slider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo","Please select image type file!");
                return View();
            }
            if (!slider.Photo.MaxSize(200))
            {
                ModelState.AddModelError("Photo", "File size must be less than 200KB");
                return View();
            }

            slider.Image = await slider.Photo.SaveImageAsync(_env.WebRootPath,"img");
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
               
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            Slider slider = await _context.Sliders.FindAsync(id);
            if (slider==null)
            {
                return NotFound();
            }
            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider slider = await _context.Sliders.FindAsync(id);
            if (slider == null)
            {
                return NotFound();
            }
            bool isDeleted= Helper.DeleteImage(_env.WebRootPath,"img",slider.Image);
            if (!isDeleted)
            {
                ModelState.AddModelError("","Error happened");
            }
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider sliderS = await _context.Sliders.FindAsync(id);
            if (sliderS == null)
            {
                return NotFound();
            }
            return View(sliderS);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Update")]
        public async Task<IActionResult> UpdatePost(int? id,Slider slider)
        {
            Slider sliderS = await _context.Sliders.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }
            if (slider.Photo == null)
            {
                return View(sliderS);
            }
            if (!slider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Please select image type file!");
                return View(sliderS);
            }
            if (!slider.Photo.MaxSize(200))
            {
                ModelState.AddModelError("Photo", "File size must be less than 200KB");
                return View(sliderS);
            }
            
            string path = Path.Combine(_env.WebRootPath, "img", sliderS.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Sliders.Remove(sliderS);

            string fileName = Guid.NewGuid().ToString() + slider.Photo.FileName;
            string paths = Path.Combine(_env.WebRootPath, "img", fileName);
            using (FileStream fileStream = new FileStream(paths, FileMode.Create))
            {
                await slider.Photo.CopyToAsync(fileStream);
            }
            slider.Image = fileName;
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
