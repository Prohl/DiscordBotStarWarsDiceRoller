using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotStarWarsDiceRoller.Dices
{
    /// <summary>
    /// Extension for <see cref="DiceForce"/>
    /// </summary>
    public class DiceForceExtension : IDiceExtension
    {
        private readonly List<char> lstKeys = new List<char>() { 'w', 'W' };

        /// <summary>
        /// Keys for the dice
        /// </summary>
        public List<char> DiceKeys => lstKeys;

        /// <summary>
        /// Name of the dice
        /// </summary>
        public string Name => "Force";

        /// <summary>
        /// Returns a new instance of <see cref="DiceForce"/>
        /// </summary>
        /// <returns></returns>
        public DiceBase GetDiceInstance()
        {
            return new DiceForce();
        }
    }
}
