using DataConverter.Configuration;

using System;

namespace DataConverter.Conversion
{
	public abstract class Converter
	{
		public static ConversionResult Convert(Options options)
		{
			if(options == null)
			{
				throw new ArgumentNullException(nameof(options));
			}

			if(!options.Parsed)
			{
				throw new ArgumentOutOfRangeException(nameof(options));
			}

			throw new NotImplementedException();
		}
	}
}
