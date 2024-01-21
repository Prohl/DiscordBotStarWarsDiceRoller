using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using DiscordBotStarWarsDiceRoller.Controller;
using DiscordBotStarWarsDiceRoller.Dices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DiscordBotStarWarsDiceRoller
{
    /// <summary>
    /// Handles Prefixes
    /// </summary>
    public class PrefixHandler
  {
    #region Members
    private readonly DiscordSocketClient socketClient;
    private readonly CommandService cService;
    #endregion

    #region Constructor
    /// <summary>
    /// Constructor - initializes the members
    /// </summary>
    /// <param name="_client"></param>
    /// <param name="_service"></param>
    public PrefixHandler(
      DiscordSocketClient _client,
      CommandService _service)
    {
      this.socketClient = _client;
      this.cService = _service;
    }
    #endregion

    /// <summary>
    /// Initializes the Handler
    /// </summary>
    /// <returns></returns>
    public void Initialize()
    {
      this.socketClient.MessageReceived += MessageReceived;
    }

    /// <summary>
    /// Adds a Module to the handler
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void AddModule<T>()
    {
      this.cService.AddModuleAsync<T>(null);
    }

    private async Task MessageReceived(SocketMessage message)
    {
      // Cast the message to a SockerUserMessage
      SocketUserMessage userMessage = message as SocketUserMessage;

      // if the casted message ist null, we have a SystemMessage - ignore it
      if (userMessage == null)
      {
        return;
      }

      // Also ignore if the message has the defined prefix, mentions the current user or comes from another bot
      int argPos = 0;
      if (!(userMessage.HasCharPrefix(Program.PREFIX, ref argPos)
        || userMessage.HasMentionPrefix(this.socketClient.CurrentUser, ref argPos))
        || userMessage.Author.IsBot)
      {
        return;
      }

      if (message.MentionedUsers.FirstOrDefault(user => ChatController.GetUniqueDiscriminator(user) == ChatController.GetUniqueDiscriminator(this.socketClient.CurrentUser)) != null)
      {
        // Remove all mentions from the content
        Regex regi = new Regex(@"<[@|#][!]?[\w]*>");
        string strMessage = regi.Replace(message.Content, "").Trim();
        // The Bot knows roll, reroll and help
        if (strMessage.StartsWith("roll", StringComparison.OrdinalIgnoreCase))
        {
          #region Roll
          // Cut the roll from the input
          string strInput = strMessage[4..].Trim();
          // Two special rolls für d100 and d10
          if (strInput == "100")
          {
            await message.Channel.SendMessageAsync($"{message.Author.Mention}{Environment.NewLine}{DiceRollerController.RollD100()}");
          }
          else if (strInput == "10")
          {
            await message.Channel.SendMessageAsync($"{message.Author.Mention}{Environment.NewLine}{DiceRollerController.RollD10()}");
          }
          else
          {
            DicePool pool = DiceRollerController.ParseAndRollDicePool(strInput, message.Author);
            // Return the results from the pool
            await message.Channel.SendMessageAsync($"{message.Author.Mention}{Environment.NewLine}{pool}");
          }
          #endregion
        }
        else if (strMessage.StartsWith("reroll", StringComparison.OrdinalIgnoreCase))
        {
          #region Reroll
          DicePool lastPool = DiceRollerController.GetLastDicePoolForUser(message.Author);
          if (lastPool != null)
          {
            // Roll the pool again
            lastPool.Roll();
            // Return the results from the pool
            await message.Channel.SendMessageAsync($"{message.Author.Mention}{Environment.NewLine}{lastPool}");
          }
          else
          {
            // No roll found for the user...
            await message.Channel.SendMessageAsync($"{message.Author.Mention} I found no roll for you to reroll.");
          }
          #endregion
        }
        else if (strMessage.StartsWith("details", StringComparison.OrdinalIgnoreCase))
        {
          #region Details of last roll
          DicePool lastPool = DiceRollerController.GetLastDicePoolForUser(message.Author);
          if (lastPool != null)
          {
            // Return the results from the pool
            await message.Channel.SendMessageAsync($"{message.Author.Mention} {lastPool.Details}");
          }
          else
          {
            // No roll found for the user...
            await message.Channel.SendMessageAsync($"{message.Author.Mention} I found no roll for you to show the details.");
          }
          #endregion
        }
        else if (strMessage.StartsWith("about", StringComparison.OrdinalIgnoreCase))
        {
          await message.Channel.SendMessageAsync($"Star Wars Dice Roller Version '{this.GetType().Assembly.GetName().Version}' under GPL-3.0 License.{Environment.NewLine}" +
            $"Source code can be found under: https://github.com/Prohl/DiscordBotStarWarsDiceRoller{Environment.NewLine}");
        }
        else if (strMessage.StartsWith("help", StringComparison.OrdinalIgnoreCase))
        {
          StringBuilder strBuilderHelp = new StringBuilder();
          strBuilderHelp.AppendLine($"{message.Author.Mention} I understand the following commands:");
          strBuilderHelp.AppendLine("- roll: Let me roll some dice for you. You can specify the dice to roll with the following keys:");
          strBuilderHelp.AppendLine(DiceExtensionFactory.GetDicekeyText());
          strBuilderHelp.AppendLine("There are 2 special dice I can roll for you:");
          strBuilderHelp.AppendLine("10: Rolls a d10.");
          strBuilderHelp.AppendLine("100: Rolls a d100.");
          strBuilderHelp.AppendLine("- reroll: I will reroll your last roll.");
          strBuilderHelp.AppendLine("- details: I will tell you the detailed result of your last roll.");

          await message.Channel.SendMessageAsync(strBuilderHelp.ToString());
        }
        else
        {
          await message.Channel.SendMessageAsync($"{message.Author.Mention} I don't understand. Type \"help\" when you need some help from me.");
        }
      }

      // Get the chatter from the controller
      var chatter = ChatController.GetChatter(message.Content);
      foreach (string pair in chatter)
      {
        await message.Channel.SendMessageAsync(pair);
      }
    }
  }
}
