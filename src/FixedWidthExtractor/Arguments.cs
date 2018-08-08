namespace Workforce.FixedWidthExtractor
{
    public class Arguments
    {
        private const char separator = '=';

        public string SourcePath { get; private set; }

        public string DestinationPath { get; private set; }

        public string TemplatePath { get; private set; }

        public Arguments(string[] args)
        {
            Load(args);
        }

        public bool AreValid()
        {
            return !string.IsNullOrEmpty(SourcePath)
                && !string.IsNullOrEmpty(DestinationPath)
                && !string.IsNullOrEmpty(TemplatePath);
        }

        public bool AreInvalid()
        {
            return !AreValid();
        }

        private void Load(string[] args)
        {
            foreach (var arg in args)
            {
                switch (GetOption(arg))
                {
                    case "from":
                        SourcePath = GetValue(arg);
                        break;
                    case "to":
                        DestinationPath = GetValue(arg);
                        break;
                    case "with-template":
                        TemplatePath = GetValue(arg);
                        break;
                }
            }
        }

        private string GetOption(string arg)
        {
            return arg.Substring(0, arg.IndexOf(separator));
        }

        private string GetValue(string arg)
        {
            return arg.Substring(arg.IndexOf(separator) + 1);
        }
    }
}
