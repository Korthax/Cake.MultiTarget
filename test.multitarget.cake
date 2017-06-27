var target = Argument("target", "A");

Task("A")
    .Does(() =>
    {
        Information("A");
    });

Task("B")
    .Does(() =>
    {
        Information("B");
    });

RunMultiple.Targets(Task, RunTarget, target);