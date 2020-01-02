
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class Utility : MonoBehaviour {

    public bool can_scroll = true;
    public int letter = 0;
    public int current_line = 0;

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
        print("test");
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

    public void run_text(List<string> lines, Text text_box, float scroll_speed, string box_name) ///default input is e
    {
        if (can_scroll)
        {
            //turns alpha component of box on
            GameObject.Find(box_name).GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
           
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
        //utility.current_line == utility.split_text(collision.gameObject.GetComponent<NPC_Attributes>().Dialogue.text).Count
        if(current_line == lines.Count)
        {
            GameObject.Find(box_name).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
            //name of gameobject is box where text is, not textbox itself

            can_scroll = true;
            letter = 0;
            current_line = 0;
        }
    }

    //to revert changes made by the dialogue_system
    public void reset_dialogue()
    {
        GameObject.Find("dialogue_box").GetComponent<Image>().color = new Color(255f, 255f, 255f, 0f);
        //name of gameobject is box where text is, not textbox itself

        can_scroll = true;
        letter = 0;
        current_line = 0;
    }
    
    public void options_choosing(int stop, string hor_ver) //default value for start is 0
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
