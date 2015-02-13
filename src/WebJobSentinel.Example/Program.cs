#region

using System;
using System.Threading;
using Serilog;

#endregion

namespace WebJobSentinel.Example
{
    namespace WebJobSentinel.Example
    {
        internal class Program : IDisposable

        {
            private static bool _running = true;
            private static Sentinel _sentinel;

            public void Dispose()
            {
                _sentinel.Dispose();
            }

            private static void Main()
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.ColoredConsole()
                    .CreateLogger();

                Log.Information("Starting");

                _sentinel = new Sentinel(
                    args => Log.Information("File Created"),
                    args => Log.Information("File Changed"));


                var task = RepeatAction.OnInterval(TimeSpan.FromSeconds(5), () =>
                {
                    //Do work
                    Log.Information("Running and waiting");
                }, new CancellationToken());


                task.Wait();

                Log.Information("Stopped");
            }
        }
    }
}