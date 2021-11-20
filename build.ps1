Write-Host "Clearing working directory..." -ForegroundColor Green

if (Test-Path "clingo\build\win")
{
    Write-Host "Deleting clingo\build\win..." -ForegroundColor Yellow
    Remove-Item "clingo\build\win" -Recurse -ErrorAction Ignore
}

Write-Host ""

Write-Host "Compiling clingo..." -ForegroundColor Green

cmake "clingo" -B"clingo\build\win" -DCLINGO_BUILD_SHARED=ON -DCLINGO_BUILD_WITH_PYTHON=OFF -DCLINGO_BUILD_WITH_LUA=OFF -DCLINGO_BUILD_APPS=OFF -G "Visual Studio 16 2019" -A x64
cmake --build "clingo\build\win" --config Release

Write-Host ""

Write-Host "Done" -ForegroundColor Green