namespace DataConverter.Interfaces
{
	public interface IOutputConverter
	{
		public bool PushOutput(object data, string outputLocation);
		string SupportedType { get; }
	}
}
