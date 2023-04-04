using Quartz;
using Quartz.Impl.Matchers;
using Schedule.Worker.Jobs;
using Schedule.Worker.Services;

namespace Schedule.Worker.Configurations;

public static class AppConfigurator
{

    public static void ConfigServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IScheduleService, ScheduleService>();

    }
    public static void ConfigQuartz(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var synchronizingSettings = configuration.GetSection("SynchronizingSettings").Get<SynchronizingSettings>();

        serviceCollection.AddSingleton(synchronizingSettings);
        serviceCollection.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();

            var timeParams = synchronizingSettings.Time?.Split(":");

            var hour = int.Parse(timeParams?[0] == null ? "0" : timeParams[0]);

            var minute = int.Parse(timeParams?[1] is null ? "0" : timeParams[1]);

            q.ScheduleJob<SynchronizerJob>(trigger => trigger
                .WithIdentity(nameof(SynchronizerJob))
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(hour, minute))
            );

            q.AddJobListener<SynchronizerJobListener>(KeyMatcher<JobKey>.KeyEquals(new JobKey(nameof(SynchronizerJob))));

        });
        
        serviceCollection.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });


    }
}

