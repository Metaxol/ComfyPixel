
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
    public bool oneTime = true;

    private PlayerController playerController;
    private Utility utility;

    private List<Image> buttons = new List<Image>();
    private List<Text> buttons_texts = new List<Text>();

    private void NPC_dialogue_choosing(string[] button_texts, int[] new_dialogue_attr_1, int[] new_dialogue_attr_2)
    {
        if (oneTime)
        {
            //instantiating buttons, always 2 since this is part of a NPC's dialogue (would get too complicated storywise)
            buttons = (List<Image>) utility.create_ui_object(new GameObject().AddComponent<Image>(), typeof(Image), 2, new string[] { "option1", "option2" }, new Vector2[] { new Vector2(100f, 100f), new Vector2(100f, 100f) }, 
                                                            new Vector3[] {new Vector3(dialogue_box.rectTransform.anchoredPosition.x - 100f, dialogue_box.rectTransform.anchoredPosition.y),
                                                            new Vector3(dialogue_box.rectTransform.anchoredPosition.x + 100f, dialogue_box.rectTransform.anchoredPosition.y)},
                                                            new Vector3[] {Vector3.zero, Vector3.zero});

            buttons_texts = (List<Text>)utility.create_ui_object(new GameObject().AddComponent<Text>(),typeof(Text), 2, new string[] { "option1_text", "option2_text" }, new Vector2[] { new Vector2(100f, 100f), new Vector2(100f, 100f) },
                                                                 new Vector3[] {new Vector3(dialogue_box.rectTransform.anchoredPosition.x - 100f, dialogue_box.rectTransform.anchoredPosition.y),
                                                                 new Vector3(dialogue_box.rectTransform.anchoredPosition.x + 100f, dialogue_box.rectTransform.anchoredPosition.y)},
                                                                 new Vector3[] { Vector3.zero, Vector3.zero });
            
            //testing purposes
            buttons[0].GetComponent<Image>().color = new Color(99f, 15, 15f, 255f);
            buttons[1].GetComponent<Image>().color = new Color(99f, 15, 15f, 255f);

            buttons_texts[0].GetComponent<RectTransform>().SetParent(GameObject.Find(buttons[0].name).transform);
            buttons_texts[1].GetComponent<RectTransform>().SetParent(GameObject.Find(buttons[1].name).transform);
            buttons_texts[0].font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            buttons_texts[1].font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            buttons_texts[0].text = button_texts[0];
            buttons_texts[1].text = button_texts[1];

            //spawn buttons once and stop dialogue from scrolling
            oneTime = false;
            print("asd");
            tag = "NPC_nottalk";
        }
        else if (!oneTime)
        {
            //depending on options chosen:
            if (Input.GetKeyDown(KeyCode.E) && (buttons[0].sprite == chosen_sprite || buttons[1].sprite == chosen_sprite))
            {
                //delete buttons and continue dialogue on line according to button chosen (also reset certain dialogue_system-attributes)                
                utility.letter = 0;
                utility.to_change = -1;
                tag = "NPC_talkable";
                oneTime = true;

                if (buttons[0].sprite == chosen_sprite)
                {
                    stop_scroll_line = new_dialogue_attr_1[0];
                    utility.current_line = new_dialogue_attr_1[1];
                }
                else if(buttons[1].sprite == chosen_sprite)
                {
                    stop_scroll_line = new_dialogue_attr_2[0];
                    utility.current_line = new_dialogue_attr_2[1];
                }

                //buttons = utility.delete_list_objects(buttons);
                //buttons_texts = utility.delete_list_objects(buttons_texts);

                foreach (GameObject i in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
                {
                    if (i.name == "New Game Object" || i.name == buttons[0].name || i.name == buttons[1].name || i.name == buttons_texts[0].name || i.name == buttons_texts[1].name)
                    {
                        Destroy(i);
                    }
                }
            }
        }
        
        //give ability to choose between buttons, always vertically because this is part of the dialogue_system
        utility.choose_buttons(buttons.ToArray(), chosen_sprite, not_chosen_sprite, 1, "ver");
    }

    private void Update()
    {
        //keep adding changing sprites/other special events in this switch statement for the npc's that need it
        if(playerController.NPC != null)
        {
            dialogue_box = GameObject.Find("dialogue_box").GetComponent<Image>();
            
            if (playerController.NPC.name == name)
            {
                if (Sprites.Length != 0)
                {
                    sprite_box = GameObject.Find("sprite_box").GetComponent<Image>();
                }

                switch (playerController.NPC.name)
                {
                    //just for testing purposes
                    case "NPC":
                        switch (utility.current_line)
                        {
                            case 0:
                                sprite_box.sprite = Sprites[0];
                                break;
                            case 1:
                                sprite_box.sprite = Sprites[1];
                                break;
                            case 3:
                                NPC_dialogue_choosing(new string[] { "Erste Option", "Zweite Option" },
                                                      new int[] { 9, 4 }, new int[] { 18, 16 });
                                break;
                            case 8:
                                NPC_dialogue_choosing(new string[] { "Erste Option", "Zweite Option" },
                                                      new int[] { 12, 9 }, new int[] { 16, 12 });
                                break;
                        }
                        break;
                }
            }
        }
    }

    private void Awake()
    {
        utility = FindObjectOfType<Utility>();
        playerController = FindObjectOfType<PlayerController>();
    }
}
