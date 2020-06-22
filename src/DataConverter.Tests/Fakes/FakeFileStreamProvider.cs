using DataConverter.Interfaces;

using System.IO;

namespace DataConverter.Tests.Fakes
{
	public class FakeFileStreamProvider : IFileStreamProvider
	{
		public string ReadData;
		public string WrittenData
		{
			get
			{
				_stream.Position = 0;
				using(var reader = new StreamReader(_stream))
				{
					return reader.ReadToEnd();
				}
			}
		}

		private Stream _stream;

		public FakeFileStreamProvider(string streamDataToRead)
		{
			ReadData = streamDataToRead;
		}

		public Stream GetFileStream(string path)
		{
			_stream = new MemoryStream();
			var writer = new StreamWriter(_stream, leaveOpen: true);
			writer.Write(ReadData);
			writer.Flush();
			writer.Dispose();
			_stream.Position = 0;
			return _stream;
		}
	}
}
