using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using database;

namespace TestingMvc
{
	public class CompleteRunVM
	{
		[Required(ErrorMessage="Required.")]
		public int TestRunID { get; set; }

		public int? AreaFilterID { get; set; }
		public SelectList AreaFilterOptions { get; set; }

		public IEnumerable<TestCase> TestCases { get; set; }

		public CompleteRunVM()
		{
			AreaFilterOptions = new SelectList(Enumerable.Empty<SelectListItem>());
			TestCases = Enumerable.Empty<TestCase>();
		}

		public void UpdateFields(TestingContext db) 
		{
			var tr = db.TestRuns.Find(TestRunID);
			var prod = db.TestRunProducts.Where(trp => trp.TestRunID == tr.TestRunID).FirstOrDefault();

			//Areas for the dropdown list.
			var areaItems = db.Areas.Where(a => a.ProductID == prod.ProductID)
				.OrderBy(a => a.Name)
				.AsEnumerable()
				.Select(a => new SelectListItem() {
					Text = a.Name,
					Value = a.AreaID.ToString()
				})
				.ToList();
			AreaFilterOptions = new SelectList(areaItems, "Value", "Text", AreaFilterID);

			//Find filtered testcases which do not have results yet.
			IQueryable<TestCase> tcs = 
				from t in db.TestCases
				from tcc in t.TestCaseConfigs
				where t.ProductID == prod.ProductID
				where tcc.ConfigID == tr.ConfigID
				where !AreaFilterID.HasValue || t.TestCaseAreas.Any(tca => tca.AreaID == AreaFilterID.Value)
				join tcr in db.TestCaseResults on new {tr.TestRunID, t.TestCaseID, tcc.ConfigID} equals new {tcr.TestRunID, tcr.TestCaseID, tcr.ConfigID} into results
				from r in results.DefaultIfEmpty()
				where r == null
				select t;

			TestCases = tcs.ToList();
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

