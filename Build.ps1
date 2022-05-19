& "D:\VS 2022\MSBuild\Current\Bin\amd64\MSBuild.exe" -t:rebuild

if (Test-Path(".\Test\")) {
    Remove-Item .\Test\ -Recurse
}

New-Item .\Test\ -ItemType Directory

foreach ($fi in Get-ChildItem(".\Debug\net6.0-windows10.0.22000.0")) {
    switch ($fi.Extension) {
        ".pdb" {
            $fi
            Remove-Item($fi)
        }
        ".json" {
            $l=".\Test\"+$fi.Name
            Move-Item $fi -Destination $l
        }
        ".dll" {
            $l=".\Test\"+$fi.Name
            Move-Item $fi -Destination $l
        }
        ".exe" {
            $l=".\Test\"+$fi.Name
            Move-Item $fi -Destination $l
        }
    }
}

foreach ($fi in Get-ChildItem(".\TestResources")) {
    $l=".\Test\"+$fi.Name
    Copy-Item $fi -Destination $l
}

Remove-Item .\Debug\net6.0-windows10.0.22000.0 -Recurse

$location=Get-Location

Set-Location(".\Test")

.\TestProgram.exe

Set-Location($location)