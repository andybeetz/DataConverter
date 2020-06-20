using CommandLine;

namespace DataConverter.Configuration
{
	public class Options
	{
		public bool Parsed { get; set; } = false;

		[Option('i', "in",
			Default = ".\\input.csv",
			HelpText = "The location of the input data",
			Required = true)]
		public string InputLocation { get; set; }

		[Option("inputtype",
			Default = "csv",
			HelpText = "The type of input, supported inputs are: csv",
			Required = true)]
		public string InputType { get; set; }

		[Option('o', "out",
			Default = ".\\output.json",
			HelpText = "The desired output location",
			Required = true)]
		public string OutputLocation { get; set; }

		[Option("outputtype",
			Default = "json",
			HelpText = "The type of output, supported outputs are: json",
			Required = true)]
		public string OutputType { get; set; }
	}
}
