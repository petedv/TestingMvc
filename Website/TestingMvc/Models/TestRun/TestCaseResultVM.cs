using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using database;

namespace TestingMvc
{
	public class TestCaseResultVM
	{
		[Required(ErrorMessage="Required.")]
		public int TestRunID { get; set; }
		[Required(ErrorMessage="Required.")]
		public int TestCaseID { get; set; }

		public TestCase TestCase { get; set; }

		[Required(ErrorMessage="Required.")]
		public bool Result { get; set; }
		public string Comments { get; set; }

		public TestCaseResultVM()
		{
			TestCase = new TestCase();
		}

		public void UpdateFields(TestingContext db)
		{
			var tr = db.TestRuns.Find(TestRunID);
			var tc = db.TestCases.Find(TestCaseID);
			if(tr != null && tc != null)
			{
				TestCase = tc;
			}
		}

		public bool DbValidation(TestingContext db, ModelStateDictionary model)
		{
			return DbValidation(db, model, false);
		}

		public bool DbValidation(TestingContext db, ModelStateDictionary model, bool updating)
		{
			return true;
		}
	}
}

