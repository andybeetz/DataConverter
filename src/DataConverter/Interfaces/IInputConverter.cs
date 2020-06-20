using System.Collections.Generic;

namespace DataConverter.Interfaces
{
	public interface IInputConverter
	{
		IEnumerable<string> SupportedTypes();
	}
}
