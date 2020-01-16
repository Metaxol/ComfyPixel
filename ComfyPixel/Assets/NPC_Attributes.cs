
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NPC_Attributes : MonoBehaviour {

    //NPC attributes, more will be added 
    public TextAsset Dialogue;
    public Sprite[] Sprites;

    //variabled for NPC's to handle dialogue
    private Image dialogue_box;
    private Image sprite_box;
    public Image choose_button;
    public Sprite chosen_sprite;
    public Sprite not_chosen_sprite;
    public int stop_scroll_line;

    private PlayerController playerController;
    private Utility utility;
    private Dialogue_System dialogue_System;

    public bool oneTime = true;
    private List<GameObject> buttons = new List<GameObject>();

    private void Update()
    {
        //keep adding changing sprites/other special events in this switch statement for the npc's that need it
        if(playerController.NPC_name == name)
        {
            switch (playerController.NPC_name)
            {
                case "NPC":
                    switch (utility.current_line)
                    {
                        //just for testing purposes
                        case 0:
                            sprite_box.sprite = Sprites[0];
                            break;
                        case 1:
                            sprite_box.sprite = Sprites[1];
                            break;
                        case 3:
                            if (oneTime)
                            {
                                buttons = utility.spawn_Buttons(choose_button, 1,
                                                                new Vector3[] { new Vector3(dialogue_box.rectTransform.anchoredPosition.x - 100f, dialogue_box.rectTransform.anchoredPosition.y),
                                                                new Vector3(dialogue_box.rectTransform.anchoredPosition.x + 100f, dialogue_box.rectTransform.anchoredPosition.y) },
                                                                new Quaternion[] { new Quaternion(), new Quaternion() },
                                                                new string[] { "option1", "option2"});
                                oneTime = false;
                            }
                            else if (!oneTime)
                            {
                                if (Input.GetKeyDown(KeyCode.E) && buttons[0].GetComponent<Image>().sprite == chosen_sprite)
                                {
                                    foreach (GameObject i in buttons.ToArray())
                                    {
                                        print("test");
                                        GameObject reference = buttons[System.Array.IndexOf(buttons.ToArray(), i)];
                                        buttons.Remove(reference);
                                        Destroy(reference);
                                    }
                                    stop_scroll_line = 5;
                                    utility.current_line = 4;
                                    //dialogue_System.run_dialogue(utility.split_text(Dialogue.text).RemoveRange(0, utility.current_line),
                                    //                            GameObject.Find("Text").GetComponent<Text>());
                                }
                                else if (Input.GetKeyDown(KeyCode.E) && buttons[1].GetComponent<Image>().sprite == chosen_sprite)
                                {
                                    foreach (GameObject i in buttons.ToArray())
                                    {
                                        GameObject reference = buttons[System.Array.IndexOf(buttons.ToArray(), i)];
                                        buttons.Remove(reference);
                                        Destroy(reference);
                                    }
                                    stop_scroll_line = 6;
                                    utility.current_line = 5;
                                }
                            }
                            utility.choose_buttons(buttons.ToArray(), chosen_sprite, not_chosen_sprite, 1, "ver");
                            break;
                    }
                    break;
            }
        }
    }

    private void Awake()
    {
        dialogue_box = GameObject.Find("dialogue_box").GetComponent<Image>();
        sprite_box = GameObject.Find("sprite_box").GetComponent<Image>();

        utility = FindObjectOfType<Utility>();
        playerController = FindObjectOfType<PlayerController>();
    }
}
