using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using database;

namespace TestingMvc.Controllers
{
    public class TestRunController : Controller
    {
		private database.TestingContext db;

		public TestRunController()
		{
			db = new database.TestingContext();
			db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s, "SQL");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		[Authorize(Roles="test")]
		public ActionResult StartRun(TestRunVM vm)
        {
			if(Request.HttpMethod == "POST")
			{
				if(ModelState.IsValid & vm.DbValidation(db, ModelState))
				{
					//Add test run tp the database
					var tr = new database.TestRun() {
						Comments = vm.RunComments,
						ComputerName = vm.ComputerName,
						ConfigID = vm.ConfigID.Value,
						Language = vm.Language,
						OS = vm.OS,
						RunDate = DateTime.Now,
					};
					db.TestRuns.Add(tr);
					var trp = new database.TestRunProduct() {
						Build = vm.Build,
						ProductID = vm.ProductID.Value,
						TestRun = tr
					};
					db.TestRunProducts.Add(trp);
					db.SaveChanges();

					Response.AppendCookie(new HttpCookie("FlashMessage", "Test run started.") {
						Expires = DateTime.Now.AddMinutes(1)
					});

					//Redirect to Manual Run
					return RedirectToAction("CompleteRun", new {
						TestRunID = tr.TestRunID
					});
				}
			}
			else
			{
				ModelState.Clear();
			}
			vm.UpdateFields(db, 
				Request.ServerVariables["remote_addr"], 
				Request.UserAgent, 
				Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"]);

            return View(vm);
        }

		[Authorize(Roles="test")]
		public ActionResult CompleteRun(CompleteRunVM vm)
		{
			vm.UpdateFields(db);
			if(!vm.TestCases.Any())
			{
				Response.AppendCookie(new HttpCookie("FlashMessage", "No more cases left for test run.") {
					Expires = DateTime.Now.AddMinutes(1)
				});
				return RedirectToAction("IncompleteRun");
			}
			return View(vm);
		}

		[Authorize(Roles="test")]
		public ActionResult CompleteTest(TestCaseResultVM vm)
		{
			if(Request.HttpMethod == "POST")
			{
				if(ModelState.IsValid & vm.DbValidation(db, ModelState))
				{
					var tr = db.TestRuns.Find(vm.TestRunID);
					var tc = db.TestCases.Find(vm.TestCaseID);

					var testresult = new TestCaseResult() {
						TestRun = tr,
						TestCase = tc,
						Config = tr.Config,
						Comment = vm.Comments,
						Result = vm.Result ? "PASSED" : "FAILED"
					};
					db.TestCaseResults.Add(testresult);
					db.SaveChanges();

					Response.AppendCookie(new HttpCookie("FlashMessage", "Test case result added.") {
						Expires = DateTime.Now.AddMinutes(1)
					});

					//Redirect to Manual Run
					return RedirectToAction("CompleteRun", new {
						TestRunID = tr.TestRunID
					});
				}
			}
			else
			{
				ModelState.Clear();
			}
			vm.UpdateFields(db);
			return View(vm);
		}

		[Authorize(Roles="test")]
		public ActionResult IncompleteRun(IncompleteTestRunVM vm)
		{
			vm.UpdateFields(db);
			return View(vm);
		}

		public ActionResult History(TestRunHistoryVM vm)
		{
			vm.UpdateFields(db);
			return View(vm);
		}

		public ActionResult ViewRun(ViewTestRunVm vm)
		{
			vm.UpdateFields(db);
			return View(vm);
		}
    }
}
