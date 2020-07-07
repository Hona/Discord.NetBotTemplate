# DiscordBotTemplate

> Discord bot template code, using the Discord.NET library.

Aimed to be quick to fork and add your commands with all the overhead done for you.

## Features

* Multiple environments for development and production
* Thread safe logging
* Clean startup with early quits ensuring correct setup
* Automatic discovery of commands (thanks to Discord.NET)
* DI setup with a few singletons for injecting into commands
* Reflection-based `help` command, meaning you never have to change the command when you add new ones
* Docker implementation, perfect for running in production.

## Setup 

Create environments folders called `Development` and `Production` - each one will be referred to as the env folder.
Then, for each environment respectively, create the following:
1. Create a `config` folder
2. Create a `discordBotToken.txt` file, and place inside your bot token.
3. Create a `config.json` file, and fill it with valid JSON matching the data inside the [Config model file](https://github.com/TheRealHona/DiscordBotTemplate/blob/master/src/DiscordBotTemplate/Models/Config.cs).

The environment folders must be placed in the root directory for Docker in production, otherwise in the build output folder for debugging

### Running in Production

Simply run `./runDocker.sh` on Linux (tested on Debian), and it will build + start the Docker container (automatic restarts on crash). 
