using System.IO;

namespace DataConverter.Interfaces
{
	public interface IFileStreamProvider
	{
		Stream GetFileStream(string path);
	}
}
