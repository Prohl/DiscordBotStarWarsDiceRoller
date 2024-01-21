using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotStarWarsDiceRoller.Dices
{
    /// <summary>
    /// Extension for <see cref="DiceProficiency"/>
    /// </summary>
    public class DiceProficiencyExtension : IDiceExtension
    {
        private readonly List<char> lstKeys = new List<char>() { 'y', 'Y' };

        /// <summary>
        /// Keys for the dice
        /// </summary>
        public List<char> DiceKeys => lstKeys;

        /// <summary>
        /// Name of the dice
        /// </summary>
        public string Name => "Proficiency";

        /// <summary>
        /// Returns a new instance of <see cref="DiceProficiency"/>
        /// </summary>
        /// <returns></returns>
        public DiceBase GetDiceInstance()
        {
            return new DiceProficiency();
        }
    }
}
