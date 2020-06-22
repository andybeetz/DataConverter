using DataConverter.Interfaces;
using DataConverter.Model;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataConverter.Conversion.Converters
{
	public class JsonConverter : IOutputConverter, IInputConverter
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

		public bool GetInput(string inputLocation, out IEnumerable<DataRecord> inputData)
		{
			if(string.IsNullOrWhiteSpace(inputLocation))
			{
				inputData = null;
				return false;
			}

			var inputStream = _fileStreamProvider.GetFileStream(inputLocation);
			using(var reader = new StreamReader(inputStream))
			{
				var json = JArray.Parse(reader.ReadToEnd());

				var jsonReader = json.CreateReader();

				var parsedData = new List<DataRecord>();



				foreach(var jsonRecord in json.Children())
				{
					var newRecord = new DataRecord();
					var children = jsonRecord.Children();
					foreach(var child in jsonRecord.Children())
					{
						if(child is JProperty)
						{
							var prop = child as JProperty;
							if(prop.Value.Type == JTokenType.Object)
							{
								var itemGroup = new ItemGroup
								{
									Name = prop.Name
								};

								// get sub nodes
								foreach(var subChild in prop.Value.Children())
								{
									var subProp = subChild as JProperty;
									itemGroup.Items.Add(new SingleItem() { Name = subProp.Name, Value = (string)subProp.Value });
								}
								newRecord.Items.Add(itemGroup);
							}
							else if(prop.Value.Type == JTokenType.String)
							{
								// get single item
								newRecord.Items.Add(new SingleItem() { Name = prop.Name, Value = (string)prop.Value });
							}
						}
					}

					parsedData.Add(newRecord);
				}

				inputData = parsedData;
			}

			return true;
		}
	}
}
