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
        // the proces steps , first check if the syntax is correct or not ,
        // secondly check if the email likliness of the email being valid or not using the confidence sscore levels 
        // finally check the Mx Check and detemine if the email can recieve email or not.
        ValidateEmailAddress validateEmailAddress = new(_emailAddress);
        ShowStatusLogo();
        validateEmailAddress.CheckEmailPossibleTypoFix();
        if (!validateEmailAddress.IsEmailAddressFormatValid())
        {
            AnsiConsole.MarkupLine("[bold red]The format you entered is invalid! Please try again.[/]");
            return;
        }
        var confidenceScore = validateEmailAddress.GetEmailAddressValidConfidenceScore();
        if (confidenceScore < 0.6)
        {
            AnsiConsole.MarkupLine("[bold red]The email you provided is likely invalid[/]");
           
        }
        else if (confidenceScore >= 0.6 & confidenceScore <= 0.8)
        {
            AnsiConsole.MarkupLine("[bold yellow]The email you provided is potentially valid[/]");
            
        }
        else if (confidenceScore > 0.8)
        {
            AnsiConsole.MarkupLine("[bold green]The email you provided is likely valid[/]");
            
        }
        if (!validateEmailAddress.EmailCanRecieveMail())
        {
            AnsiConsole.MarkupLine("[bold red]The email you provided is un capable of recieving emails[/]");
            
        }
        else
        {
            AnsiConsole.MarkupLine("[bold green]The email you provided is un capable of recieving emails[/]");
        }
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