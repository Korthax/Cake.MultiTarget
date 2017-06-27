var target = Argument("target", "A");

Setup(context => 
{
    foreach(var property in GetType().GetProperties())
        Information(string.Format("P: {0}", property));

    foreach(var field in GetType().GetFields())
        Information(string.Format("F: {0}", field));

    foreach(var method in GetType().GetMethods())
        Information(string.Format("M: {0}", method));
});

Task("C")
    .Does(() =>
    {
        Information("C");
    });

Task("D")
    .IsDependentOn("C")
    .Does(() =>
    {
        Information("D");
    });

RunIsolated.Target(Tasks, Task, RunTarget, target);