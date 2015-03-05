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

	public class TestingContext : DbContext
	{
		public DbSet<Area> Areas { get; set; }
		public DbSet<Config> Configs { get; set; }
		public DbSet<ConfigField> ConfigFields { get; set; }
		public DbSet<ConfigValue> ConfigValues { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<TestCase> TestCases { get; set; }
		public DbSet<TestCaseArea> TestCaseAreas { get; set; }
		public DbSet<TestCaseConfig> TestCaseConfigs { get; set; }
		public DbSet<TestCaseResult> TestCaseResults { get; set; }
		public DbSet<TestRun> TestRuns { get; set; }
		public DbSet<TestRunProduct> TestRunProducts { get; set; }
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			// PostgreSQL uses the public schema by default - not dbo.
			modelBuilder.HasDefaultSchema("public");
			Database.SetInitializer<TestingContext>(null);

			base.OnModelCreating(modelBuilder);
		}
	}

}

