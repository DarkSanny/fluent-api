using System;
using System.Globalization;
using FluentAssertions;
using NUnit.Framework;

namespace ObjectPrinting
{
	[TestFixture]
	public class ObjectPrinting_Should
	{
		private class Testee<T>
		{
			public T Value { get; set; }
		}

		[Test]
		public void Printer_ShouldChangeSerializationForTypes()
		{
			var obj = new Testee<int>() {Value = 10};
			var printingConfig = ObjectPrinter.For<Testee<int>>().Printing<int>().Using(v => v + "!");

			var type = obj.GetType();

			printingConfig.PrintToString(obj).Should()
				.Be(type.Name + Environment.NewLine + "\tValue = 10!" + Environment.NewLine);
		}

		[Test]
		public void Printer_SHouldChangeSerializationForProperties()
		{
			var obj = new Testee<int>() {Value = 10};
			var printingConfig = ObjectPrinter.For<Testee<int>>().Printing(t => t.Value)
				.Using(v => v + "!");

			var type = obj.GetType();

			printingConfig.PrintToString(obj).Should()
				.Be(type.Name + Environment.NewLine + "\tValue = 10!" + Environment.NewLine);
		}

		[Test]
		public void Printer_ShouldExcludeTypes()
		{
			var obj = new Testee<int>() {Value = 10};
			var printingConfig = ObjectPrinter.For<Testee<int>>().ExcludeType<int>();

			var type = obj.GetType();

			printingConfig.PrintToString(obj).Should()
				.Be(type.Name + Environment.NewLine);
		}

		[Test]
		public void Printer_ShouldExcludeProperties()
		{
			var obj = new Testee<int>() {Value = 10};
			var printingConfig = ObjectPrinter.For<Testee<int>>().ExcludeProperty(t => t.Value);

			var type = obj.GetType();

			printingConfig.PrintToString(obj).Should()
				.Be(type.Name + Environment.NewLine);
		}

		[Test]
		public void Printing_ShouldChangeCultureForNumbersTypes()
		{
			var obj = new Testee<double>() {Value = 10.1};
			var culture = new CultureInfo("ar-SA");
			var printingConfig = ObjectPrinter.For<Testee<double>>().Printing<double>().UsingCulture(culture);

			var type = obj.GetType();

			printingConfig.PrintToString(obj).Should()
				.Be(type.Name + Environment.NewLine + "\tValue = 10.1" + Environment.NewLine);
		}

		[Test]
		public void Printing_ShouldTrim()
		{
			var obj = new Testee<string>() {Value = "qwerty"};
			var printingConfig = ObjectPrinter.For<Testee<string>>().Printing<string>().TrimToLength(5);

			var type = obj.GetType();

			printingConfig.PrintToString(obj).Should()
				.Be(type.Name + Environment.NewLine + "\tValue = qwert" + Environment.NewLine);
		}

		[Test]
		public void Printing_ShouldNotTrim_WhenShortLine()
		{
			var obj = new Testee<string>() { Value = "" };
			var printingConfig = ObjectPrinter.For<Testee<string>>().Printing<string>().TrimToLength(7);

			var type = obj.GetType();

			printingConfig.PrintToString(obj).Should()
				.Be(type.Name + Environment.NewLine + "\tValue = " + Environment.NewLine);
		}

		[Test]
		public void Objects_ShouldHaveDefaultPrinting()
		{
			var obj = new Testee<int>() { Value = 10 };

			var type = obj.GetType();

			obj.PrintToString().Should()
				.Be(type.Name + Environment.NewLine + "\tValue = 10" + Environment.NewLine);
		}

		[Test]
		public void Objects_ShouldHaveDefaultPrintingWithConfig()
		{
			var obj = new Testee<int>() { Value = 10 };

			var type = obj.GetType();

			obj.PrintToString(c => c.Printing<int>().Using(v => v + "!")).Should()
				.Be(type.Name + Environment.NewLine + "\tValue = 10!" + Environment.NewLine);
		}

	}
}