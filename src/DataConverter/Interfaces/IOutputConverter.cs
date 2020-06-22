using DataConverter.Model;

using System.Collections.Generic;

namespace DataConverter.Interfaces
{
	public interface IOutputConverter
	{
		public bool PushOutput(IEnumerable<DataRecord> data, string outputLocation);
		string SupportedType { get; }
	}
}
