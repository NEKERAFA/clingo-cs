echo -e "\033[92mClearing working directory...\033[0m"

if [ -f "./clingo/build/linux/i386"]; then
    echo -e "\033[93mDeleting ./clingo/build/linux/i386...\033[0m"
    rm -r "./clingo/build/linux/i386"
fi

if [ -f "./clingo/build/linux/amd64"]; then
    echo -e "\033[93mDeleting ./clingo/build/linux/amd64...\033[0m"
    rm -r "./clingo/build/linux/amd64"
fi

if [ -f "./src/ClingoSharp/runtimes/linux/i386"]; then
    echo -e "\033[93mDeleting ./src/ClingoSharp/runtimes/linux/i386...\033[0m"
    rm -r "./src/ClingoSharp/runtimes/linux/i386"
fi

if [ -f "./src/ClingoSharp/runtimes/linux/amd64"]; then
    echo -e "\033[93mDeleting ./src/ClingoSharp/runtimes/linux/amd64...\033[0m"
    rm -r "./src/ClingoSharp/runtimes/linux/amd64"
fi

echo ""

echo -e "\033[92mCompiling clingo...\033[0m"

echo -e "\033[93mCompiling Linux x86 version...\033[0m"

cmake "clingo" -B"./clingo/build/linux/i386" -DCLINGO_BUILD_SHARED=ON -DCLINGO_BUILD_WITH_PYTHON=OFF -DCLINGO_BUILD_WITH_LUA=OFF -DCLINGO_BUILD_APPS=OFF -m32
cmake --build "./clingo/build/linux/i368" -DCMAKE_BUILD_TYPE=Release

echo -e "\033[93mCopying Linux x86 version to ./src/ClingoSharp/runtimes/linux-i386/native...\033[0m"
mkdir -p "./src/ClingoSharp/runtimes/linux-i386/native"
cp "./clingo/build/linux/i386/clingo.so" "./src/ClingoSharp/runtimes/linux-i386/native"

echo -e "\033[93mCopying Linux x86 version to ./tests/runtimes/linux-i386/native...\033[0m"
mkdir -p "./tests/runtimes/linux-i386/native"
cp "./clingo/build/linux/i386/clingo.so" "./tests/runtimes/linux-i386/native"

echo ""

echo -e "\033[93mCompiling Linux x64 version...\033[0m"

cmake "clingo" -B"./clingo/build/linux/amd64" -DCLINGO_BUILD_SHARED=ON -DCLINGO_BUILD_WITH_PYTHON=OFF -DCLINGO_BUILD_WITH_LUA=OFF -DCLINGO_BUILD_APPS=OFF -m64
cmake --build "./clingo/build/linux/amd64" -DCMAKE_BUILD_TYPE=Release

echo -e "\033[93mCopying Linux x64 version to ./src/ClingoSharp/runtimes/linux-amd64/native...\033[0m"
mkdir -p "./src/ClingoSharp/runtimes/linux-amd64/native"
cp "./clingo/build/linux/amd64/clingo.so" "./src/ClingoSharp/runtimes/linux-amd64/native"

echo -e "\033[93mCopying Linux x64 version to ./tests/runtimes/linux-amd64/native...\033[0m"
mkdir -p "./tests/runtimes/linux-amd64/native"
cp "./clingo/build/linux/amd64/clingo.so" "./tests/runtimes/linux-amd64/native"

echo ""

echo -e "\033[92mDone\033[0m"