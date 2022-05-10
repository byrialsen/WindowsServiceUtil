namespace WindowsServiceUtil
{
    using System;
    using System.Linq;

    /// <summary>
    /// <see cref="Arguments"/> class that has all parse arguments
    /// </summary>
    internal class Arguments
    {
        public int Timeout { get; private set; }

        public Command Command { get; private set; }

        public string ServiceName { get; private set; }

        /// <summary>
        /// Private constructor
        /// </summary>
        private Arguments() 
        {
            Timeout = 30000;
            Command = Command.Unknown;
        }

        /// <summary>
        /// Parse arguments into properties
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Arguments Parse(string[] args)
        {
            // init
            args = args ?? new string[0];
            Arguments argument = new Arguments();

            // check number of parameters 
            if (args.Length < 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            // resolve command
            if (Enum.TryParse<Command>(args[0], true, out Command command))
            {
                argument.Command = command;
            }

            foreach (string arg in args.Skip(1))
            {
                string[] parts = arg.Split(new char[] { '=' }, 2, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 2)
                {
                    string key = parts[0]
                        .Trim(new char[] { '/', '-'} )
                        .Trim()
                        .ToLower();

                    string value = parts[1].Trim();

                    switch (key)
                    {
                        case "name":
                            argument.ServiceName = value;
                            break;

                        case "timeout":
                            if (int.TryParse(value, out int timeout))
                            {
                                argument.Timeout = timeout;
                            }
                            break;
                    }
                }
            }

            return argument;
        }
    }
}