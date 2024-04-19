using Polly;
using RetryResilience;
using Service.Model;
using Service.Sms;

namespace WebApi.Extensions
{
    public static class DefineServicesToContainerWebApplication
    {
        public static void AddSimulateSms(this WebApplicationBuilder webApplication)
        {
            webApplication.Services.AddScoped<ISimulateSms, SimulateSms>();
        }

        public static void AddResiliencePipeline(this WebApplicationBuilder webApplication)
        {
            webApplication.Services.AddResiliencePipeline<string, SmsResponse>
            (SmsPipeLine.KeyPipeLine, pipeline => { pipeline.AddRetry(SmsPipeLine.StrategySms(5)); });
        }
    }
}