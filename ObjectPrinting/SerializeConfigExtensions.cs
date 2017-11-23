using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPrinting
{
	public static class SerializeConfigExtensions
	{

		public static PrintingConfig<TOwner> UsingCulture<TOwner>(this SerializeConfig<TOwner, int> config, CultureInfo info)
		{
			var pc = ((ISerializeConfig<TOwner, int>) config).PrintingConfig;
			AddCulure(pc, typeof(int), info);
			return pc;
		}

		public static PrintingConfig<TOwner> UsingCulture<TOwner>(this SerializeConfig<TOwner, long> config, CultureInfo info)
		{
			var pc = ((ISerializeConfig<TOwner, long>)config).PrintingConfig;
			AddCulure(pc, typeof(long), info);
			return pc;
		}

		public static PrintingConfig<TOwner> UsingCulture<TOwner>(this SerializeConfig<TOwner, double> config, CultureInfo info)
		{
			var pc = ((ISerializeConfig<TOwner, double>)config).PrintingConfig;
			AddCulure(pc, typeof(double), info);
			return pc;
		}

		public static PrintingConfig<TOwner> UsingCulture<TOwner>(this SerializeConfig<TOwner, float> config, CultureInfo info)
		{
			var pc = ((ISerializeConfig<TOwner, float>)config).PrintingConfig;
			AddCulure(pc, typeof(float), info);
			return pc;
		}

		private static void AddCulure<TOwner>(PrintingConfig<TOwner> pc, Type type, CultureInfo culture)
		{
			pc.AddCulture(type, culture);
		}

		public static PrintingConfig<TOwner> Using<TOwner>(this SerializeConfig<TOwner, string> config,
			int length)
		{
			var pc = ((ISerializeConfig<TOwner, string>)config).PrintingConfig;
			Func<string, string> func = (s) => s.Substring(0, Math.Min(s.Length-1, length));
			pc.AddSerializeType(typeof(string), func);
			return pc;
		}
	}
}
