using DataConverter.Interfaces;

using System.Collections.Generic;
using System.Linq;

namespace DataConverter.Conversion
{
	public class ConverterFactory : IConverterFactory
	{
		private List<IInputConverter> _inputConverters = new List<IInputConverter>();
		private List<IOutputConverter> _outputConverters = new List<IOutputConverter>();

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
			return _inputConverters.Where(i => i.SupportedType.ToLowerInvariant() == inputConverterType.ToLowerInvariant()).FirstOrDefault();
		}

		public IOutputConverter GetOutputConverter(string outputConverterType)
		{
			return _outputConverters.Where(o => o.SupportedType.ToLowerInvariant() == outputConverterType.ToLowerInvariant()).FirstOrDefault();
		}
	}
}