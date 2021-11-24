echo -e "\033[92mClearing working directory...\033[0m"

if [ -d "./clingo/build/linux" ]; then
    echo -e "\033[93mDeleting clingo/build/linux...\033[0m"
    rm -r "clingo/build/linux"
fi

echo ""

echo -e "\033[92mCompiling clingo...\033[0m"

cmake -B"clingo/build/linux" -DCLINGO_BUILD_SHARED=ON -DCLINGO_BUILD_WITH_PYTHON=OFF -DCLINGO_BUILD_WITH_LUA=OFF -DCLINGO_BUILD_APPS=OFF "clingo"
cmake --build "clingo/build/linux"

echo ""

echo -e "\033[92mDone\033[0m"