
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Settings_System : MonoBehaviour {

    private Image settings_holder = null;
    private List<Image> settings_options;
    private List<Text> settings_texts;
    private Image quitting_choices = null;
    private List<Image> quit_buttons = null;
    private Image game_settings_choices = null;

    private Utility utility;

    private void spawn_settings_menu()
    {
        Image game_coverup = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) },
                                                               1, new string[] { "cover_game" }, new Vector2[] { new Vector2(1920f, 1080f) }, new Vector3[] { new Vector3(0f, 0f) }, new Vector3[] { Vector3.zero });
        game_coverup.color = new Color(0f, 0f, 0f, 110f / 255f);

        settings_holder = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 1, new string[] { "inventory_holder" }, new Vector2[] { new Vector2(1046.8f, 1239f) },
                                                     new Vector3[] { new Vector3(13f, -2.2888e-05f) }, new Vector3[] { Vector3.zero });

        Image settings_sprite = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 1, new string[] { "settings_sprite" }, new Vector2[] { new Vector2(550f, 550f) },
                                                             new Vector3[] { new Vector3(13f, 129.1f) }, new Vector3[] { Vector3.zero });
        settings_sprite.sprite = Resources.Load<Sprite>("Settings_System_Graphics/GameSettings_Symbol");
        settings_sprite.rectTransform.SetParent(settings_holder.rectTransform);

        Image quit_sprite = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 1, new string[] { "quit_sprite" }, new Vector2[] { new Vector2(650f, 650f) },
                                                            new Vector3[] { new Vector3(0f, -90f) }, new Vector3[] { Vector3.zero });
        quit_sprite.sprite = Resources.Load<Sprite>("Settings_System_Graphics/Quit_Symbol");
        quit_sprite.rectTransform.SetParent(settings_holder.rectTransform);

        settings_options = (List<Image>)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 2, new string[] { "game_settings", "quit" }, 
                                                                 new Vector2[] { new Vector2(600f, 600f), new Vector2(600f, 600f) },
                                                                 new Vector3[] { new Vector3(-3f, 35f),  new Vector3(-3f, -205f) }, new Vector3[] { Vector3.zero, Vector3.zero });
        foreach(Image i in settings_options)
        {
            i.sprite = Resources.Load<Sprite>("Settings_System_Graphics/Not_Chosen_Settings");
            i.rectTransform.SetParent(settings_holder.rectTransform);
        }

        settings_texts = (List<Text>)utility.create_ui_object(new GameObject().AddComponent<Text>(), new System.Type[] { typeof(Text) }, 2, new string[] { "settings_sprite_text", "quit_sprite_text" }, 
                                                              new Vector2[] { new Vector2(301.4f, 92.6f), new Vector2(301.4f, 92.6f) },
                                                              new Vector3[] { new Vector3(-14.2f, 23f), new Vector3(-14.2f, -216f) }, new Vector3[] { Vector3.zero, Vector3.zero });

        settings_texts[0].text = "Settings";
        settings_texts[1].text = "Quit";

        foreach (Text i in settings_texts)
        {
            i.rectTransform.SetParent(settings_options[settings_texts.IndexOf(i)].rectTransform);
            i.fontSize = 50;
            i.font = Resources.Load<Font>("Dialogue_System_Graphics/dpcomic");
            i.color = new Color(0f, 0f, 0f, 255f);
            i.alignment = TextAnchor.MiddleCenter;
        }

        settings_holder.sprite = Resources.Load<Sprite>("Settings_System_Graphics/Settings_Holder");
        Time.timeScale = 0;
    }

    public void player_handling()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && enabled == false)
        {
            FindObjectOfType<Inventory_System>().Inventory_System_bool = false;
            FindObjectOfType<NPC_Dialogue_System>().NPC_Dialogue_System_bool = false;
            enabled = true;
            utility.multiple_to_change.Add(utility.to_change);
            utility.to_change = -1;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && enabled == true)
        {
            Time.timeScale = 1;
            FindObjectOfType<Inventory_System>().Inventory_System_bool = true;
            FindObjectOfType<NPC_Dialogue_System>().NPC_Dialogue_System_bool = true;
            utility.delete_with_names(new string[] { settings_holder.name, "cover_game" });
            settings_holder = null;
            enabled = false;
            utility.to_change = utility.multiple_to_change[0];
            utility.multiple_to_change.Remove(utility.multiple_to_change[0]);
        }
    }

    private void create_quitting_menu()
    {
        quitting_choices = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 1, new string[] { "quitting_choices" }, new Vector2[] { new Vector2(1200f, 1200f) },
                                                   new Vector3[] { new Vector3(-529f, -2.2888e-05f) }, new Vector3[] { Vector3.zero });
        quitting_choices.sprite = Resources.Load<Sprite>("Settings_System_Graphics/Not_Chosen_Settings");
        quitting_choices.rectTransform.SetParent(settings_holder.rectTransform);

        quit_buttons = (List<Image>)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 2, new string[] { "quit_yes", "quit_no"},
                                                                         new Vector2[] { new Vector2(500f, 500f), new Vector2(630f, 650f)},
                                                                         new Vector3[] { new Vector3(-654f, -58f), new Vector3(-429.9f, -42.3f) }, new Vector3[] { Vector3.zero, Vector3.zero});

        foreach (Image i in quit_buttons)
        {
            i.rectTransform.SetParent(quitting_choices.rectTransform);
            if(quit_buttons.IndexOf(i) == 0)
            {
                i.sprite = Resources.Load<Sprite>("Settings_System_Graphics/Agree_Not_Chosen");
            }
            else if(quit_buttons.IndexOf(i) == 1)
            {
                i.sprite = Resources.Load<Sprite>("Settings_System_Graphics/Close_Not_Chosen");
            }  
        }

        Text quitting_text = (Text)utility.create_ui_object(new GameObject().AddComponent<Text>(), new System.Type[] { typeof(Text) }, 1, new string[] { "quitting_text" },
                                                            new Vector2[] { new Vector2(444.23f, 48.9f) },
                                                            new Vector3[] { new Vector3(-547.06f, 24.44999f) }, new Vector3[] { Vector3.zero });
        quitting_text.font = Resources.Load<Font>("Dialogue_System_Graphics/dpcomic");
        quitting_text.fontSize = 40;
        quitting_text.color = new Color(0, 0, 0, 255f);
        quitting_text.alignment = TextAnchor.MiddleCenter;
        quitting_text.text = "Do you want to quit?";
        quitting_text.rectTransform.SetParent(quitting_choices.rectTransform);

        utility.to_change = -1;
    }

    private void create_gamesettings_menu()
    {
        game_settings_choices = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 1, new string[] { "quitting_choices" }, new Vector2[] { new Vector2(1200f, 1200f) },
                                                                new Vector3[] { new Vector3(551f, -2.2888e-05f) }, new Vector3[] { Vector3.zero });
        game_settings_choices.sprite = Resources.Load<Sprite>("Settings_System_Graphics/Settings_Holder");
    }

    private void use_settings_menu()
    {
        if (settings_holder != null)
        {
            if (quitting_choices == null)
            {
                utility.choose_buttons(settings_options.ToArray(), new Sprite[] { Resources.Load<Sprite>("Settings_System_Graphics/Chosen_Settings"), Resources.Load<Sprite>("Settings_System_Graphics/Chosen_Settings") },
                    new Sprite[] { Resources.Load<Sprite>("Settings_System_Graphics/Not_Chosen_Settings"), Resources.Load<Sprite>("Settings_System_Graphics/Not_Chosen_Settings") }, 1, "hor");

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    settings_texts[0].rectTransform.localPosition = new Vector3(-11.2f, -20);
                    settings_texts[1].rectTransform.localPosition = new Vector3(-11.2f, -11f);
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    settings_texts[1].rectTransform.localPosition = new Vector3(-11.2f, -21f);
                    settings_texts[0].rectTransform.localPosition = new Vector3(-11.2f, -12f);
                }
            }
            else if (quitting_choices != null)
            {
                utility.choose_buttons(quit_buttons.ToArray(), new Sprite[] { Resources.Load<Sprite>("Settings_System_Graphics/Agree_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/Close_Chosen") },
                                                               new Sprite[] { Resources.Load<Sprite>("Settings_System_Graphics/Agree_Not_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/Close_Not_Chosen") }, 
                                                                              1, "ver");
                if (quit_buttons[0].sprite == Resources.Load<Sprite>("Settings_System_Graphics/Agree_Chosen"))
                {
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        Application.Quit();
                        print("Application should quit now.");
                    }
                }
                else if(quit_buttons[1].sprite == Resources.Load<Sprite>("Settings_System_Graphics/Close_Chosen"))
                {
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        Destroy(GameObject.Find(quitting_choices.name));
                    }
                }
            }

            if(settings_texts[0].rectTransform.localPosition == new Vector3(-11.2f, -20) && Input.GetKeyDown(KeyCode.Return))
            {
                create_gamesettings_menu();
            }
            else if(settings_texts[1].rectTransform.localPosition == new Vector3(-11.2f, -21f) && Input.GetKeyDown(KeyCode.Return) && quitting_choices == null)
            {
                create_quitting_menu();
            }
        }
    }


    private void Awake()
    {
        utility = FindObjectOfType<Utility>();
    }

    private void Update()
    {
        use_settings_menu();
    }

    private void OnEnable()
    {
        spawn_settings_menu();
    }
}
