using System;
using System.Collections.Generic;
using System.IO;
using LEAP.Apps.ReadViewerDesktop.Core.Services;

namespace LEAP.Apps.ReadViewerDesktop.Services
{
    public class CommandParameterServiceProvider : ICommandParameterService
    {
        private readonly Dictionary<string, string> _parameters;

        public CommandParameterServiceProvider()
        {
            _parameters = new Dictionary<string, string>();

            var args = Environment.GetCommandLineArgs();

            var previousCommand = string.Empty;

            // Check for file parameter
            if (args.Length > 1)
            {
                var fileName = args[1];

                if (File.Exists(fileName))
                {
                    var extension = Path.GetExtension(fileName)?.ToLower();

                    _parameters["launchfilepath"] = fileName;
                    _parameters["launchfileextension"] = extension;
                }
            }

            // Extract command pairs and flags
            foreach (var arg in args)
            {
                if (arg.Length > 1 && arg.Substring(0, 1) == "/")
                {
                    // Is Command
                    previousCommand = arg.Substring(1, arg.Length - 1).Trim().ToLower();

                    if (!_parameters.ContainsKey(previousCommand))
                    {
                        _parameters[previousCommand] = true.ToString();
                    }
                }
                else if (!string.IsNullOrEmpty(previousCommand))
                {
                    // Is Value
                    _parameters[previousCommand] = arg;
                }
            }

        }

        public string GetValue(string key)
        {
            return _parameters.ContainsKey(key.Trim().ToLower()) ? _parameters[key] : null;
        }
    }
}
