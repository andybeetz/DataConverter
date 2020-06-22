using DataConverter.Conversion.Converters;
using DataConverter.Model;
using DataConverter.Tests.Fakes;

using NUnit.Framework;

using System.Collections.Generic;

namespace DataConverter.Tests.UnitTests.Conversion.Converters.CsvConverterTests
{
	[TestFixture]
	public class PushOutput
	{
		[Test]
		public void PushOutput_WhenCalled_OutputsCorrectCSV()
		{
			// Arrange
			var expectedCsv = "name,address_line1,address_line2\r\nDave,street,town\r\nAndy,someRoad,someTown\r\n";
			var fileStreamProvider = new FakeFileStreamProvider("");
			var outputLocation = "someOutputLocation";
			var csvConverter = new CsvConverter(fileStreamProvider);
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
			var result = csvConverter.PushOutput(data, outputLocation);
			var convertedData = fileStreamProvider.WrittenData;

			// Assert
			Assert.That(convertedData, Is.EqualTo(expectedCsv));
		}
	}
}
