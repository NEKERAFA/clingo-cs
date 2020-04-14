IF NOT EXIST "clingo\build\x86\bin\Release\clingo.dll" (
   cmake "clingo" -B"clingo\build\x86" -DCLINGO_BUILD_SHARED=ON -DCLINGO_BUILD_WITH_PYTHON=OFF -DCLINGO_BUILD_WITH_LUA=OFF -DCLINGO_BUILD_APPS=OFF -G "Visual Studio 16 2019" -A Win32
   cmake --build "clingo\build\x86" --config Release
)

IF NOT EXIST "clingo\build\x64\bin\Release\clingo.dll" (
   cmake "clingo" -B"clingo\build\x64" -DCLINGO_BUILD_SHARED=ON -DCLINGO_BUILD_WITH_PYTHON=OFF -DCLINGO_BUILD_WITH_LUA=OFF -DCLINGO_BUILD_APPS=OFF -G "Visual Studio 16 2019" -A x64
   cmake --build "clingo\build\x64" --config Release
)

IF NOT EXIST "tests\bin\Debug\netcoreapp3.1\lib32" mkdir "tests\bin\Debug\netcoreapp3.1\lib32"
xcopy /y "clingo\build\x86\bin\Release\clingo.dll" "tests\bin\Debug\netcoreapp3.1\lib32"

IF NOT EXIST "tests\bin\Debug\netcoreapp3.1\lib" mkdir "tests\bin\Debug\netcoreapp3.1\lib"
xcopy /y "clingo\build\x64\bin\Release\clingo.dll" "tests\bin\Debug\netcoreapp3.1\lib"
