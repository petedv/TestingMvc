using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using database;

namespace TestingMvc
{
	public class TestRunVM
	{
		[Display(Name="Test Run")]
		public int? TestRunID { get; set; }
		[Required(ErrorMessage="Required."), Display(Name="Product ID")]
		public int? ProductID { get; set; }
		[Required(ErrorMessage="Required.")]
		public string Build { get; set; }
		[Display(Name="Computer Name")]
		public string ComputerName { get; set; }
		[Display(Name="Operating System")]
		public string OS { get; set; }
		public string Language { get; set; }
		[Display(Name="Run Comments")]
		public string RunComments { get; set; }
		[Required(ErrorMessage="Required."), Display(Name="Config")]
		public int? ConfigID { get; set; }

		public SelectList ProductOptions { get; set; }
		public IEnumerable<Tuple<int, List<ConfigValue>>> ConfigOptions { get; set; }

		public TestRunVM()
		{
			ConfigOptions = Enumerable.Empty<Tuple<int, List<ConfigValue>>>();
			ProductOptions = new SelectList(Enumerable.Empty<SelectListItem>());
		}

		public void UpdateFields(TestingContext db) 
		{
			UpdateFields(db, null, null, null);
		}

		public void UpdateFields(TestingContext db, string ipToResolve, string userAgent, string lang)
		{

			ProductOptions = new SelectList(
				db.Products.ToList(), 
				"ProductID", 
				"Name",
				ProductID);

			if (ProductID.HasValue)
			{
				ConfigOptions = db.ConfigValues
					.Include(cv => cv.Config)
					.Where(cv => cv.ProductID == ProductID.Value)
					.OrderBy(cf => cf.ConfigField.FieldName)
					.GroupBy(cv => cv.ConfigID)
					.AsEnumerable()
					.Select(grp => Tuple.Create(grp.Key, grp.ToList()))
					.ToList();
			}

			if(!string.IsNullOrWhiteSpace(ipToResolve) && string.IsNullOrWhiteSpace(ComputerName))
			{
				var hostentry = System.Net.Dns.GetHostEntry(ipToResolve);
				if(hostentry != null)
				{
					var compname = hostentry.HostName.Split('.');
					ComputerName = compname[0].ToUpper();
				}
			}
			if(!string.IsNullOrWhiteSpace(userAgent) && string.IsNullOrWhiteSpace(OS))
			{
				//prefill os of client
				if(userAgent.Contains("Windows NT 5.1"))
				{
					OS = "Windows XP";
				}
				else if(userAgent.Contains("Windows NT 6.0"))
				{
					OS = "Windows Vista\"";
				}
				else if(userAgent.Contains("Windows NT 6.1"))
				{
					OS = "Windows 7";
				}
				else if(userAgent.Contains("Windows NT 6.3"))
				{
					OS = "Windows 8";
				}
				else
				{
					var parts = userAgent.Split('(', ')');
					if(parts.Length > 1)
					{
						OS = parts[1];
					}
				}
			}
			if (!string.IsNullOrWhiteSpace(lang) && string.IsNullOrWhiteSpace(Language))
			{
				//prefill client language
				if (lang.Contains("en"))
				{
					Language = "English";
				}
				else if (lang.Contains("de"))
				{
					Language = "German";
				}
				else if (lang.Contains("fr"))
				{
					Language = "French";
				}
				else if (lang.Contains("es"))
				{
					Language = "Spanish";
				}
			}
		}

		public bool DbValidation(TestingContext db, ModelStateDictionary model)
		{
			return DbValidation(db, model, false);
		}

		public bool DbValidation(TestingContext db, ModelStateDictionary model, bool updating)
		{
			bool result = true;
			if(model == null)
				model = new ModelStateDictionary();

			if(db == null)
				return result;

			if (db.Products.Find(ProductID) == null)
			{
				result = false;
				model.AddModelError("ProductID", "Product selected not found in db.");
			}

			if (db.Configs.Find(ConfigID) == null)
			{
				result = false;
				model.AddModelError("ConfigID", "Config selected not found in db.");
			}

			return result;
		}
	}
}

