using Microsoft.VisualBasic;
using static Google.Protobuf.WellKnownTypes.Field.Types;
using System.Text;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PacketGenerator
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            string[] lines;
            if (args.Length == 0)
            {
                lines = File.ReadAllLines("../../../proto/Protocol.proto");
            }
            else
            {
                Console.WriteLine(args[0]);
                lines = File.ReadAllLines(args[0]);
            }

            List<string> messageNames = new List<string>();
            foreach (var line in lines)
            {
                if (line.Contains("message") == false)
                    continue;

                string name = line;
                name = name
                    .Replace("message", string.Empty)
                    .Replace("{", string.Empty)
                    .Trim();

                messageNames.Add(name);
            }

            string serverData = "";
            string clientData = "";
            foreach (var m in messageNames)
            {
                var data = string.Format(PacketManagerFormat.DataFormat, m, m.Replace("_", string.Empty));
                if (m.StartsWith("S_"))
                {
                    clientData += data;
                }
                else if (m.StartsWith("C_"))
                {
                    serverData += data;
                }
            }

            string serverText = string.Format(PacketManagerFormat.mainFormat, serverData);
            string clientText = string.Format(PacketManagerFormat.mainFormat, clientData);
            File.WriteAllText("ServerPacketManager.cs", serverText);
            File.WriteAllText("ClientPacketManager.cs", clientText);
        }
    }
}