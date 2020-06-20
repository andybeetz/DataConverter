using DataConverter.Interfaces;

using System.Collections.Generic;
using System.Linq;

namespace DataConverter.Tests.Fakes.ConverterFactory
{
	public class FakeConverterFactory : IConverterFactory
	{
		private List<IInputConverter> _inputConverters = new List<IInputConverter>();
		private List<IOutputConverter> _outputConverters = new List<IOutputConverter>();

		public FakeConverterFactory(IEnumerable<IInputConverter> inputConverters, IEnumerable<IOutputConverter> outputConverters)
		{
			_inputConverters = inputConverters.ToList();
			_outputConverters = outputConverters.ToList();
		}

		public void AddInputConverter(IInputConverter inputConverter)
		{
			if(inputConverter != null)
			{
				_inputConverters.Add(inputConverter);
			}
		}

		public void AddOutputConverter(IOutputConverter outputConverter)
		{
			if(outputConverter != null)
			{
				_outputConverters.Add(outputConverter);
			}
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
