using Service.Model;

namespace Service.Sms;

public interface ISimulateSms
{
    Task<SmsResponse> SendSms(byte probabilityFailed);
}