
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

    public GameObject referenc_name;

    //will be replayed by other ui_object-spawning method, so you can eventually remove it
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

    public object create_ui_object<T>(T actual_type, System.Type type_of_object, int amount_objects, string[] button_name, Vector2[] scale, Vector3[] position, Vector3[] rotation)
    {
        //assign two variables, for comfortability (dont always have to use arrays to pass on values)
        List<GameObject> objects = new List<GameObject>();
        GameObject single_object = null;
        
        for (int c = 0; c < amount_objects; c++)
        {
            //assignment if array of objects wanted
            if (amount_objects > 1)
            {
                objects.Add(new GameObject()); //instantiates object as GameObject
                objects[c].transform.SetParent(GameObject.Find("Canvas").transform, false); //set ui object as child to canvas to see it
                objects[c].name = button_name[c]; //assigns name
                objects[c].gameObject.AddComponent(type_of_object); //assigns any type of component (image, text, etc...)
                objects[c].GetComponent<RectTransform>().sizeDelta = scale[c]; //sets scale
                objects[c].GetComponent<RectTransform>().anchoredPosition = position[c]; //sets pos
                objects[c].GetComponent<RectTransform>().Rotate(rotation[c]); //sets rot
            }
            //assignment if single object wanted
            else
            {
                single_object = new GameObject();
                single_object.transform.SetParent(GameObject.Find("Canvas").transform, false);
                single_object.name = button_name[c];
                single_object.gameObject.AddComponent(type_of_object);
                single_object.GetComponent<RectTransform>().sizeDelta = scale[c];
                single_object.GetComponent<RectTransform>().anchoredPosition = position[c];
                single_object.GetComponent<RectTransform>().Rotate(rotation[c]);
            }
        }

        List<T> actual_list = new List<T>();

        if(amount_objects > 1)
        {
            for (int c = 0; c < amount_objects; c++)
            {
                actual_list.Add(objects[c].GetComponent<T>());
            }
        }

        delete_with_names(new string[] { "New Game Object" });

        //return based on amount of objects wanted
        if (amount_objects == 1)
        {
            return single_object.GetComponent<T>();
        }
        else
        {
            return actual_list;
        }
    }

    public void delete_with_names(string[] names_delete)
    {
        foreach (GameObject i in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            foreach(string o in names_delete)
            {
                if(i.name == o)
                {
                    Destroy(i);
                }
            }
        }
    }

    public void choose_buttons(Image[] button, Sprite button_chosen, Sprite button_not_chosen, int stop, string ver_hor)
    {
        options_choosing(stop, ver_hor); //can choose between buttons
        foreach(Image i in button)
        {
            if (to_change == System.Array.IndexOf(button, i)) //working with index specification
            {
                button[System.Array.IndexOf(button, i)].sprite = button_chosen; //chosen sprite, if number evaluates
            }
            else
            {
                button[System.Array.IndexOf(button, i)].sprite = button_not_chosen; //default sprite, if otherwise 
            }
        }
    }

    public List<T> delete_list_objects<T>(List<T> list_to_delete) where T: MonoBehaviour
    {
        //cant use GameObject for T, dont have to tho so always use actual type (good practice too, cant overuse GameObject type for everything)
        foreach(T i in list_to_delete.ToArray())
        {
            //create reference to element in list
            T reference = list_to_delete[System.Array.IndexOf(list_to_delete.ToArray(), i)];

            //remove reference from list
            list_to_delete.Remove(reference);

            //finally destroy reference in scene
            Destroy(reference.gameObject);
        }

        //always return empty list (empties original list)
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
            //not directly able to choose option, have to press key to choose direction of option-choosing
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
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
            //not directly able to choose option, have to press key to choose direction of option-choosing
            if (Input.GetKeyDown(KeyCode.UpArrow))
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
