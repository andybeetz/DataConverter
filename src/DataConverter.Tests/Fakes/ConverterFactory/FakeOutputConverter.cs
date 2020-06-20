using DataConverter.Interfaces;

using System.Collections.Generic;

namespace DataConverter.Tests.Fakes.ConverterFactory
{
	public class FakeOutputConverter : IOutputConverter
	{
		private List<string> _supportedTypes = new List<string>();
		private bool _succeeds;

		public FakeOutputConverter(string supportedType, bool succeeds)
		{
			_supportedTypes.Add(supportedType);
			_succeeds = succeeds;
		}

		public bool PushOutput(object data, string outputType, string outputLocation)
		{
			ReceivedData = data;
			return _succeeds;
		}

		public object ReceivedData { get; set; }
	}
}
