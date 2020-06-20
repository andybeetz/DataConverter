
using DataConverter.Configuration;
using DataConverter.Conversion;
using DataConverter.Interfaces;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace DataConverter
{
	class Program
	{
		static int Main(string[] args)
		{
			Application_Start();

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

		static void Application_Start()
		{
			//setup our DI
			var serviceProvider = new ServiceCollection()
				.AddSingleton<IConverterFactory, ConverterFactory>(x => { return new ConverterFactory(); })
				.BuildServiceProvider();

			// initialise the converter
			Converter.Init(serviceProvider.GetService<IConverterFactory>());
		}
	}
}
