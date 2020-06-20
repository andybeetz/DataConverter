using DataConverter.Configuration;
using DataConverter.Conversion;

using NUnit.Framework;

using System;

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
		public void Convert_InputTypeNotRecognised_ThrowsArgumentOutOfRange()
		{
			void Convert()
			{
				//Arrange
				Options options = new Options() { InputType = "unknowninputtype", Parsed = true };

				//Act
				Converter.Convert(options);
			}

			//Assert
			Assert.Throws(typeof(ArgumentOutOfRangeException), Convert);
		}
	}
}
