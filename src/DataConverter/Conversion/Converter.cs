using DataConverter.Configuration;
using DataConverter.Interfaces;

using System;

namespace DataConverter.Conversion
{
	public static class Converter
	{
		private static IConverterFactory _converterFactory;

		public static void Init(IConverterFactory converterFactory)
		{
			_converterFactory = converterFactory;
		}

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

			// make sure we support the input and output types
			var inputConverter = _converterFactory.GetInputConverter(options.InputType);
			if(inputConverter == null)
			{
				return new ConversionResult(ConversionResultType.Failed);
			}

			var outputConverter = _converterFactory.GetOutputConverter(options.OutputType);
			if(outputConverter == null)
			{
				return new ConversionResult(ConversionResultType.Failed);
			}

			if(outputConverter.DoWork())
			{
				return new ConversionResult(ConversionResultType.Successful);
			}

			return new ConversionResult(ConversionResultType.Successful);
		}
	}
}
