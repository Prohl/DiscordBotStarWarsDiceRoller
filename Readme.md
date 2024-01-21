# DiscordBotStarWarsDiceRoller

The StarWarsDiceRoller (Swadir) is a simple Discord Bot to help manage a Star Wars RPG game over Discord.

## What to do

1. Install Visual Studio. I recommend 2022, haven't tested it with VS Code.
2. Download the sources and open the project in Visual Studio. When you compile it, there should be one error.
3. Follow the instructions under https://docs.stillu.cc/guides/getting_started/first-bot.html to create a Discord Bot. As soon as you got the token open the properties of the project, navigate to "Resources" and create an entry with the key "TOKEN" and the value is your token.
4. Secondly, you need the ID of your Guild (Server). Switch Discord to Developer Mode. Then you can rightclick on your Server and there should be an option "Copy ID" at the bottom of the menu. Go back to the Resources file and create a key "GUILD_ID" and copy the id in the value.
5. Now the solution should compile and directly connect to Discord as your bot.

## How to use it

* The bot works with SlashCommands. A list of available commands should be displayed by Discord.
* The bot also responds, when he is adressed directly (@botname). This is the behaviour from Version 1.0.0, I left in.
* There are two commands, I haven't transfered to SlashCommands:
  * `about` : Shows the Version and general information about the bot
  * `help` : Shows the old help information
* When you type @botname help he will list all commands he understands with an explanation.
* As a little gimmick: You can add a file named `chatter.txt` into the executing folder of the bot, he will answer with a value if someone writes the key in a message (even if the bot is not adressed). Each line should contain a key-value pair divided by `:`, e.g. `hi:Hello there!`.
