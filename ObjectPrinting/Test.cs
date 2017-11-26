using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectPrinting.Tests;

namespace ObjectPrinting
{
	public class Test
	{

		public static void Main()
		{
			var person = new Person { Name = "Alex", Age = 19, Parent = new Person() {Name = "Sanny"}};

			var printer = ObjectPrinter.For<Person>()
				.Printing(t => t.Age).Using(t => t + " yo")
				.Printing<string>().TrimToLength(2);
			
			Console.Write(printer.PrintToString(person));
		}

	}
}
