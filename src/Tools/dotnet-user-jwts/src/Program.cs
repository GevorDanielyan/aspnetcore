// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.Tools.Internal;

namespace Microsoft.AspNetCore.Authentication.JwtBearer.Tools;

public class Program
{
    private readonly IConsole _console;
    private readonly IReporter _reporter;

    public Program(IConsole console)
    {
        _console = console;
        _reporter = new ConsoleReporter(console);
    }

    public static void Main(string[] args)
    {
        new Program(PhysicalConsole.Singleton).Run(args);
    }

    public void Run(string[] args)
    {
        ProjectCommandLineApplication userJwts = new(_reporter)
        {
            Name = "dotnet user-jwts"
        };

        userJwts.HelpOption("-h|--help");

        // dotnet user-jwts list
        ListCommand.Register(userJwts);
        // dotnet user-jwts create
        CreateCommand.Register(userJwts);
        // dotnet user-jwts print ecd045
        PrintCommand.Register(userJwts);
        // dotnet user-jwts delete ecd045
        DeleteCommand.Register(userJwts);
        // dotnet user-jwts clear
        ClearCommand.Register(userJwts);
        // dotnet user-jwts key
        KeyCommand.Register(userJwts);

        // Show help information if no subcommand/option was specified.
        userJwts.OnExecute(() => userJwts.ShowHelp());

        userJwts.Execute(args);
    }
}
