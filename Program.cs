using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using DiscordBotStarWarsDiceRoller.Dices;
using DiscordBotStarWarsDiceRoller.Properties;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    #region Constanzen
    /// <summary>
    /// PreFix for the commands
    /// </summary>
    public const char PREFIX = '/';
    #endregion

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
      // Register the dice-extensions
      DiceExtensionFactory.AddExtension(new DiceAbilityExtensions());
      DiceExtensionFactory.AddExtension(new DiceBoostExtension());
      DiceExtensionFactory.AddExtension(new DiceChallengeExtension());
      DiceExtensionFactory.AddExtension(new DiceDifficultyExtension());
      DiceExtensionFactory.AddExtension(new DiceForceExtension());
      DiceExtensionFactory.AddExtension(new DiceProficiencyExtension());
      DiceExtensionFactory.AddExtension(new DiceSetbackExtension());

      // Changes that needed to be made for the update from v2 to v3 of Discord.net
      // See: https://discordnet.dev/guides/v2_v3_guide/v2_to_v3_guide.html
      var dsConfig = new DiscordSocketConfig()
      {
        GatewayIntents = GatewayIntents.AllUnprivileged
      };
      DiscordSocketClient discordClient = new DiscordSocketClient(dsConfig);

      using IHost host = Host.CreateDefaultBuilder().ConfigureServices((_, services) =>
      {
        services.AddSingleton(discordClient);
        services.AddSingleton(s => new InteractionService(s.GetRequiredService<DiscordSocketClient>()));
        services.AddSingleton<InteractionHandler>();
        services.AddSingleton(s => new CommandService());
        services.AddSingleton<PrefixHandler>();
      }).Build();

      await RunAsynch(host);
    }

    private async Task RunAsynch(IHost _host)
    {
      using IServiceScope serviceScope = _host.Services.CreateScope();
      IServiceProvider provider = serviceScope.ServiceProvider;

      var client = provider.GetRequiredService<DiscordSocketClient>();
      var slashCommands = provider.GetRequiredService<InteractionService>();
      await provider.GetRequiredService<InteractionHandler>().Initialize();
      var prefixCommands = provider.GetRequiredService<PrefixHandler>();
      prefixCommands.AddModule<PrefixModule>();
      prefixCommands.Initialize();

      client.Log += Log;
      // slashCommands.Log += async (LogMessage msg) => Console.WriteLine(msg.Message);

      client.Ready += async () => 
      {
        await slashCommands.RegisterCommandsToGuildAsync(UInt64.Parse(Resources.GUILD_ID));
        Console.WriteLine("Bot ready");
      };

      // Token
      string strToken = Resources.TOKEN;
      await client.LoginAsync(TokenType.Bot, strToken);
      await client.StartAsync();

      // Block this task until the program is closed.
      await Task.Delay(-1);
    }

    private Task Log(LogMessage msg)
    {
      Console.WriteLine(msg.ToString());
      return Task.CompletedTask;
    }
  }
}
