using DataConverter.Interfaces;

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

		public bool PushOutput(object data, string outputType, string outputLocation)
		{
			ReceivedData = data;
			return _succeeds;
		}

		public object ReceivedData { get; set; }
	}
}
