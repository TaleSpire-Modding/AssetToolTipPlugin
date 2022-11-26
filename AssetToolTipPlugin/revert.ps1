$json = Get-Content "../manifest.json" | ConvertFrom-Json
$version = $json.version_number
(Get-Content AssetToolTip.cs) -Replace "$version.0","0.0.0.0"  | Set-Content AssetToolTip.cs
(Get-Content AssetToolTipPlugin.csproj) -Replace "<Version>$version</Version>","<Version>0.0.0</Version>" | Set-Content AssetToolTipPlugin.csproj