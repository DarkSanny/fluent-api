using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPrinting
{
	public class SerializeConfig<TOwner, TType> : PrintingConfig<TOwner> , ISerializeConfig<TOwner, TType>
	{

		private PrintingConfig<TOwner> printingConfig;
		private string propertyName;

		public SerializeConfig(PrintingConfig<TOwner> printingConfig, string propertyName)
		{
			this.printingConfig = printingConfig;
			this.propertyName = propertyName;
		}

		public PrintingConfig<TOwner> Using(Func<TType, string> serializer)
		{
			if (propertyName == null)
				printingConfig.AddSerializeType(typeof(TType), serializer);
			else
				printingConfig.AddSerializeProperty(propertyName, serializer);
			return printingConfig;
		}

		PrintingConfig<TOwner> ISerializeConfig<TOwner, TType>.PrintingConfig => printingConfig;
	}
}
