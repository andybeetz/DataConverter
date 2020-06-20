using DataConverter.Conversion;
using DataConverter.Tests.Fakes.ConverterFactory;

using NUnit.Framework;

namespace DataConverter.Tests.UnitTests.Conversion.ConverterFactoryTests
{
	[TestFixture]
	public class GetInputConverter
	{
		[Test]
		public void GetInputConverter_NoConvertersPresent_ReturnsNull()
		{
			// Arrange
			var converterType = "sometype";
			var factory = new ConverterFactory();

			// Act
			var converter = factory.GetInputConverter(converterType);

			// Assert
			Assert.That(converter, Is.Null);
		}

		[Test]
		public void GetInputConverter_NoConverterMatchingType_ReturnsNull()
		{
			// Arrange
			var converterType = "csv";
			var fakeConverter = new FakeInputConverter("fake");
			var factory = new ConverterFactory();
			factory.AddInputConverter(fakeConverter);

			// Act
			var converter = factory.GetInputConverter(converterType);

			// Assert
			Assert.That(converter, Is.Null);
		}

		[Test]
		public void GetInputConverter_MatchingConverter_ReturnsConverter()
		{
			// Arrange
			var converterType = "fake";
			var fakeConverter = new FakeInputConverter(converterType);
			var factory = new ConverterFactory();
			factory.AddInputConverter(fakeConverter);

			// Act
			var converter = factory.GetInputConverter(converterType);

			// Assert
			Assert.That(converter, Is.InstanceOf<FakeInputConverter>());
		}
	}
}
