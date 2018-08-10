using System;
using System.Text;

namespace Workforce.FixedWidthExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            RegisterServices();

            var arguments = new Arguments(args);

            if (arguments.AreInvalid())
                throw new ArgumentException("Some arguments are invalid.");

            var template = TemplateReader.Read(arguments.TemplatePath);

            var dataFlow = new DataFlow(
                arguments.SourcePath,
                arguments.DestinationPath,
                template);

            dataFlow.Run();
        }

        static void RegisterServices()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }
    }
}
