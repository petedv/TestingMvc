using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using database;

namespace TestingMvc
{
	public class IncompleteTestRunVM
	{
		public int? ProductID { get; set; }
		public string Build { get; set; }

		public SelectList Products { get; set; }
		public SelectList Builds { get; set; }

		public IEnumerable<Tuple<TestRun, int>> TestRuns { get; set; }

		public IncompleteTestRunVM()
		{
			Products = new SelectList(Enumerable.Empty<SelectListItem>());
			Builds = new SelectList(Enumerable.Empty<SelectListItem>());
			TestRuns = Enumerable.Empty<Tuple<TestRun, int>>();
		}

		public void UpdateFields(TestingContext db)
		{
			//Fill in the products options
			var prods = db.Products.AsEnumerable()
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

				var tcs = 
					from tr in db.TestRuns // Select from testruns
					from trp in tr.TestRunProducts  // inner join testrunproducts
					where trp.ProductID == ProductID.Value // filter to one product
					where Build == null || trp.Build == Build // filter to build if it has a value
					from tcc in tr.Config.TestCaseConfig //inner join on testcaseconfig. (Only adds testcases with same config as testrun)
					join tcr in db.TestCaseResults on new { tr.TestRunID, tcc.TestCaseID, tcc.ConfigID } equals new {tcr.TestRunID, tcr.TestCaseID, tcr.ConfigID} into results
					from r in results.DefaultIfEmpty()  // left join testcaseresults on all of the above keys
					where r == null  // Where there are no test case results
					select new { TestRun = tr, TestCaseConfig = tcc };
				TestRuns =
					from a in tcs.AsEnumerable() //Run Query
					group a by a.TestRun into grp // group by testrun so we can count testcases
					select Tuple.Create(grp.Key, grp.Count()); 
			}
			else 
			{
				TestRuns = Enumerable.Empty<Tuple<TestRun, int>>();
				Builds = new SelectList(Enumerable.Empty<SelectListItem>());
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

