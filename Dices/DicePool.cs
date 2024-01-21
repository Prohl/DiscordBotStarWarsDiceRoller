using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscordBotStarWarsDiceRoller.Dices
{
  /// <summary>
  /// A dicepool
  /// </summary>
  public class DicePool : List<DiceBase>
  {
    /// <summary>
    /// Rolls all the dice in the pool
    /// </summary>
    public void Roll()
    {
      ForEach(dice => dice.Roll());
    }

    /// <summary>
    /// Returns a string with a detailed representation of the result
    /// </summary>
    public string Details
    {
      get
      {
        StringBuilder strBuilderReturn = new StringBuilder("Detailed results: ");
        bool bolFirstDice = true;
        foreach (DiceBase diceAktuell in this)
        {
          // For each dice after the first do something between the dice
          if (bolFirstDice == false)
          {
            strBuilderReturn.Append(" | ");
          }
          else
          {
            bolFirstDice = false;
          }

          for (int intCount = 0; intCount < diceAktuell.CountSuccess; intCount++)
          {
            strBuilderReturn.Append("S");
          }
          for (int intCount = 0; intCount < diceAktuell.CountFailure; intCount++)
          {
            strBuilderReturn.Append("F");
          }
          for (int intCount = 0; intCount < diceAktuell.CountAdvantage; intCount++)
          {
            strBuilderReturn.Append("A");
          }
          for (int intCount = 0; intCount < diceAktuell.CountThreat; intCount++)
          {
            strBuilderReturn.Append("Th");
          }
          for (int intCount = 0; intCount < diceAktuell.CountTriumph; intCount++)
          {
            strBuilderReturn.Append("Tr");
          }
          for (int intCount = 0; intCount < diceAktuell.CountDespair; intCount++)
          {
            strBuilderReturn.Append("D");
          }
          for (int intCount = 0; intCount < diceAktuell.CountLightForce; intCount++)
          {
            strBuilderReturn.Append("Fl");
          }
          for (int intCount = 0; intCount < diceAktuell.CountDarkForce; intCount++)
          {
            strBuilderReturn.Append("Fd");
          }

          strBuilderReturn.Append($" ({diceAktuell.Result})");
        }

        return strBuilderReturn.ToString();
      }
    }

    /// <summary>
    /// ToString to get a userfriendly text with the result
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      StringBuilder strBuilderReturn = new StringBuilder();

      // Suppress successes/failures/advantages/threats if the pool consists only of force dice
      if (this.All(dice => dice is DiceForce) == false)
      {
        // First we count the sucesses / failures
        int intSuccessesFailures = this.Sum(dice => dice.CountSuccess) - this.Sum(dice => dice.CountFailure);
        if (intSuccessesFailures > 0)
        {
          strBuilderReturn.AppendLine($"Successes: {intSuccessesFailures}");
        }
        else if (intSuccessesFailures < 0)
        {
          strBuilderReturn.AppendLine($"Failures: {-intSuccessesFailures}");
        }
        else
        {
          strBuilderReturn.AppendLine("No successes or failures");
        }

        // Then we count the advantages / threats
        int intAdvantagesThreats = this.Sum(dice => dice.CountAdvantage) - this.Sum(dice => dice.CountThreat);
        if (intAdvantagesThreats > 0)
        {
          strBuilderReturn.AppendLine($"Advantages: {intAdvantagesThreats}");
        }
        else if (intAdvantagesThreats < 0)
        {
          strBuilderReturn.AppendLine($"Threats: {-intAdvantagesThreats}");
        }
        else
        {
          strBuilderReturn.AppendLine("No advantages or threats");
        }
      }

      // Triumphes and Despairs don't cancel each other out
      int intTriumphes = this.Sum(dice => dice.CountTriumph);
      if (intTriumphes > 0)
      {
        strBuilderReturn.AppendLine($"Triumphes: {intTriumphes}");
      }

      int intDespairs = this.Sum(dice => dice.CountDespair);
      if (intDespairs > 0)
      {
        strBuilderReturn.AppendLine($"Despairs: {intDespairs}");
      }

      // Force points
      int intLightForce = this.Sum(dice => dice.CountLightForce);
      if (intLightForce > 0)
      {
        strBuilderReturn.AppendLine($"Light force points: {intLightForce}");
      }

      int intDarkForce = this.Sum(dice => dice.CountDarkForce);
      if (intDarkForce > 0)
      {
        strBuilderReturn.AppendLine($"Dark force points: {intDarkForce}");
      }

      return strBuilderReturn.ToString();
    }
  }
}
