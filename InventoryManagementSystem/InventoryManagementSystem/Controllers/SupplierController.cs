using InventoryManagementSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Supplier
        public ActionResult Index()
        {
            var supplier = Supplier.GetSuppliers();
            return View(supplier);
        }

        // GET: Supplier/Details/5
        public ActionResult Details(Guid id)
        {
            var supplier = Supplier.GetSuppliers();
            return View(supplier.Where(x => x.SupplierId == id).FirstOrDefault());
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(Supplier supplier)
        {
            try
            {
                // assign Guid unique ID of the product
                supplier.SupplierId = System.Guid.NewGuid();

                // JsonFile
                string haveJsonFile = System.IO.File.ReadAllText(Supplier.SupplierFile);
                List<Supplier> supplierlist = new List<Supplier>();

                // Deserialize the objects 
                if (!string.IsNullOrEmpty(haveJsonFile))
                {
                    JsonConvert
                        .DeserializeObject<List<Supplier>>(haveJsonFile)
                        .ForEach(h =>
                        {
                            supplierlist.Add(h);
                        });
                }

                // Add new record
                supplierlist.Add(supplier);

                //Serialize the object for JSON file
                string newJsonResult = JsonConvert.SerializeObject(supplierlist,
                                       Formatting.Indented);

                System.IO.File.WriteAllText(Supplier.SupplierFile, newJsonResult);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(Guid id)
        {
            var supplier = Supplier.GetSuppliers();
            return View(supplier.Where(x => x.SupplierId == id).FirstOrDefault());
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, Supplier supplier)
        {
            try
            {
                // Load JsonFile
                string haveJsonFile = System.IO.File.ReadAllText(Supplier.SupplierFile);
                List<Supplier> objProduct = JsonConvert.DeserializeObject<List<Supplier>>(haveJsonFile);
                objProduct.ForEach(sup =>
                {
                    if (sup.SupplierId == id)
                    {
                        sup.SupplierName = supplier.SupplierName;
                        sup.PhoneNumber = supplier.PhoneNumber;
                        sup.Email = supplier.Email;
                    }
                });
                // Serialize the object for JSON file
                string newJsonResult = JsonConvert.SerializeObject(objProduct, Formatting.Indented);

                System.IO.File.WriteAllText(Supplier.SupplierFile, newJsonResult);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(Guid id)
        {
            var supplier = Supplier.GetSuppliers();
            return View(supplier.Where(x => x.SupplierId == id).FirstOrDefault());
        }

        // POST: Supplier/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, Supplier supplier)
        {
            try
            {
                // JsonFile
                string haveJsonFile = System.IO.File.ReadAllText(Supplier.SupplierFile);
                List<Supplier> suppliers = JsonConvert.DeserializeObject<List<Supplier>>(haveJsonFile);

                if (suppliers != null && suppliers.Count > 0)
                {
                    Supplier supplierToDiscontinue = suppliers.Where(x => x.SupplierId == id).FirstOrDefault();

                    if (supplierToDiscontinue != null)
                    {
                        suppliers.Remove(supplierToDiscontinue);
                    }
                }

                //Serialize the object for JSON file
                string newJsonResult = JsonConvert.SerializeObject(suppliers, Formatting.Indented);

                System.IO.File.WriteAllText(Supplier.SupplierFile, newJsonResult);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
