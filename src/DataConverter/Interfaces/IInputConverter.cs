using DataConverter.Model;

using System.Collections.Generic;

namespace DataConverter.Interfaces
{
	public interface IInputConverter
	{
		bool GetInput(string inputLocation, out IEnumerable<DataRecord> inputData);
		string SupportedType { get; }
	}
}
