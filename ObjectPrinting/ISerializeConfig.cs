using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectPrinting
{
	public interface ISerializeConfig<TOwner, TType>
	{
		PrintingConfig<TOwner> PrintingConfig { get; }
	}
}
