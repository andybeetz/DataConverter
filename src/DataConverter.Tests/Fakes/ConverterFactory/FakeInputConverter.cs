using DataConverter.Interfaces;

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

		public bool GetInput(string inputType, string inputLocation, out object inputData)
		{
			inputData = inputLocation;

			return _succeeds;
		}
	}
}
