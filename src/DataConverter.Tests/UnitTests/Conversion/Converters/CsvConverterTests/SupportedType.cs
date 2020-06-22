using DataConverter.Conversion.Converters;

using NUnit.Framework;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConverter.Tests.UnitTests.Conversion.Converters.CsvConverterTests
{
	[TestFixture]
	public class SupportedType
	{
		[Test]
		public void SupportedType_ReturnsCorrectType()
		{
			// Arrange
			var expectedType = "csv";
			var csvConverter = new CsvConverter();

			// Act
			var result = csvConverter.SupportedType;

			// Assert
			Assert.That(result, Is.EqualTo(expectedType));
		}
	}
}
