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
	public class TestCaseConfig
	{
		[Key, Column(Order=0), ForeignKey("TestCase")]
		public int TestCaseID { get; set; }
		[Key, Column(Order=1), ForeignKey("Config")]
		public int ConfigID { get; set; }

		public virtual TestCase TestCase { get; set; }
		public virtual Config Config { get; set; }
	}
}

