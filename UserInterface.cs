using EmailVerify.Controllers;
using Spectre.Console;

namespace EmailVerify;

internal class UserInterface
{
    private string? _emailAddress;
    public void ShowMainMenu()
    {
        while (true)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[bold teal]Email Verifier\n[/]");
            AnsiConsole.MarkupLine("[red3_1]────────────────────────────────────\n[/]");

            string userEntry = AnsiConsole.Ask<string>("[white]Enter An Email Address Or 0 To Exit:[/]");
            if (Int32.TryParse(userEntry, out int exit))
                if (exit == 0)
                    break;
            _emailAddress = userEntry;

            ProcessUserEntry();
            AnsiConsole.MarkupLine($"[white]Press Any Key To Continue[/]");
            Console.ReadKey();
        }
    }

    private void ProcessUserEntry()
    {
        ValidateEmailAddress validateEmailAddress = new(_emailAddress);
        validateEmailAddress.CheckEmailPossibleTypoFix();

        ShowStatusLogo();
    }

    private void ShowStatusLogo()
    {
        AnsiConsole.Status()
                   .Start("Processing...", ctx =>
                   {
                       // Simulate some work
                       AnsiConsole.MarkupLine("Processing Email Address...");
                       Thread.Sleep(1000);

                       // Update the status and spinner
                       ctx.Status("Processing");
                       ctx.Spinner(Spinner.Known.Star);
                       ctx.SpinnerStyle(Style.Parse("green"));
                   });
    }
}