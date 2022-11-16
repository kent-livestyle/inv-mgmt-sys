using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace InventoryManagementSystem.Models
{
    public class Product
    {
        /// <summary>
        /// Products file in JSON format in App_Data
        /// </summary>
        public static string ProductFile = HttpContext.Current.Server.MapPath("~/App_Data/products.json");

        /// <summary>
        /// Product unique ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product SKU
        /// </summary>
        public string SKU { get; set; }

        /// <summary>
        /// Product Quantity whether stock is available
        /// </summary>
        [Display(Name = "Availabity")]
        public int Quantity { get; set; }

        /// <summary>
        /// TODO: References of the supplier / manufacturer ???
        /// </summary>
        [Required]
        [Display(Name = "Supplier")]
        public string ManufacturerId { get; set; }
        public IEnumerable<SelectListItem> Supplier { get; set; }


        /// <summary>
        /// Get list of the Products in JSON file
        /// </summary>
        /// <returns></returns>
        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            if (File.Exists(ProductFile))
            {
                // File exists..
                string content = File.ReadAllText(ProductFile);
                // Deserialize the objects 
                products = JsonConvert.DeserializeObject<List<Product>>(content);

                // Returns the products, either empty list or containing the Client(s).
                return products;
            }
            else
            {
                // Create the JSON product file
                File.Create(ProductFile).Close();
                // Write data to it; [] means an array
                // List<Product> would throw an error if [] is not wrapping text
                File.WriteAllText(ProductFile, "[]");

                // Re run the function
                GetProducts();
            }

            return products;
        }
    }
}