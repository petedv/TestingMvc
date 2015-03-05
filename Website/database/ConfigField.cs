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
	public class ConfigField
	{
		[Key, Column(Order=0), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ConfigFieldID { get; set; }
		[Key, Column(Order=1), ForeignKey("Product")]
		public int ProductID { get; set; }

		public string FieldName { get; set; }
		public string FieldType { get; set; }

		public virtual Product Product { get; set; }
		public virtual ICollection<ConfigValue> ConfigValues { get; set; }
	}
}

