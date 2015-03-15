using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

	public class ProductVM
	{
		[Required(ErrorMessage="Required.")]
		public int ProductID { get; set; }
		[Required(ErrorMessage="Required.")]
		public string ProductName { get; set; }

		//Config Fields Section
		public ConfigFieldVM ConfigFieldsModel { get; set; }

		// Config Values Section
		public ConfigValuesVM ConfigValuesModel { get; set; } 

		// Areas Section
		public ProductAreasVM ProductAreasModel { get; set; }

		//Other Products
		public SelectList AllProducts { get; private set; }

		public ProductTabs SelectedTab { get; set; }

		public ProductVM() {
			this.ProductID = 0;
			this.SelectedTab = ProductTabs.ConfigFields;
			this.AllProducts = new SelectList(Enumerable.Empty<SelectListItem>());
		}

		public ProductVM(TestingContext db, int? productID, ProductTabs selectedTab = ProductTabs.ConfigFields)
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

		public bool DbValidation(TestingContext db, ModelStateDictionary model)
		{
			return DbValidation(db, model, false);
		}

		public bool DbValidation(TestingContext db, ModelStateDictionary model, bool updating)
		{
			bool result = true;
			if(model == null)
				model = new ModelStateDictionary();

			if(db == null)
				return result;

			if (!updating)
			{
				if (db.Products.Any(p => p.Name.ToLower() == ProductName.ToLower()))
				{
					model.AddModelError("ProductName", "Product already exists.");
					result = false;
				}
			}
			return result;
		}
	}
}

