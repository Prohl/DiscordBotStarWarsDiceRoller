using System;
using System.Collections.Generic;
using System.Text;
using DiscordBotStarWarsDiceRoller.Controller;

namespace DiscordBotStarWarsDiceRoller.Dices
{
    /// <summary>
    /// Proficiency dice
    /// </summary>
    public class DiceProficiency : DiceBase
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
                    case 6:
                    case 10:
                    case 11:
                        return 0;
                    case 2:
                    case 3:
                    case 7:
                    case 8:
                    case 9:
                    case 12:
                        return 1;
                    case 4:
                    case 5:
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
                    case 5:
                    case 12:
                        return 0;
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                        return 1;
                    case 10:
                    case 11:
                        return 2;
                    default:
                        throw new InvalidOperationException("Unknown result");
                }
            }
        }

        /// <summary>
        /// Number of triumphs
        /// </summary>
        public override int CountTriumph
        {
            get
            {
                if (intResult == 12)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Get the result for this dice
        /// </summary>
        /// <returns></returns>
        protected override int GetResult()
        {
            return DiceRollerController.RollD12();
        }
    }
}
