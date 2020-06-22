using CsvHelper;

using DataConverter.Interfaces;
using DataConverter.Model;

using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace DataConverter.Conversion.Converters
{
	public class CsvConverter : IInputConverter
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
	}
}
