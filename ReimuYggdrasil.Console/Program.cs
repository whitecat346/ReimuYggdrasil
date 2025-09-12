using System.Text;
using Serilog;

namespace ReimuYggdrasil.Console;

public static class Program
{
    private static void LoggerInitialize()
    {
        Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.File(path: "/logs/",
                                  fileSizeLimitBytes: 10 * 1024 * 1024,
                                  encoding: Encoding.UTF8,
                                  rollingInterval: RollingInterval.Day,
                                  buffered: true,
                                  retainedFileCountLimit: 10)
                    .CreateLogger();
    }

    private static async Task Main(string[] args)
    {
        LoggerInitialize();

        // args = ["-port:<port>"]

        var port = "5478";
        foreach (var arg in args)
        {
            var spilied = arg.Split(':', 2, StringSplitOptions.RemoveEmptyEntries);
            switch (spilied[0])
            {
                case "-port":
                    port = spilied.Length > 1 ? spilied[1] : "null";
                    break;
            }
        }

        if (!short.TryParse(port, out var portNum))
        {
            Log.Error("Failed to get port num: Invalid port form. {Port}", port);
            return;
        }

        Log.Information("Get server port: {Port}", portNum);

        await Core.ReimuYggdrasil.RunServer(portNum);

        Log.Information("Started server.");
    }
}
