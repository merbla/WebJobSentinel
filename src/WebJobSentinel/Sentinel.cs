#region

using System;
using System.IO;

#endregion

namespace WebJobSentinel
{
    public class Sentinel : IDisposable
    {
        public const string WebjobsShutdownFile = "WEBJOBS_SHUTDOWN_FILE";
        private readonly FileSystemWatcher _shutdownFileSystemWatcher;

        public Sentinel()
        {
            string shutdownFile = Environment.GetEnvironmentVariable(WebjobsShutdownFile);

            _shutdownFileSystemWatcher = new FileSystemWatcher(Path.GetDirectoryName(shutdownFile));
            _shutdownFileSystemWatcher.Created += (sender, args) => OnShutdownFileCreated(args);
            _shutdownFileSystemWatcher.Changed += (sender, args) => OnShutdownFileChanged(args);
            _shutdownFileSystemWatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName | NotifyFilters.LastWrite;
            _shutdownFileSystemWatcher.IncludeSubdirectories = false;
            _shutdownFileSystemWatcher.EnableRaisingEvents = true;
        }

        public Sentinel(Action<FileSystemEventArgs> onShutdownFileCreated,
            Action<FileSystemEventArgs> onShutdownFileChanged) : this()
        {
            OnShutdownFileCreated = onShutdownFileCreated;
            OnShutdownFileChanged = onShutdownFileChanged;
        }

        public Action<FileSystemEventArgs> OnShutdownFileCreated { get; set; }
        public Action<FileSystemEventArgs> OnShutdownFileChanged { get; set; }

        public void Dispose()
        {
            _shutdownFileSystemWatcher.Dispose();
        }
    }
}
