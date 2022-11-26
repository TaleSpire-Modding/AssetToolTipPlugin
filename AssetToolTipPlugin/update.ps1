$json = Get-Content "../manifest.json" | ConvertFrom-Json
$version = $json.version_number
(Get-Content AssetToolTip.cs) -Replace "0.0.0.0", "$version.0" | Set-Content AssetToolTip.cs
(Get-Content AssetToolTipPlugin.csproj) -Replace "<Version>0.0.0</Version>", "<Version>$version</Version>" | Set-Content AssetToolTipPlugin.csproj