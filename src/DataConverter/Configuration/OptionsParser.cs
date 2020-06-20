using CommandLine;

using System.Collections.Generic;

namespace DataConverter.Configuration
{
	public abstract class OptionsParser
	{
		public static Options Parse(IEnumerable<string> arguments)
		{
			if(arguments == null)
			{
				return new Options();
			}

			Options options = null;

			Parser.Default.ParseArguments<Options>(arguments)
				   .WithParsed<Options>(o =>
				   {
					   options = o;
					   options.Parsed = true;
				   })
				   .WithNotParsed<Options>(o =>
				   {
					   options = new Options();
				   });

			return options;
		}
	}
}
