using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DiscordBotStarWarsDiceRoller
{
  /// <summary>
  /// Main class
  /// </summary>
  public class Program
  {
    private DiscordSocketClient discordClient;

    private Dictionary<string, string> chatter;

    private Dictionary<string, DicePool> dicLastDicepool4User = new Dictionary<string, DicePool>();

    /// <summary>
    /// Main - Method
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    => new Program().MainAsync().GetAwaiter().GetResult();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task MainAsync()
    {
      this.ReadChatter();

      // Register the dice-extensions
      DiceExtensionFactory.AddExtension(new DiceAbilityExtensions());
      DiceExtensionFactory.AddExtension(new DiceBoostExtension());
      DiceExtensionFactory.AddExtension(new DiceChallengeExtension());
      DiceExtensionFactory.AddExtension(new DiceDifficultyExtension());
      DiceExtensionFactory.AddExtension(new DiceForceExtension());
      DiceExtensionFactory.AddExtension(new DiceProficiencyExtension());
      DiceExtensionFactory.AddExtension(new DiceSetbackExtension());

      this.discordClient = new DiscordSocketClient();
      this.discordClient.Log += Log;
      this.discordClient.MessageReceived += MessageReceived;

      // Token
      string strToken = Properties.Resources.TOKEN;

      await this.discordClient.LoginAsync(TokenType.Bot, strToken);
      await this.discordClient.StartAsync();

      // Block this task until the program is closed.
      await Task.Delay(-1);
    }

    private Task Log(LogMessage msg)
    {
      Console.WriteLine(msg.ToString());
      return Task.CompletedTask;
    }

    private async Task MessageReceived(SocketMessage message)
    {
      if (message.MentionedUsers.FirstOrDefault(user => GetUniqueDiscriminator(user) == GetUniqueDiscriminator(this.discordClient.CurrentUser)) != null)
      {
        // Remove all mentions from the content
        Regex regi = new Regex(@"<[@|#][!]?[\w]*>");
        string strMessage = regi.Replace(message.Content, "").Trim();
        // The Bot knows roll, reroll and help
        if (strMessage.StartsWith("roll", StringComparison.OrdinalIgnoreCase))
        {
          #region Roll
          // Cut the roll from the input
          string strInput = strMessage[4..];
          // Now take every char and try to find a Dice for it and add it to the DicePool
          DicePool pool = new DicePool();
          foreach (char key in strInput)
          {
            DiceBase dice = DiceExtensionFactory.GetDiceForKey(key);
            if (dice != null)
            {
              pool.Add(dice);
            }
          }

          // Now roll the pool
          pool.Roll();
          // Save the pool so that the user can reroll it later
          this.dicLastDicepool4User[GetUniqueDiscriminator(message.Author)] = pool;
          // Return the results from the pool
          await message.Channel.SendMessageAsync($"{message.Author.Mention}{Environment.NewLine}{pool}");
          #endregion
        }
        else if (strMessage.StartsWith("reroll", StringComparison.OrdinalIgnoreCase))
        {
          #region Reroll
          if (this.dicLastDicepool4User.TryGetValue(GetUniqueDiscriminator(message.Author), out DicePool pool))
          {
            // Roll the pool again
            pool.Roll();
            // Return the results from the pool
            await message.Channel.SendMessageAsync($"{message.Author.Mention} {pool}");
          }
          else
          {
            // No roll found for the user...
            await message.Channel.SendMessageAsync($"{message.Author.Mention} I found no roll for you to reroll.");
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
          strBuilderHelp.AppendLine("- roll: let me roll some dice for you. You can specify the dice to roll with the following keys:");
          strBuilderHelp.AppendLine(DiceExtensionFactory.GetDicekeyText());
          strBuilderHelp.AppendLine("- reroll: I will reroll your last roll.");

          await message.Channel.SendMessageAsync(strBuilderHelp.ToString());
        }
        else
        {
          await message.Channel.SendMessageAsync($"{message.Author.Mention} I don't understand. Type \"help\" when you need some help from me.");
        }
      }

      // Never respond to a bot and only if we have a chatter file
      if (message.Author.IsBot == false && this.chatter != null)
      {
        // Response to see, that the Bot is still responding
        foreach (KeyValuePair<string, string> pair in this.chatter)
        {
          if (message.Content.Contains(pair.Key, StringComparison.OrdinalIgnoreCase))
          {
            await message.Channel.SendMessageAsync(pair.Value);
            break;
          }
        }
      }
    }

    private void ReadChatter()
    {
      // Read chatter file
      if (System.IO.File.Exists(@"chatter.txt"))
      {
        string[] chatterLines = System.IO.File.ReadAllLines(@"chatter.txt");

        // Write chatter file into dictionary
        this.chatter = new Dictionary<string, string>();

        foreach (string line in chatterLines)
        {
          string[] strLineSplit = line.Split(':');
          if (strLineSplit.Length > 1)
          {
            StringBuilder builder = new StringBuilder();
            builder.Append(strLineSplit[1]);
            for (int index = 2; index < strLineSplit.Length; index++)
            {
              builder.Append(':');
              builder.Append(strLineSplit[index]);
            }

            chatter.Add(strLineSplit[0], builder.ToString());
          }
        }
      }
    }

    /// <summary>
    /// Return username#discriminator of the user
    /// </summary>
    /// <param name="_user"></param>
    /// <returns></returns>
    private string GetUniqueDiscriminator(SocketUser _user)
    {
      return $"{_user.Username}#{_user.Discriminator}";
    }
  }
}
