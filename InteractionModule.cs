using Discord;
using Discord.Interactions;
using DiscordBotStarWarsDiceRoller.Controller;
using DiscordBotStarWarsDiceRoller.Dices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotStarWarsDiceRoller
{
  /// <summary>
  /// Module for the <see cref="InteractionHandler"/>
  /// </summary>
  public class InteractionModule : InteractionModuleBase<SocketInteractionContext>
  {
    /// <summary>
    /// Handles a roll command
    /// </summary>
    /// <returns></returns>
    [SlashCommand("roll", "Roll some dies")]
    public async Task HandleRollCommand(string dicepool)
    {
      DicePool pool = DiceRollerController.ParseAndRollDicePool(dicepool, base.Context.User);

      await RespondAsync($"Dicepool: {dicepool}{Environment.NewLine}{pool}");
    }

    /// <summary>
    /// Handles a d10 roll
    /// </summary>
    /// <returns></returns>
    [SlashCommand("roll10", "Rolls a d10")]
    public async Task HandleRoll10()
    {
      await RespondAsync(DiceRollerController.RollD10().ToString());
    }

    /// <summary>
    /// Handles a d100 roll
    /// </summary>
    /// <returns></returns>
    [SlashCommand("roll100", "Rolls a d100")]
    public async Task HandleRoll100()
    {
      await RespondAsync(DiceRollerController.RollD100().ToString());
    }

    /// <summary>
    /// Handles a reroll command
    /// </summary>
    /// <returns></returns>
    [SlashCommand("reroll", "Rerolls the last roll")]
    public async Task HandleReroll()
    {
      DicePool pool = DiceRollerController.GetLastDicePoolForUser(base.Context.User);
      if (pool != null)
      {
        // Roll the pool again
        pool.Roll();
        // Return the results from the pool
        await RespondAsync(pool.ToString());
      }
      else
      {
        // No roll found for the user...
        await RespondAsync("I found no roll for you to reroll.");
      }
    }

    /// <summary>
    /// Handles the user wish to see the details of his last roll
    /// </summary>
    /// <returns></returns>
    [SlashCommand("showdetails", "Shows the details for the last roll")]
    public async Task HandleShowDetails()
    {
      DicePool lastPool = DiceRollerController.GetLastDicePoolForUser(base.Context.User);
      if (lastPool != null)
      {
        // Return the results from the pool
        await RespondAsync(lastPool.Details);
      }
      else
      {
        // No roll found for the user...
        await RespondAsync("I found no roll for you to show the details.");
      }
    }
  }
}
