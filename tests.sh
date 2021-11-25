echo "\033[92mCopying clingo dependencies...\033[0m"

mkdir -p tests/ClingoSharp.Tests/bin/Debug/net6.0
cp clingo/build/linux/bin/libclingo.so tests/ClingoSharp.Tests/bin/Debug/net6.0

echo "\033[92mCopying test files dependencies...\033[0m"

cp -R tests/ClingoSharp.Tests/files tests/ClingoSharp.Tests/bin/Debug/net6.0

echo "\033[92mExecuting tests...\033[0m"

dotnet test --collect:"XPlat Code Coverage" --settings tests/ClingoSharp.Tests/coverlet.runsettings
cp tests/ClingoSharp.Tests/TestResults/*/coverage.info coverage.info
sed -i "s/\$PSScroptRoot\\//g" coverage.info

echo "\n\033[92mExecuting tests...\033[0m"
