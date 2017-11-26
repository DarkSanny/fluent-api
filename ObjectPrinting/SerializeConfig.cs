using System;

namespace ObjectPrinting
{
	public class SerializeConfig<TOwner, TType> : PrintingConfig<TOwner> , ISerializeConfig<TOwner>
	{

		private readonly PrintingConfig<TOwner> printingConfig;
		private readonly string propertyName;

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

		PrintingConfig<TOwner> ISerializeConfig<TOwner>.PrintingConfig => printingConfig;
	}
}
