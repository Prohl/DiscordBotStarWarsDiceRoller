using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotStarWarsDiceRoller.Dices
{
    /// <summary>
    /// Extension for <see cref="DiceDifficulty"/>
    /// </summary>
    public class DiceDifficultyExtension : IDiceExtension
    {
        private readonly List<char> lstKeys = new List<char>() { 'p', 'P' };

        /// <summary>
        /// Keys for the dice
        /// </summary>
        public List<char> DiceKeys => lstKeys;

        /// <summary>
        /// Name of the dice
        /// </summary>
        public string Name => "Difficulty";

        /// <summary>
        /// Returns a new instance of <see cref="DiceDifficulty"/>
        /// </summary>
        /// <returns></returns>
        public DiceBase GetDiceInstance()
        {
            return new DiceDifficulty();
        }
    }
}
