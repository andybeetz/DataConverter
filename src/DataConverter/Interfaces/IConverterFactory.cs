namespace DataConverter.Interfaces
{
	public interface IConverterFactory
	{
		IInputConverter GetInputConverter(string inputConverterType);
		IOutputConverter GetOutputConverter(string outputType);
	}
}
