using Service.Model;
using Service.Sms.Exception;
using static System.Console;
namespace Service.Sms;

public class SimulateSms : ISimulateSms
{
    public async Task<SmsResponse> SendSms(byte probabilityFailed)
    {
        var random = new Random();
        var chance = random.Next(100);

        if (chance < probabilityFailed)
        {
            AfterOperationConsole("Simulating an error in sending an SMS", ConsoleColor.Red);
            throw new SmsException("EX567");
        }

        AfterOperationConsole("SMS sent successfully.", ConsoleColor.Green);
        return new SmsResponse();
    }

    private void AfterOperationConsole(object value, ConsoleColor consoleColor)
    {
        ForegroundColor = consoleColor;
        WriteLine(value);
        ResetColor();
    }
}