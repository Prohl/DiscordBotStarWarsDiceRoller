using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotStarWarsDiceRoller
{
  /// <summary>
  /// Force dice
  /// </summary>
  public class DiceForce : DiceBase
  {
    /// <summary>
    /// Number of light force points
    /// </summary>
    public override int CountLightForce
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
          case 6:
          case 7:
            return 0;
          case 8:
          case 9:
            return 1;
          case 10:
          case 11:
          case 12:
            return 2;
          default:
            throw new InvalidOperationException("Unknown result");
        }
      }
    }

    /// <summary>
    /// Number of dark force points
    /// </summary>
    public override int CountDarkForce
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
          case 6:
            return 1;
          case 7:
            return 2;
          case 8:
          case 9:
          case 10:
          case 11:
          case 12:
            return 0;
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
      return DiceRollerController.RollD12();
    }
  }
}
