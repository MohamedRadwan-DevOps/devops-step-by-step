# clear existing folder
$testPath = "C:\tests"

if (Test-Path -Path $testPath) {
    Remove-Item -Recurse -Force $testPath
    mkdir $testPath
}

Copy-Item -Path . -Destination $testPath -Recurse -Force
