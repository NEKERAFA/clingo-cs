Write-Host "Clearing working directory..." -ForegroundColor Green

if (Test-Path ".\tests\bin\Debug\netcoreapp3.1\runtimes\native\win-x86\")
{
    Write-Host "Deleting .\tests\bin\Debug\netcoreapp3.1\runtimes\native\win-x86\..." -ForegroundColor Yellow
    Remove-Item ".\tests\bin\Debug\netcoreapp3.1\runtimes\native\win-x86\" -Recurse -ErrorAction Ignore
}

if (Test-Path ".\tests\bin\Debug\netcoreapp3.1\runtimes\native\win-x64\")
{
    Write-Host "Deleting .\tests\bin\Debug\netcoreapp3.1\runtimes\native\win-x64\..." -ForegroundColor Yellow
    Remove-Item ".\tests\bin\Debug\netcoreapp3.1\runtimes\native\win-x64\" -Recurse -ErrorAction Ignore
}

Write-Host "Copying clingo dependencies..." -ForegroundColor Green

Copy-Item ".\clingo\build\win\x86\bin\Release" ".\tests\bin\Debug\netcoreapp3.1\runtimes\native\win-x86\" -Recurse
Copy-Item ".\clingo\build\win\x64\bin\Release" ".\tests\bin\Debug\netcoreapp3.1\runtimes\native\win-x64\" -Recurse

Write-Host "Executing tests..." -ForegroundColor Green

dotnet test --configuration Debug /p:CollectCoverage=true /p:CoverletOutput=results/ /p:CoverletOutputFormat=lcov
(Get-Content ".\tests\results\coverage.info") -replace [Regex]::Escape($PSScriptRoot), [Regex]::Escape("") | Out-File -encoding ASCII coverage.info

Write-Host ""

Write-Host "Done" -ForegroundColor Green