using Quartz;

namespace QuartzApp;

public class TestJob(ILogger<TestJob> logger) : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Test Job is executing {}", DateTime.Now);

        return Task.CompletedTask;
    }
}