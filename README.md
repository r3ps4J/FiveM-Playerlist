# FiveM-Playerlist
GTA:O styled playerlist for FiveM servers (future proof: supports > 32 players).

### Preview:

![](https://i.r3ps4j.nl/images/hqB.png)

### Features:

- Configurable rows through a server event or export.
- Configured rows update live as soon as the event/export is called, even if someone has the playerlist open at that time.
- GTA:O scaleform used, so it looks exactly like GTA:O.
- Future proof, it supports more than 32 online players.
- "Server Name" indicator updates with the "sv_projectName" convar, defaults to "FiveM" if not set up.
- "Server Type" indicator updates with the "sv_serverType" convar, defaults to "Public" if not set up.

### Installation

Download the latest version, drag the folder from the zip file into your resources folder and add `start playerlist` to your server.cfg file.


### Configuration

There is no config file to change any of the "visual" settings for player rows. You will have to create your own script to modify the rows through the playerlist api.


### Developer info

To change the player's row settings, trigger this server event:
```lua
TriggerEvent("fs:setPlayerRowConfig", 1 --[[ player id ]], "SNAIL" --[[ crew tag ]], 12 --[[ rank number ]], 50 --[[ job points ]], true --[[ show job points icon ]])
```


#### Parameters

|type|name|description|
|:-|:-|:-|
|_int_|**playerServerId**|This is the player's server id for the player you want to change the row of.|
|_string_|**crewText**|The text to display for the "crew" tag behind the player's username. Pass an empty string (`""`) to disable the crew label.|
|_int_|**rankNumber**|The number to display for the "rank" value. Set to -1 to disable.|
|_int_|**jobPointsAmount**|The number to display for the "job points (jp)" value. Set to -1 to disable.|
|_bool_|**showJobPointsIcon**|Should the "(JP)" icon be visible next to the job points number?|


You can access this event from both C# or Lua scripts. By default the crew tag, job points amount and job points logo are all hidden for all players, only if you add them using the event will it make them visible for that specific player row. (syncing for all clients is managed by the resource)


You can also use the provided server export (`setPlayerRowConfig()`) however, due to some issues (possibly a bug with FiveM) this is kind of buggy now. Use the event for now, once I figure out why some parameters are not getting passed on when using the export I'll make sure to add documentation for the server export.

### Setting up convars

The information shown on the playerlist is configurable by using convars in your server.cfg file. Add the following to the server.cfg to set one up:
```cfg
set sv_serverType "Private"
```

#### Convars

|type|name|default|description|
|:-|:-|:-|:-|
|_string_|**sv_projectName**|"FiveM"|This is the server name that is shown in the server list, should already be set up. See [this](https://github.com/citizenfx/fivem/commit/9b1cedf5239e912fd1135898b41b00d7dbe60e2b) commit description for details.|
|_string_|**sv_serverType**|"Public"|This is the server type that is shown in the server list, you will need to add this convar if you want to use it. Set the convar to an empty string (`""`) to disable the server type from being shown.|

The default column is what will be shown for each convar if the convar has not been set up.

### Download / Source Code

Download the resource on [GitHub](https://github.com/r3ps4J/FiveM-Playerlist). Make sure to go to the "releases" page and download the latest release, don't download the repository as that's useless if you don't know how to use visual studio or don't want to edit the resource.


### Usage in-game

When in-game, press "Z" to open the first page, press "Z" again to go to the next page (just like the playerlist in GTA:O). If you're at the last page, pressing "Z" will close the playerlist. If the playerlist is open and you don't close it yourself, then it will auto-close after a couple of seconds.
For controller support, use DPAD-DOWN.

Note, if other resources on your server disable the "Z" key or the "DPAD-DOWN" (`INPUT_MULTIPLAYER_INFO` / `20`) control, then you won't be able to toggle the playerlist.

## About this fork
I edited the project to show the same information the playerlist in GTA Online gives you.

Here I will state the changes I made in this fork. The readme above is edited accordingly.
- Added the possibility to change the rank number. This removed the player's server ID as rank number.
- Removed "Max Players" indicator and replaced it with the current page number and the max amount of pages. This will be hidden if there is only 1 page.
- Added the possibility to change the server name. This will update to the "sv_projectName" convar and default to "FiveM" if the convar is not set up.
- Added the server type behind the server name. This will update to the "sv_serverType" convar and default to "Public" if the convar is not set up. It can be hidden by setting the convar to an empty string (`""`).
- Moved the playercount so that it shows behind the server type.

### Credit
All credit goes to [Tom Grobbe](https://github.com/TomGrobbe), I simply edited his work. You can find his original repository [here](https://github.com/DevTestingPizza/FiveM-Playerlist).