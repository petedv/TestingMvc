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
	public class TestCaseResult
	{
		[Key, Column(Order=0), ForeignKey("TestRun")]
		public int TestRunID { get; set; }
		[Key, Column(Order=1), ForeignKey("TestCase")]
		public int TestCaseID { get; set; }
		public string Result { get; set; }
		public string Comment { get; set; }
		[Key, Column(Order=4), ForeignKey("Config")]
		public int ConfigID { get; set; }

		//Navigation properties
		public virtual TestRun TestRun { get; set; }
		public virtual TestCase TestCase { get; set; }
		public virtual Config Config { get; set; }
	}
}

