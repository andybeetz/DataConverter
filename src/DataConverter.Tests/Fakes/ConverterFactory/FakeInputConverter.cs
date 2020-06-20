using DataConverter.Interfaces;

using System.Collections.Generic;

namespace DataConverter.Tests.Fakes.ConverterFactory
{
	public class FakeInputConverter : IInputConverter
	{
		private List<string> _supportedTypes = new List<string>();
		private bool _succeeds;

		public FakeInputConverter(string supportedType, bool succeeds)
		{
			_supportedTypes.Add(supportedType);
			_succeeds = succeeds;
		}

		public bool GetInput(string inputType, string inputLocation, out object inputData)
		{
			inputData = inputLocation;

			return _succeeds;
		}
	}
}
