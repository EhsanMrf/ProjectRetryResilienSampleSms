namespace Service.Sms.Exception;

public class SmsException(string codeMessage) : System.Exception
{
    public string CodeMessage { get; set; } = codeMessage;
}