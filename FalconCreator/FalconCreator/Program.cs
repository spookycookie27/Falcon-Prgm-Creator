using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using FalconCreator.AkaiModels;
using Newtonsoft.Json;

namespace FalconCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Loading Akai Programs");
            var programs = LoadPrograms();

            foreach (var program in programs)
            {
                WriteFalconProgram(program);
            }


            Console.WriteLine("Finished");
        }

        private static void WriteFalconProgram(AkaiProgram program)
        {
            var name = program.Name;
            Console.WriteLine("Creating Falcon Program");
            var xmlCreator = new FalconXmlCreator(program);
            var xml = xmlCreator.CreateXml();
            using (var writer = XmlWriter.Create($@"D:\FalconPrograms\{name}.uvip",
                new XmlWriterSettings {OmitXmlDeclaration = true, Indent = true})) xml.Save(writer);
            Console.WriteLine("Creating Falcon Program");
        }

        private static List<AkaiProgram> LoadPrograms()
        {
            List<AkaiProgram> programs;
            using (StreamReader r = new StreamReader("AllPrograms.json"))
            {
                string json = r.ReadToEnd();
                programs = JsonConvert.DeserializeObject<List<AkaiProgram>>(json);
            }

            return programs;
        }
    }
}
