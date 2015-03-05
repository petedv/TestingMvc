using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using database;

namespace TestingMvc
{
	public class TestCaseVM
	{
		//Project Details
		[Required(ErrorMessage="Product is required")]
		public int ProductID { get; set; }
		[Required(ErrorMessage="Functional Area is required")]
		public int AreaID { get; set; }

		public SelectList Products { get; set; }
		public SelectList Areas { get; set; }

		//Test Identification
		public int? TestCaseID { get; set; }
		[Required(ErrorMessage="Test ID is required")]
		public string TestCaseCode { get; set; }
		public string Requirement { get; set; }
		[Range(1, 5)]
		public int Priority { get; set; }
		[Required(ErrorMessage="Title is required")]
		public string Title { get; set; }

		public SelectList Priorities { get; set; }

		//Test Details
		[Required(ErrorMessage="Test Steps are required")]
		public string Steps { get; set; }
		public string Preconditions { get; set; }
		[Required(ErrorMessage="Expected Outcome is required")]
		public string ExpectedOutcome { get; set; }

		//Configurations
		public IEnumerable<Config> Configs { get; set; }
		[Required(ErrorMessage="Valid configs are required")]
		public int[] SelectedConfigs { get; set; }

		public TestCaseVM()
		{
			Products = new SelectList(Enumerable.Empty<SelectListItem>());
			Areas = new SelectList(Enumerable.Empty<SelectListItem>());
			Priorities = new SelectList(Enumerable.Empty<SelectListItem>());
			Configs = Enumerable.Empty<Config>();
		}

		public TestCaseVM(TestingContext db)
		{
			UpdateFields(db);
		}

		private static int[] _testCasePriorities = Enumerable.Range(1, 5).ToArray();

		public void UpdateFields(TestingContext db)
		{
			var prods = db.Products.ToList();
			Products = new SelectList(prods.Select(p => new SelectListItem() { 
				Text = p.Name, 
				Value = p.ProductID.ToString() 
			}), "Value", "Text", ProductID);

			var ars = db.Areas.Where(a => a.ProductID == ProductID).ToList();
			Areas = new SelectList(ars.Select(a => new SelectListItem() {
				Text=a.Name,
				Value=a.AreaID.ToString()
			}), "Value", "Text", AreaID);

			Priorities = new SelectList(_testCasePriorities.Select(p => new SelectListItem() {
				Text = p.ToString(),
				Value = p.ToString()
			}), "Value", "Text");

			var confs = db.Configs.Where(c => c.ProductID == ProductID).ToList();
			Configs = confs.AsEnumerable();
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

			if (db.Products.Find(ProductID) == null)
			{
				result = false;
				model.AddModelError("ProductID", "Product selected not found in db.");
			}
				
			if (db.Areas.Find(AreaID) == null)
			{
				result = false;
				model.AddModelError("AreaID", "Area selected not found in db.");
			}

			if(!updating)
			{
				if(db.TestCases.Where(tc => tc.ProductID == ProductID && tc.TestCaseCode.ToLower() == TestCaseCode.ToLower()).Any())
				{
					result = false;
					model.AddModelError("TestCaseCode", "Test ID must be unique.");
				}
			}
			return result;
		}
	}
}

