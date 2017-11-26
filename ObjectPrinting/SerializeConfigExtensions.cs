using System;
using System.Globalization;

namespace ObjectPrinting
{
	public static class SerializeConfigExtensions
	{

		public static PrintingConfig<TOwner> UsingCulture<TOwner>(this SerializeConfig<TOwner, int> config, CultureInfo info)
		{
			var printingConfig = ((ISerializeConfig<TOwner>) config).PrintingConfig;
			AddCulture(printingConfig, typeof(int), info);
			return printingConfig;
		}

		public static PrintingConfig<TOwner> UsingCulture<TOwner>(this SerializeConfig<TOwner, long> config, CultureInfo info)
		{
			var printingConfig = ((ISerializeConfig<TOwner>)config).PrintingConfig;
			AddCulture(printingConfig, typeof(long), info);
			return printingConfig;
		}

		public static PrintingConfig<TOwner> UsingCulture<TOwner>(this SerializeConfig<TOwner, double> config, CultureInfo info)
		{
			var pc = ((ISerializeConfig<TOwner>)config).PrintingConfig;
			AddCulture(pc, typeof(double), info);
			return pc;
		}

		public static PrintingConfig<TOwner> UsingCulture<TOwner>(this SerializeConfig<TOwner, float> config, CultureInfo info)
		{
			var printingConfig = ((ISerializeConfig<TOwner>)config).PrintingConfig;
			AddCulture(printingConfig, typeof(float), info);
			return printingConfig;
		}

		private static void AddCulture<TOwner>(PrintingConfig<TOwner> printingConfig, Type type, CultureInfo culture)
		{
			printingConfig.AddCulture(type, culture);
		}

		public static PrintingConfig<TOwner> TrimToLength<TOwner>(this SerializeConfig<TOwner, string> config,
			int length)
		{
			var printingConfig = ((ISerializeConfig<TOwner>)config).PrintingConfig;
			Func<string, string> func = (s) => s.Substring(0, Math.Min(Math.Max(0, s.Length), length));
			printingConfig.AddSerializeType(typeof(string), func);
			return printingConfig;
		}
	}
}
