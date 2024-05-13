using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemVisitor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var visitor = new FileSystemVisitor(file => true);

            visitor.Start += (sender, e) => Console.WriteLine("Starting directory traversal.");
            visitor.Finish += (sender, e) => Console.WriteLine("Finished directory traversal.");
            visitor.FileFound += (sender, file) => Console.WriteLine($"File found: {file}");
            visitor.DirectoryFound += (sender, dir) => Console.WriteLine($"Directory found: {dir}");
            visitor.FilteredFileFound += (sender, file) => Console.WriteLine($"Filtered file found: {file}");
            visitor.FilteredDirectoryFound += (sender, dir) => Console.WriteLine($"Filtered directory found: {dir}");

            string path = @"C:\Users\Armando_Mora\Git-NET\.NET-Mentoring-Program\FileSystemVisitor";
            foreach (var item in visitor.TraverseWithEvents(path)) { }
        }
    }
}
