# WindowsServiceUtil
WindowsServiceUtil (wsu.exe) is a command line utility to help with (un)install Windows Services. This first version is actually just a help to stop a Windows Service because I experienced that eventhough ```net.exe``` is used to synchoniously stop the service, then Windows can still have lock on the files for an unknown period of time aften ```net.exe``` returns STOPPED. 

## API

> wsu.exe stop -name=```SERVICENAME``` -timeout=```TIME_IN_MS```


*In generelt the utility is case insensitive.*

*```Timeout``` has a default value of 30000.*

*```ServiceName``` is mandatory.*

### Examples

> wsu.exe stop -name=ISWindowsService -timeout=3000
>
> wsu.exe stop -name="IS Windows Service" -timeout=3000
>
> wsu.exe stop -name="IS Windows Service"

## How does it work

### Stop
Under the hood the utility uses ```sc.exe``` to query the Windows Service process id (PID). Then the service is requested to stop using ```net.exe```. After ```net.exe``` returns the PID is used to wait for the actual process to Exit. When the process is exit'ed, ```wsu.exe``` will return an ExitCode. 
Any installer (InnoSetup) can then continue to update the actual files (Install process) or the files can be deleted (uninstall process).

### ExitCodes

```Csharp
/// <summary>
/// Successfully 
/// </summary>
internal const int Success = 0;

/// <summary>
/// Sucessfully and was initially running
/// </summary>
internal const int SuccessNeedsRestart = 1;

/// <summary>
/// Command is unknown error
/// </summary>
internal const int UnknownCommand = 10;

/// <summary>
/// Failed with unknown error
/// </summary>
internal const int FailedUnknown = 11;

/// <summary>
/// Missing parameters error
/// </summary>
internal const int MissingParameters = 12;

/// <summary>
/// Stopping Windows Service failed
/// </summary>
internal const int ServiceStopFailed = 13;
```

## What's included

- 'Source' folder contains both the code related to 'WindowsServiceUtil' and also a sample of a Windows Service. 
- The binary ```wsu.exe``` is added to the root of repo.
- A sample InnoSetup script ```WindowsService.iss``` is place in the root of the repo.

# How to tryout

- Clone this repository.
- Start PowerShell as 'Administrator'.
- Run '.\Build.ps1' to publish service files into '.\BuildOutput'.
- Compile and run script '.\WindowsService.iss'.


