using System;
using System.Collections.Generic;
using System.Data.Entity;
using database;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestingMvc.Controllers
{
    public class TestCaseController : Controller
    {
		private database.TestingContext db;

		public TestCaseController()
		{
			db = new database.TestingContext ();
			db.Database.Log = s => System.Diagnostics.Debug.WriteLine(s, "SQL");
		}

		[HttpGet, Authorize(Roles="test")]
		public ActionResult AddTest(int? productid)
		{
			var vm = new TestCaseVM() { ProductID = productid ?? 0 };
			vm.UpdateFields(db);
			if(Request.IsAjaxRequest())
				return PartialView("_TestCaseForm", vm);
			return View (vm);
		}

		[HttpPost, Authorize(Roles="test")]
		public ActionResult AddTest(TestCaseVM vm)
		{
			if(ModelState.IsValid & vm.DbValidation(db, ModelState))  
			{
				//Create a new TestCase
				var tc = new database.TestCase() {
					ProductID = vm.ProductID,
					TestCaseCode = vm.TestCaseCode,
					Requirements = vm.Requirement,
					Priority = vm.Priority,
					Title = vm.Title,
					Steps = vm.Steps,
					Preconditions = vm.Preconditions,
					Expected = vm.ExpectedOutcome
				};
				db.TestCases.Add(tc);
				//db.SaveChanges();

				var area = db.Areas.Find(vm.AreaID);
				//Link the selected area
				db.TestCaseAreas.Add(new TestCaseArea() {
					TestCase = tc,
					Area = area
				});
				//db.SaveChanges();

				//Link the selected configs
				if(vm.SelectedConfigs.Any())
				{
					var selectedConfs = db.Configs.Where(c => vm.SelectedConfigs.Contains(c.ConfigID)).ToList();
					if(selectedConfs.Any())
					{
						db.TestCaseConfigs.AddRange(selectedConfs.Select(c => new TestCaseConfig() {
							TestCaseID = tc.TestCaseID,
							ConfigID = c.ConfigID
						}));
						db.SaveChanges();
					}
				}

				//Success. Redirect
				Response.AppendCookie(new HttpCookie("FlashMessage", "Test Case Added Successfully") {
					Expires = DateTime.Now.AddMinutes(1)
				});
				return RedirectToAction("AddTest"); 
			}
			vm.UpdateFields(db);
			return View (vm);
		}

		[HttpGet, Authorize(Roles="test")]
		public ActionResult FindTest()
		{
			return View (new TestCaseSearchVM (db, -1));
		}

		[HttpGet, Authorize(Roles="test")]
		public ActionResult FilterTestCases(TestCaseSearchVM vm)
		{
			return PartialView ("_TestCaseSearch", new TestCaseSearchVM(db, vm.ProductID));
		}

		[HttpGet, Authorize(Roles="test")]
		public ActionResult FindTestCases(TestCaseSearchVM vm)
		{
			var query = db.TestCases.Include("TestCaseAreas").AsQueryable();
			if(vm.ProductID > 0)
			{
				query = query.Where(tc => tc.ProductID == vm.ProductID);
			}
			if (vm.AreaID > 0) {
				query = query.Where(tc => tc.TestCaseAreas.Any (tca => tca.Area.AreaID == vm.AreaID));
			}
			if(!String.IsNullOrWhiteSpace(vm.TestIdSearch))
			{
				query = query.Where(tc => tc.TestCaseCode.ToLower().Contains(vm.TestIdSearch.ToLower()));
			}
			vm.MatchingTestCases = query.ToList ();
			vm.UpdateOptions (db);

			return View ("FindTest", vm);
		}

		[Authorize(Roles="test")]
		public ActionResult EditTest(TestCaseVM vm)
		{
			if (vm.TestCaseID == null || vm.TestCaseID <= 0)
				return RedirectToAction("AddTest");
				
			//Form Submit?
			if(Request.HttpMethod == "POST")
			{
				if(ModelState.IsValid & vm.DbValidation(db, ModelState, true))
				{
					try
					{
						var tcase = db.TestCases.Find(vm.TestCaseID);
						if(tcase != null)
						{
							tcase.ProductID = vm.ProductID;
							tcase.TestCaseCode = vm.TestCaseCode;
							tcase.Requirements = vm.Requirement;
							tcase.Priority = vm.Priority;
							tcase.Title = vm.Title;
							tcase.Steps = vm.Steps;
							tcase.Preconditions = vm.Preconditions;
							tcase.Expected = vm.ExpectedOutcome;
							//db.SaveChanges();

							//Make sure only the selected area is connected
							var newArea = db.Areas.Find(vm.AreaID);
							var currentAreas = db.TestCaseAreas.Where(tca => tca.TestCaseID == vm.TestCaseID).ToList();
							var areasToRemove = currentAreas.Where(ca => ca.AreaID != vm.AreaID);
							if (areasToRemove.Any())
								db.TestCaseAreas.RemoveRange(currentAreas);
							if (!currentAreas.Any(ca => ca.AreaID == vm.AreaID))
							{
								db.TestCaseAreas.Add(new TestCaseArea() {
									Area = newArea,
									TestCase = tcase
								});
							}
							//db.SaveChanges();

							//Test cases missing from db. 
							var testsConfigs = db.TestCaseConfigs.Where(tcc => tcc.TestCaseID == vm.TestCaseID);
							var toAdd = vm.SelectedConfigs
								.GroupJoin(testsConfigs, i => i, tcc => tcc.ConfigID, (i, grp) => new { confID = i, inDb = grp.Any() })
								.Where(o => !o.inDb)
								.Select(o => new TestCaseConfig() {
									ConfigID = o.confID,
									TestCase = tcase
								});
							if (toAdd.Any())
								db.TestCaseConfigs.AddRange(toAdd);
							//Test cases in db that are not selected
							var toRemove = 
								from tcc in db.TestCaseConfigs
								where tcc.TestCaseID == vm.TestCaseID
								where !vm.SelectedConfigs.Contains(tcc.ConfigID)
								select tcc;
							if (toRemove.Any())
								db.TestCaseConfigs.RemoveRange(toRemove);

							db.SaveChanges();

							Response.AppendCookie(new HttpCookie("FlashMessage", "Test case updated") {
								Expires = DateTime.Now.AddMinutes(1)
							});
							return RedirectToAction("EditTest", new { TestCaseID = vm.TestCaseID });
						}
					}
					catch(Exception ex)
					{
						ModelState.AddModelError("ProductID", ex);
					}
				}
			}
			else
			{
				ModelState.Clear();
				if(vm.TestCaseID > 0)
				{
					var tcase = db.TestCases.Find(vm.TestCaseID);
					if(tcase != null)
					{
						vm.ProductID = tcase.ProductID;
						vm.TestCaseCode = tcase.TestCaseCode;
						vm.Requirement = tcase.Requirements;
						vm.Priority = tcase.Priority;
						vm.Title = tcase.Title;
						vm.Steps = tcase.Steps;
						vm.Preconditions = tcase.Preconditions;
						vm.ExpectedOutcome = tcase.Expected;
						vm.SelectedConfigs = db.TestCaseConfigs.Where(tcc => tcc.TestCaseID == vm.TestCaseID)
							.Select(tcc => tcc.ConfigID)
							.ToArray();
						vm.AreaID = db.TestCaseAreas.Where(tca => tca.TestCaseID == vm.TestCaseID)
							.Select(tca => tca.AreaID)
							.FirstOrDefault();
					}
					else
					{
						Response.AppendCookie(new HttpCookie("FlashMessage", "alert|Test case doesn't exist") {
							Expires = DateTime.Now.AddMinutes(1)
						});
						return RedirectToAction("FindTest");
					}
				}
			}
			vm.UpdateFields(db);
			return View (vm);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				db.Dispose ();
			}
			base.Dispose (disposing);
		}
    }
}
