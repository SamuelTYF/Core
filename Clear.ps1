if (Test-Path(".\Test\")) {
    Remove-Item(".\Test\")
}

New-Item .\Test\ -ItemType Directory

foreach ($fi in Get-ChildItem(".\Debug\net5.0-windows")) {
    switch ($fi.Extension) {
        ".pdb" {
            $fi
            Remove-Item($fi)
        }
        ".json" {
            $l=".\Test\"+$fi.Name
            Copy-Item $fi -Destination $l
        }
        ".dll" {
            $l=".\Test\"+$fi.Name
            Copy-Item $fi -Destination $l
        }
    }
}

Copy-Item .\Debug\net5.0-windows\TestProgram.exe -Destination .\Test\TestProgram.exe

$location=Get-Location

Set-Location(".\Test")

.\TestProgram.exe

Set-Location($location)