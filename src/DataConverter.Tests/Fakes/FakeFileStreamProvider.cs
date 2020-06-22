using DataConverter.Interfaces;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataConverter.Tests.Fakes
{
	public class FakeFileStreamProvider : IFileStreamProvider
	{
		public string ReadData;
		public FakeFileStreamProvider(string streamDataToRead)
		{
			ReadData = streamDataToRead;
		}

		public Stream GetFileStream(string path)
		{
			var stream = new MemoryStream();
			var writer = new StreamWriter(stream, leaveOpen: true);
			writer.Write(ReadData);
			writer.Flush();
			writer.Dispose();
			stream.Position = 0;
			return stream;
		}
	}
}
