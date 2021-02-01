Just a silly little project to further gamify Minecraft by creating a leaderboard of kills.
It loads the usercache to get user IDs, then loads the stats for each user.
The browser then renders a table of kills.

`dotnet publish -c Release --self-contained --runtime linux-x64`

NOTE: Remember to change `Properties/launchSettings.json` to not be localhost

Create a systemd unit file to start on boot, be sure to change paths to code and minecraft server
```
[Unit]
Description=Minecraft leaderboard project

[Service]
WorkingDirectory=/opt/mc-leaders
ExecStart=/opt/mc-leaders/minecraft-leaderboard
SyslogIdentifier=AspNetSite

Restart=always
RestartSec=5

KillSignal=SIGINT
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false
Environment=DOTNET_ROOT=/opt/rh/rh-dotnet31/root/usr/lib64/dotnet
Environment=ASPNETCORE_URLS=http://*:5000;https://*:5001
Environment=MINECRAFT_HOME=/opt/minecraft-server

[Install]
WantedBy=multi-user.target
```

Then reload the service definitions:

`sudo systemctl daemon-reload`

Then start the service:

`sudo systemctl start minecraft-leaderboard`

TODO:
- make columns sortable
- improve data and loops on index page, could probably use a single data set or something clever
- add more stuff