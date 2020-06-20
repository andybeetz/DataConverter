using DataConverter.Conversion;
using DataConverter.Tests.Fakes.ConverterFactory;

using NUnit.Framework;

namespace DataConverter.Tests.UnitTests.Conversion.ConverterFactoryTests
{
	[TestFixture]
	public class GetOutputConverter
	{
		[Test]
		public void GetOutputConverter_NoConvertersPresent_ReturnsNull()
		{
			// Arrange
			var converterType = "sometype";
			var factory = new ConverterFactory();

			// Act
			var converter = factory.GetOutputConverter(converterType);

			// Assert
			Assert.That(converter, Is.Null);
		}

		[Test]
		public void GetOutputConverter_NoConverterMatchingType_ReturnsNull()
		{
			// Arrange
			var converterType = "csv";
			var fakeConverter = new FakeOutputConverter("fake");
			var factory = new ConverterFactory();
			factory.AddOutputConverter(fakeConverter);

			// Act
			var converter = factory.GetOutputConverter(converterType);

			// Assert
			Assert.That(converter, Is.Null);
		}

		[Test]
		public void GetOutputConverter_MatchingConverter_ReturnsConverter()
		{
			// Arrange
			var converterType = "fake";
			var fakeConverter = new FakeOutputConverter(converterType);
			var factory = new ConverterFactory();
			factory.AddOutputConverter(fakeConverter);

			// Act
			var converter = factory.GetOutputConverter(converterType);

			// Assert
			Assert.That(converter, Is.InstanceOf<FakeOutputConverter>());
		}
	}
}
