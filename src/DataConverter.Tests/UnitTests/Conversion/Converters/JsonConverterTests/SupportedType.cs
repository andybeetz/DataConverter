using DataConverter.Conversion.Converters;
using DataConverter.Tests.Fakes;

using NUnit.Framework;

namespace DataConverter.Tests.UnitTests.Conversion.Converters.JsonConverterTests
{
	[TestFixture]
	public class SupportedType
	{
		[Test]
		public void SupportedType_ReturnsCorrectType()
		{
			// Arrange
			var expectedType = "json";
			var fileStreamProvider = new FakeFileStreamProvider("");
			var jsonConverter = new JsonConverter(fileStreamProvider);

			// Act
			var result = jsonConverter.SupportedType;

			// Assert
			Assert.That(result, Is.EqualTo(expectedType));
		}
	}
}
