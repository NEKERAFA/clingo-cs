Write-Host "Clearing working directory..." -ForegroundColor Green

if (Test-Path ".\tests\bin\Debug\netcoreapp3.1\runtimes\win\native\")
{
    Write-Host "Deleting .\tests\bin\Debug\netcoreapp3.1\runtimes\win\native\..." -ForegroundColor Yellow
    Remove-Item ".\tests\bin\Debug\netcoreapp3.1\runtimes\win\native\" -Recurse -ErrorAction Ignore
}

if (Test-Path ".\tests\bin\Debug\netcoreapp3.1\files\")
{
    Write-Host "Deleting .\tests\bin\Debug\netcoreapp3.1\files\..." -ForegroundColor Yellow
    Remove-Item ".\tests\bin\Debug\netcoreapp3.1\files\" -Recurse -ErrorAction Ignore
}

Write-Host "Copying clingo dependencies..." -ForegroundColor Green

Copy-Item ".\clingo\build\win\bin\Release" ".\tests\bin\Debug\netcoreapp3.1\runtimes\win\native\" -Recurse

Write-Host "Copying test files dependencies..." -ForegroundColor Green

Copy-Item "tests\files" "tests\bin\Debug\net6.0" -Recurse

Write-Host "Executing tests..." -ForegroundColor Green

dotnet test --collect:"XPlat Code Coverage" --settings tests/ClingoSharp.Tests/coverlet.runsettings
(Get-Content ".\tests\TestResults\*\coverage.info") -replace [Regex]::Escape("$($PSScriptRoot)\"), [Regex]::Escape("") | Out-File -encoding ASCII "coverage.info"

Write-Host ""

Write-Host "Done" -ForegroundColor Green