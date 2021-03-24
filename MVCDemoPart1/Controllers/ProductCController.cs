using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCDemoPart1.Models;

namespace MVCDemoPart1.Controllers
{
    public class ProductCController : Controller
    {
        static List<Product> listProducts = new List<Product>() {
            new Product { Id = 1, Name = "Iphone", Price = 300 },
            new Product { Id = 2, Name = "SS Phone", Price = 444 }
        };
        public IActionResult Search(string searchText)
        {
            if (searchText == null || searchText.Length==0)
            {
                ViewBag.ErrorSearch = "Must enter something to search!";
                return View("Index",listProducts);
            }
            var r = listProducts.Where(p => p.Name.Contains(searchText));
            return View("Index", r.ToList());
        }
        public IActionResult Delete(int Id)
        {
            var productToDelete = listProducts.SingleOrDefault(p => p.Id == Id);
            listProducts.Remove(productToDelete);
            return RedirectToAction("Index");

        }

        public IActionResult CreateSubmit(Product product)
        {
            if (!listProducts.Exists(p=>p.Id==product.Id) )
            {
                listProducts.Add(product);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Id", "Id is not valid");
                return View("Create");
            }
            
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View(listProducts);
        }
    }
}
