using System;
using System.Collections.Generic;
using System.IO;

namespace FileSystemVisitor
{
    public class FileSystemVisitor
    {
        public event EventHandler Start;
        public event EventHandler Finish;
        public event EventHandler<string> FileFound;
        public event EventHandler<string> DirectoryFound;
        public event EventHandler<string> FilteredFileFound;
        public event EventHandler<string> FilteredDirectoryFound;

        private readonly Func<string, bool> _filter;

        public FileSystemVisitor() : this(null) { }

        public FileSystemVisitor(Func<string, bool> filter)
        {
            _filter = filter;
        }

        public IEnumerable<string> Traverse(string path)
        {
            foreach (var directory in Directory.EnumerateDirectories(path))
            {
                if (_filter == null || _filter(directory))
                    yield return directory;

                foreach (var file in Traverse(directory))
                    yield return file;
            }

            foreach (var file in Directory.EnumerateFiles(path))
            {
                if (_filter == null || _filter(file))
                    yield return file;
            }
        }

        public IEnumerable<string> TraverseWithEvents(string path)
        {
            Start?.Invoke(this, EventArgs.Empty);

            foreach (var directory in Directory.EnumerateDirectories(path))
            {
                DirectoryFound?.Invoke(this, directory);

                if (_filter == null || _filter(directory))
                {
                    FilteredDirectoryFound?.Invoke(this, directory);
                    yield return directory;
                }

                foreach (var file in TraverseWithEvents(directory))
                    yield return file;
            }

            foreach (var file in Directory.EnumerateFiles(path))
            {
                FileFound?.Invoke(this, file);

                if (_filter == null || _filter(file))
                {
                    FilteredFileFound?.Invoke(this, file);
                    yield return file;
                }
            }
            Finish?.Invoke(this, EventArgs.Empty);
        }
    }

}