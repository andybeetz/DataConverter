using DataConverter.Interfaces;

using System.Collections.Generic;
using System.Linq;

namespace DataConverter.Tests.Fakes.ConverterFactory
{
	public class FakeConverterFactory : IConverterFactory
	{
		private IEnumerable<IInputConverter> _inputConverters;
		private IEnumerable<IOutputConverter> _outputConverters;

		public FakeConverterFactory(IEnumerable<IInputConverter> inputConverters, IEnumerable<IOutputConverter> outputConverters)
		{
			_inputConverters = inputConverters;
			_outputConverters = outputConverters;
		}

		public IInputConverter GetInputConverter(string inputConverterType)
		{
			return _inputConverters.FirstOrDefault();
		}

		public IOutputConverter GetOutputConverter(string outputType)
		{
			return _outputConverters.FirstOrDefault();
			;
		}
	}
}
