using DataConverter.Conversion.Converters;
using DataConverter.Model;
using DataConverter.Tests.Fakes;

using NUnit.Framework;

using System.Collections.Generic;
using System.Linq;

namespace DataConverter.Tests.UnitTests.Conversion.Converters.JsonConverterTests
{
	[TestFixture]
	public class GetInput
	{
		[Test]
		public void GetInput_LocationIsEmpty_ReturnsFailedResult()
		{
			// Arrange
			var inputLocation = "";
			var fileStreamProvider = new FakeFileStreamProvider("");
			var jsonConverter = new JsonConverter(fileStreamProvider);

			// Act
			var result = jsonConverter.GetInput(inputLocation, out IEnumerable<DataRecord> outputData);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public void GetInput_JsonParsedFromLocation_ReturnsParsedData()
		{
			// Arrange
			var inputLocation = "testDataLocation";
			var jsonData = new string("[\r\n  {\r\n    \"name\": \"Dave\",\r\n    \"address\": {\r\n      \"line1\": \"street\",\r\n      \"line2\": \"town\"\r\n    }\r\n  }\r\n]");
			var fileStreamProvider = new FakeFileStreamProvider(jsonData);
			var jsonConverter = new JsonConverter(fileStreamProvider);

			// Act
			var result = jsonConverter.GetInput(inputLocation, out IEnumerable<DataRecord> outputData);
			var record = outputData.First();

			// Assert
			Assert.That(record.Items, Has.Exactly(2).Items);
			Assert.That(record.Items[0].Name, Is.EqualTo("name"));
			Assert.That((record.Items[0] as SingleItem).Value, Is.EqualTo("Dave"));
			Assert.That(record.Items[1].Name, Is.EqualTo("address"));
			Assert.That((record.Items[1] as ItemGroup).Items, Has.Exactly(2).Items);
			Assert.That((record.Items[1] as ItemGroup).Items[0].Name, Is.EqualTo("line1"));
			Assert.That(((record.Items[1] as ItemGroup).Items[0] as SingleItem).Value, Is.EqualTo("street"));
			Assert.That((record.Items[1] as ItemGroup).Items[1].Name, Is.EqualTo("line2"));
			Assert.That(((record.Items[1] as ItemGroup).Items[1] as SingleItem).Value, Is.EqualTo("town"));
		}

		[Test]
		public void GetInput_JsonWithMultipleRecordsParsedFromLocation_ReturnsParsedData()
		{
			// Arrange
			var inputLocation = "testDataLocation";
			var jsonData = new string("[\r\n  {\r\n    \"name\": \"Dave\",\r\n    \"address\": {\r\n      \"line1\": \"street\",\r\n      \"line2\": \"town\"\r\n    }\r\n  },\r\n  {\r\n    \"name\": \"Andy\",\r\n    \"address\": {\r\n      \"line1\": \"someRoad\",\r\n      \"line2\": \"someTown\"\r\n    }\r\n  }\r\n]");
			var fileStreamProvider = new FakeFileStreamProvider(jsonData);
			var jsonConverter = new JsonConverter(fileStreamProvider);

			// Act
			var result = jsonConverter.GetInput(inputLocation, out IEnumerable<DataRecord> outputData);
			var record = outputData.ToList()[1];

			// Assert
			Assert.That(record.Items, Has.Exactly(2).Items);
			Assert.That(record.Items[0].Name, Is.EqualTo("name"));
			Assert.That((record.Items[0] as SingleItem).Value, Is.EqualTo("Andy"));
			Assert.That(record.Items[1].Name, Is.EqualTo("address"));
			Assert.That((record.Items[1] as ItemGroup).Items, Has.Exactly(2).Items);
			Assert.That((record.Items[1] as ItemGroup).Items[0].Name, Is.EqualTo("line1"));
			Assert.That(((record.Items[1] as ItemGroup).Items[0] as SingleItem).Value, Is.EqualTo("someRoad"));
			Assert.That((record.Items[1] as ItemGroup).Items[1].Name, Is.EqualTo("line2"));
			Assert.That(((record.Items[1] as ItemGroup).Items[1] as SingleItem).Value, Is.EqualTo("someTown"));
		}
	}
}
