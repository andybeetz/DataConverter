using System.Collections.Generic;

namespace DataConverter.Model
{
	public class DataRecord
	{
		public List<RecordItem> Items = new List<RecordItem>();
	}

	public class RecordItem
	{
		public string Name;
	}

	public class SingleItem : RecordItem
	{
		public string Value;
	}

	public class ItemGroup : RecordItem
	{
		public List<RecordItem> Items = new List<RecordItem>();
	}
}
