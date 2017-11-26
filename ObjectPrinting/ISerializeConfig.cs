namespace ObjectPrinting
{
	public interface ISerializeConfig<TOwner>
	{
		PrintingConfig<TOwner> PrintingConfig { get; }
	}
}
