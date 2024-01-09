<div align="center">
  
# World Eater Progress Tracker

A simple [Minecraft Console Client (MCC)](https://mccteam.github.io/) script that enables players on Minecraft redstone servers to easily monitor the progress of world eaters without the need to enter the game.
It uses the `!!vris` command from the [Where Is](https://github.com/Lazy-Bing-Server/WhereIs-MCDR) [MCDReforged](https://github.com/Fallen-Breath/MCDReforged) plugin to track the location of an AFK player or bot attached to the world eater flying machines.

</div>

## How To Use
### Downloading the Script
Please download the script and place it in the same folder as your Minecraft Console Client (MCC) executable.
### Required Variables
To use this script, you must modify these variables at the beginning of the script according to the conditions of your world eater:\
`botName`: The name of the AFK player or the bot.\
`worldEaterDirection`: The running direction of the world eater, which can be set to `worldEaterDirections.xDirection` or `worldEaterDirections.yDirection`.\
`worldEaterStartPos`: The minimum coordinate the bot can reach on the working axis.\
`worldEaterEndPos`: The maximum coordinate the bot can reach on the working axis.

### Optional Variables
You can also set these optional variables:\
`autoReconnect`: Whether to try to reconnect when the connection is lost.\
`runningRetrieveInterval`: Data retrieval intervals in **seconds** when the world eater is running.\
`stoppedRetrieveInterval`: Data retrieval intervals in **seconds** when the world eater is stopped.
> By separating the intervals into two variables, you can set a lower running interval to get accurate data, and a higher stopped interval to avoid annoyance.

`refreshInterval`: Display refresh interval in **milliseconds**.\
`debugMode`: Display additional information for debugging.

### Running the Script in MCC
After logging into your server, use the command `/script WorldEaterProgressTracker.cs` to run the script.
For additional guidance, you may refer to the [official MCC documentation](https://mccteam.github.io/guide/).
