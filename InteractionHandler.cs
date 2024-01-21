using Discord.Interactions;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotStarWarsDiceRoller
{
  /// <summary>
  /// 
  /// </summary>
  public class InteractionHandler
  {
    #region Members
    private readonly DiscordSocketClient socketClient;
    private readonly InteractionService iaService;
    private readonly IServiceProvider provider;
    #endregion

    #region Constructor
    /// <summary>
    /// Constructor - initializes the members
    /// </summary>
    /// <param name="_client"></param>
    /// <param name="_service"></param>
    /// <param name="_provider"></param>
    public InteractionHandler(
      DiscordSocketClient _client,
      InteractionService _service,
      IServiceProvider _provider)
    {
      this.socketClient = _client;
      this.iaService = _service;
      this.provider = _provider;
    }
    #endregion

    /// <summary>
    /// Initializes the Handler
    /// </summary>
    /// <returns></returns>
    public async Task Initialize()
    {
      await this.iaService.AddModulesAsync(Assembly.GetEntryAssembly(), this.provider);

      this.socketClient.InteractionCreated += async (args) =>
      {
        try
        {
          var socketInteractions = new SocketInteractionContext(this.socketClient, args);
          await this.iaService.ExecuteCommandAsync(socketInteractions, this.provider);
        }
        catch(Exception ex)
        {
          Console.WriteLine(ex.ToString());
        }
      };
    }
  }
}
