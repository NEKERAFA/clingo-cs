Write-Host "Clearing working directory..." -ForegroundColor Green

if (Test-Path ".\tests\bin\Debug\netcoreapp3.1\runtimes\win-x86\native\")
{
    Write-Host "Deleting .\tests\bin\Debug\netcoreapp3.1\runtimes\win-x86\native\..." -ForegroundColor Yellow
    Remove-Item ".\tests\bin\Debug\netcoreapp3.1\runtimes\win-x86\native\" -Recurse -ErrorAction Ignore
}

if (Test-Path ".\tests\bin\Debug\netcoreapp3.1\runtimes\win-x64\native\")
{
    Write-Host "Deleting .\tests\bin\Debug\netcoreapp3.1\runtimes\win-x64\native\..." -ForegroundColor Yellow
    Remove-Item ".\tests\bin\Debug\netcoreapp3.1\runtimes\win-x64\native\" -Recurse -ErrorAction Ignore
}

if (Test-Path ".\tests\bin\Debug\netcoreapp3.1\files\")
{
    Write-Host "Deleting .\tests\bin\Debug\netcoreapp3.1\files\..." -ForegroundColor Yellow
    Remove-Item ".\tests\bin\Debug\netcoreapp3.1\files\" -Recurse -ErrorAction Ignore
}

Write-Host "Copying clingo dependencies..." -ForegroundColor Green

Copy-Item ".\clingo\build\win\x86\bin\Release" ".\tests\bin\Debug\netcoreapp3.1\runtimes\win-x86\native\" -Recurse
Copy-Item ".\clingo\build\win\x64\bin\Release" ".\tests\bin\Debug\netcoreapp3.1\runtimes\win-x64\native\" -Recurse

Write-Host "Copying test files dependencies..." -ForegroundColor Green

Copy-Item ".\tests\files" ".\tests\bin\Debug\netcoreapp3.1\files" -Recurse

Write-Host "Executing tests..." -ForegroundColor Green

dotnet test /p:CollectCoverage=true /p:CoverletOutput=results/ /p:CoverletOutputFormat=lcov
(Get-Content ".\tests\results\coverage.info") -replace [Regex]::Escape("$($PSScriptRoot)\"), [Regex]::Escape("") | Out-File -encoding ASCII "coverage.info"

Write-Host ""

Write-Host "Done" -ForegroundColor Green