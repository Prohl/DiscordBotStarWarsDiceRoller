using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotStarWarsDiceRoller.Dices
{
    /// <summary>
    /// Extension for the dice
    /// </summary>
    public interface IDiceExtension
    {
        /// <summary>
        /// Returns the keys (letters) for the specific dice - must be unique
        /// </summary>
        List<char> DiceKeys { get; }

        /// <summary>
        /// The name of the dice
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns a new instance of the dice for the extension
        /// </summary>
        /// <returns></returns>
        DiceBase GetDiceInstance();
    }
}
