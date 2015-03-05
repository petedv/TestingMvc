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
	public class Product
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ProductID { get; set; }
		public string Name { get; set; }

		public virtual ICollection<TestCase> TestCases { get; set; }
		public virtual ICollection<TestRunProduct> TestRunProducts { get; set; }
		public virtual ICollection<Config> Configs { get; set; }
		public virtual ICollection<Area> Areas { get; set; }
	}
}

