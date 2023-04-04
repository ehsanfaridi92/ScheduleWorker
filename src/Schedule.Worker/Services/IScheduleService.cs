namespace Schedule.Worker.Services;

public interface IScheduleService
{
    public Task DoSomething(CancellationToken cancellationToken=default);
}

