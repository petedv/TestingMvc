using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using database;

namespace TestingMvc
{
	public class TestCaseSearchVM
	{
		public IEnumerable<SelectListItem> ProductOptions { get; set; }
		public IEnumerable<SelectListItem> AreaOptions { get; set; }
		public IEnumerable<SelectListItem> ConfigOptions { get; set; }

		public int ProductID { get; set; }
		public int AreaID { get; set; }
		public int ConfigID { get; set; }
		public string TestIdSearch { get; set; }

		public IEnumerable<TestCase> MatchingTestCases { get; set; }

		public TestCaseSearchVM()
		{
			InitWithEmpty ();
		}

		public TestCaseSearchVM(TestingContext db, int productID)
		{
			InitWithEmpty ();
			this.ProductID = productID;
			UpdateOptions (db);
		}

		private void InitWithEmpty() {
			this.ProductOptions = Enumerable.Empty<SelectListItem> ();
			this.AreaOptions = Enumerable.Empty<SelectListItem> ();
			this.ConfigOptions = Enumerable.Empty<SelectListItem> ();
			this.MatchingTestCases = Enumerable.Empty<TestCase> ();
			this.TestIdSearch = "";
		}

		public void UpdateOptions(TestingContext db) {
			this.ProductOptions = db.Products.AsEnumerable ().Select (p => new SelectListItem () {
				Text = p.Name,
				Value = p.ProductID.ToString(),
				Selected = p.ProductID == this.ProductID
			});

			var prod = db.Products.Find (this.ProductID);
			if (prod != null) {
				this.ConfigOptions = db.Configs
					.Where (c => c.ProductID == this.ProductID).AsEnumerable ()
					.Select (c => new SelectListItem () {
						Text = c.Name,
						Value = c.ConfigID.ToString()
					}).ToList();

				this.AreaOptions = db.Areas
					.Where (a => a.ProductID == this.ProductID).AsEnumerable ()
					.Select (a => new SelectListItem () {
						Text = a.Name,
						Value = a.AreaID.ToString()
					}).ToList();
			}
		}

	}
}

