
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Settings_System : MonoBehaviour {

    //basic settings choosing between quitting-game and game_settings_options
    private Image settings_holder = null;
    private List<Image> settings_options;
    private List<Text> settings_texts;

    //variables made for quitting game
    private Image quitting_choices = null;
    private List<Image> quit_buttons = null;

    //variables for settings game_settings_options
    private Image game_settings_choices = null;
    private List<Image> game_settings_buttons = null;
    private List<Image> sounds_graphics = null;
    private int[] s_g_settings = new int[] { 3, 2};
    private List<Text> s_g_texts = null;

    private Utility utility;

    private void spawn_settings_menu()
    {
        //black background when settings are activated
        Image game_coverup = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) },
                                                               1, new string[] { "cover_game" }, new Vector2[] { new Vector2(1920f, 1080f) }, new Vector3[] { new Vector3(0f, 0f) }, new Vector3[] { Vector3.zero });
        game_coverup.color = new Color(0f, 0f, 0f, 110f / 255f);

        //contains options to choose between quitting-game and game_settings_options
        settings_holder = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 1, new string[] { "settings_holder" }, new Vector2[] { new Vector2(1046.8f, 1239f) },
                                                     new Vector3[] { new Vector3(13f, -2.2888e-05f) }, new Vector3[] { Vector3.zero });

        //setting quit-sprite-choice
        Image settings_sprite = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 1, new string[] { "settings_sprite" }, new Vector2[] { new Vector2(550f, 550f) },
                                                             new Vector3[] { new Vector3(13f, 129.1f) }, new Vector3[] { Vector3.zero });
        settings_sprite.sprite = Resources.Load<Sprite>("Settings_System_Graphics/GameSettings_Symbol");
        settings_sprite.rectTransform.SetParent(settings_holder.rectTransform);

        //settings game_settings-sprite-choice
        Image quit_sprite = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 1, new string[] { "quit_sprite" }, new Vector2[] { new Vector2(650f, 650f) },
                                                            new Vector3[] { new Vector3(0f, -90f) }, new Vector3[] { Vector3.zero });
        quit_sprite.sprite = Resources.Load<Sprite>("Settings_System_Graphics/Quit_Symbol");
        quit_sprite.rectTransform.SetParent(settings_holder.rectTransform);

        //creating buttons to choose between quitting-game and game_settings_options
        settings_options = (List<Image>)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 2, new string[] { "game_settings", "quit" }, 
                                                                 new Vector2[] { new Vector2(600f, 600f), new Vector2(600f, 600f) },
                                                                 new Vector3[] { new Vector3(-3f, 35f),  new Vector3(-3f, -205f) }, new Vector3[] { Vector3.zero, Vector3.zero });
        foreach(Image i in settings_options)
        {
            i.sprite = Resources.Load<Sprite>("Settings_System_Graphics/Not_Chosen_Settings");
            i.rectTransform.SetParent(settings_holder.rectTransform);
        }

        //buttons-texts to choose between quitting-game and game_settings_options
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

        //pausing time
        Time.timeScale = 0;
    }

    public void player_handling()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && enabled == false)
        {
            FindObjectOfType<Inventory_System>().Inventory_System_bool = false;
            FindObjectOfType<NPC_Dialogue_System>().NPC_Dialogue_System_bool = false;
            enabled = true; //disabling this script
            utility.multiple_to_change.Add(utility.to_change); //to allow multiple multiple-choices
            utility.to_change = -1;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && enabled == true)
        {
            Time.timeScale = 1; //resuming time
            FindObjectOfType<Inventory_System>().Inventory_System_bool = true;
            FindObjectOfType<NPC_Dialogue_System>().NPC_Dialogue_System_bool = true;

            //deleting settings-holder which is containing settings_options
            utility.delete_with_names(new string[] { settings_holder.name, "cover_game" });
            settings_holder = null;
            enabled = false; //disabling this script
            utility.to_change = utility.multiple_to_change[0]; //to allow multiple multiple-choices
            utility.multiple_to_change.Remove(utility.multiple_to_change[0]); //remove multiple multiple-choices
        }
    }

    private void create_quitting_menu()
    {
        //creating quitting_choices (X and V)
        quitting_choices = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 1, new string[] { "quitting_choices" }, new Vector2[] { new Vector2(1200f, 1200f) },
                                                   new Vector3[] { new Vector3(-529f, -2.2888e-05f) }, new Vector3[] { Vector3.zero });
        quitting_choices.sprite = Resources.Load<Sprite>("Settings_System_Graphics/Not_Chosen_Settings");
        quitting_choices.rectTransform.SetParent(settings_holder.rectTransform);

        quit_buttons = (List<Image>)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 2, new string[] { "quit_yes", "quit_no"},
                                                                         new Vector2[] { new Vector2(500f, 500f), new Vector2(630f, 650f)},
                                                                         new Vector3[] { new Vector3(-654f, -58f), new Vector3(-429.9f, -42.3f) }, new Vector3[] { Vector3.zero, Vector3.zero});

        //settings quitting_choices sprites, respectively
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

        //creating text to make quitting game more clear
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
        utility.to_change = -1; //so you can choose properly

        //creating gamesettings_holder
        game_settings_choices = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 1, new string[] { "gsettings_choices" }, new Vector2[] { new Vector2(1200f, 1200f) },
                                                                new Vector3[] { new Vector3(551f, -2.2888e-05f) }, new Vector3[] { Vector3.zero });
        game_settings_choices.sprite = Resources.Load<Sprite>("Settings_System_Graphics/Settings_Holder");
        game_settings_choices.rectTransform.SetParent(settings_holder.rectTransform);
        
        //creating buttons for sound and graphics _options
        game_settings_buttons = (List<Image>)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 3, new string[] { "sound_gsettings", "graphics_gsettings", "close_gsettings" }, 
                                                                      new Vector2[] { new Vector2(500f, 500f), new Vector2(500f, 500f), new Vector2(600f, 600f) },
                                                                      new Vector3[] { new Vector3(527f, 121.3f), new Vector3(527f, -133f), new Vector3(708f, -271f) }, new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero });
        foreach(Image i in game_settings_buttons)
        {
            i.sprite = Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen");
            i.rectTransform.SetParent(game_settings_choices.rectTransform);

            if(game_settings_buttons.IndexOf(i) == 2)
            {
                i.sprite = Resources.Load<Sprite>("Settings_System_Graphics/Close_Not_Chosen");
            }
        }

        //creating sprites for sound and graphics
        sounds_graphics = (List<Image>)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 2, new string[] { "sounds_g", "graphics_g" },
                                                                new Vector2[] { new Vector2(400f, 400f), new Vector2(250f, 250f)},
                                                                new Vector3[] { new Vector3(513.2f, 97.9f), new Vector3(509.3f, -156f) }, new Vector3[] { Vector3.zero, Vector3.zero});
        foreach(Image i in sounds_graphics)
        {
            if(sounds_graphics.IndexOf(i) == 0)
            {
                i.sprite = Resources.Load<Sprite>("Settings_System_Graphics/SSettings_" + s_g_settings[0].ToString());
                i.rectTransform.SetParent(game_settings_buttons[0].rectTransform);
            }
            else if(sounds_graphics.IndexOf(i) == 1)
            {
                i.sprite = Resources.Load<Sprite>("Settings_System_Graphics/GSettings_" + s_g_settings[1].ToString());
                i.rectTransform.SetParent(game_settings_buttons[1].rectTransform);
            }
        }

        //creating texts to make choosing options for sound and graphics more clear, especially graphics
        s_g_texts = (List<Text>)utility.create_ui_object(new GameObject().AddComponent<Text>(), new System.Type[] { typeof(Text) }, 2, new string[] { "sound_sprite_text", "graphics_sprite_text" },
                                                         new Vector2[] { new Vector2(205.11f, 28.23f), new Vector2(205.11f, 28.23f) },
                                                         new Vector3[] { new Vector3(524.74f, 201.9001f), new Vector3(524.74f, -52f) }, new Vector3[] { Vector3.zero, Vector3.zero });
        foreach(Text i in s_g_texts)
        {
            //to remember chosen settings (so its not reset to highest)
            if(s_g_texts.IndexOf(i) == 0)
            {
                switch (s_g_settings[0])
                {
                    case 0:
                       i.text = "No Sound";
                        break;
                    case 1:
                        i.text = "Low Sound";
                        break;
                    case 2:
                        i.text = "Medium Sound";
                        break;
                    case 3:
                        i.text = "High Sound";
                        break;
                }
                i.rectTransform.SetParent(sounds_graphics[0].rectTransform);
            }
            else if(s_g_texts.IndexOf(i) == 1)
            {
                switch (s_g_settings[1])
                {
                    case 0:
                        i.text = "Low Graphics";
                        break;
                    case 1:
                        i.text = "Medium Graphics";
                        break;
                    case 2:
                        i.text = "High Graphics";
                        break;
                }
                i.rectTransform.SetParent(sounds_graphics[1].rectTransform);
            }

            i.alignment = TextAnchor.MiddleCenter;
            i.color = Color.black;
            i.font = Resources.Load<Font>("Dialogue_System_Graphics/dpcomic");
            i.fontSize = 30;
        }
    }

    private void use_settings_menu()
    {
        if (settings_holder != null)
        {
            if (quitting_choices == null && game_settings_choices == null) //able to choose in basic settings_holder
            {
                //choosing between buttons in settings_holder
                utility.choose_buttons(settings_options.ToArray(), new Sprite[] { Resources.Load<Sprite>("Settings_System_Graphics/Chosen_Settings"), Resources.Load<Sprite>("Settings_System_Graphics/Chosen_Settings") },
                    new Sprite[] { Resources.Load<Sprite>("Settings_System_Graphics/Not_Chosen_Settings"), Resources.Load<Sprite>("Settings_System_Graphics/Not_Chosen_Settings") }, 1, "hor");

                //moving settings_buttons
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
            else if (quitting_choices != null && game_settings_choices == null) //able to choose in quitting-game_holder
            {
                //choosing between buttons, whether to quit game or close holder
                utility.choose_buttons(quit_buttons.ToArray(), new Sprite[] { Resources.Load<Sprite>("Settings_System_Graphics/Agree_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/Close_Chosen") },
                                                               new Sprite[] { Resources.Load<Sprite>("Settings_System_Graphics/Agree_Not_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/Close_Not_Chosen") }, 
                                                                              1, "ver");
                if (quit_buttons[0].sprite == Resources.Load<Sprite>("Settings_System_Graphics/Agree_Chosen"))
                {
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        Application.Quit(); //quitting game (doesnt work in editor)
                        print("Application should quit now.");
                    }
                }
                else if(quit_buttons[1].sprite == Resources.Load<Sprite>("Settings_System_Graphics/Close_Chosen"))
                {
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        Destroy(GameObject.Find(quitting_choices.name)); //if not quitting game, closing quitting-game_holder
                    }
                }
            }
            else if(game_settings_choices != null && quitting_choices == null) //able to choose between game_settings_options
            {
                //choosing between buttons, which options (graphics or sound) to set
                utility.choose_buttons(game_settings_buttons.ToArray(), new Sprite[] { Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"),
                                                                                       Resources.Load<Sprite>("Settings_System_Graphics/Close_Chosen")},
                                                                        new Sprite[] { Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen"),
                                                                                       Resources.Load<Sprite>("Settings_System_Graphics/Close_Not_Chosen")}, 2, "hor");

                if(game_settings_buttons[0].sprite == Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen")) //if sound chosen
                {
                    //moving buttons of game_options up and down
                    sounds_graphics[0].rectTransform.localPosition = new Vector3(-13.79999f, -34.8f);
                    sounds_graphics[1].rectTransform.localPosition = new Vector3(-17.70001f, -23.39999f);

                    //audio settings with 1/3, always adding up or subtracting it
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        s_g_settings[0] += 1;
                        AudioListener.volume += 0.3f;

                        if (s_g_settings[0] > 3)
                        {
                            s_g_settings[0] = 3;
                            AudioListener.volume = 1.0f;
                        }
                        else if(s_g_settings[0] == 3)
                        {
                            AudioListener.volume = 1.0f;
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        s_g_settings[0] -= 1;
                        AudioListener.volume -= 0.3f;

                        if(s_g_settings[0] < 0)
                        {
                            s_g_settings[0] = 0;
                            AudioListener.volume = 0f;
                        }
                        else if(s_g_settings[0] == 0)
                        {
                            AudioListener.volume = 0f;
                        }
                    }

                    //setting text of sound_option
                    switch (s_g_settings[0])
                    {
                        case 0:
                            s_g_texts[0].text = "No Sound";
                            break;
                        case 1:
                            s_g_texts[0].text = "Low Sound";
                            break;
                        case 2:
                            s_g_texts[0].text = "Medium Sound";
                            break;
                        case 3:
                            s_g_texts[0].text = "High Sound";
                            break;
                    }

                    //settings active sprite for chosen settings
                    sounds_graphics[0].sprite = Resources.Load<Sprite>("Settings_System_Graphics/SSettings_" + s_g_settings[0].ToString());
                }
                else if(game_settings_buttons[1].sprite == Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen")) //if graphics chosen
                {
                    //moving buttons of game_options up and down
                    sounds_graphics[1].rectTransform.localPosition = new Vector3(-17.70001f, -34.39999f);
                    sounds_graphics[0].rectTransform.localPosition = new Vector3(-13.79999f, -23.39999f);

                    //settings int for graphics_options
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        s_g_settings[1] += 1;

                        if(s_g_settings[1] > 2)
                        {
                            s_g_settings[1] = 2;
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        s_g_settings[1] -= 1;

                        if(s_g_settings[1] < 0)
                        {
                            s_g_settings[1] = 0;
                        }
                    }

                    //settings text of graphics (for clarification) aswell as actual graphic level of game
                    switch (s_g_settings[1])
                    {
                        case 0:
                            s_g_texts[1].text = "Low Graphics";
                            QualitySettings.SetQualityLevel(0, true);
                            break;
                        case 1:
                            s_g_texts[1].text = "Medium Graphics";
                            QualitySettings.SetQualityLevel(3, true);
                            break;
                        case 2:
                            s_g_texts[1].text = "High Graphics";
                            QualitySettings.SetQualityLevel(5, true);
                            break;
                    }

                    //settings active sprite for chosen settings
                    sounds_graphics[1].sprite = Resources.Load<Sprite>("Settings_System_Graphics/GSettings_" + s_g_settings[1].ToString());
                }
                else if(game_settings_buttons[2].sprite == Resources.Load<Sprite>("Settings_System_Graphics/Close_Chosen"))
                {
                    //resettings positions of other buttons
                    sounds_graphics[0].rectTransform.localPosition = new Vector3(-13.79999f, -23.39999f);
                    sounds_graphics[1].rectTransform.localPosition = new Vector3(-17.70001f, -23.39999f);

                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        utility.to_change = 0;
                        Destroy(GameObject.Find(game_settings_choices.name)); //quitting game_settings and destroying game_settings_options_holder
                    }
                }
            }

            //if optionjs chosen, return_key pressed, game_settings_holder null can call other menus
            if(Input.GetKeyDown(KeyCode.Return) && game_settings_choices == null && settings_options[0].sprite == Resources.Load<Sprite>("Settings_System_Graphics/Chosen_Settings"))
            {
                create_gamesettings_menu();
            }
            else if(Input.GetKeyDown(KeyCode.Return) && quitting_choices == null && settings_options[1].sprite == Resources.Load<Sprite>("Settings_System_Graphics/Chosen_Settings"))
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
