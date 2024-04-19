using Microsoft.AspNetCore.Mvc;
using Polly;
using Polly.Registry;
using RetryResilience;
using Service.Model;
using Service.Sms;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SmsController(ISimulateSms simulateSms, ResiliencePipelineProvider<string> pipeline) : ControllerBase
{
    private readonly ResiliencePipeline<SmsResponse> _pipeline = pipeline.GetPipeline<SmsResponse>(SmsPipeLine.KeyPipeLine);

    [HttpPost]
    public async Task<SmsResponse> Post() => await simulateSms.SendSms(85);


    [HttpPost("SendSmsResilience")]
    public async Task<SmsResponse> SendSmsWithResilience()
    {
        var executeAsync = await _pipeline.ExecuteAsync(async _ => await simulateSms.SendSms(85));
        return executeAsync;
    }

}