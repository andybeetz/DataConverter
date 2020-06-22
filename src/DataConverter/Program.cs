
using DataConverter.Configuration;
using DataConverter.Conversion;
using DataConverter.Conversion.Converters;
using DataConverter.Infrastructure;
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
			var returnCode = 1;

			if(options.Parsed)
			{
				returnCode = 0;

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
					returnCode = 1;
				}
				catch(ArgumentOutOfRangeException outofRangeException)
				{
					Console.WriteLine($"An error occured during conversion: {outofRangeException.Message + System.Environment.NewLine + outofRangeException.StackTrace}");
					returnCode = 1;
				}
				catch(Exception exception)
				{
					Console.WriteLine($"An unexpected error occured during conversion: {exception.Message + System.Environment.NewLine + exception.StackTrace}");
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
				.AddTransient<IFileStreamProvider, FileStreamProvider>()
				.BuildServiceProvider();

			// add converters to the factory
			serviceProvider.GetService<IConverterFactory>().AddInputConverter(new CsvConverter(serviceProvider.GetService<IFileStreamProvider>()));
			serviceProvider.GetService<IConverterFactory>().AddOutputConverter(new JsonConverter(serviceProvider.GetService<IFileStreamProvider>()));
			serviceProvider.GetService<IConverterFactory>().AddOutputConverter(new CsvConverter(serviceProvider.GetService<IFileStreamProvider>()));

			// initialise the converter
			Converter.Init(serviceProvider.GetService<IConverterFactory>());
		}
	}
}
