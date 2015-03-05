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
	public class TestRunProduct
	{
		[Key, Column(Order=0), ForeignKey("TestRun")]
		public int TestRunID { get; set; }
		[Key, Column(Order=1), ForeignKey("Product")]
		public int ProductID { get; set; }

		public TestRun TestRun { get; set; }
		public Product Product { get; set; }

		public string Build { get; set; }
	}
}

