Write-Host "Copying clingo dependencies..." -ForegroundColor Green

Copy-Item "clingo\build\win\bin\Release\clingo.dll" "tests\bin\Debug\net6.0"

Write-Host "Copying test files dependencies..." -ForegroundColor Green

Copy-Item "tests\ClingoSharp.Tests\files" "tests\bin\Debug\net6.0" -Recurse

Write-Host "Executing tests..." -ForegroundColor Green

dotnet test --collect:"XPlat Code Coverage" --settings tests/ClingoSharp.Tests/coverlet.runsettings
(Get-Content ".\tests\ClingoSharp.Tests\TestResults\*\coverage.info") -replace [Regex]::Escape("$($PSScriptRoot)\"), [Regex]::Escape("") | Out-File -encoding ASCII "coverage.info"

Write-Host ""

Write-Host "Done" -ForegroundColor Green