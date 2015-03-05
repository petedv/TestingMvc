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
	public class TestCase
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int TestCaseID { get; set; }
		public string TestCaseCode { get; set; }
		public string Title { get; set; }
		public string Preconditions { get; set; }
		public string Steps { get; set; }
		public string Expected { get; set; }
		public int Priority { get; set; }
		public string Requirements { get; set; }
		public string ScriptName { get; set; }
		public int ProductID { get; set; }

		public virtual Product Product { get; set; }
		public virtual ICollection<TestCaseResult> TestCaseResults { get; set; }
		public virtual ICollection<TestCaseArea> TestCaseAreas { get; set; }
		public virtual ICollection<TestCaseConfig> TestCaseConfigs { get; set; }
	}
}

