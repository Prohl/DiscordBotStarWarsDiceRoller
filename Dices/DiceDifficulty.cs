using System;
using System.Collections.Generic;
using System.Text;
using DiscordBotStarWarsDiceRoller.Controller;

namespace DiscordBotStarWarsDiceRoller.Dices
{
    /// <summary>
    /// Difficulty dice
    /// </summary>
    public class DiceDifficulty : DiceBase
    {
        /// <summary>
        /// Number of failures
        /// </summary>
        public override int CountFailure
        {
            get
            {
                switch (intResult)
                {
                    case 1:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        return 0;
                    case 2:
                    case 8:
                        return 1;
                    case 3:
                        return 2;
                    default:
                        throw new InvalidOperationException("Unknown result");
                }
            }
        }

        /// <summary>
        /// Number of threats
        /// </summary>
        public override int CountThreat
        {
            get
            {
                switch (intResult)
                {
                    case 1:
                    case 2:
                    case 3:
                        return 0;
                    case 4:
                    case 5:
                    case 6:
                    case 8:
                        return 1;
                    case 7:
                        return 2;
                    default:
                        throw new InvalidOperationException("Unknown result");
                }
            }
        }

        /// <summary>
        /// Returns the numerical result of the dice
        /// </summary>
        /// <returns></returns>
        protected override int GetResult()
        {
            return DiceRollerController.RollD8();
        }
    }
}
