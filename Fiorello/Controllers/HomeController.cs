using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiorello.DAL;
using Fiorello.Models;
using Fiorello.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Fiorello.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            HttpContext.Session.SetString("name","Kamran");
            HomeVM homeVM = new HomeVM
            {
                Sliders = _db.Sliders.ToList(),
                SliderContent = _db.SliderContents.FirstOrDefault(),
                Categories=_db.Categories.Where(c=>c.IsDeleted==false).ToList(),
                Descriptions=_db.Descriptions.FirstOrDefault(),
                Opportunities=_db.Opportunities.Include(o=>o.Description).ToList(),
                FlowerExperts=_db.FlowerExperts.Where(fe=>fe.IsDeleted==false).FirstOrDefault(),
                Experts=_db.Experts.Include(fe=>fe.FlowerExperts).Where(e=>e.IsDeleted==false).ToList(),
            };
            return View(homeVM);
        }
        public async Task<IActionResult> AddBasket(int id)
        {
            Product product = await _db.Products.FindAsync(id);
            if (product==null)
            {
                return NotFound();
            }
            List<BasketVM> basket;
            if (Request.Cookies["basket"]!=null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }
            BasketVM isExist = basket.FirstOrDefault(p => p.Id == id);
            if (isExist==null)
            {
                basket.Add(new BasketVM
                {
                    Id = id,
                    Count = 1
                });
            }
            else
            {
                isExist.Count += 1;
            }
            Response.Cookies.Append("basket",JsonConvert.SerializeObject(basket));
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Basket()
        {
            List<BasketVM> dbBasket = new List<BasketVM>();
            ViewBag.Total = 0;
            if (Request.Cookies["basket"]!=null)
            {
                List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
                foreach (BasketVM pro in basket)
                {
                    Product dbProduct = await _db.Products.FindAsync(pro.Id);
                    pro.Title = dbProduct.Title;
                    pro.Price = dbProduct.Price * pro.Count;
                    pro.Image = dbProduct.Image;
                    dbBasket.Add(pro);
                    ViewBag.Total += pro.Price;
                }
            }
            return View(dbBasket);
        }
        public IActionResult Delete(int id)
        {
            List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            BasketVM pro = basket.FirstOrDefault(p=>p.Id==id);
            basket.Remove(pro);
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));
            return RedirectToAction(nameof(Basket));
        }
    }
}
