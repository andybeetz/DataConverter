using DataConverter.Conversion.Converters;
using DataConverter.Tests.Fakes;

using NUnit.Framework;

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
			var fileStreamProvider = new FakeFileStreamProvider("");
			var csvConverter = new CsvConverter(fileStreamProvider);

			// Act
			var result = csvConverter.SupportedType;

			// Assert
			Assert.That(result, Is.EqualTo(expectedType));
		}
	}
}
