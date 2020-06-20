using DataConverter.Configuration;

using NUnit.Framework;

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
	}
}
