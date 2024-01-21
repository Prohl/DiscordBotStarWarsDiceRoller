using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotStarWarsDiceRoller
{
  /// <summary>
  /// Handler for the <see cref="PrefixHandler"/>
  /// </summary>
  public class PrefixModule : ModuleBase<SocketCommandContext>
  {
    /// <summary>
    /// Handles a roll command...
    /// No Idea what else it does ;-)
    /// </summary>
    /// <returns></returns>
    [Command("roll")]
    public async Task HandleRollCommand() => await Context.Message.ReplyAsync("roll");

    /// <summary>
    /// Handles a roll command...
    /// No Idea what else it does ;-)
    /// </summary>
    /// <returns></returns>
    [Command("roll10")]
    public async Task HandleRoll10() => await Context.Message.ReplyAsync("roll10");

    /// <summary>
    /// Handles a roll command...
    /// No Idea what else it does ;-)
    /// </summary>
    /// <returns></returns>
    [Command("roll100")]
    public async Task HandleRoll100() => await Context.Message.ReplyAsync("roll100");

    /// <summary>
    /// Handles a reroll command...
    /// No Idea what else it does ;-)
    /// </summary>
    /// <returns></returns>
    [Command("reroll")]
    public async Task HandleRerollCommand() => await Context.Message.ReplyAsync("reroll");

    /// <summary>
    /// Handles a reroll command...
    /// No Idea what else it does ;-)
    /// </summary>
    /// <returns></returns>
    [Command("showdetails")]
    public async Task HandleShowDetails() => await Context.Message.ReplyAsync("showdetails");
  }
}
