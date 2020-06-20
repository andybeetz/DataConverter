using DataConverter.Configuration;

using NUnit.Framework;

using System.Collections.Generic;

namespace DataConverter.Tests.UnitTests.Configuration.OptionsParserTests
{
	[TestFixture]
	public class Parse
	{
		[Test]
		public void Parse_NullArguments_ReturnsOptions()
		{
			// Arrange

			// Act
			var options = OptionsParser.Parse(null);

			// Assert
			Assert.That(options, Is.Not.Null);
		}

		[Test]
		public void Parse_NullArguments_ParsedIsFalse()
		{
			// Arrange

			// Act
			var options = OptionsParser.Parse(null);

			// Assert
			Assert.That(options.Parsed, Is.False);
		}

		[Test]
		public void Parse_ValidOptions_ParsedIsTrue()
		{
			// Arrange
			var arguments = new List<string>() { "-i", "myfile.csv", "--inputtype", "csv", "-o", "myfile.json", "--outputtype", "json" };

			// Act
			var options = OptionsParser.Parse(arguments);

			// Assert
			Assert.That(options.Parsed, Is.True);
		}

		[Test]
		public void Parse_ValidOptions_ParsedValuesMatchInput()
		{
			// Arrange
			var arguments = new List<string>() { "-i", "myfile.csv", "--inputtype", "csv", "-o", "myfile.json", "--outputtype", "json" };

			// Act
			var options = OptionsParser.Parse(arguments);

			// Assert
			Assert.That(options.InputLocation, Is.EqualTo("myfile.csv"));
			Assert.That(options.InputType, Is.EqualTo("csv"));
			Assert.That(options.OutputLocation, Is.EqualTo("myfile.json"));
			Assert.That(options.OutputType, Is.EqualTo("json"));
		}

		[Test]
		public void Parse_MissingRequiredOptions_ParsedIsFalse()
		{
			// Arrange
			var arguments = new List<string>() { "-i", "myfile.csv", "-o", "myfile.json" };

			// Act
			var options = OptionsParser.Parse(arguments);

			// Assert
			Assert.That(options.Parsed, Is.False);
		}
	}
}
