using Quartz;

namespace Schedule.Worker.Jobs;

public class SynchronizerJobListener : IJobListener
{
    public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        Console.WriteLine("ScheduleWorker synchronizer job to be executed.");
        return Task.CompletedTask;
    }

    public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        Console.WriteLine("ScheduleWorker synchronizer job execution vetoed.");
        return Task.CompletedTask;
    }

    public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException,
        CancellationToken cancellationToken = new CancellationToken())
    {
        Console.WriteLine("ScheduleWorker synchronizer was executed.");
        return Task.CompletedTask;
    }

    public string Name => nameof(SynchronizerJob);
}

