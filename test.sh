echo -e "\033[92mClearing working directory...\033[0m"

if [ -d "tests/bin/Debug/net6.0/runtimes/linux-x64/native" ]; then
    echo -e "\033[93mDeleting tests/bin/Debug/net6.0/runtimes/linux-x64/native...\033[0m"
    rm -rf "tests/bin/Debug/net6.0/runtimes/linux-x64/native"
    mkdir -p "tests/bin/Debug/net6.0/runtimes/linux-x64/native"
fi

if [ -d "tests/bin/Debug/files" ]; then
    echo -e "\033[93mDeleting tests/bin/Debug/net6.0/files...\033[0m"
    rm -rf "tests/bin/Debug/net6.0/files"
fi

echo -e "\033[92mCopying clingo dependencies...\033[0m"

cp "clingo/build/linux/bin/libclingo.so" "tests/bin/Debug/net6.0/runtimes/linux-x64/native"

echo -e "\033[92mCopying test files dependencies...\033[0m"

cp -R "tests/files" "tests/bin/Debug/net6.0"

echo -e "\033[92mExecuting tests...\033[0m"

dotnet test /p:CollectCoverage=true /p:CoverletOutput=results/ /p:CoverletOutputFormat=lcov
sed -i "s/$PSScroptRoot//g" "tests\results\coverage.info"

echo -e "\n\033[92mExecuting tests...\033[0m"