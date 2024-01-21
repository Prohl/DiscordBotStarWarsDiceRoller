using Discord;
using Discord.WebSocket;
using DiscordBotStarWarsDiceRoller.Dices;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotStarWarsDiceRoller.Controller
{
  /// <summary>
  /// Controller for roling - includes the randomizer and methods to roll several dice
  /// </summary>
  public static class DiceRollerController
  {
    /// <summary>
    /// The last dicepool for every user
    /// </summary>
    private static readonly Dictionary<string, DicePool> dicLastDicepool4User = new Dictionary<string, DicePool>();

    /// <summary>
    /// The Randoomizer that's being used
    /// </summary>
    private static readonly Random Randy = new Random((int)DateTime.Now.Ticks);

    /// <summary>
    /// Rolls a d10 and returns the result
    /// </summary>
    /// <returns></returns>
    public static int RollD6() => Randy.Next(1, 7);

    /// <summary>
    /// Rolls a d10 and returns the result
    /// </summary>
    /// <returns></returns>
    public static int RollD8() => Randy.Next(1, 9);

    /// <summary>
    /// Rolls a d10 and returns the result
    /// </summary>
    /// <returns></returns>
    public static int RollD10() => Randy.Next(1, 11);

    /// <summary>
    /// Rolls a d10 and returns the result
    /// </summary>
    /// <returns></returns>
    public static int RollD12() => Randy.Next(1, 13);

    /// <summary>
    /// Rolls a d100 and returns this result
    /// </summary>
    /// <returns></returns>
    public static int RollD100() => Randy.Next(1, 101);

    #region Last Roll
    /// <summary>
    /// Remembers the last roll for the user
    /// </summary>
    /// <param name="_user"></param>
    /// <param name="_pool"></param>
    public static void AddLastRoll4User(
      SocketUser _user,
       DicePool _pool)
    {
      dicLastDicepool4User[ChatController.GetUniqueDiscriminator(_user)] = _pool;
    }

    /// <summary>
    /// Return the last roll for the user or null if there is none
    /// </summary>
    /// <param name="_user"></param>
    /// <returns></returns>
    public static DicePool GetLastDicePoolForUser(SocketUser _user)
    {
      if (dicLastDicepool4User.TryGetValue(ChatController.GetUniqueDiscriminator(_user), out DicePool pool))
      {
        return pool;
      }
      else
      {
        // No roll found for the user...
        return null;
      }
    }
    #endregion

    #region DicePool
    /// <summary>
    /// Parses the given string into a dicepool, rolls it and returns it
    /// </summary>
    /// <param name="_strDicePool">The string that is parsed into the pool</param>
    /// <param name="_rollingUser">The executing user to memorize his last pool</param>
    /// <returns></returns>
    public static DicePool ParseAndRollDicePool(
      string _strDicePool,
      SocketUser _rollingUser)
    {
      // Now take every char and try to find a Dice for it and add it to the DicePool
      DicePool pool = new DicePool();
      foreach (char key in _strDicePool)
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
      DiceRollerController.AddLastRoll4User(_rollingUser, pool);

      return pool;
    }
    #endregion
  }
}
