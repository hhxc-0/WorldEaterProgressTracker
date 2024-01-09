<div align="center">
  
# World Eater Progress Tracker

English | [简体中文](/README_CN.md)

This is a simple [Minecraft Console Client (MCC)](https://mccteam.github.io/) script that allows players on Minecraft redstone servers to monitor the progress of world eaters without needing to enter the game.
It uses the `!!vris` command provided by the [Where Is](https://github.com/Lazy-Bing-Server/WhereIs-MCDR) plugin of [MCDReforged](https://github.com/Fallen-Breath/MCDReforged) to track the location of an AFK player or bot attached to the world eater flying machines.

</div>

## How To Use
### Downloading the Script
Please download the script and place it in the same folder as your Minecraft Console Client (MCC) executable.
### Required Variables
To use this script, you must modify these variables at the beginning of the script according to the conditions of your world eater:\
`botName`: The name of the AFK player or bot.\
`worldEaterDirection`: The running direction of the world eater, which can be set to `worldEaterDirections.xDirection` or `worldEaterDirections.yDirection`.\
`worldEaterStartPos`: The minimum coordinate the bot can reach in the running direction.\
`worldEaterEndPos`: The maximum coordinate the bot can reach in the running direction.

### Optional Variables
You can also set these optional variables:\
`autoReconnect`: Whether to try to reconnect when the connection is lost.\
`runningRetrieveInterval`: Data retrieval intervals in **seconds** while the world eater is running.\
`stoppedRetrieveInterval`: Data retrieval intervals in **seconds** while the world eater is stopped.
> By separating the intervals into two variables, you can set a lower running interval for accurate data, and a higher stopped interval to avoid annoyance.

`refreshInterval`: Display refresh interval in **milliseconds**.\
`debugMode`: Display additional debugging information.

### Running the Script in MCC
After logging into your server using MCC, run the script with the command `/script WorldEaterProgressTracker.cs`.
For additional guidance, you may refer to the [official MCC documentation](https://mccteam.github.io/guide/).
