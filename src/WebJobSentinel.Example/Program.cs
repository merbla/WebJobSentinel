#region

using System;
using System.Threading;
using Serilog;

#endregion

namespace WebJobSentinel.Example
{
    namespace WebJobSentinel.Example
    {
        internal class Program :IDisposable

        {
            private static bool _running = true;
            private static Sentinel _sentinel;

            private static void Main()
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.ColoredConsole()
                    .CreateLogger();


                Log.Information("Starting");


                _sentinel = new Sentinel(args => Log.Information("File Created"),
                    args => Log.Information("File Changed"));


                RepeatAction.OnInterval(TimeSpan.FromSeconds(2), () =>
                {
                    Log.Information("Running and waiting");

                }, new CancellationToken());


                //// Run as long as we didn't get a shutdown notification
                //while (_running)
                //{
                //    // Here is my actual work
                //    Log.Information("Running and waiting " + DateTime.UtcNow);
                //    Thread.Sleep(1000);
                //}

                Log.Information("Stopped");
            }

            public void Dispose()
            {
                _sentinel.Dispose();
            }
        }
    }
}