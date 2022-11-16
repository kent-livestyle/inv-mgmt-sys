using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.Mvc;

namespace InventoryManagementSystem.Models
{
    public class Supplier
    {
        /// <summary>
        /// supplier JSON file in App_Data
        /// </summary>
        public static string SupplierFile = HttpContext.Current.Server.MapPath("~/App_Data/suppliers.json");

        /// <summary>
        /// Supplier ID
        /// </summary>
        public Guid SupplierId { get; set; }
        
        /// <summary>
        /// Supplier Name
        /// </summary>
        [Display(Name = "Supplier Name")] 
        public string SupplierName { get;set; }
        
        /// <summary>
        /// Supplier Contact number
        /// </summary>
        [Display(Name = "Contact Number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// supplier email
        /// </summary>
        public string Email { get; set; }

        public static List<Supplier> GetSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();

            if (File.Exists(SupplierFile))
            {
                // File exists..
                string content = File.ReadAllText(SupplierFile);
                // Deserialize the objects 
                suppliers = JsonConvert.DeserializeObject<List<Supplier>>(content);

                // Returns the products, either empty list or containing the Client(s).
                return suppliers;
            }
            else
            {
                // Create the JSON product file
                File.Create(SupplierFile).Close();
                // Write data to it; [] means an array
                // List<Supplier> would throw an error if [] is not wrapping text
                File.WriteAllText(SupplierFile, "[]");

                // Re run the function
                GetSuppliers();
            }

            return suppliers;
        }

        public List<SelectListItem> Suppliers { get; set; }
    }
}