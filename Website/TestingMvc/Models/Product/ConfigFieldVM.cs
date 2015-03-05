using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using database;

namespace TestingMvc
{
	public class ConfigFieldVM
	{
		public int ProductID { get; set; }

		//New Field
		public string Name { get; set; }
		public string Type { get; set; }

		public SelectList ConfigFieldTypes { 
			get {
				return new SelectList(_configFieldTypes.Select(s => new SelectListItem() {
					Text = s,
					Value = s
				}), "Value", "Text");
			} 
		}

		//Current Fields
		public Dictionary<string, string> ConfigFields { get; set; }

		public ConfigFieldVM()
		{
			this.ConfigFields = new Dictionary<string, string>();
		}

		public ConfigFieldVM(TestingContext db, int productID)
		{
			UpdateConfigFields(db, productID);
		}

		public void UpdateConfigFields(TestingContext db, int productID)
		{
			//Config Field Types
			this.ConfigFields = db.ConfigFields.Where(cf => cf.ProductID == productID)
				.ToDictionary(cf => cf.FieldName, cf => cf.FieldType);
		}

		public bool IsValid(TestingContext db, ModelStateDictionary model)
		{
			bool result = true;
			if(model == null)
				model = new ModelStateDictionary();
			if(string.IsNullOrWhiteSpace(Name))
			{
				result = false;
				model.AddModelError("Name", "Name is required.");
			} 
			else if(db != null && db.ConfigFields.Where(cf => cf.ProductID == ProductID && cf.FieldName == Name).Any())
			{
				result = false;
				model.AddModelError("Name", "Name must be unique.");
			}
			return result;
		}

		private static string[] _configFieldTypes = new string[] { "Alphanumeric", "Numeric", "Boolean" };
	}
}

