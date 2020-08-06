param($packagePath, $certificatePath, [SecureString] $certificatePassword, [SecureString] $apiKey)

write-host "signing package"

nuget.exe sign $PSBoundParameters["$packagePath"] -CertificatePath $PSBoundParameters["$certificatePath"] -CertificatePassword $PSBoundParameters["$certificatePassword"] -Timestamper http://timestamp.digicert.com

write-host "pushing package"

nuget push $PSBoundParameters["$packagePath"] -ApiKey $PSBoundParameters["$apiKey"] -Source https://api.nuget.org/v3/index.json