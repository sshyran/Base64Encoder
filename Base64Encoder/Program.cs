namespace Base64Encoder
{
    using System;
    using System.IO;

    // A simple demonstration program to show how to encode and decode a file using base64,
    // via the built-in .Net Convert class.
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Run(args);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Caught exception: " + ex);
            }

            Console.Out.Write("Press <ENTER> to quit. ");
            Console.In.ReadLine();

            return;
        }

        private static void Run(string[] args)
        {
            if(args.Length != 1)
            {
                throw new InvalidOperationException("Incorrect number of arguments.");
            }

            string filename = args[0];
            if (!File.Exists(filename))
            {
                throw new InvalidOperationException("File '" + filename + "' could not be found.");
            }

            Log("About to read file: '{0}'.", filename);
            byte[] contents = File.ReadAllBytes(filename);
            Log("Read {0} bytes.", contents.Length);

            Log("About to convert to base 64.");
            string base64 = Convert.ToBase64String(contents);
            Log("Converted to {0} characters.", base64.Length);

            string base64Filename = filename + ".base64";
            Log("Saving base64 encoded text to: ", base64Filename);
            File.WriteAllText(base64Filename, base64);
            Log("Encoding complete.");

            Log("About to read encoded file: '{0}'.", base64Filename);
            string base64FromFile = File.ReadAllText(base64Filename);
            Log("Read {0} characters.", base64FromFile.Length);

            Log("About to convert from base 64.");
            byte[] decodedBase64 = Convert.FromBase64String(base64FromFile);
            Log("Converted to {0} bytes.", decodedBase64.Length);

            string decodedFilename = filename + ".decoded";
            Log("Saving decoded bytes to: ", decodedFilename);
            File.WriteAllBytes(decodedFilename, decodedBase64);
            Log("Decoding complete.");

            Log("About to compare original and decoded bytes.");
            if (contents.Length != decodedBase64.Length)
            {
                Log("Number of decoded bytes ({0}) is different from original ({1}).", decodedBase64.Length, contents.Length);
            }
            else
            {
                int? differenceFoundAt = null;
                for(int counter = 0; counter < contents.Length; counter++)
                {
                    if (contents[counter] != decodedBase64[counter])
                    {
                        differenceFoundAt = counter;
                        break;
                    }
                }

                if (differenceFoundAt.HasValue)
                {
                    Log("Decoded bytes were different. (This should never happen.) First difference found at index: ", differenceFoundAt.Value);
                }
                else
                {
                    Log("Decoded bytes match the original content!");
                }
            }

            return;
        }

        static void Log(string message, params object[] args)
        {
            Console.Out.WriteLine(message, args);

            return;
        }
    }
}
