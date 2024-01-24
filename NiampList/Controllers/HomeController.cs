using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NiampList.Models;


namespace NiampList.Controllers
{
    public class HomeController : Controller
    {

        //  server-side pagination
       
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            Product_and_CategoryCURD db=new Product_and_CategoryCURD();
           List<Product> products = db.pagination(pageSize, page);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double) db.GetTotalProductCount() / pageSize);
          
           // ViewBag.TotalPages = (int)Math.Ceiling((double)dbContext.GetTotalProductCount() / PageSize);

            return View(products);
        }

        public ActionResult categorys()
        {
            ViewBag.Message = "Your Category page.";

            Product_and_CategoryCURD db = new Product_and_CategoryCURD();
            List<Category> obj = db.GetCategories();
            return View(obj);
        }

       

        public ActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category ctey)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    Product_and_CategoryCURD pro = new Product_and_CategoryCURD();
                    bool check = pro.AddCategory(ctey);
                    if (check == true)
                    {
                        TempData["InsertMessage"] = "Data Has beed Inserted Successfully";
                        ModelState.Clear();
                        return RedirectToAction("categorys");
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }



        public ActionResult Edit(int id)
        {
            Product_and_CategoryCURD pr= new Product_and_CategoryCURD();
            var row=pr.GetCategories().Find(model => model.CategoryId == id);
            return View(row);
        }

        [HttpPost]
        public ActionResult Edit(int id,Category cat)
        {
            if (ModelState.IsValid == true)
            {
                Product_and_CategoryCURD pro = new Product_and_CategoryCURD();
                bool check = pro.UpdateCategory(cat);
                if (check == true)
                {
                    TempData["UpdateMessage"] = "Data Has beed Updated Successfully.";
                    ModelState.Clear();
                    return RedirectToAction("categorys");
                }
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            Product_and_CategoryCURD pr = new Product_and_CategoryCURD();
            var row = pr.GetCategories().Find(model => model.CategoryId == id);
            return View(row);
        }

        [HttpPost]
        public ActionResult Delete(int id, Category cat)
        {
            
                Product_and_CategoryCURD pro = new Product_and_CategoryCURD();
                
                    bool check = pro.DeleteCategory(id);
                    if (check == true)
                    {
                        TempData["DeleteMessage"] = "Data Has beed Deleted Successfully.";

                        return RedirectToAction("categorys");
                    }         
            return View();
        }



        // Product

        // Product List
        public ActionResult Product()
        {
            ViewBag.Message = "Your  Product page.";
            Product_and_CategoryCURD db = new Product_and_CategoryCURD();
            List<Product> obj = db.GetProducts();
            return View(obj);

        }
        // Add Product
        public ActionResult ProductCreate()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ProductCreate(Product pro)
        {
            try
            {
                if (ModelState.IsValid == true)
                {
                    Product_and_CategoryCURD proandcat = new Product_and_CategoryCURD();
                    bool check = proandcat.AddProduct(pro);
                    if (check == true)
                    {
                        TempData["InsertMessage"] = "Data Has beed Inserted Successfully";
                        ModelState.Clear();
                        return RedirectToAction("Product");
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }


        // Update product

        public ActionResult UpdateProduct(int id)
        {
            Product_and_CategoryCURD pr = new Product_and_CategoryCURD();
            var row = pr.GetProducts().Find(model => model.ProductId == id);
            return View(row);
        }

        [HttpPost]
        public ActionResult UpdateProduct(int id, Product pro)
        {
            if (ModelState.IsValid == true)
            {
                Product_and_CategoryCURD proandcat = new Product_and_CategoryCURD();
                bool check = proandcat.UpdateProduct(pro);
                if (check == true)
                {
                    TempData["UpdateMessage"] = "Data Has beed Updated Successfully.";
                    ModelState.Clear();
                    return RedirectToAction("Product");
                }
            }
            return View();
        }

        // Delete Product
        public ActionResult ProductDelete(int id)
        {
            Product_and_CategoryCURD pr = new Product_and_CategoryCURD();
            var row = pr.GetProducts().Find(model => model.ProductId == id);
            return View(row);
        }

        [HttpPost]
        public ActionResult ProductDelete(int id, Product pro)
        {

            Product_and_CategoryCURD proandcat = new Product_and_CategoryCURD();

            bool check = proandcat.DeleteProduct(id);
            if (check == true)
            {
                TempData["DeleteMessage"] = "Data Has beed Deleted Successfully.";

                return RedirectToAction("Product");
            }
            return View();
        }
    }
}