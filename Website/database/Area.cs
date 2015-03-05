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
	public class Area
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int AreaID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		[ForeignKey("Product")]
		public int ProductID { get; set; }

		public virtual Product Product { get; set; }
		public virtual ICollection<TestCaseArea> TestCaseAreas { get; set; }
	}
}

