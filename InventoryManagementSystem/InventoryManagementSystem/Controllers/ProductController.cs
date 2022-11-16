using InventoryManagementSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class ProductController : Controller
    {

        // GET: Product
        public ActionResult Index()
        {
            Supplier sup = new Supplier();
            sup.Suppliers = GetSuppliers();
            var products = Product.GetProducts();
            ViewData["Suppliers"] = sup.Suppliers;
            return View(products);
        }

        // GET: Product/Details/5
        public ActionResult Details(Guid id)
        {
            var products = Product.GetProducts();
            return View(products.Where(x => x.Id == id).FirstOrDefault());
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            Supplier sup = new Supplier();
            sup.Suppliers = GetSuppliers();
            ViewData["Suppliers"] = sup.Suppliers;
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                // assign Guid unique ID of the product
                product.Id = System.Guid.NewGuid();
                // JsonFile
                string haveJsonFile = System.IO.File.ReadAllText(Product.ProductFile);
                List<Product> obj = new List<Product>();

                // Deserialize the objects 
                if (!string.IsNullOrEmpty(haveJsonFile))
                {
                    JsonConvert.DeserializeObject<List<Product>>(haveJsonFile).ForEach(h =>
                    {
                        obj.Add(h);
                    });
                }

                // Add new record
                obj.Add(product);

                //Serialize the object for JSON file
                string newJsonResult = JsonConvert.SerializeObject(obj,
                                       Formatting.Indented);

                System.IO.File.WriteAllText(Product.ProductFile, newJsonResult);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(Guid id)
        {
            var products = Product.GetProducts();
            return View(products.Where(x => x.Id == id).FirstOrDefault());
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, Product product)
        {
            try
            {
                // Load JsonFile
                string haveJsonFile = System.IO.File.ReadAllText(Product.ProductFile);
                List<Product> objProduct = JsonConvert.DeserializeObject<List<Product>>(haveJsonFile);
                objProduct.ForEach(prod =>
                {
                    if (prod.Id == id)
                    {
                        prod.Name = product.Name;
                        prod.SKU = product.SKU;
                        prod.Quantity = product.Quantity;
                        prod.ManufacturerId = product.ManufacturerId;
                    }
                });

                //Serialize the object for JSON file
                string newJsonResult = JsonConvert.SerializeObject(objProduct, Formatting.Indented);
                System.IO.File.WriteAllText(Product.ProductFile, newJsonResult);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(Guid id)
        {
            var products = Product.GetProducts();
            return View(products.Where(x => x.Id == id).FirstOrDefault());
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, Product product)
        {
            try
            {
                // JsonFile
                string haveJsonFile = System.IO.File.ReadAllText(Product.ProductFile);
                List<Product> products = JsonConvert.DeserializeObject<List<Product>>(haveJsonFile);

                if (products != null && products.Count > 0)
                {
                    Product productToDelete = products.Where(x => x.Id == id).FirstOrDefault();

                    if (productToDelete != null)
                    {
                        products.Remove(productToDelete);
                    }
                }
                //Serialize the object for JSON file
                string newJsonResult = JsonConvert.SerializeObject(products, Formatting.Indented);
                System.IO.File.WriteAllText(Product.ProductFile, newJsonResult);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public List<SelectListItem> GetSuppliers()
        {
            var list = new List<SelectListItem>();
            try
            {
                // JsonFile
                string haveJsonFile = System.IO.File.ReadAllText(Supplier.SupplierFile);
                List<Supplier> suppliers = JsonConvert.DeserializeObject<List<Supplier>>(haveJsonFile);
                foreach (Supplier supplier in suppliers)
                {
                    list.Add(new SelectListItem { Text = supplier.SupplierName, Value = supplier.SupplierId.ToString() });
                }
            }
            catch (Exception ex)
            {

                list.Add(new SelectListItem { Text = ex.Message.ToString(), Value = "0" });
            }
            return list;
        }
    }
}
