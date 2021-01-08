using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotStarWarsDiceRoller
{
  /// <summary>
  /// Challenge dice
  /// </summary>
  public class DiceChallenge : DiceBase
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
          case 6:
          case 7:
          case 10:
          case 11:
            return 0;
          case 2:
          case 3:
          case 8:
          case 9:
          case 12:
            return 1;
          case 4:
          case 5:
            return 2;
          default:
            throw new InvalidOperationException("Unknown result");
        }
      }
    }

    /// <summary>
    /// Number of threats
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
          case 5:
          case 12:
            return 0;
          case 6:
          case 7:
          case 8:
          case 9:
            return 1;
          case 10:
          case 11:
            return 2;
          default:
            throw new InvalidOperationException("Unknown result");
        }
      }
    }

    /// <summary>
    /// Number if despairs
    /// </summary>
    public override int CountDespair
    {
      get
      {
        if (this.intResult == 12)
        {
          return 1;
        }
        else
        {
          return 0;
        }
      }
    }

    /// <summary>
    /// Returns the numerical result of the dice
    /// </summary>
    /// <returns></returns>
    protected override int GetResult()
    {
      return DiceRollerController.RollD12();
    }
  }
}
