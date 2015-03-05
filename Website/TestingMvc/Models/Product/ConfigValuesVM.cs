using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using database;

namespace TestingMvc
{
	public class ConfigValuesVM
	{
		public int ProductID { get; set; }

		//New Config
		public Dictionary<string, string> ConfigFields;
		public string ValidationError { get; set; }

		//Existing Configs
		public IEnumerable<Tuple<int, List<ConfigValue>>> ExistingConfigs { get; set; }

		public ConfigValuesVM()
		{
			this.ConfigFields = new Dictionary<string, string>();
		}

		public ConfigValuesVM(TestingContext db, int productID)
		{
			UpdateFromDB(db, productID);
		}

		public void UpdateFromDB(TestingContext db, int productID)
		{
			this.ProductID = productID;
			//Config Field Types
			this.ConfigFields = db.ConfigFields.Where(cf => cf.ProductID == productID)
				.OrderBy(cf => cf.FieldName)
				.ToDictionary(cf => cf.FieldName, cf => cf.FieldType);
			//Not sure if this is required. Loads the entities into context
//			db.ConfigValues.Include(cv => cv.Config)
//				.Where(cv => cv.ProductID == 1)
//				.Load();
			this.ExistingConfigs = db.ConfigValues
				.Include(cv => cv.Config)
				.Where(cv => cv.ProductID == productID)
				.OrderBy(cf => cf.ConfigField.FieldName)
				.GroupBy(cv => cv.ConfigID)
				.AsEnumerable()
				.Select(grp => Tuple.Create(grp.Key, grp.ToList()))
				.ToList();
		}
	}
}

