#define ServiceName 'ISWindowsService'

[Setup]
AppName=IS Windows Service
AppVersion=1.0.0
DefaultDirName={autopf}\ISWindowService
DisableWelcomePage=No

[Files]
Source: "wsu.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "BuildOutput\*"; DestDir: "{app}"; Flags: ignoreversion createallsubdirs recursesubdirs

[Run]
// create service
Filename: "{sys}\sc.exe"; Parameters: "create ""{#ServiceName}"" start= auto binPath= ""{app}\InnoSetupWindowsService.exe"""; Flags: runhidden
// start service (only if 'DoStartService' return true)  displayname=""{#ServiceName}"" start= auto binPath= ""{app}\InnoSetupWindowsService.exe"""; Flags: runhidden
// start service (only if 'DoStartService' return true) 
Filename: "{sys}\net.exe"; Parameters: "start ""{#ServiceName}"""; Flags: runhidden; Check: DoStartService

[UninstallRun]
// delete service
Filename: "{sys}\sc.exe"; Parameters: "delete ""{#ServiceName}"""; Flags: runhidden 

[Code]
var 
  RestartService : Boolean;

const
  Success = 0;
  SuccessNeedsRestart = 1;
  UnknownCommand = 10;
  FailedUnknown = 11;
  MissingParameters = 12;
  ServiceStopFailed = 13;

// return whether restart is needed
function DoStartService(): Boolean;
begin
  Result := RestartService;
end;

// Stop Windows Service
function StopService(wsu: String; ServiceName: String): Integer;
var
  ResultCode: Integer;
begin
  Log('wsu file used: ' + wsu)

  if (FileExists(wsu)) then
  begin
    // run wsu to stop service
    if (Exec(wsu, 'stop name="' + ServiceName + '"', '', SW_HIDE, ewWaitUntilTerminated, ResultCode)) then
    begin
      // wsu ran successfully, use exitcode as result
      Result := ResultCode;
    end
    else
    begin
      // failed to execute wsu, thus return error 
      Result := FailedUnknown; // use wsu error codes
    end;
  end
  else
  begin
    // wsu file not present means we are installing for the first time.
    // thus return success but not restart
    Result := Success; 
  end;

  // log
  Log('StopService() returns: ' + IntToStr(Result))
end;

// Prepare to install
function PrepareToInstall(var NeedsRestart: Boolean): String;
var
  Res: Integer;
  ErrMessage: String;
  wsu: String;
begin
  // unpack newest version to temp folder  
  ExtractTemporaryFile('wsu.exe');
  
  // get temp file reference
  wsu := ExpandConstant('{tmp}\wsu.exe');
          
  // stop service
  Res := StopService(wsu, ExpandConstant('{#ServiceName}'));

  // handle restart
  if (Res = SuccessNeedsRestart) then
  begin
    RestartService := true;
  end;
   
  // handle error
  if (Res >= UnknownCommand) then
  begin
    ErrMessage := 'Failed to stop service with error code: ' + IntToStr(Res);
    Log(ErrMessage)
    Result := ErrMessage;
  end;
end;

// uninstall step handler
procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
var
  Res: Integer;
  wsu: String;
  ErrMessage: String;
begin 
  case CurUninstallStep of
    usUninstall:
    begin
      wsu := ExpandConstant('{app}\wsu.exe');

      Res := StopService(wsu, ExpandConstant('{#ServiceName}'));

      // log error
      if (Res >= UnknownCommand) then
      begin
        ErrMessage := 'Failed to stop service with error code: ' + IntToStr(Res);
        Log(ErrMessage)
      end;
    end;
  end;
end;