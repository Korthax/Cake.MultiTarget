using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Core;

public static class RunIsolated
{
    public static CakeReport Target(IReadOnlyList<CakeTask> tasks, Func<string, CakeTaskBuilder<ActionTask>> task, Func<string, CakeReport> runTarget, string target)
    {
        if (tasks == null)
            throw new ArgumentNullException(nameof(tasks));

        if (task == null)
            throw new ArgumentNullException(nameof(task));

        if (runTarget == null)
            throw new ArgumentNullException(nameof(runTarget));

        if (target == null)
            throw new ArgumentNullException(nameof(target));

        var targetToRun = tasks
            .Where(x => x.Name.Equals(target))
            .Cast<ActionTask>()
            .FirstOrDefault();

        if (targetToRun == null)
            throw new CakeException($"Cannot find task that matches '{target}'.");

        var taskName = $"Isolated: {target}";
        var isolatedTargetTask = task(taskName);

        foreach(var action in targetToRun.Actions)
            isolatedTargetTask.Does(action);

        foreach (var action in targetToRun.Criterias)
            isolatedTargetTask.WithCriteria(action);

        if(targetToRun.ErrorHandler != null)
            isolatedTargetTask.OnError(targetToRun.ErrorHandler);

        if(targetToRun.Description != null)
            isolatedTargetTask.Description(targetToRun.Description);

        if(targetToRun.FinallyHandler != null)
            isolatedTargetTask.Finally(targetToRun.FinallyHandler);

        if(targetToRun.ErrorReporter != null)
            isolatedTargetTask.ReportError(targetToRun.ErrorReporter);

        return runTarget(taskName);
    }
}