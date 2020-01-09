
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

    private PlayerController playerController;
    private Utility utility;
    private Dialogue_System Dialogue_System;

    public bool oneTime = true;

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
                            List<GameObject> buttons = new List<GameObject>();
                            if (oneTime)
                            {
                                buttons = utility.spawn_Buttons(choose_button, 1,
                                                                new Vector3[] { new Vector3(dialogue_box.rectTransform.anchoredPosition.x + 100f, dialogue_box.rectTransform.anchoredPosition.y),
                                                                new Vector3(dialogue_box.rectTransform.anchoredPosition.x - 100f, dialogue_box.rectTransform.anchoredPosition.y) },
                                                                new Quaternion[] { new Quaternion(), new Quaternion() },
                                                                new string[] { "option1", "option2"});
                                oneTime = false;
                            }
                            print("test");
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
