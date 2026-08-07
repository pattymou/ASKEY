$ErrorActionPreference = "Stop"
$TaskName = "OpenClaw Gateway Hidden"

Stop-ScheduledTask -TaskName $TaskName -ErrorAction SilentlyContinue
Start-Sleep -Seconds 2
Start-ScheduledTask -TaskName $TaskName
Start-Sleep -Seconds 3

$task = Get-ScheduledTask -TaskName $TaskName
$info = Get-ScheduledTaskInfo -TaskName $TaskName
Write-Host "Task: $TaskName"
Write-Host "State: $($task.State)"
Write-Host "LastTaskResult: $($info.LastTaskResult)"
