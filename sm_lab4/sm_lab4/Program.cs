using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace sm_lab4
{
    class Program
    {
         static List<string> GetTickets(string dirPath)
        {
            List<string> tickets;
            if (!Directory.Exists(dirPath))
            {
                throw new ArgumentException("not existing direcrtory");
            }

            tickets = Directory.GetFiles(dirPath, "*.pdf", SearchOption.AllDirectories).ToList();
            return tickets;
        }

        static List<string> GetStudentFiles(string dirPath)
        {
            List<string> StudentFiles;
            if (!Directory.Exists(dirPath))
                throw new ArgumentException("not existing direcrtory");
            StudentFiles = Directory.GetFiles(dirPath, "*.txt", SearchOption.AllDirectories).ToList();

            if (StudentFiles.Count == 0)
                throw new ArgumentException("no .txt files");
            return StudentFiles;
        }

        static List<string> GetFIO(List<string> studentFiles)
        {
            List<string> studentFIO = new List<string>();
            string name;
            foreach (var element in studentFiles)
            {
                if (new FileInfo(element).Length == 0)
                    throw new ArgumentException("empty file");
                using (StreamReader file = new StreamReader(element))
                {
                    List<string> tmp = new List<string>();
                    while ((name = file.ReadLine()) != null)
                    {
                        tmp = name.Split(' ').ToList();
                        name = Path.GetFileNameWithoutExtension(element) + ' ' + tmp[0] + ' ' + tmp[1][0] + ' ' + tmp[2][0];
                        studentFIO.Add(name);
                    }
                }
            }

            return studentFIO;
        }
        
        static void ResultFiles(List<string> tickets, List<string> students, string path)
        {
            Random rand = new Random();
            string resultPath;
            
            foreach (var element in students)
            {
                resultPath = Path.GetFullPath(path) + "\\ " + element + ".pdf";
                File.Copy($"{tickets[rand.Next(0, tickets.Count)]}", resultPath);
            }
        }
        
        static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Incorrect args");
                return;
            }

            List<string> names;
            List<string> tickets;
            
            try
            {
                names = GetFIO(GetStudentFiles(args[1]));
                tickets = GetTickets(args[0]);
                ResultFiles(tickets, names, args[2]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}