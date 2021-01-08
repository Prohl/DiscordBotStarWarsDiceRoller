using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscordBotStarWarsDiceRoller
{
  /// <summary>
  /// The factory for the dice extension
  /// </summary>
  public static class DiceExtensionFactory
  {
    private static List<IDiceExtension> lstExtensions = new List<IDiceExtension>();

    /// <summary>
    /// Adds the extension to the extensionlist. Checks for duplicate keys
    /// </summary>
    /// <param name="_extension"></param>
    public static void AddExtension(IDiceExtension _extension)
    {
      // Check for unique keys
      if (lstExtensions.Any(ef => ef.DiceKeys.Any(key => _extension.DiceKeys.Contains(key))))
      {
        throw new InvalidOperationException("Duplicate keys are not allowed!");
      }

      lstExtensions.Add(_extension);
    }

    /// <summary>
    /// Get's an Text with each dicename and the associated keys
    /// </summary>
    /// <returns></returns>
    public static string GetDicekeyText()
    {
      StringBuilder strBuilderReturn = new StringBuilder();
      foreach (IDiceExtension extension in lstExtensions)
      {
        strBuilderReturn.AppendLine($"{extension.Name}: {string.Join(", ", extension.DiceKeys)}");
      }

      return strBuilderReturn.ToString();
    }

    /// <summary>
    /// Returns the instance of a dice for the given key.
    /// Returns null if no extension with a matching key was found
    /// </summary>
    /// <param name="_key"></param>
    /// <returns></returns>
    public static DiceBase GetDiceForKey(char _key)
    {
      foreach (IDiceExtension extension in lstExtensions)
      {
        if (extension.DiceKeys.Contains(_key))
        {
          return extension.GetDiceInstance();
        }
      }

      // No extension has a matching key....
      return null;
    }
  }
}
