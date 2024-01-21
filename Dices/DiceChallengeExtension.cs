using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotStarWarsDiceRoller.Dices
{
    /// <summary>
    /// Extension for <see cref="DiceChallenge"/>
    /// </summary>
    public class DiceChallengeExtension : IDiceExtension
    {
        private readonly List<char> lstKeys = new List<char>() { 'r', 'R' };

        /// <summary>
        /// Returns the keys for this dce
        /// </summary>
        public List<char> DiceKeys => lstKeys;


        /// <summary>
        /// Name of the dice
        /// </summary>
        public string Name => "Challenge";

        /// <summary>
        /// Returns a new instance of <see cref="DiceChallenge"/>
        /// </summary>
        /// <returns></returns>
        public DiceBase GetDiceInstance()
        {
            return new DiceChallenge();
        }
    }
}
