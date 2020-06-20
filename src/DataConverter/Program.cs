
using DataConverter.Configuration;

namespace DataConverter
{
	class Program
	{
		static int Main(string[] args)
		{
			var options = OptionsParser.Parse(args);

			if(options.Parsed)
			{
				return 0;
			}
			else
			{
				return 1;
			}
		}
	}
}
