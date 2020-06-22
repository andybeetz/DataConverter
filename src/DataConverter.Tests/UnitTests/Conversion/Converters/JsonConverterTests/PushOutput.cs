using DataConverter.Conversion.Converters;
using DataConverter.Model;
using DataConverter.Tests.Fakes;

using NUnit.Framework;

using System.Collections.Generic;

namespace DataConverter.Tests.UnitTests.Conversion.Converters.JsonConverterTests
{
	[TestFixture]
	public class PushOutput
	{
		[Test]
		public void PushOutput_WhenCalled_OutputsCorrectJson()
		{
			// Arrange
			;
			var expectedJson = "[\r\n  {\r\n    \"name\": \"Dave\",\r\n    \"address\": {\r\n      \"line1\": \"street\",\r\n      \"line2\": \"town\"\r\n    }\r\n  },\r\n  {\r\n    \"name\": \"Andy\",\r\n    \"address\": {\r\n      \"line1\": \"someRoad\",\r\n      \"line2\": \"someTown\"\r\n    }\r\n  }\r\n]";
			var fileStreamProvider = new FakeFileStreamProvider("");
			var outputLocation = "someOutputLocation";
			var jsonConverter = new JsonConverter(fileStreamProvider);
			var data = new List<DataRecord>();
			var record = new DataRecord();
			record.Items.Add(new SingleItem() { Name = "name", Value = "Dave" });
			record.Items.Add(new ItemGroup() { Name = "address", Items = new List<RecordItem>() { new SingleItem() { Name = "line1", Value = "street" }, new SingleItem() { Name = "line2", Value = "town" } } });
			var record2 = new DataRecord();
			record2.Items.Add(new SingleItem() { Name = "name", Value = "Andy" });
			record2.Items.Add(new ItemGroup() { Name = "address", Items = new List<RecordItem>() { new SingleItem() { Name = "line1", Value = "someRoad" }, new SingleItem() { Name = "line2", Value = "someTown" } } });

			data.Add(record);
			data.Add(record2);

			// Act
			var result = jsonConverter.PushOutput(data, outputLocation);
			var convertedData = fileStreamProvider.WrittenData;

			// Assert
			Assert.That(convertedData, Is.EqualTo(expectedJson));
		}
	}
}
