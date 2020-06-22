using DataConverter.Configuration;
using DataConverter.Conversion;
using DataConverter.Interfaces;
using DataConverter.Model;
using DataConverter.Tests.Fakes.ConverterFactory;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;

namespace DataConverter.Tests.UnitTests.Conversion.ConverterTests
{
	[TestFixture]
	public class Convert
	{
		[Test]
		public void Convert_OptionsIsNull_ThrowsArgumentNull()
		{
			void Convert()
			{
				//Arrange
				Options options = null;

				//Act
				Converter.Convert(options);
			}

			//Assert
			Assert.Throws(typeof(ArgumentNullException), Convert);
		}

		[Test]
		public void Convert_OptionsIsNotParsed_ThrowsArgumentOutOfRange()
		{
			void Convert()
			{
				//Arrange
				Options options = new Options();

				//Act
				Converter.Convert(options);
			}

			//Assert
			Assert.Throws(typeof(ArgumentOutOfRangeException), Convert);
		}

		[Test]
		public void Convert_InputTypeNotRecognised_ReturnsFailedConversionResult()
		{
			//Arrange
			Options options = new Options() { InputType = "unknowninputtype", Parsed = true };
			Converter.Init(new FakeConverterFactory(new List<IInputConverter>(), new List<IOutputConverter>()));

			//Act
			var result = Converter.Convert(options);

			//Assert
			Assert.That(result.Type, Is.EqualTo(ConversionResultType.Failed));
		}

		[Test]
		public void Convert_OutputTypeNotRecognised_ReturnsFailedConversionResult()
		{
			//Arrange
			Options options = new Options() { OutputType = "unknownoutputtype", Parsed = true };
			Converter.Init(new FakeConverterFactory(new List<IInputConverter>(), new List<IOutputConverter>()));

			//Act
			var result = Converter.Convert(options);

			//Assert
			Assert.That(result.Type, Is.EqualTo(ConversionResultType.Failed));
		}

		[Test]
		public void Convert_ConvertersSucceed_ReturnsSuccessfulConversionResult()
		{
			//Arrange
			Options options = new Options() { InputType = "supported", InputLocation = "pass", OutputType = "supported", OutputLocation = "pass", Parsed = true };
			Converter.Init(new FakeConverterFactory(new List<IInputConverter>() { new FakeInputConverter("supported", true) }, new List<IOutputConverter>() { new FakeOutputConverter("supported", true) }));

			//Act
			var result = Converter.Convert(options);

			//Assert
			Assert.That(result.Type, Is.EqualTo(ConversionResultType.Successful));
		}

		[Test]
		public void Convert_OutputConverterFails_ReturnsFailedConversionResult()
		{
			//Arrange
			Options options = new Options() { InputType = "supported", InputLocation = "pass", OutputType = "supported", OutputLocation = "pass", Parsed = true };
			Converter.Init(new FakeConverterFactory(new List<IInputConverter>() { new FakeInputConverter("supported", true) }, new List<IOutputConverter>() { new FakeOutputConverter("supported", false) }));

			//Act
			var result = Converter.Convert(options);

			//Assert
			Assert.That(result.Type, Is.EqualTo(ConversionResultType.Failed));
		}

		[Test]
		public void Convert_InputConverterFails_ReturnsFailedConversionResult()
		{
			//Arrange
			Options options = new Options() { InputType = "supported", InputLocation = "pass", OutputType = "supported", OutputLocation = "pass", Parsed = true };
			Converter.Init(new FakeConverterFactory(new List<IInputConverter>() { new FakeInputConverter("supported", false) }, new List<IOutputConverter>() { new FakeOutputConverter("supported", true) }));

			//Act
			var result = Converter.Convert(options);

			//Assert
			Assert.That(result.Type, Is.EqualTo(ConversionResultType.Failed));
		}

		[Test]
		public void Convert_InputConverterSucceeds_SendsDataToOutput()
		{
			//Arrange
			Options options = new Options() { InputType = "supported", InputLocation = "pass", OutputType = "supported", OutputLocation = "pass", Parsed = true };
			var outputConverter = new FakeOutputConverter("supported", true);
			var inputConverter = new FakeInputConverter("supported", true);
			Converter.Init(new FakeConverterFactory(new List<IInputConverter>() { inputConverter }, new List<IOutputConverter>() { outputConverter }));

			//Act
			var result = Converter.Convert(options);
			var item = outputConverter.ReceivedData.First().Items.First() as SingleItem;

			//Assert
			Assert.That(item.Value, Is.EqualTo(options.InputLocation));
		}
	}
}
