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
	public class TestRun
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int TestRunID { get; set; }
		public DateTime RunDate { get; set; }
		public string Comments { get; set; }
		public string ComputerName { get; set; }
		public string OS { get; set; }
		public string Language { get; set; }
		public int ConfigID { get; set; }
		[MaxLength(90)]
		public string CpuSpeed { get; set; }
		[MaxLength(90)]
		public string TotalPhysicalMemory { get; set; }
		[MaxLength(90)]
		public string AvailablePhysicalMemory { get; set; }
		[MaxLength(90)]
		public string TotalVirtualMemory { get; set; }
		[MaxLength(90)]
		public string AvailableVirtualMemory { get; set; }

		public virtual Config Config { get; set; }
		public virtual ICollection<TestRunProduct> TestRunProducts { get; set; }
		public virtual ICollection<TestCaseResult> TestCaseResults { get; set; }
	}
}

