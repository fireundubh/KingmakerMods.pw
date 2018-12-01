; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "KingmakerMods.pw"
#define MyAppVersion "1.0.17"
#define MyAppPublisher "fireundubh"
#define MyAppURL "https://www.nexusmods.com/pathfinderkingmaker/mods/32"
#define MyAppExeName "PatchworkLauncher.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{93A04479-347F-4DE1-9E4E-D308633D7C01}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={localappdata}\{#MyAppName}
DefaultGroupName={#MyAppName}
AllowNoIcons=yes
LicenseFile=E:\repos\KingmakerMods.pw\Installer\license.rtf
OutputBaseFilename=KingmakerMods.pw-{#MyAppVersion}
Compression=lzma
SolidCompression=yes
PrivilegesRequired=admin
DisableDirPage=no

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Dirs]
Name: "{app}\Mods"
Name: "{code:GamePathOrCustom}\Kingmaker_Data\Mods\Localization\deDE"
Name: "{code:GamePathOrCustom}\Kingmaker_Data\Mods\Localization\enGB"
Name: "{code:GamePathOrCustom}\Kingmaker_Data\Mods\Localization\frFR"
Name: "{code:GamePathOrCustom}\Kingmaker_Data\Mods\Localization\ruRU"
Name: "{code:GamePathOrCustom}\Kingmaker_Data\Mods\Localization\zhCN"

[Files]
Source: "E:\repos\KingmakerMods.pw Launcher\PatchworkLauncher\bin\Release\Mods\KingmakerMods.pw.dll"; DestDir: "{app}\Mods"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw Launcher\PatchworkLauncher\bin\Release\PEVerify"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw Launcher\PatchworkLauncher\bin\Release\PatchworkLauncher.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw Launcher\PatchworkLauncher\bin\Release\AppInfo.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw Launcher\PatchworkLauncher\bin\Release\Mono.Cecil.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw Launcher\PatchworkLauncher\bin\Release\Mono.Cecil.Mdb.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw Launcher\PatchworkLauncher\bin\Release\Mono.Cecil.Pdb.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw Launcher\PatchworkLauncher\bin\Release\Mono.Cecil.Rocks.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw Launcher\PatchworkLauncher\bin\Release\Patchwork.Attributes.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw Launcher\PatchworkLauncher\bin\Release\Patchwork.Engine.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw Launcher\PatchworkLauncher\bin\Release\pevrfyrc.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw Launcher\PatchworkLauncher\bin\Release\Serilog.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw Launcher\PatchworkLauncher\bin\Release\Serilog.Sinks.File.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw Launcher\PatchworkLauncher\bin\Release\OpenAssemblyCreator.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw Launcher\PatchworkLauncher\bin\Release\PatchworkLauncher.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw Launcher\PatchworkLauncher\bin\Release\Serilog.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw Launcher\PatchworkLauncher\bin\Release\preferences.pw.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw Launcher\PatchworkLauncher\bin\Release\Serilog.Sinks.File.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw Launcher\PatchworkLauncher\bin\Release\Serilog.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw\KingmakerMods.ini"; DestDir: "{code:GamePathOrCustom}"; Flags: ignoreversion confirmoverwrite uninsneveruninstall
Source: "E:\repos\KingmakerMods.pw Launcher\packages\ini-parser.2.5.2\lib\net20\INIFileParser.dll"; DestDir: "{code:GamePathOrCustom}\Kingmaker_Data\Managed"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw Launcher\PatchworkLauncher\bin\Release\Patchwork.Attributes.dll"; DestDir: "{code:GamePathOrCustom}\Kingmaker_Data\Managed"; Flags: ignoreversion
Source: "E:\repos\UserConfig\UserConfig\bin\Release\UserConfig.dll"; DestDir: "{code:GamePathOrCustom}\Kingmaker_Data\Managed"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw\Kingmaker_Data\Mods\Localization\enGB\KingmakerMods.pw.json"; DestDir: "{code:GamePathOrCustom}\Kingmaker_Data\Mods\Localization\deDE"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw\Kingmaker_Data\Mods\Localization\enGB\KingmakerMods.pw.json"; DestDir: "{code:GamePathOrCustom}\Kingmaker_Data\Mods\Localization\enGB"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw\Kingmaker_Data\Mods\Localization\enGB\KingmakerMods.pw.json"; DestDir: "{code:GamePathOrCustom}\Kingmaker_Data\Mods\Localization\frFR"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw\Kingmaker_Data\Mods\Localization\enGB\KingmakerMods.pw.json"; DestDir: "{code:GamePathOrCustom}\Kingmaker_Data\Mods\Localization\ruRU"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw\Kingmaker_Data\Mods\Localization\enGB\KingmakerMods.pw.json"; DestDir: "{code:GamePathOrCustom}\Kingmaker_Data\Mods\Localization\zhCN"; Flags: ignoreversion
Source: "E:\repos\KingmakerMods.pw\Installer\settings.pw.xml"; DestDir: "{app}"; Flags: ignoreversion;
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{code:GamePathOrCustom}\KingmakerMods.ini"; Description: "Edit KingmakerMods.ini"; Flags: postinstall shellexec
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent runascurrentuser
Filename: https://discordapp.com/invite/E5pe74u; Description: "Join the Pathfinder Kingmaker Discord"; Flags: postinstall shellexec
Filename: {#MyAppURL}; Description: "Visit KingmakerMods.pw at Nexus Mods"; Flags: postinstall shellexec
Filename: https://www.patreon.com/fireundubh; Description: "Visit fireundubh at Patreon"; Flags: postinstall shellexec

[Code]
var
  GameDirPage: TInputDirWizardPage;

procedure InitializeWizard;
var
  gamePath: String;
begin
  gamePath := ExpandConstant('{code:GamePath}');

  GameDirPage := CreateInputDirPage(wpSelectDir,
    'Select Game Directory', 'Where is the game installed?',
    'Select the folder in which Kingmaker.exe resides, then click Next.',
    False, '');
  GameDirPage.Add('');

  if gamePath <> '' then
    GameDirPage.Values[0] := GetPreviousData('GameDir', gamePath);

  if gamePath = '' then
    GameDirPage.Values[0] := GetPreviousData('GameDir', '');
end;

function GetGameDir(Param: String): String;
begin
  { Return the selected DataDir }
  Result := GameDirPage.Values[0];
end;

procedure RegisterPreviousData(PreviousDataKey: Integer);
begin
  SetPreviousData(PreviousDataKey, 'GameDir', GameDirPage.Values[0]);
end;

function GamePath(Param: String): String;
var
  value: String;
begin
  // 32-bit - these should always return false
  if RegQueryStringValue(HKLM, 'SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 640820', 'InstallLocation', value) then begin
      Result := value;
      exit;
  end;
  if RegQueryStringValue(HKLM, 'SOFTWARE\Wow6432Node\GOG.com\Games\1982293831', 'PATH', value) then begin
      Result := value;
      exit;
  end;

  // 64-bit - one of these should work
  if RegQueryStringValue(HKLM64, 'SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 640820', 'InstallLocation', value) then begin
      Result := value;
      exit;
  end;
  if RegQueryStringValue(HKLM64, 'SOFTWARE\Wow6432Node\GOG.com\Games\1982293831', 'PATH', value) then begin
      Result := value;
      exit;
  end;

  Result := '';
  //RaiseException('Cannot find game path from Windows Registry. Is the game installed?');
end;

function GamePathOrCustom(Param: String): String;
var
  value: String;
begin
  value := ExpandConstant('{code:GamePath}');

  if value <> '' then begin
    Result := value;
    exit;
  end;

  value := ExpandConstant('{code:GetGameDir}');

  if value <> '' then begin
    Result := value;
    exit;
  end;
end;

procedure UpdateBaseFolder;
var
  FileData: String;
begin
    LoadStringFromFile(ExpandConstant('{app}\settings.pw.xml'), FileData);
    StringChangeEx(FileData, 'REPLACEME', ExpandConstant('{code:GamePath}'), True);
    SaveStringToFile(ExpandConstant('{app}\settings.pw.xml'), FileData, False);
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  if CurStep = ssPostInstall then
  begin
    UpdateBaseFolder;
  end;
end;