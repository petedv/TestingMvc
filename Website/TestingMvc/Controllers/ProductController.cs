using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestingMvc.Controllers
{
    public class ProductController : Controller
    {
		private database.TestingContext db;

		public ProductController()
		{
			db = new database.TestingContext ();
		}

		[HttpGet, Authorize(Roles="test")]
		public ActionResult AddProduct()
		{
			return View("AddProduct", new GenericModel());
		}

		[HttpPost, Authorize(Roles="test")]
		public ActionResult AddProduct(GenericModel productInfo)
		{
			if(db.Products.Where(p => p.Name.ToLower() == productInfo.Name.ToLower()).Any())
			{
				productInfo.Error = "Product Already Exists";
			} 
			else 
			{
				try
				{
					var newProduct = new database.Product();
					newProduct.Name = productInfo.Name;
					db.Products.Add(newProduct);
					db.SaveChanges();

					return RedirectToAction("EditProduct", new {
						ProductID=newProduct.ProductID, 
						SelectedTab=ProductTabs.ConfigFields
					});
				}
				catch (Exception ex)
				{
					productInfo.Error = ex.ToString();
				}
			}
			return View("AddProduct", productInfo);
		}

		[Authorize(Roles="test")]
		public ActionResult EditProduct(EditProductVM vm)
		{
			vm.UpdateFromDB(db);
			return View(vm);
		}

		[HttpPost, Authorize(Roles="test")]
		public ActionResult AddConfigField(ConfigFieldVM vm)
		{
			if(vm.IsValid(db, ModelState))
			{
				try
				{
					var cf = new database.ConfigField() {
						ProductID = vm.ProductID,
						FieldName = vm.Name,
						FieldType = vm.Type
					};
					db.ConfigFields.Add(cf);
					db.SaveChanges();

					return RedirectToAction("EditProduct", new {
						ProductID = vm.ProductID,
						SelectedTab = ProductTabs.ConfigFields
					}); 
				}
				catch (Exception ex)
				{
					this.ModelState.AddModelError("Name", ex);
				}
			}
			vm.UpdateConfigFields(db, vm.ProductID);
			var editVm = new EditProductVM(db, vm.ProductID, ProductTabs.ConfigFields);
			editVm.ConfigFieldsModel = vm;
			return View("EditProduct", editVm);
		}

		[HttpPost, Authorize(Roles="test")]
		public ActionResult RemoveConfigField(ConfigFieldVM vm)
		{
			if(!string.IsNullOrWhiteSpace(vm.Name))
			{
				try
				{
					var field = db.ConfigFields.Where(cf => cf.ProductID == vm.ProductID && cf.FieldName == vm.Name).FirstOrDefault();
					if(field != null)
					{
						db.ConfigFields.Remove(field);
						db.SaveChanges();
					}
					else
					{
						ModelState.AddModelError("General", "Field doesn't exist");
					}
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("General", ex);
				}
				vm.Name = null;
			}
			vm.UpdateConfigFields(db, vm.ProductID);
			return PartialView("_ConfigFields", vm);
		}

		[HttpPost, Authorize(Roles="test")]
		public ActionResult AddConfigValues(int productID, string configName, Dictionary<string, string> values)
		{
			//TODO add validation for existing configs.
			if (productID > 0 && !string.IsNullOrWhiteSpace(configName))
			{
				var conf = new database.Config() {
					Name = configName,
					ProductID = productID
				};
				conf = db.Configs.Add(conf);

				foreach (var kvp in values)
				{
					if(!db.Configs.Where(c => c.ProductID == productID && c.Name == configName).Any())
					{
						var field = db.ConfigFields
							.Where(cf => cf.ProductID == productID && cf.FieldName == kvp.Key)
							.FirstOrDefault();
						if(field != null)
						{
							var cv = new database.ConfigValue() {
								ProductID = productID,
								ConfigFieldID = field.ConfigFieldID,
								Config = conf,
								Value = kvp.Value
							};
							db.ConfigValues.Add(cv);
						}
					}
				}
				db.SaveChanges();
			}
			//var vm = new ConfigFieldVM(db, productID);
			return View("EditProduct", new EditProductVM(db, productID, ProductTabs.ConfigValues));
		}

		[HttpPost, Authorize(Roles="test")]
		public ActionResult AddArea(ProductAreasVM vm)
		{
			var area = new database.Area() {
				Name = vm.Name,
				Description = vm.Description,
				ProductID = vm.ProductID
			};
			if(vm.ValidateArea(this.ModelState, db, area))
			{
				db.Areas.Add(area);
				db.SaveChanges();
			}
			else
			{
				EditProductVM newModel = new EditProductVM(db, vm.ProductID, ProductTabs.Areas);
				newModel.ProductAreasModel = vm;
				return View("EditProduct", newModel);
			}
			return RedirectToAction("EditProduct", new {
				ProductID = vm.ProductID,
				SelectedTab = ProductTabs.Areas
			});
		}

		[HttpPost, Authorize(Roles="test")]
		public ActionResult DeleteArea(int areaID)
		{
			int? productID = null;
			var area = db.Areas.Find(areaID);
			if(area != null)
			{
				productID = area.ProductID;
				db.Areas.Remove(area);
				db.SaveChanges();
			}
			return View("EditProduct", new EditProductVM(db, productID, ProductTabs.Areas));
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
