using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace System
{
  static class Extension
  {
    #region String
    /// <summary>
    /// Ensures string is no longer than the given length. Cuts the string and adds ellipsis at the end if longer
    /// </summary>
    /// <param name="sourceString"></param>
    /// <param name="maxLength">Maxmium length of the string</param>
    /// <returns></returns>
    public static string TripleDot(this string sourceString, uint maxLength)
    {
      if (sourceString.Length > maxLength && maxLength > 3)
        return sourceString.Substring(0, (int) maxLength - 3) + "…";
      else
        return sourceString;
    }
    #endregion


    public static string Humanify(this string sourceString, IDictionary<string, string> preProcessing = null, IDictionary<string, string> postProcessing = null)
    {
      // nested utility method
      string ApplyConversions(string src, IDictionary< string, string> patterns)
      {
        if (patterns != null)
          foreach (KeyValuePair<string, string> kv in patterns)
          {
            var r = new Regex(kv.Key);
            src = r.Replace(src, kv.Value);
          }

        return src;
      }

      // apply pre conversion patterns
      sourceString = ApplyConversions(sourceString, preProcessing);

      // do pre cleanups that helps with readability
      sourceString = sourceString.Replace("And", "&");

      // add a space before upper chars
      string humanifiedStr = "";
      foreach(char c in sourceString)
      {
        if (Char.IsUpper(c))
          humanifiedStr += $" {c}";
        else
          humanifiedStr += c;
      }

      // do post cleanups
      humanifiedStr = humanifiedStr.Replace("Non ", "Non");  // e.g. NonBearing should not be "Non Bearing"

      // apply post conversion patterns
      humanifiedStr = ApplyConversions(humanifiedStr, postProcessing);

      return humanifiedStr;
    }
  }
}
