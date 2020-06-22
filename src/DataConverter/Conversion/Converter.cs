using DataConverter.Configuration;
using DataConverter.Interfaces;
using DataConverter.Model;

using System;
using System.Collections.Generic;

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
			var outputConverter = _converterFactory.GetOutputConverter(options.OutputType);

			if(inputConverter != null && outputConverter != null)
			{
				if(inputConverter.GetInput(options.InputLocation, out IEnumerable<DataRecord> inputData))
				{
					if(outputConverter.PushOutput(inputData, options.OutputLocation))
					{
						return new ConversionResult(ConversionResultType.Successful);
					}
				}
			}

			return new ConversionResult(ConversionResultType.Failed);
		}
	}
}
