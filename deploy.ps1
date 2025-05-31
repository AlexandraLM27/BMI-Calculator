# === CONFIG ===
$solutionRoot = "D:\ProiectMetDezv\BMI-Calculator"
$uiProject = "$solutionRoot\BMI.UI"
$apiProject = "$solutionRoot\BMI.API"
$tempUiPublish = "$solutionRoot\BMI.UI.Publish"
$deployOutput = "$solutionRoot\Deploy"
$apiWwwroot = "$apiProject\wwwroot"

Write-Host "🔧 STEP 1: Curățare wwwroot..."
Remove-Item -Recurse -Force -ErrorAction SilentlyContinue "$apiWwwroot"
New-Item -ItemType Directory -Path "$apiWwwroot" | Out-Null

Write-Host "🚀 STEP 2: Public UI separat..."
dotnet publish "$uiProject" -c Release -o "$tempUiPublish"

Write-Host "📦 STEP 3: Copiere UI în API/wwwroot..."
robocopy "$tempUiPublish\wwwroot" "$apiWwwroot" /E > $null

Write-Host "🧹 STEP 4: Curățare folder Deploy..."
Remove-Item -Recurse -Force -ErrorAction SilentlyContinue "$deployOutput"
New-Item -ItemType Directory -Path "$deployOutput" | Out-Null

Write-Host "📦 STEP 5: Publicare API + UI în Deploy..."
dotnet publish "$apiProject" -c Release -o "$deployOutput"

Write-Host "`n✅ Deployment completat!"
Write-Host "Poți rula aplicația cu:"
Write-Host "    cd '$deployOutput'"
Write-Host "    dotnet BMI.API.dll"
