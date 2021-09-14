properties {
	$folder = $deploymentDir + "\" + $buildLabel	
}

$folder
$buildLabel

$solution_file = "Strimm.JobAutomationWinService.sln"
$testMessage = 'No tests to run!'
$compileMessage = 'Executed Compile!'
$cleanMessage = 'Executed Clean!'
$executable= $folder + "\Strimm.AutomationWinService.exe"

$executable

task default -depends test

task test -depends compile, clean { 
$testMessage
}

task compile -depends clean { 
	$compileMessage
	exec { msbuild $solution_file /t:Build /p:Configuration=Debug /v:q }
}

task clean { 
	$cleanMessage
	exec { msbuild $solution_file /t:Clean /v:q }
}

task deploy -depends test, compile, clean {
if (test-path $folder) {
	if (Get-Service "StrimmJobAutomation" -ErrorAction SilentlyContinue)
	{
		'Uninstalling existing service'
		exec { & $executable uninstall }
	}
	'Removing existing deployment'
	rd $folder -rec -force 
}

copy-item 'Strimm.AutomationWinService\bin\Debug' $folder -force -recurse -verbose
copy-item 'Strimm.AutomationWinService\views' $folder -force -recurse -verbose
copy-item 'Strimm.AutomationWinService\static' $folder -force -recurse –verbose
copy-item 'BuildDeployAndInstall_AutomationWinService.ps1' $folder -force -Verbose
}

task install {
exec { & $executable install start }
}

task uninstall {
exec { & $executable uninstall }
}

task ? -Description "Helper to display task info" {
	Write-Documentation
}