; LANDIS-II Extension information
#define CoreRelease "LANDIS-II-V8"
#define ExtensionName "Output-PnET"
#define AppVersion "{VERSION}"
#define AppPublisher "LANDIS-II Foundation"
#define AppURL "http://www.landis-ii.org/"

; Build directory
#define BuildDir "..\..\src\bin\Release"

; LANDIS-II installation directories
#define ExtDir "C:\Program Files\LANDIS-II-v8\extensions"
#define AppDir "C:\Program Files\LANDIS-II-v8"
#define LandisPlugInDir "C:\Program Files\LANDIS-II-v8\plug-ins-installer-files"
#define ExtensionsCmd AppDir + "\commands\landis-ii-extensions.cmd"

[Setup]
AppId={{C3EFDCBA-0578-4719-9E87-7F18E53EFCB1}
AppName={#CoreRelease} {#ExtensionName}
AppVersion={#AppVersion}
AppVerName={#CoreRelease} {#ExtensionName} v{#AppVersion}
AppPublisher={#AppPublisher}
AppPublisherURL={#AppURL}
AppSupportURL={#AppURL}
AppUpdatesURL={#AppURL}
DefaultDirName={pf}\{#ExtensionName}
DisableDirPage=yes
DefaultGroupName={#ExtensionName}
DisableProgramGroupPage=yes
LicenseFile=LANDIS-II_Binary_license.rtf
OutputDir={#SourcePath}
OutputBaseFilename={#CoreRelease} {#ExtensionName} {#AppVersion}-setup
Compression=lzma
SolidCompression=yes
VersionInfoVersion={#AppVersion}

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: {#BuildDir}\Landis.Extension.Output.PnET-v6.dll; DestDir: {#ExtDir}; Flags: uninsneveruninstall ignoreversion

Source: {#BuildDir}\Landis.Library.PnETCohorts-v2.dll; DestDir: {#ExtDir}; Flags: uninsneveruninstall replacesameversion
Source: {#BuildDir}\Landis.Library.UniversalCohorts-v2.dll; DestDir: {#ExtDir}; Flags: uninsneveruninstall replacesameversion
Source: {#BuildDir}\Landis.Library.Metadata-v2.dll; DestDir: {#ExtDir}; Flags: uninsneveruninstall replacesameversion
Source: {#BuildDir}\Landis.Library.Parameters-v2.dll; DestDir: {#ExtDir}; Flags: uninsneveruninstall replacesameversion

Source: ..\examples\biomass-Pnet-succession-example\*.txt; DestDir: {#AppDir}\examples\{#ExtensionName}; Flags: ignoreversion
Source: ..\examples\biomass-Pnet-succession-example\*.gis; DestDir: {#AppDir}\examples\{#ExtensionName}; Flags: ignoreversion skipifsourcedoesntexist
Source: ..\examples\biomass-Pnet-succession-example\*.img; DestDir: {#AppDir}\examples\{#ExtensionName}; Flags: ignoreversion skipifsourcedoesntexist
Source: ..\examples\biomass-Pnet-succession-example\*.bat; DestDir: {#AppDir}\examples\{#ExtensionName}; Flags: ignoreversion skipifsourcedoesntexist

#define InfoTxt "PnET-Output.txt"
Source: {#InfoTxt}; DestDir: {#LandisPlugInDir}

[Run]
Filename: {#ExtensionsCmd}; Parameters: "remove ""Output-PnET"" "; WorkingDir: {#LandisPlugInDir}
Filename: {#ExtensionsCmd}; Parameters: "add ""{#InfoTxt}"" "; WorkingDir: {#LandisPlugInDir}

[UninstallRun]
Filename: {#ExtensionsCmd}; Parameters: "remove ""Output-PnET"" "; WorkingDir: {#LandisPlugInDir}
