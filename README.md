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

The environment folders must be placed in the root directory for Docker in production, otherwise in the build output folder for debugging

### Running in Production

Simply run `./runDocker.sh` on Linux (tested on Debian), and it will build + start the Docker container (automatic restarts on crash). 
