echo "\033[92mCopying clingo dependencies...\033[0m"

mkdir -p tests/bin/Debug/net6.0
cp clingo/build/linux/bin/libclingo.so.4.0 tests/bin/Debug/net6.0/libclingo.so

echo "\033[92mCopying test files dependencies...\033[0m"

cp -R tests/files tests/bin/Debug/net6.0

echo "\033[92mExecuting tests...\033[0m"

dotnet test --collect:"XPlat Code Coverage" --settings tests/coverlet.runsettings
cp tests/TestResults/*/coverage.info coverage.info
sed "s/\$PSScriptRoot\\//g" -i coverage.info

echo "\n\033[92mDone...\033[0m"
