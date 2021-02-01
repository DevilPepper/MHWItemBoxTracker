try {
   $client = new-object System.Net.WebClient
   $client.DownloadFile("https://raw.githubusercontent.com/wiki/$env:GITHUB_REPOSITORY/Changelog.md","$pwd/ChangelogDL.md")
   mv -Force ChangelogDL.md Changelog.md
}
catch {
    if (!(Test-Path "$pwd/Changelog.md")) {
        echo $null >> Changelog.md
    }
}
