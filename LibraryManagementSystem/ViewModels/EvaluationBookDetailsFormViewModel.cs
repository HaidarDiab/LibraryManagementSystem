using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.ViewModels
{
	public class EvaluationBookDetailsFormViewModel
	{
		public Book Book { get; set; }
		public IEnumerable<Evaluation> Evaluations { get; set; }
	}
}