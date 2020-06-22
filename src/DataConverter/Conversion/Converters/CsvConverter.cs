using CsvHelper;

using DataConverter.Interfaces;
using DataConverter.Model;

using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace DataConverter.Conversion.Converters
{
	public class CsvConverter : IInputConverter, IOutputConverter
	{
		private IFileStreamProvider _fileStreamProvider;

		public CsvConverter(IFileStreamProvider fileStreamProvider)
		{
			_fileStreamProvider = fileStreamProvider;
		}

		public string SupportedType => "csv";

		public bool GetInput(string inputLocation, out IEnumerable<DataRecord> inputData)
		{
			if(string.IsNullOrWhiteSpace(inputLocation))
			{
				inputData = null;
				return false;
			}

			var inputStream = _fileStreamProvider.GetFileStream(inputLocation);
			DataTable parsedData;

			using(var reader = new StreamReader(inputStream))
			using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
			{
				csv.Configuration.HasHeaderRecord = true;
				using(var dr = new CsvDataReader(csv))
				{
					parsedData = new DataTable();
					parsedData.Load(dr);
				}
			}

			var data = new List<DataRecord>();

			foreach(DataRow row in parsedData.Rows)
			{
				var record = new DataRecord();
				foreach(DataColumn column in row.Table.Columns)
				{
					if(column.ColumnName.Contains("_"))
					{
						var existingItem = (ItemGroup)record.Items.Where(i => i.Name == column.ColumnName.Split('_').First()).FirstOrDefault();
						if(existingItem != null)
						{
							var item = new SingleItem
							{
								Name = column.ColumnName.Split('_')[1],
								Value = row.Field<string>(column)
							};
							existingItem.Items.Add(item);
						}
						else
						{
							var group = new ItemGroup()
							{
								Name = column.ColumnName.Split('_').First()
							};
							var item = new SingleItem
							{
								Name = column.ColumnName.Split('_')[1],
								Value = row.Field<string>(column)
							};
							group.Items.Add(item);
							record.Items.Add(group);
						}
					}
					else
					{
						var item = new SingleItem
						{
							Name = column.ColumnName,
							Value = row.Field<string>(column)
						};
						record.Items.Add(item);
					}
				}

				data.Add(record);
			}

			inputData = data;

			return true;
		}

		public bool PushOutput(IEnumerable<DataRecord> data, string outputLocation)
		{
			if(data == null || data.Count() == 0)
			{
				return false;
			}

			foreach(var record in data)
			{
				foreach(var item in record.Items)
				{
					if(item is SingleItem)
					{
					}

					if(item is ItemGroup)
					{
						var itemGroup = item as ItemGroup;

						foreach(var groupedItem in itemGroup.Items)
						{
						}
					}
				}
			}

			var stream = _fileStreamProvider.GetFileStream(outputLocation);
			using(var writer = new StreamWriter(stream, leaveOpen: true))
			using(var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
			{
				// write header
				foreach(var item in data.First().Items)
				{
					if(item is SingleItem)
					{
						csv.WriteField(((SingleItem)item).Name);
					}

					if(item is ItemGroup)
					{
						var itemGroup = item as ItemGroup;
						var firstPartOfName = item.Name;

						foreach(var groupedItem in itemGroup.Items)
						{
							csv.WriteField($"{firstPartOfName}_{((SingleItem)groupedItem).Name}");
						}
					}
				}
				csv.NextRecord();

				foreach(var record in data)
				{
					foreach(var item in record.Items)
					{
						if(item is SingleItem)
						{
							csv.WriteField(((SingleItem)item).Value);
						}

						if(item is ItemGroup)
						{
							var itemGroup = item as ItemGroup;

							foreach(var groupedItem in itemGroup.Items)
							{
								csv.WriteField(((SingleItem)groupedItem).Value);
							}
						}
					}
					csv.NextRecord();
				}
				csv.Flush();
			}

			return true;
		}
	}
}
