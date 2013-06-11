[IO.Directory]::SetCurrentDirectory((Convert-Path (Get-Location -PSProvider FileSystem)))

New-Item -path '.\Deploy' -type directory -force
New-Item -path '.\Deploy\Visual Studio 2008\Addins\en-US' -type directory -force
New-Item -path '.\Deploy\Visual Studio 2010\Addins\en-US' -type directory -force
New-Item -path '.\Deploy\Visual Studio 2012\Addins\en-US' -type directory -force

# Copy over the core binaries.
Copy-Item -Path '.\Switch2008\bin\Release\Switch2008.dll' -Destination '.\Deploy\Visual Studio 2008\Addins' -Force
Copy-Item -Path '.\Switch2010\bin\Release\Switch2010.dll' -Destination '.\Deploy\Visual Studio 2010\Addins' -Force
Copy-Item -Path '.\Switch2012\bin\Release\Switch2012.dll' -Destination '.\Deploy\Visual Studio 2012\Addins' -Force
Copy-Item -Path '.\Switch2008\bin\Release\SwitchCore.dll' -Destination '.\Deploy\Visual Studio 2008\Addins' -Force
Copy-Item -Path '.\Switch2010\bin\Release\SwitchCore.dll' -Destination '.\Deploy\Visual Studio 2010\Addins' -Force
Copy-Item -Path '.\Switch2012\bin\Release\SwitchCore.dll' -Destination '.\Deploy\Visual Studio 2012\Addins' -Force
Copy-Item -Path '.\SwitchConfiguration.xml' -Destination '.\Deploy\Visual Studio 2008\Addins' -Force
Copy-Item -Path '.\SwitchConfiguration.xml' -Destination '.\Deploy\Visual Studio 2010\Addins' -Force
Copy-Item -Path '.\SwitchConfiguration.xml' -Destination '.\Deploy\Visual Studio 2012\Addins' -Force

Copy-Item -Path '.\Switch2008\Switch2008.AddIn' -Destination '.\Deploy\Visual Studio 2008\Addins' -Force
Copy-Item -Path '.\Switch2010\Switch2010.AddIn' -Destination '.\Deploy\Visual Studio 2010\Addins' -Force
Copy-Item -Path '.\Switch2012\Switch2012.AddIn' -Destination '.\Deploy\Visual Studio 2012\Addins' -Force

# Generate the resources
Start-Process -FilePath 'C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\resgen.exe' -ArgumentList "Resources.resx Resources.Resources" -WorkingDirectory '.\Switch2008\' -Wait
Start-Process -FilePath 'C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\al.exe' -ArgumentList "/t:lib /embed:Resources.resources /culture:en-US /out:Switch2008.Resources.dll" -WorkingDirectory '.\Switch2008\' -Wait
Start-Process -FilePath 'C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\resgen.exe' -ArgumentList "Resources.resx Resources.Resources" -WorkingDirectory '.\Switch2010\' -Wait
Start-Process -FilePath 'C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\al.exe' -ArgumentList "/t:lib /embed:Resources.resources /culture:en-US /out:Switch2010.Resources.dll" -WorkingDirectory '.\Switch2010\' -Wait
Start-Process -FilePath 'C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\resgen.exe' -ArgumentList "Resources.resx Resources.Resources" -WorkingDirectory '.\Switch2012\' -Wait
Start-Process -FilePath 'C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\al.exe' -ArgumentList "/t:lib /embed:Resources.resources /culture:en-US /out:Switch2012.Resources.dll" -WorkingDirectory '.\Switch2012\' -Wait

# Move the resources.
Move-Item -Path '.\Switch2008\Switch2008.Resources.dll' -Destination '.\Deploy\Visual Studio 2008\Addins\en-US' -Force
Move-Item -Path '.\Switch2010\Switch2010.Resources.dll' -Destination '.\Deploy\Visual Studio 2010\Addins\en-US' -Force
Move-Item -Path '.\Switch2012\Switch2012.Resources.dll' -Destination '.\Deploy\Visual Studio 2012\Addins\en-US' -Force