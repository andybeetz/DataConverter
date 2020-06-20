namespace DataConverter.Interfaces
{
	public interface IInputConverter
	{
		bool GetInput(string inputLocation, out object inputData);
		string SupportedType { get; }
	}
}
