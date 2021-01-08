using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotStarWarsDiceRoller
{
  /// <summary>
  /// Setback dice
  /// </summary>
  public class DiceSetback : DiceBase
  {
    /// <summary>
    /// Number of failures
    /// </summary>
    public override int CountFailure
    {
      get
      {
        switch (this.intResult)
        {
          case 1:
          case 2:
          case 5:
          case 6:
            return 0;
          case 3:
          case 4:
            return 1;
          default:
            throw new InvalidOperationException("Unknown result");
        }
      }
    }

    /// <summary>
    ///  Number of threats
    /// </summary>
    public override int CountThreat
    {
      get
      {
        switch (this.intResult)
        {
          case 1:
          case 2:
          case 3:
          case 4:
            return 0;
          case 5:
          case 6:
            return 1;
          default:
            throw new InvalidOperationException("Unknown result");
        }
      }
    }

    /// <summary>
    /// Returns the numerical result of the dice
    /// </summary>
    /// <returns></returns>
    protected override int GetResult()
    {
      return DiceRollerController.RollD6();
    }
  }
}
