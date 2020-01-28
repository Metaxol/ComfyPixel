
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class Utility : MonoBehaviour {

    //scrolling-text variables
    public bool can_scroll = true;
    public int letter = -1;
    public int current_line = 0;

    public int to_change = -1; //choosing variable 

    //this method will be replaced by create_ui_object()
    public List<GameObject> spawn_Buttons(Image button, int amount_of_buttons, Vector3[] button_positions, Quaternion[] button_rotations, string[] button_texts)
    {
        List<GameObject> lines = new List<GameObject>(); //creates list of buttons, so it can be used

        for (int c = 0; c <= amount_of_buttons; c++)
        {
            //buttons added to list with specified position aswell as rotation
            var button_instance = Instantiate(button.gameObject, button_positions[c], button_rotations[c]) as GameObject;
            button_instance.transform.SetParent(GameObject.Find("Canvas").transform, false);
            lines.Add(button_instance);

            //every button has a text_box, specified here
            button_instance.transform.GetChild(0).GetComponent<Text>().text = button_texts[c];
        }

        return lines; //returns list of buttons (gameobjects) for further use by other methods
    }
    //this method will be replaced by create_ui_object()

    //create any type of object
    public object create_object(System.Type type_of_object, int amount_objects, string[] button_name, Vector2[] scale, Vector3[] position, Quaternion[] rotation)
    {
        //assign two variables, for comfortability (dont always have to use arrays to pass on values)
        GameObject[] objects = new GameObject[amount_objects];
        GameObject single_object = null;

        for (int c = 0; c < amount_objects; c++)
        {
            //assignment if array of objects wanted
            if (amount_objects > 1)
            {
                objects[c] = new GameObject(); //instantiates object as GameObject
                objects[c].name = button_name[c]; //assigns name
                objects[c].gameObject.AddComponent(type_of_object); //assigns any type of component (image, text, etc...)
                objects[c].transform.localScale = scale[c]; //sets scale
                objects[c].transform.position = position[c]; //sets pos
                objects[c].transform.rotation = rotation[c]; //sets rot
            }
            //assignment if single object wanted
            else
            {
                single_object = new GameObject();
                single_object.name = button_name[c];
                single_object.gameObject.AddComponent(type_of_object);
                single_object.transform.localScale = scale[c];
                single_object.transform.position = position[c];
                single_object.transform.rotation = rotation[c];
            }
        }

        //return based on amount of objects wanted
        if (amount_objects == 1)
        {
            return single_object;
        }
        else
        {
            return objects;
        }
    }

    public void choose_buttons(GameObject[] button, Sprite button_chosen, Sprite button_not_chosen, int stop, string ver_hor)
    {
        options_choosing(stop, ver_hor); //can choose between buttons
        foreach(GameObject i in button)
        {
            if (to_change == System.Array.IndexOf(button, i)) //working with index specification
            {
                button[System.Array.IndexOf(button, i)].GetComponent<Image>().sprite = button_chosen; //chosen sprite, if number evaluates
            }
            else
            {
                button[System.Array.IndexOf(button, i)].GetComponent<Image>().sprite = button_not_chosen; //default sprite, if otherwise 
            }
        }
    }

    //delete a list full of active objects (preferable used for buttons)
    public List<GameObject> delete_list_objects(List<GameObject> list_to_delete)
    {
        foreach(GameObject i in list_to_delete.ToArray())
        {
            //create reference to any object in array
            GameObject reference = list_to_delete[System.Array.IndexOf(list_to_delete.ToArray(), i)];

            //remove the object by referencing it with refernce in List<>
            list_to_delete.Remove(reference);

            //destroy object through reference
            Destroy(reference);
        }

        //always returns empty List<> to empty the original List<>
        return list_to_delete;
    }

    public List<string> split_text(string text)
    {
        //take some form of string as input
        //divides into individual lines, always divided at \n

        List<string> lines = new List<string>();

        foreach (string line in text.Split('\n'))
        {
            lines.Add(line);
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
            //signal to to give ability to go to next lin
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
    
    public void options_choosing(int stop, string hor_ver) //default value for start is 0
    {
        //change value with the right/left arrows
        if(hor_ver == "ver")
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //to not have any option chosen at beginning, to_change is set to -1
                if(to_change == -1)
                {
                    to_change = stop;
                }
                else
                {
                    to_change += 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (to_change == -1)
                {
                    to_change = 0;
                }
                else if(to_change != 0)
                {
                    to_change -= 1;
                }
            }
            
        }

        //change value with up/down arrows
        else if(hor_ver == "hor")
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //to not have any option chosen at beginning, to_change is set to -1
                if (to_change == -1)
                {
                    to_change = 0;
                }
                else if(to_change != 0)
                {
                    to_change -= 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (to_change == -1)
                {
                    to_change = stop;
                }
                else
                {
                    to_change += 1;
                }
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
        }else if(to_change < 0 && to_change != -1)
        {
            to_change = 0;
        }
    }
}
