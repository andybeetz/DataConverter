using DataConverter.Interfaces;
using DataConverter.Model;

using System.Collections.Generic;

namespace DataConverter.Conversion.Converters
{
	public class CsvConverter : IInputConverter
	{
		private IFileStreamProvider _fileStreamProvider;

		public CsvConverter(IFileStreamProvider fileStreamProvider)
		{
			_fileStreamProvider = fileStreamProvider;
		}

		public string SupportedType => "csv";

		public bool GetInput(string inputLocation, out IEnumerable<DataRecord> inputData)
		{
			inputData = null;

			return false;
		}
	}
}
