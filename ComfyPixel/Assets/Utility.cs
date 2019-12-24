
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Utility : MonoBehaviour {

    protected List<string> split_text(string text)
    {
        ///take some form string-text as input
        ///divides into individual lines, always divided at newlines

        List<string> lines = new List<string>();

        foreach (string line in text.Split('\n'))
        {
            if (line != "")
            {
                lines.Add(line);
            }
            else ///make sure not to have any empty strings
            {
                continue;
            }
        }

        return lines; ///returns list of strings
    }

    protected void run_text(List<string> lines, Text text_box, float scroll_speed, bool can_interact) ///default key to interact with dialogue is e
    {
        
    }
}
