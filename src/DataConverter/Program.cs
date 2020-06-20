
using DataConverter.Configuration;
using DataConverter.Conversion;

using System;

namespace DataConverter
{
	class Program
	{
		static int Main(string[] args)
		{
			var options = OptionsParser.Parse(args);
			var returnCode = 0;

			if(options.Parsed)
			{
				// Convert data
				try
				{
					var conversionResult = Converter.Convert(options);

					if(conversionResult.Type != ConversionResultType.Successful)
					{
						returnCode = 1;
					}
				}
				catch(ArgumentNullException nullException)
				{
					Console.WriteLine($"An error occured during conversion: {nullException.Message + System.Environment.NewLine + nullException.StackTrace}");
				}
				catch(ArgumentOutOfRangeException outofRangeException)
				{
					Console.WriteLine($"An error occured during conversion: {outofRangeException.Message + System.Environment.NewLine + outofRangeException.StackTrace}");
				}
				finally
				{
					returnCode = 1;
				}
			}

			return returnCode;
		}
	}
}
