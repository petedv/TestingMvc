using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using database;

namespace TestingMvc
{
	public class TestRunHistoryVM
	{
		[Display(Name="Product")]
		public int? ProductID { get; set; }
		public string Build { get; set; }

		public SelectList Products { get; private set; }
		public SelectList Builds { get; private set; }

		public IEnumerable<TestRun> TestRuns { get; private set; }
		public Dictionary<int, IEnumerable<TestCaseResult>> TestRunResults { get; private set; }

		public TestRunHistoryVM()
		{
			Products = new SelectList(Enumerable.Empty<SelectListItem>());
			Builds = new SelectList(Enumerable.Empty<SelectListItem>());
			TestRuns = Enumerable.Empty<TestRun>();
			TestRunResults = new Dictionary<int, IEnumerable<TestCaseResult>>();
		}

		public void UpdateFields(TestingContext db)
		{
			//Fill in the products options
			var prods = db.Products
				.OrderBy(o => o.Name)
				.AsEnumerable()
				.Select(p => new SelectListItem() {
					Text = p.Name,
					Value = p.ProductID.ToString()
				});
			Products = new SelectList(prods, "Value", "Text", ProductID);

			if(ProductID.HasValue)
			{
				//Fill in the builds options
				var builds = db.TestRunProducts
					.Where(trp => trp.ProductID == ProductID.Value)
					.GroupBy(trp => trp.Build)
					.Select(grp => grp.Key)
					.OrderByDescending(o => o)
					.AsEnumerable()
					.Select(s => new SelectListItem() {
					Text = s,
					Value = s
				});
				Builds = new SelectList(builds, "Value", "Text", Build);

				TestRuns = 
					(from trp in db.TestRunProducts
					where trp.ProductID == ProductID.Value
					where Build == null || trp.Build == Build
					orderby trp.TestRun.RunDate descending
					select trp.TestRun).Take(8).AsEnumerable();

				TestRunResults = TestRuns.ToDictionary(
					tr => tr.TestRunID,
					tr => tr.TestCaseResults.AsEnumerable()
				);
			}
		}

		public bool DbValidation(TestingContext db, ModelStateDictionary model)
		{
			return DbValidation(db, model, false);
		}

		public bool DbValidation(TestingContext db, ModelStateDictionary model, bool updating)
		{
			return true;
		}
	}
}

