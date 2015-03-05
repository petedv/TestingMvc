using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using database;

namespace TestingMvc
{
	public class HomeVM
	{
		public LoginVM LoginInfo { get; set; }

		public IEnumerable<TestCase> NewestTestCases { get; protected set; }
		public IEnumerable<TestRun> LatestTestRuns { get; protected set; }
		public IEnumerable<Tuple<Product, TimeSpan>> PreviousRuns { get; protected set; }

		public HomeVM()
		{
			NewestTestCases = Enumerable.Empty<TestCase>();
			LatestTestRuns = Enumerable.Empty<TestRun>();
			PreviousRuns = Enumerable.Empty<Tuple<Product, TimeSpan>>();
			LoginInfo = new LoginVM();
		}

		public void UpdateFields(TestingContext db)
		{
			//PreviousRuns
			//SELECT p.ProductName, t.rundate FROM test_run t, product p 
			//WHERE t.ProductID = p.ProductID AND t.rundate = (SELECT MAX(t1.rundate) 
			//FROM test_run t1 WHERE t1.ProductID = t.ProductID) ORDER BY p.ProductName ASC;
			var prodRuns = 
				from tr in db.TestRuns
				from p in tr.TestRunProducts
				select new { TestRun=tr, Product=p.Product} into testproducts
				group testproducts by testproducts.Product into grp
				select new { Product = grp.Key, LatestRun = grp.Max(tr => tr.TestRun.RunDate)};
			PreviousRuns = prodRuns
				.AsEnumerable()
				.OrderByDescending(a => a.LatestRun)
				.Take(5)
				.Select(a => Tuple.Create(a.Product, DateTime.Now.Subtract(a.LatestRun)));

			//Latest TestRuns
			//SELECT t.rundate, p.ProductName, t.Build FROM test_run t, product p 
			//WHERE t.ProductID = p.ProductID ORDER by t.rundate DESC Limit 10;
			LatestTestRuns = db.TestRuns
				.OrderByDescending(tr => tr.RunDate)
				.Take(7)
				.ToList();

			//Newest Cases
			//SELECT p.ProductName, t.TestcaseId, t.Title FROM test_case t, product p 
			//WHERE t.ProductID=p.ProductID ORDER by t.testid DESC Limit 10;
			NewestTestCases = db.TestCases
				.OrderByDescending(tc => tc.TestCaseID)
				.Include(tc => tc.Product)
				.Take(7)
				.ToList();
		}
	}
}

