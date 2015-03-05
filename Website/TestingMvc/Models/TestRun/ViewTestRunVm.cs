using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using database;

namespace TestingMvc
{
	public class ViewTestRunVm
	{
		//Filters 
		[Display(Name="Product")]
		public int? ProductID { get; set; }
		public string Build { get; set; }
		[Display(Name="Test Run")]
		public int? TestRunID { get; set; }

		public TestRun TestRun { get; private set; }
		public IEnumerable<IGrouping<Area, TestCaseResult>> Results { get; private set; }
		public Dictionary<int, Tuple<string, string, string>> AreaResults { get; private set; }

		public SelectList ProductOptions { get; private set; }
		public SelectList BuildOptions { get; private set; }
		public SelectList TestRunOptions { get; private set; }

		public ViewTestRunVm()
		{
			ProductOptions = new SelectList(Enumerable.Empty<SelectListItem>());
			BuildOptions = new SelectList(Enumerable.Empty<SelectListItem>());
			TestRunOptions = new SelectList(Enumerable.Empty<SelectListItem>());
			Results = Enumerable.Empty<IGrouping<Area, TestCaseResult>>();
			AreaResults = new Dictionary<int, Tuple<string, string, string>>();
		}

		public void UpdateFields(TestingContext db)
		{
			var prods = db.Products
				.OrderBy(p => p.Name)
				.AsEnumerable()
				.Select(p => new SelectListItem() {
					Text = p.Name,
					Value = p.ProductID.ToString()
				});
			ProductOptions = new SelectList(prods, "Value", "Text", ProductID);

			if (ProductID.HasValue)
			{
				var selectedProduct = db.Products.Find(ProductID.Value);
				if (selectedProduct != null)
				{
					//Create select list items for relevant builds.
					var blds = selectedProduct.TestRunProducts
						.OrderByDescending(trp => trp.Build)
						.AsEnumerable()
						.Select(trp => new SelectListItem() {
							Value = trp.Build,
							Text = trp.Build
						});
					BuildOptions = new SelectList(blds, "Value", "Text", Build);

					//Find test runs linked to the selected product and build
					var trps = 
						from trp in db.TestRunProducts
						where trp.ProductID == ProductID.Value
						where Build == null || trp.Build == Build
						select trp;
					//Get the testrun object and create selectlistitems.
					var testruns = 
						(from tr in db.TestRuns
						join trp in trps on tr.TestRunID equals trp.TestRunID into grp
						where grp.Any()
						orderby tr.TestRunID descending
						select tr)
						.AsEnumerable()
						.Select(tr => new SelectListItem() {
							Value = tr.TestRunID.ToString(),
							Text = tr.TestRunID.ToString()
						}); 
					TestRunOptions = new SelectList(
						testruns,
						"Value",
						"Text",
						TestRunID);
				}
			}

			if (TestRunID.HasValue)
			{
				TestRun = db.TestRuns.Find(TestRunID.Value);
				if (TestRun != null)
				{
					//Find the test case results
					var resultsQuery = 
						from tcr in db.TestCaseResults
						where tcr.TestRunID == TestRunID.Value
						from a in tcr.TestCase.TestCaseAreas
						orderby a.Area.Name, tcr.TestCase.TestCaseCode
						group tcr by a.Area into grp
						select grp;
					Results = resultsQuery.AsEnumerable();

					foreach (var grp in Results)
					{
						var totalTestCases = grp.Key.TestCaseAreas.Count();
						double totalCompleted = grp.Count();
						double totalPassed = grp.Where(tcr => tcr.Result.ToLower().Contains("pass")).Count();
						double totalFailed = grp.Where(tcr => tcr.Result.ToLower().Contains("fail")).Count();
						AreaResults[grp.Key.AreaID] = Tuple.Create(
							string.Format("Of {0} test cases in this functional area, {1} were run. {2} passed, {3} failed and {4} were not run.", totalTestCases, totalCompleted, totalPassed, totalFailed, totalTestCases-totalCompleted),
							string.Format("{0}%", Math.Round((double)(totalPassed / totalCompleted) * 100.0, 2)),
							string.Format("{0}%", Math.Round((double)(totalCompleted / totalTestCases) * 100.0, 2))
						);
					}
				}
			}
		}
	}
}

