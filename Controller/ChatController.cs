using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotStarWarsDiceRoller.Controller
{
  /// <summary>
  /// Controller for several things concerning the Chat
  /// </summary>
  public static class ChatController
  {
    private static Dictionary<string, string> chatter = new Dictionary<string, string>();

    static ChatController()
    {
      // Read chatter file
      if (System.IO.File.Exists(@"chatter.txt"))
      {
        string[] chatterLines = System.IO.File.ReadAllLines(@"chatter.txt");

        // Write chatter file into dictionary
        chatter = new Dictionary<string, string>();

        foreach (string line in chatterLines)
        {
          string[] strLineSplit = line.Split(':');
          if (strLineSplit.Length > 1)
          {
            StringBuilder builder = new StringBuilder();
            builder.Append(strLineSplit[1]);
            for (int index = 2; index < strLineSplit.Length; index++)
            {
              builder.Append(':');
              builder.Append(strLineSplit[index]);
            }

            chatter.Add(strLineSplit[0], builder.ToString());
          }
        }
      }
    }

    /// <summary>
    /// Checks if the source contains any traces of chatter and returns the responses
    /// </summary>
    /// <param name="_source"></param>
    /// <returns></returns>
    public static List<string> GetChatter(string _source)
    {
      List<string> chatterReturn = new List<string>();
      foreach (KeyValuePair<string, string> pair in chatter)
      {
        if (_source.Contains(pair.Key, StringComparison.OrdinalIgnoreCase))
        {
          chatterReturn.Add(pair.Value);
        }
      }
      return chatterReturn;
    }

    /// <summary>
    /// Return username#discriminator of the user
    /// </summary>
    /// <param name="_user"></param>
    /// <returns></returns>
    public static string GetUniqueDiscriminator(SocketUser _user)
    {
      return $"{_user.Username}#{_user.Discriminator}";
    }
  }
}
