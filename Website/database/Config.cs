using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace database
{
	/// <summary>
	/// A name for an associated set of ConfigValues
	/// </summary>
	public class Config
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ConfigID { get; set; }
		[ForeignKey("Product")]
		public int ProductID { get; set; }
		public string Name { get; set; }

		//Navigation Properties
		public virtual Product Product { get; set; }
		public virtual ICollection<ConfigValue> ConfigValues { get; set; }
		public virtual ICollection<TestCaseResult> TestCaseResults { get; set; }
		public virtual ICollection<TestCaseConfig> TestCaseConfig { get; set; }
		public virtual ICollection<TestRun> TestRuns { get; set; }
	}
}

