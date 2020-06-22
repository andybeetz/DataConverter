using DataConverter.Interfaces;

using System.IO;

namespace DataConverter.Infrastructure
{
	public class FileStreamProvider : IFileStreamProvider
	{
		public Stream GetFileStream(string path)
		{
			if(File.Exists(path))
			{
				return new FileStream(path, FileMode.Open);
			}

			return new FileStream(path, FileMode.CreateNew);
		}
	}
}
