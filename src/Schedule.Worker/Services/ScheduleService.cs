namespace Schedule.Worker.Services;

public class ScheduleService : IScheduleService
{

    public async Task DoSomething(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("do something ...");
        await Task.CompletedTask;
    }
}

