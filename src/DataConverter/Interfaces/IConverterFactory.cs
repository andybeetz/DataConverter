namespace DataConverter.Interfaces
{
	public interface IConverterFactory
	{
		IInputConverter GetInputConverter(string inputConverterType);
		IOutputConverter GetOutputConverter(string outputConverterType);

		public void AddInputConverter(IInputConverter inputConverter);
		public void AddOutputConverter(IOutputConverter outputConverter);
	}
}
