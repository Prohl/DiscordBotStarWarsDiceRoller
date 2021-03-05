using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotStarWarsDiceRoller
{
  /// <summary>
  /// Base-Class for nearly any dice
  /// </summary>
  public abstract class DiceBase
  {
    /// <summary>
    /// The result from the roll as int
    /// </summary>
    protected int intResult;

    /// <summary>
    /// Method to roll the dice
    /// </summary>
    /// <returns></returns>
    public void Roll()
    {
      this.intResult = this.GetResult();
    }

    /// <summary>
    /// Method to get the result for the specific dice
    /// </summary>
    /// <returns></returns>
    protected abstract int GetResult();

    /// <summary>
    /// Returns the result of the roll
    /// </summary>
    public int Result => this.intResult;

    /// <summary>
    /// Number of successes
    /// </summary>
    public virtual int CountSuccess => 0;

    /// <summary>
    /// Number of failures
    /// </summary>
    public virtual int CountFailure => 0;

    /// <summary>
    /// Number of advantages
    /// </summary>
    public virtual int CountAdvantage => 0;

    /// <summary>
    /// Number of threats
    /// </summary>
    public virtual int CountThreat => 0;

    /// <summary>
    /// Number of triumphs
    /// </summary>
    public virtual int CountTriumph => 0;

    /// <summary>
    /// Number if Despairs
    /// </summary>
    public virtual int CountDespair => 0;

    /// <summary>
    /// Number of light force points
    /// </summary>
    public virtual int CountLightForce => 0;

    /// <summary>
    /// Number of dark force points
    /// </summary>
    public virtual int CountDarkForce => 0;
  }
}
