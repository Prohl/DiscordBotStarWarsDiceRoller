using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotStarWarsDiceRoller
{
  /// <summary>
  /// Controller for roling - includes the randomizer and methods to roll several dice
  /// </summary>
  public static class DiceRollerController
  {
    /// <summary>
    /// The Randoomizer that's being used
    /// </summary>
    private static Random Randy = new Random((int)DateTime.Now.Ticks);

    /// <summary>
    /// Rolls a d10 and returns the result
    /// </summary>
    /// <returns></returns>
    public static int RollD6()
    {
      return Randy.Next(1, 7);
    }

    /// <summary>
    /// Rolls a d10 and returns the result
    /// </summary>
    /// <returns></returns>
    public static int RollD8()
    {
      return Randy.Next(1, 9);
    }

    /// <summary>
    /// Rolls a d10 and returns the result
    /// </summary>
    /// <returns></returns>
    public static int RollD10()
    {
      return Randy.Next(1, 11);
    }

    /// <summary>
    /// Rolls a d10 and returns the result
    /// </summary>
    /// <returns></returns>
    public static int RollD12()
    {
      return Randy.Next(1, 13);
    }
  }
}
