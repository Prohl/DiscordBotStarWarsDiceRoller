using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotStarWarsDiceRoller
{
  /// <summary>
  /// Extension for <see cref="DiceSetback"/>
  /// </summary>
  public class DiceSetbackExtension : IDiceExtension
  {
    private readonly List<char> lstKeys = new List<char>() { 'b' , 'B' };

    /// <summary>
    /// Keys for the dice
    /// </summary>
    public List<char> DiceKeys => lstKeys;

    /// <summary>
    /// Name of the dice
    /// </summary>
    public string Name => "Setback";

    /// <summary>
    /// Returns a new instance of <see cref="DiceSetback"/>
    /// </summary>
    /// <returns></returns>
    public DiceBase GetDiceInstance()
    {
      return new DiceSetback();
    }
  }
}
