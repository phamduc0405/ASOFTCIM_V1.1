# Update-Version.ps1

# Lấy thư mục gốc của script
$projectRoot = Split-Path -Parent $MyInvocation.MyCommand.Definition

# Đường dẫn tới AssemblyInfo.cs và BuildInfo.txt
$assemblyInfoPath = Join-Path $projectRoot "Properties\AssemblyInfo.cs"
$buildInfoPath = Join-Path $projectRoot "BuildInfo.txt"

function Increment-Version([int]$major, [int]$minor, [int]$build, [int]$revision) {
    $revision += 1
    if ($revision -gt 99) {
        $revision = 0
        $build += 1
        if ($build -gt 99) {
            $build = 0
            $minor += 1
            if ($minor -gt 99) {
                $minor = 0
                $major += 1
            }
        }
    }
    return "$major.$minor.$build.$revision"
}

$lines = Get-Content $assemblyInfoPath
$newLines = @()
$newVersion = ""

foreach ($line in $lines) {
    if ($line -match 'AssemblyVersion\("(\d+)\.(\d+)\.(\d+)\.(\d+)"\)') {
        $newVersion = Increment-Version $matches[1] $matches[2] $matches[3] $matches[4]
        $newLines += "[assembly: AssemblyVersion(`"$newVersion`")]"
    }
    elseif ($line -match 'AssemblyFileVersion\("(\d+)\.(\d+)\.(\d+)\.(\d+)"\)') {
        $newLines += "[assembly: AssemblyFileVersion(`"$newVersion`")]"
    }
    else {
        $newLines += $line
    }
}

Set-Content -Path $assemblyInfoPath -Value $newLines

# Ghi thông tin build
$buildTime = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
Set-Content -Path $buildInfoPath -Value "Version=$newVersion`nBuildTime=$buildTime"

Write-Host "✅ Version updated to $newVersion at $buildTime"
