using System;
using System.Collections.Generic;
using System.Text;
using DiscordBotStarWarsDiceRoller.Controller;

namespace DiscordBotStarWarsDiceRoller.Dices
{
    /// <summary>
    /// An ability dice
    /// </summary>
    public class DiceAbility : DiceBase
    {
        /// <summary>
        /// Number of successes
        /// </summary>
        public override int CountSuccess
        {
            get
            {
                switch (intResult)
                {
                    case 1:
                    case 5:
                    case 6:
                    case 8:
                        return 0;
                    case 2:
                    case 3:
                    case 7:
                        return 1;
                    case 4:
                        return 2;
                    default:
                        throw new InvalidOperationException("Unknown result");
                }
            }
        }

        /// <summary>
        /// Number of advantages
        /// </summary>
        public override int CountAdvantage
        {
            get
            {
                switch (intResult)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return 0;
                    case 5:
                    case 6:
                    case 7:
                        return 1;
                    case 8:
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
