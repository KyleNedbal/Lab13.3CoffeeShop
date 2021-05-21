using Dapper.Contrib.Extensions;
using Lab13._2CoffeeShopProductList.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab13._2CoffeeShopProductList.Controllers
{
    public class ProductController : Controller
    {
        static MySqlConnection db = new MySqlConnection("Server = localhost; Database=lab13point2;Uid=root;Password=abc123");
        public IActionResult Index()
        {
            List<Product> prods = db.GetAll<Product>().ToList();
            return View(prods);
        }

        public IActionResult Details(string id)
        {
            Product prod = db.Get<Product>(id);
            if (prod == null)
            {
                return Content("No product found with that ID");
            }
            return View(prod);
        }

        public IActionResult AllBagels(string category)
        {
            List<Product> prods = db.GetAll<Product>().ToList();
            List<Product> bagels = new List<Product>();
            foreach (Product product in prods)
            {
                if (product.category == "bagel")
                {
                    bagels.Add(product);
                }
            }
            return View(bagels);
        }
        public IActionResult AllLattes(string category)
        {
            List<Product> prods = db.GetAll<Product>().ToList();
            List<Product> lattes = new List<Product>();
            foreach (Product product in prods)
            {
                if (product.category == "latte")
                {
                    lattes.Add(product);
                }
            }
            return View(lattes);
        }

        public IActionResult AddForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product prod)
        {
            db.Insert(prod);
            return RedirectToAction("Index","Home"); //not redirecting to home/index
        }
        
        public IActionResult Delete(int id)
        {
            Product prod = db.Get<Product>(id);
            db.Delete(prod);
            return RedirectToAction("Index", "Home"); //not redirecting to home/index
        }

        public IActionResult EditForm(int id)
        {
            Product prod = db.Get<Product>(id);
            return View(prod);
        }

        [HttpPost]
        public IActionResult Edit(Product prod)
        {
            db.Update(prod);
            return RedirectToAction("Index", "Home"); //not redirecting to home/index
        }
    }
}
