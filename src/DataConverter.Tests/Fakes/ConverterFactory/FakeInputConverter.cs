using DataConverter.Interfaces;
using DataConverter.Model;

using System.Collections.Generic;
using System.Linq;

namespace DataConverter.Tests.Fakes.ConverterFactory
{
	public class FakeInputConverter : IInputConverter
	{
		private string _supportedType;
		private bool _succeeds;

		public FakeInputConverter(string supportedType, bool succeeds = true)
		{
			_supportedType = supportedType;
			_succeeds = succeeds;
		}

		public string SupportedType => _supportedType;

		public bool GetInput(string inputLocation, out IEnumerable<DataRecord> inputData)
		{
			inputData = new List<DataRecord>() { new DataRecord() };
			inputData.First().Items.Add(new SingleItem() { Name = "fake", Value = inputLocation });

			return _succeeds;
		}
	}
}
