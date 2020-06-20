namespace DataConverter.Conversion
{
	public class ConversionResult
	{
		public ConversionResultType Type { get; }

		public ConversionResult(ConversionResultType resultType)
		{
			Type = resultType;
		}
	}
}
