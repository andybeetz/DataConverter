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

			var inputConverter = _converterFactory.GetInputConverter(options.InputType);
			if(inputConverter == null)
			{
				return new ConversionResult(ConversionResultType.Failed);
			}

			if(!inputConverter.GetInput(options.InputType, options.InputLocation, out object inputData))
			{
				return new ConversionResult(ConversionResultType.Failed);
			}

			var outputConverter = _converterFactory.GetOutputConverter(options.OutputType);
			if(outputConverter == null)
			{
				return new ConversionResult(ConversionResultType.Failed);
			}

			if(!outputConverter.PushOutput(inputData, options.OutputType, options.OutputLocation))
			{
				return new ConversionResult(ConversionResultType.Failed);
			}

			return new ConversionResult(ConversionResultType.Successful);
		}
	}
}
