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
				//1. Исключить из сериализации свойства определенного типа
				//.ExcludeType<int>()
				//2. Указать альтернативный способ сериализации для определенного типа
				.Printing(t => t.Age).Using(t => t + " yo")
				.Printing<string>().Using(2);
				//3. Для числовых типов указать культуру
				//.Printing<int>().UsingCulture(new CultureInfo("en"))
				//4. Настроить сериализацию конкретного свойства
				//.Printing(t => t.Age).Using(t => t.ToString())
				//5. Настроить обрезание строковых свойств (метод должен быть виден только для строковых свойств)
				//.Printing<string>().Using(5)
				//6. Исключить из сериализации конкретного свойства
				//.ExcludeProperty(t => t.Age)
				//.ExcludeType<double>()
				//.Printing(t => t.Name).Using(t => t + " is dead");
			
			Console.Write(printer.PrintToString(person));
		}

	}
}
