using DataConverter.Interfaces;
using DataConverter.Model;

using System.Collections.Generic;

namespace DataConverter.Tests.Fakes.ConverterFactory
{
	public class FakeOutputConverter : IOutputConverter
	{
		private string _supportedType;
		private bool _succeeds;

		public string SupportedType => _supportedType;

		public FakeOutputConverter(string supportedType, bool succeeds = true)
		{
			_supportedType = supportedType;
			_succeeds = succeeds;
		}

		public bool PushOutput(IEnumerable<DataRecord> data, string outputLocation)
		{
			ReceivedData = data;
			return _succeeds;
		}

		public IEnumerable<DataRecord> ReceivedData { get; set; }
	}
}
