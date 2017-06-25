using System;
using Cake.Core;
using Cake.Core.Diagnostics;

public static class RunMultiple
{
    private const string TaskName = "Run multiple targets";

    public static CakeReport Targets(Func<string, CakeTaskBuilder<ActionTask>> task, Func<string, CakeReport> runTarget, string targets)
    {
        if(task == null)
            throw new ArgumentNullException(nameof(task));

        if(runTarget == null)
            throw new ArgumentNullException(nameof(runTarget));

        if(targets == null)
            throw new ArgumentNullException(nameof(targets));

        var tokens = targets.Split(',');

        if (tokens.Length == 0)
            throw new CakeException("At least one target must be provided.");

        var multiTargetTask = task(TaskName)
            .Does(context =>
            {
                context.Log.Information($"Targets ran: {string.Join(", ", tokens)}");
            });

        foreach(var token in tokens)
            multiTargetTask.IsDependentOn(token);

        return runTarget(TaskName);
    }
}