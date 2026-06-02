<#
.SYNOPSIS
    MVC Template - Project Rename Tool
.DESCRIPTION
    Renames all occurrences of 'Template' in file/folder names within the Template directory.
#>

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$Root = Join-Path $ScriptDir "Template"

if (-not (Test-Path $Root -PathType Container)) {
    Write-Error "Error: Template directory not found at $Root"
    exit 1
}

Write-Host "========================================="
Write-Host "  MVC Template - Project Rename Tool"
Write-Host "========================================="
Write-Host ""
$NewName = Read-Host "Enter new project name"

if ([string]::IsNullOrWhiteSpace($NewName)) {
    Write-Error "Error: Project name cannot be empty."
    exit 1
}

Write-Host ""
Write-Host "Renaming 'Template' -> '$NewName'..."
Write-Host ""

# Rename files first (sorted by depth descending to avoid path issues)
$files = Get-ChildItem -Path $Root -Recurse -File -Filter "*Template*" |
    Where-Object {
        $_.FullName -notmatch [regex]::Escape("\bin\") -and
        $_.FullName -notmatch [regex]::Escape("\obj\") -and
        $_.FullName -notmatch [regex]::Escape("\.idea\")
    } |
    Sort-Object { $_.FullName.Split([IO.Path]::DirectorySeparatorChar).Count } -Descending

foreach ($file in $files) {
    $newBase = $file.Name -replace "Template", $NewName
    if ($file.Name -ne $newBase) {
        Write-Host "  FILE: $($file.Name) -> $newBase"
        Rename-Item -Path $file.FullName -NewName $newBase
    }
}

# Rename directories (sorted by depth descending)
$dirs = Get-ChildItem -Path $Root -Recurse -Directory -Filter "*Template*" |
    Where-Object {
        $_.FullName -notmatch [regex]::Escape("\bin\") -and
        $_.FullName -notmatch [regex]::Escape("\obj\") -and
        $_.FullName -notmatch [regex]::Escape("\.idea\")
    } |
    Sort-Object { $_.FullName.Split([IO.Path]::DirectorySeparatorChar).Count } -Descending

foreach ($dir in $dirs) {
    $newBase = $dir.Name -replace "Template", $NewName
    if ($dir.Name -ne $newBase) {
        Write-Host "  DIR:  $($dir.Name) -> $newBase"
        Rename-Item -Path $dir.FullName -NewName $newBase
    }
}

Write-Host ""
Write-Host "Done! Project renamed to '$NewName'."