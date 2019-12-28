
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class Utility : MonoBehaviour {

    private bool can_scroll = true;
    private int letter = 0;
    private int current_line = 0;

    public int to_change = 0;

    public List<string> split_text(string text)
    {
        //take some form of string as input
        //divides into individual lines, always divided at \n

        List<string> lines = new List<string>();

        foreach (string line in text.Split('\n'))
        {
            if (line != "")
            {
                lines.Add(line);
            }
            else //make sure not to have any empty strings
            {
                continue;
            }
        }

        return lines; //returns list<> of strings
    }

    private IEnumerator scroll_text(float writing_delay, Text text_box, List<string> lines)
    {
        yield return new WaitForSeconds(writing_delay);
        //after a certain amount of delay, write a letter
        try
        {
            text_box.text += lines[current_line][letter];
            //if an error is produced, give ability to go to next line

            can_scroll = true;
            letter += 1;
        }
        catch
        {
            //signal to to give ability to go to next line
            letter = -1;
        }
    }

    public void run_text(List<string> lines, Text text_box, float scroll_speed) ///default input is e
    {
        if (can_scroll)
        {
            StartCoroutine(scroll_text(scroll_speed, text_box, lines));
            can_scroll = false;
            //introduce bool to prevent repeated execution of coroutine
        }

        if (letter >= 1)
        {
            //ability to complete current line
            if (Input.GetKeyDown(KeyCode.E))
            {
                text_box.text = lines[current_line];

                letter = lines[current_line].Length;
                //intentionally producing error to stop further scrolling of current line
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && letter == -1) //said signal is being received here
        {
            //all values that need to be reset when the next line is going to be shown
            text_box.text = "";
            current_line += 1;
            letter = 0;
            can_scroll = true;
        }
    }

    public void options_choosing(int stop, string hor_ver) ///default value for start is 0
    {
        //change value with the right/left arrows
        if(hor_ver == "ver")
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                to_change += 1;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                to_change -= 1;
            }
        }

        //change value with up/down arrows
        else if(hor_ver == "hor")
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                to_change += 1;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                to_change -= 1;
            }
        }

        //if moving method isnt hor or ver, throw an error
        else
        {
            throw new System.Exception("No valid moving method chosen!");
        }

        //restrict movement of value to certain range
        if(to_change > stop)
        {
            to_change = stop;
        }else if(to_change < 0)
        {
            to_change = 0;
        }
    }

    public void set_zero()
    {
        //to reset choosing to beginning
        to_change = 0; 
    }
}
