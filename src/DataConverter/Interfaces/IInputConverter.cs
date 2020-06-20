namespace DataConverter.Interfaces
{
	public interface IInputConverter
	{
		bool GetInput(string inputType, string inputLocation, out object inputData);
		string SupportedType { get; }
	}
}
