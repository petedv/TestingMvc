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
	public class ConfigValue
	{
		[Key, Column(Order=0), ForeignKey("ConfigField")]
		public int ConfigFieldID { get; set; }
		[Key, Column(Order=1), ForeignKey("ConfigField")]
		public int ProductID { get; set; }
		[Key, Column(Order=2), ForeignKey("Config")]
		public int ConfigID { get; set; }

		public string Value { get; set; }

		public virtual ConfigField ConfigField { get; set; }
		public virtual Config Config { get; set; }
	}
}

