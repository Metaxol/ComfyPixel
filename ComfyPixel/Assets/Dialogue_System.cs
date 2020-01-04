
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Dialogue_System : MonoBehaviour {

    private Utility utility;

    public void run_dialogue(List<string> lines, Text TextBox)
    {
        //just uses utility-class function to start dialogue
        utility.run_text(lines, TextBox, 0.1f);
    }

    private void let_options_choose(string[] button_name, int[] line_go_to)
    {
        GameObject.Find(button_name[0]).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        GameObject.Find(button_name[1]).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

        utility.options_choosing(1, "ver");

        switch (utility.to_change)
        {
            case 0:
                GameObject.Find(button_name[0]).GetComponent<Image>().color = new Color(100f, 1f, 1f, 1f);
                GameObject.Find(button_name[1]).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    utility.current_line = line_go_to[0]-1;
                }
                break;
            case 1:
                GameObject.Find(button_name[1]).GetComponent<Image>().color = new Color(100f, 1f, 1f, 1f);
                GameObject.Find(button_name[0]).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    utility.current_line = line_go_to[1]-1;
                    GameObject.Find(button_name[0]).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                    GameObject.Find(button_name[1]).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                }
                break;

        /* make button choosing and events happening method in utility, because it has multiple use cases
        */
        }
        
    }

    private void Update()
    {
        //let_options_choose(new string[] { "choosing_button0", "choosing_button1"});
    }

    private void Awake()
    {
        utility = FindObjectOfType<Utility>();
    }
}

