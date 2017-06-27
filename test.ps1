Write-Host -ForegroundColor Cyan "Running Net45 tests..."
.\tools\Cake\Cake.exe test.multitarget.net45.cake --target="A,B"
.\tools\Cake\Cake.exe test.isolatedtarget.net45.cake --target="D"

Write-Host -ForegroundColor Cyan "`nRunning NetStandard1.6 tests..."
dotnet exec .\tools\Cake.CoreCLR\Cake.dll test.multitarget.netstandard16.cake --target="A,B"
dotnet exec .\tools\Cake.CoreCLR\Cake.dll test.isolatedtarget.netstandard16.cake --target="D"