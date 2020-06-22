using DataConverter.Conversion.Converters;
using DataConverter.Model;

using NUnit.Framework;

using System.Collections.Generic;

namespace DataConverter.Tests.UnitTests.Conversion.Converters.CsvConverterTests
{
	[TestFixture]
	public class GetInput
	{
		[Test]
		public void GetInput_LocationIsEmpty_ReturnsFailedResult()
		{
			// Arrange
			var inputLocation = "";
			var csvConverter = new CsvConverter();

			// Act
			var result = csvConverter.GetInput(inputLocation, out IEnumerable<DataRecord> outputData);

			// Assert
			Assert.That(result, Is.False);
		}
	}
}
