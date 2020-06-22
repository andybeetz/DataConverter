using DataConverter.Conversion.Converters;
using DataConverter.Model;
using DataConverter.Tests.Fakes;

using NUnit.Framework;

using System;
using System.Collections.Generic;
using System.Linq;

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
			var fileStreamProvider = new FakeFileStreamProvider("");
			var csvConverter = new CsvConverter(fileStreamProvider);

			// Act
			var result = csvConverter.GetInput(inputLocation, out IEnumerable<DataRecord> outputData);

			// Assert
			Assert.That(result, Is.False);
		}

		[Test]
		public void GetInput_CsvParsedFromLocation_ReturnsParsedData()
		{
			// Arrange
			var inputLocation = "testDataLocation";
			var csvData = new string($"name,address_line1,address_line2{Environment.NewLine}Dave,street,town");
			var fileStreamProvider = new FakeFileStreamProvider(csvData);
			var csvConverter = new CsvConverter(fileStreamProvider);

			// Act
			var result = csvConverter.GetInput(inputLocation, out IEnumerable<DataRecord> outputData);
			var record = outputData.First();

			// Assert
			Assert.That(record.Items, Has.Exactly(2).Items);
			Assert.That(record.Items[0].Name, Is.EqualTo("name"));
			Assert.That((record.Items[0] as SingleItem).Value, Is.EqualTo("Dave"));
			Assert.That(record.Items[1].Name, Is.EqualTo("address"));
			Assert.That((record.Items[1] as ItemGroup).Items, Has.Exactly(2).Items);
			Assert.That((record.Items[1] as ItemGroup).Items[0].Name, Is.EqualTo("line1"));
			Assert.That(((record.Items[1] as ItemGroup).Items[0] as SingleItem).Name, Is.EqualTo("street"));
			Assert.That((record.Items[1] as ItemGroup).Items[1].Name, Is.EqualTo("line2"));
			Assert.That(((record.Items[1] as ItemGroup).Items[1] as SingleItem).Name, Is.EqualTo("town"));
		}
	}
}
