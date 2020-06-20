using DataConverter.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;

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

		public IEnumerable<string> SupportedTypes()
		{
			return _supportedTypes;
		}
	}
}
