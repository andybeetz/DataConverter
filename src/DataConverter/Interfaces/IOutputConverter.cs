namespace DataConverter.Interfaces
{
	public interface IOutputConverter
	{
		public bool PushOutput(object data, string outputType, string outputLocation);
		string SupportedType { get; }
	}
}
