using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using database;

namespace TestingMvc
{
	public enum ProductTabs : int
	{
		ConfigFields = 1,
		ConfigValues = 2,
		Areas = 3
	}

	public class EditProductVM
	{
		public int ProductID { get; set; }
		public string ProductName { get; set; }

		//Config Fields Section
		public ConfigFieldVM ConfigFieldsModel { get; set; }

		// Config Values Section
		public ConfigValuesVM ConfigValuesModel { get; set; } 

		// Areas Section
		public ProductAreasVM ProductAreasModel { get; set; }

		//Other Products
		public SelectList AllProducts { get; set; }

		public ProductTabs SelectedTab { get; set; }

		public EditProductVM() {
			this.ProductID = 0;
			this.SelectedTab = ProductTabs.ConfigFields;
			this.AllProducts = new SelectList(Enumerable.Empty<SelectListItem>());
		}

		public EditProductVM(TestingContext db, int? productID, ProductTabs selectedTab = ProductTabs.ConfigFields)
		{
			this.ProductID = productID ?? 0;
			this.SelectedTab = selectedTab;
			this.AllProducts = new SelectList(Enumerable.Empty<SelectListItem>());
			this.UpdateFromDB(db);
		}

		public void UpdateFromDB(TestingContext db) 
		{
			var options = db.Products.AsEnumerable().Select(p => new SelectListItem() {
				Value = p.ProductID.ToString(),
				Text = p.Name
			}).ToList();
			this.AllProducts = new SelectList(options, "Value", "Text", this.ProductID);

			var dbProd = db.Products.Find(this.ProductID);
			if(dbProd != null)
			{
				this.ProductName = dbProd.Name;

				this.ConfigFieldsModel = new ConfigFieldVM(db, this.ProductID);
				this.ConfigValuesModel = new ConfigValuesVM(db, this.ProductID);
				this.ProductAreasModel = new ProductAreasVM(db, this.ProductID);
			}
			else
			{
				this.ConfigFieldsModel = new ConfigFieldVM();
				this.ConfigValuesModel = new ConfigValuesVM();
				this.ProductAreasModel = new ProductAreasVM();
			}
		}
	}
}

