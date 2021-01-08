using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotStarWarsDiceRoller
{
  /// <summary>
  /// Extension for <see cref="DiceAbility"/>
  /// </summary>
  public class DiceAbilityExtensions : IDiceExtension
  {
    private readonly List<char> lstKeys = new List<char>() { 'g' , 'G' };

    /// <summary>
    /// The keys for this dice
    /// </summary>
    public List<char> DiceKeys => lstKeys;

    /// <summary>
    /// Name of the dice
    /// </summary>
    public string Name => "Ability";

    /// <summary>
    /// Returns a new instance of <see cref="DiceAbility"/>
    /// </summary>
    /// <returns></returns>
    public DiceBase GetDiceInstance()
    {
      return new DiceAbility();
    }
  }
}
