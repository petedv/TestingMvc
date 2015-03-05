using System;

namespace TestingMvc
{
	public class GenericModel
	{
		public string Name { get; set; }

		public string Error { get; set; }

		public bool HasError 
		{ 
			get { return !string.IsNullOrWhiteSpace(Error); } 
		}
	}
}

