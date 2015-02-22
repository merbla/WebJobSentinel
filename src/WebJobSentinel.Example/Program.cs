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
                var cts = new CancellationTokenSource();

                Log.Logger = new LoggerConfiguration()
                    .WriteTo.ColoredConsole()
                    .CreateLogger();

                Log.Information("Starting");

                _sentinel = new Sentinel(
                    args => Log.Information("File Created"),
                    args =>
                    {
                        Log.Information("File Changed");
                        cts.Cancel(false);
                    });


                //Alternatively
                //_sentinel.OnShutdownFileChanged += args => { };
                //_sentinel.OnShutdownFileCreated += args => { };

              
                var task = RepeatAction.OnInterval(TimeSpan.FromSeconds(5), () =>
                {
                    //Do work
                    Log.Information("Running and waiting");
                }, cts.Token);


                task.Wait();

                Log.Information("Stopped");
            }
        }
    }
}