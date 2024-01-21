using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotStarWarsDiceRoller.Dices
{
    /// <summary>
    /// Extension for <see cref="DiceBoost"/>
    /// </summary>
    public class DiceBoostExtension : IDiceExtension
    {
        private readonly List<char> lstKeys = new List<char>() { 'l', 'L' };

        /// <summary>
        /// Keys for this dice
        /// </summary>
        public List<char> DiceKeys => lstKeys;

        /// <summary>
        /// Name of the dice
        /// </summary>
        public string Name => "Boost";

        /// <summary>
        /// Returns a new instance of <see cref="DiceBoost"/>
        /// </summary>
        /// <returns></returns>
        public DiceBase GetDiceInstance()
        {
            return new DiceBoost();
        }
    }
}
