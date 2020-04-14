if [ ! -f "clingo\build\i686\bin\Release\clingo.so" ]; then
   cmake "clingo" -B"clingo\build\i386" -DCLINGO_BUILD_SHARED=ON -DCLINGO_BUILD_WITH_PYTHON=OFF -DCLINGO_BUILD_WITH_LUA=OFF -DCLINGO_BUILD_APPS=OFF -m32
   cmake --build "clingo\build\i368"
fi

if [ ! -f "clingo\build\x86_64\bin\Release\clingo.so" ]; then
   cmake "clingo" -B"clingo\build\x86_64" -DCLINGO_BUILD_SHARED=ON -DCLINGO_BUILD_WITH_PYTHON=OFF -DCLINGO_BUILD_WITH_LUA=OFF -DCLINGO_BUILD_APPS=OFF -m64
   cmake --build "clingo\build\x86_64"
fi

mkdir -p -v "tests\bin\Debug\netcoreapp3.1\lib32"
xcopy /y "clingo\build\i368\bin\Release\clingo.so" "tests\bin\Debug\netcoreapp3.1\lib32"

mkdir -p -v "tests\bin\Debug\netcoreapp3.1\lib"
xcopy /y "clingo\build\x86_64\bin\Release\clingo.so" "tests\bin\Debug\netcoreapp3.1\lib"