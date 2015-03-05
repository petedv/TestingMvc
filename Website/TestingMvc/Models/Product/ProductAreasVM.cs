using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using database;

namespace TestingMvc
{
	public class ProductAreasVM
	{
		public int ProductID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public IEnumerable<Area> ExistingAreas { get; set; }

		public ProductAreasVM() {}

		public ProductAreasVM(TestingContext db, int productID)
		{
			this.ProductID = productID;
			this.UpdateExistingAreas(db);
		}

		public void UpdateExistingAreas(TestingContext db)
		{
			this.ExistingAreas = db.Areas.Where(a => a.ProductID == this.ProductID).AsEnumerable();
		}

		public bool ValidateArea(ModelStateDictionary modelState, TestingContext db, Area areaToValidate)
		{
			if(areaToValidate.Name.Trim().Length == 0)
				modelState.AddModelError("Name", "Name is required.");
			if(db.Areas.Where(a => a.ProductID == areaToValidate.ProductID && a.Name == areaToValidate.Name).Any())
				modelState.AddModelError("Name", "Area already exists!");
			return modelState.IsValid;
		}
	}
}

