using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using database;

namespace Tests
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");

 			using (var db = new database.TestingContext ()) {
				db.Database.Log = s => Console.WriteLine(s);

				int prodID = 10;
				string Build = null;

				var tcs = 
					from tr in db.TestRuns
					from trp in tr.TestRunProducts
					where trp.ProductID == prodID
					where (Build == null || trp.Build == Build)
					from tcc in tr.Config.TestCaseConfig
					join tcr in db.TestCaseResults on new { tr.TestRunID, tcc.TestCaseID, tcc.ConfigID } equals new {tcr.TestRunID, tcr.TestCaseID, tcr.ConfigID} into results
					from r in results.DefaultIfEmpty()
					where r == null
					select new { TestRun = tr, TestCaseConfig = tcc };
				var grps = 
					from a in tcs.AsEnumerable()
					group a by a.TestRun;
					
				foreach (var grp in grps)
				{
					Console.WriteLine("{0}: {1}", grp.Key.TestRunID, grp.Count());
				}
				Console.WriteLine("Here");
			}


//			var md5 = System.Security.Cryptography.MD5.Create ();
//			byte[] pass = md5.ComputeHash (System.Text.Encoding.UTF8.GetBytes ("admin"));
//			Console.WriteLine (String.Join ("", pass.Select (b => b.ToString ("x2"))));
		}
	}
}
