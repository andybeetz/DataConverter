using DataConverter.Interfaces;
using DataConverter.Model;

using System;
using System.Collections.Generic;

namespace DataConverter.Conversion.Converters
{
	public class CsvConverter : IInputConverter
	{
		public string SupportedType => "csv";

		public bool GetInput(string inputLocation, out IEnumerable<DataRecord> inputData)
		{
			inputData = null;

			return false;
		}
	}
}
