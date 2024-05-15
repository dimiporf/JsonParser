# Ensure the current directory is the project root
$projectDir = "C:\Users\dimip\source\repos\JsonParser\JsonParser"

# Get all JSON files in the tests/step1 folder
$files = Get-ChildItem -Path "$projectDir\tests\step1\*.json"

# Loop through each file and test the JSON parser
foreach ($file in $files) {
    Write-Host "Testing $($file.Name)..."
    dotnet run --project "$projectDir" -- $file.FullName
}
