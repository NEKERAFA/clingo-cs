echo -e "\033[92mClearing working directory...\033[0m"

if [ -d "./clingo/build/linux" ]; then
    echo -e "\033[93mDeleting ./clingo/build/linux...\033[0m"
    rm -r "./clingo/build/linux"
fi

echo ""

echo -e "\033[92mCompiling clingo...\033[0m"

echo -e "\033[93mCompiling Linux x86 version...\033[0m"

cmake -E env CXXFLAGS="-m32" cmake -B"./clingo/build/linux/i386" -DCLINGO_BUILD_SHARED=ON -DCLINGO_BUILD_WITH_PYTHON=OFF -DCLINGO_BUILD_WITH_LUA=OFF -DCLINGO_BUILD_APPS=OFF "clingo"
cmake --build "./clingo/build/linux/i386"

echo ""

echo -e "\033[93mCompiling Linux x64 version...\033[0m"

cmake -E env CXXFLAGS="-m64" cmake -B"./clingo/build/linux/amd64" -DCLINGO_BUILD_SHARED=ON -DCLINGO_BUILD_WITH_PYTHON=OFF -DCLINGO_BUILD_WITH_LUA=OFF -DCLINGO_BUILD_APPS=OFF "clingo"
cmake --build "./clingo/build/linux/amd64"

echo ""

echo -e "\033[92mDone\033[0m"