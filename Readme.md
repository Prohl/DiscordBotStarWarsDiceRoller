# DiscordBotStarWarsDiceRoller

The StarWarsDiceRoller (Swadir) is a simple Discord Bot to help manage a Star Wars RPG game over Discord.

## What to do

1. Install Visual Studio. 2017 and 2019 works, haven't tested it with VS Code.
2. Install the Discord.net plugin (https://github.com/discord-net/Discord.Net). Best with NuGet.
3. Downoad the sources and open the project in Visual Studio. When you compile it, there should be one error.
4. Follow the instructions under https://docs.stillu.cc/guides/getting_started/first-bot.html to create a Discord Bot. As soon as you got the token open the properties of the project, navigate to "Resources" and create an entry with the key "TOKEN" and the value is your token.
5. Now the solution should compile and directly connect to discord as your bot.

## How to use it

* The bot only responds, when he is adressed directly (@botname).
* When you type @botname help he will list all commands he understands with an explanation:
    * `roll`: let me roll some dice for you. The dice to roll will be given by keys (characters) associated with the dice. For the binding ask the bot for "help" and he will tell it to you.
    * `reroll`: I will reroll the last roll from the user.
    * `about`: shows the Version and some more info about Swadir
* As a little gimmick there's a file called chatter.txt in the project. The file consists of key-value pairs divided by ":". When you copy that file into the executing folder of the bot, he will answer with a value if someone writes the key in a message (even if the bot is not adressed).
