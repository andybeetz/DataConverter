using DataConverter.Interfaces;

namespace DataConverter.Conversion
{
	public class ConverterFactory : IConverterFactory
	{
		public IInputConverter GetInputConverter(string inputConverterType)
		{
			throw new System.NotImplementedException();
		}

		public IOutputConverter GetOutputConverter(string outputType)
		{
			throw new System.NotImplementedException();
		}
	}
}