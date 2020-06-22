using DataConverter.Interfaces;
using DataConverter.Model;

using Newtonsoft.Json.Linq;

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataConverter.Conversion.Converters
{
	public class JsonConverter : IOutputConverter
	{
		private IFileStreamProvider _fileStreamProvider;

		public string SupportedType => "json";

		public JsonConverter(IFileStreamProvider fileStreamProvider)
		{
			_fileStreamProvider = fileStreamProvider;
		}

		public bool PushOutput(IEnumerable<DataRecord> data, string outputLocation)
		{
			if(data == null || data.Count() == 0)
			{
				return false;
			}

			var jsonDoc = new JArray();

			foreach(var record in data)
			{
				var jRecord = new JObject();

				foreach(var item in record.Items)
				{
					if(item is SingleItem)
					{
						jRecord.Add(item.Name, JToken.FromObject(((SingleItem)item).Value));
					}

					if(item is ItemGroup)
					{
						var itemGroup = item as ItemGroup;

						var jGroup = new JObject();

						foreach(var groupedItem in itemGroup.Items)
						{
							jGroup.Add(groupedItem.Name, JToken.FromObject(((SingleItem)groupedItem).Value));
						}

						jRecord.Add(item.Name, jGroup);
					}
				}

				jsonDoc.Add(jRecord);
			}

			var stream = _fileStreamProvider.GetFileStream(outputLocation);
			using(var writer = new StreamWriter(stream, leaveOpen: true))
			{
				writer.Write(jsonDoc.ToString());
				writer.Flush();
				writer.Close();
			}

			return true;
		}
	}
}
