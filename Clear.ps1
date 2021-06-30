if (Test-Path(".\Test\")) {
    Remove-Item .\Test\ -Recurse
}

if (Test-Path(".\Debug\")) {
    Remove-Item .\Debug\ -Recurse
}

if (Test-Path(".\Release\")) {
    Remove-Item .\Release\ -Recurse
}