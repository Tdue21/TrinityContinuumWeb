## Create publish package for the Trinity Continuum REST api for Windows x64.

Remove-Item -Recurse -Force -ErrorAction SilentlyContinue "./api/win-x64/*.*"

& dotnet publish `
    "..\Source\Backend\TrinityContinuum.Server\TrinityContinuum.Server.csproj" `
    -r win-x64 `
    -c Release `
    -f net9.0 `
    --self-contained false `
    --no-restore `
    --no-build `
    -o "./api/win-x64" 

Compress-Archive `
    -Path "./api/win-x64/*" `
    -DestinationPath "./api-win-x64.zip" `
    -Force    

## Create publish package for the Trinity Continuum Web App for Windows x64.

Remove-Item -Recurse -Force -ErrorAction SilentlyContinue "./app/win-x64/*.*"

& dotnet publish `
    "..\Source\Frontend\TrinityContinuum.WebApp\TrinityContinuum.WebApp.csproj" `
    -r win-x64 `
    -c Release `
    -f net9.0 `
    --self-contained false `
    --no-restore `
    --no-build `
    -o "./app/win-x64"

Compress-Archive `
    -Path "./app/win-x64/*" `
    -DestinationPath "./app-win-x64.zip" `
    -Force    

