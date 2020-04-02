
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory_System : MonoBehaviour {

    private Utility utility;
    private PlayerController playerController;

    //general interfaces for inv_system
    private Image inv_Holder = null;
    private List<Image> inv_options_holder = null;

    //individual stuff for general interfaces
    private List<Image> items_places = new List<Image>(12);
    public List<Image> items_places_sprites = new List<Image>(12);
    private Sprite[] remember_sprites = new Sprite[12];
    private bool inventory_bool;
    public bool stats_bool;
    public enum item_type{permament, temporary};
    [SerializeField] private string current_item_text;

    public bool Inventory_System_bool = true;

    private void spawn_inv_menu()
    {
        playerController.gameObject.GetComponent<Animator>().SetBool("isWalking", false);
        playerController.canMove = false;

        //creating main-window for inventory
        inv_Holder = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 1, new string[] { "inventory_holder" }, new Vector2[] { new Vector2(1800f, 2000f) },
                                                           new Vector3[] { new Vector3(-556f, -1.2398e-05f) }, new Vector3[] { Vector3.zero });
        inv_Holder.sprite = Resources.Load<Sprite>("Settings_System_Graphics/Settings_Holder");


        //different windows you can choose between for inv_options
        inv_options_holder = (List<Image>)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 3, new string[] { "stats_holder", "attacks_holder", "items_holder"}, 
                                                                   new Vector2[] { new Vector2(400f, 400f), new Vector2(600f, 400f), new Vector2(1067.3f, 800f)},
                                                                   new Vector3[] { new Vector3(-751f, -330f), new Vector3(-499f, -330f), new Vector3(-597.4f, -8)}, 
                                                                   new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero, });

        //sprites in stats_holder-window
        List<Image> stats_sprites = (List<Image>)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 2, new string[] { "att_stat", "def_stat" },
                                                                          new Vector2[] { new Vector2(200f, 200f), new Vector2(200f, 200f)},
                                                                          new Vector3[] { new Vector3(-791.2f, -294.1f), new Vector3(-791.2f, -377.2f) },
                                                                          new Vector3[] { Vector3.zero, Vector3.zero});
        foreach(Image i in stats_sprites)
        {
            if(stats_sprites.IndexOf(i) == 0)
            {
                i.sprite = Resources.Load<Sprite>("Inventory_System_Graphics/Damage_Stat");
            }
            else if(stats_sprites.IndexOf(i) == 1)
            {
                i.sprite = Resources.Load<Sprite>("Inventory_System_Graphics/Defense_Stat");
            }

            i.rectTransform.SetParent(inv_options_holder[0].rectTransform);
        }

        //texts for sprites
        List<Text> stats_sprites_texts = (List<Text>)utility.create_ui_object(new GameObject().AddComponent<Text>(), new System.Type[] { typeof(Text) }, 2, new string[] { "att_stat_text", "def_stat_text" },
                                                                              new Vector2[] { new Vector2(70f, 70f), new Vector2(70f, 70f) },
                                                                              new Vector3[] { new Vector3(-719.5f, -296.9f), new Vector3(-719.5f, -377.2f) },
                                                                              new Vector3[] { Vector3.zero, Vector3.zero });

        foreach(Text i in stats_sprites_texts)
        {
            i.font = Resources.Load<Font>("Dialogue_System_Graphics/dpcomic");
            i.fontSize = 40;
            i.color = Color.black;
            i.alignment = TextAnchor.MiddleCenter;
            if(stats_sprites_texts.IndexOf(i) == 0)
            {
                i.rectTransform.SetParent(stats_sprites[0].rectTransform);
                i.text = (playerController.player_stats[0] + playerController.added_player_stats[0]).ToString();
            }
            else if(stats_sprites_texts.IndexOf(i) == 1)
            {
                i.rectTransform.SetParent(stats_sprites[1].rectTransform);
                i.text = (playerController.player_stats[1] + playerController.added_player_stats[1]).ToString();
            }
        }

        foreach (Image i in inv_options_holder)
        {
            i.sprite = Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen");
            i.rectTransform.SetParent(inv_Holder.rectTransform);
        }

        items_places = (List<Image>)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 12, new string[] { "items_place", "items_place", "items_place", "items_place", "items_place", "items_place", "items_place", "items_place", "items_place", "items_place", "items_place", "items_place", },
                                                                              new Vector2[] { new Vector2(200f, 200f), new Vector2(200f, 200f), new Vector2(200f, 200f), new Vector2(200f, 200f), new Vector2(200f, 200f), new Vector2(200f, 200f), new Vector2(200f, 200f), new Vector2(200f, 200f), new Vector2(200f, 200f), new Vector2(200f, 200f), new Vector2(200f, 200f),  new Vector2(200f, 200f) },
                                                                              new Vector3[] { new Vector3(-757f, 87f), new Vector3(-757f+104f, 87f), new Vector3(-757f + 104f*2, 87f), new Vector3(-757f + 104f*3, 87f), new Vector3(-757f, 87f-106f), new Vector3(-757f + 104f, 87f-106f), new Vector3(-757f + 104f * 2, 87f-106f), new Vector3(-757f + 104f * 3, 87f-106), new Vector3(-757f, 87f-106*2), new Vector3(-757f+104f, 87f-106*2), new Vector3(-757f + 104f*2, 87f-106*2), new Vector3(-757f + 104f*3, 87f-106*2),},
                                                                              new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, });
        foreach (Image i in items_places)
        {
            i.sprite = Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen");
            i.rectTransform.SetParent(inv_options_holder[2].rectTransform);
        }
        remember_sprites[0] = Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen");
        //y=106.07806, x=103.9969
        items_places_sprites = (List<Image>)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 12, new string[] { "items_place_sprite", "items_place_sprite", "items_place_sprite", "items_place_sprite", "items_place_sprite", "items_place_sprite", "items_place_sprite", "items_place_sprite", "items_place_sprite", "items_place_sprite", "items_place_sprite", "items_place_sprite" },
                                                                     new Vector2[] { new Vector2(82.12f, 82.142f), new Vector2(82.12f, 82.142f), new Vector2(82.12f, 82.142f), new Vector2(82.12f, 82.142f), new Vector2(82.12f, 82.142f), new Vector2(82.12f, 82.142f), new Vector2(82.12f, 82.142f), new Vector2(82.12f, 82.142f), new Vector2(82.12f, 82.142f), new Vector2(82.12f, 82.142f), new Vector2(82.12f, 82.142f), new Vector2(82.12f, 82.142f) },
                                                                     new Vector3[] { new Vector3(-757.9969f, 83.97806f), new Vector3(-757.9969f+ 103.9969f, 83.97806f), new Vector3(-757.9969f+103.9969f*2, 83.97806f), new Vector3(-757.9969f+103.9969f*3, 83.97806f), new Vector3(-757.9969f, 83.97806f-106.07806f), new Vector3(-757.9969f + 103.9969f, 83.97806f - 106.07806f), new Vector3(-757.9969f + 103.9969f * 2, 83.97806f - 106.07806f), new Vector3(-757.9969f + 103.9969f * 3, 83.97806f - 106.07806f), new Vector3(-757.9969f, 83.97806f - 106.07806f*2), new Vector3(-757.9969f + 103.9969f, 83.97806f - 106.07806f*2), new Vector3(-757.9969f + 103.9969f * 2, 83.97806f - 106.07806f*2), new Vector3(-757.9969f + 103.9969f * 3, 83.97806f - 106.07806f*2), },
                                                                     new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero });

        foreach (Image i in items_places_sprites)
        {
            i.rectTransform.SetParent(items_places[items_places_sprites.IndexOf(i)].rectTransform);
            print(items_places_sprites.IndexOf(i));
        }
    }

    public void add_item(Sprite item_sprite, int stat_amount, int increase_stat, item_type type, int amount_rounds=0)
    {
        playerController.added_player_stats[stat_amount] += increase_stat;
        current_item_text = "This is a test item. It will increase your Attack Points by +1.";
        foreach(Sprite i in remember_sprites)
        {
            if(i == null)
            {
                remember_sprites[A]
            }
            break;
        }
    }

    public void close_inv_menu()
    {
        //catching error when function called when inv_holder not existing in scene
        try
        {
            Destroy(GameObject.Find(inv_Holder.name));
        }
        catch
        {

        }
        inventory_bool = false;
        inv_Holder = null;
        playerController.canMove = true;
        utility.to_change = -1; //to choose between different option-systems
        FindObjectOfType<NPC_Dialogue_System>().NPC_Dialogue_System_bool = true;
    }

    private void use_inv_men()
    {
        if (Inventory_System_bool)
        {
            //closing and starting inv_system
            if (Input.GetKeyDown(KeyCode.D) && inv_Holder == null)
            {
                spawn_inv_menu();
            }
            else if (Input.GetKeyDown(KeyCode.F) && inv_Holder != null && !inventory_bool && !stats_bool)
            {
                close_inv_menu();
            }
        }

        if (inv_Holder != null)
        {

            FindObjectOfType<NPC_Dialogue_System>().NPC_Dialogue_System_bool = false;

            //choosing between inv_options
            if (!inventory_bool && !stats_bool)
            {
                utility.choose_buttons(inv_options_holder.ToArray(), new Sprite[] { Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen") },
                       new Sprite[] { Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen") },
                       2, "ver");
            }

            //moving specific elements in windows when chosen/not chosen
            if(inv_options_holder[0].sprite == Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"))
            {
                GameObject.Find("att_stat").GetComponent<Image>().rectTransform.localPosition = new Vector3(-40.20007f, 22.49997f);
                GameObject.Find("def_stat").GetComponent<Image>().rectTransform.localPosition = new Vector3(-40.20007f, -60.6f);
                if (items_places[items_places.Count - 1].rectTransform.position != new Vector3(515.0f, 415.0f, 0f))
                {
                    foreach (Image i in items_places)
                    {

                        i.rectTransform.localPosition -= new Vector3(0, -22f, 0);
                    }
                }

                Image text_inv_holder = null;
                Text stats_text = null;

                if (Input.GetKeyDown(KeyCode.D) && !stats_bool)
                {
                    utility.can_scroll = true;
                    utility.letter = 0;
                    utility.current_line = 0;
                    stats_bool = true;
                    text_inv_holder = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 1, new string[] { "stats_text_holder" }, new Vector2[] { new Vector2(830f, 650f) },
                                                           new Vector3[] { new Vector3(-71f, -351.7f) }, new Vector3[] { Vector3.zero });
                    text_inv_holder.sprite = Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen");
                    stats_text = (Text)utility.create_ui_object(new GameObject().AddComponent<Text>(), new System.Type[] { typeof(Text) }, 1, new string[] { "stats_text" }, new Vector2[] { new Vector2(325f, 230f) },
                                                           new Vector3[] { new Vector3(-71f, -360.9f) }, new Vector3[] { Vector3.zero });
                    stats_text.rectTransform.SetParent(text_inv_holder.rectTransform);
                    stats_text.fontSize = 45;
                    stats_text.font = Resources.Load<Font>("Dialogue_System_Graphics/dpcomic");
                    stats_text.color = Color.black;
                }

                if (stats_bool)
                {
                    if (Input.GetKeyDown(KeyCode.E) && GameObject.Find("stats_text").GetComponent<Text>().text == ("You have " + playerController.player_stats[0] + " Attack points (+" + playerController.added_player_stats[0] + ") and " + playerController.player_stats[1] + " Defense points (+" + playerController.added_player_stats[1] + ")."))
                    {
                        Destroy(GameObject.Find("stats_text_holder"));
                        stats_bool = false;
                        utility.can_scroll = true;
                        utility.letter = 0;
                        utility.current_line = 0;
                    }
                    utility.run_text(new List<string> { "You have " + playerController.player_stats[0] + " Attack points (+" + playerController.added_player_stats[0] + ") and "  + playerController.player_stats[1] + " Defense points (+" + playerController.added_player_stats[1] + ")." }, GameObject.Find("stats_text").GetComponent<Text>(), 0.03f);
                }
            }
            else if(inv_options_holder[1].sprite == Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"))
            {
                GameObject.Find("att_stat").GetComponent<Image>().rectTransform.localPosition = new Vector3(-40.20007f, 35.89996f);
                GameObject.Find("def_stat").GetComponent<Image>().rectTransform.localPosition = new Vector3(-40.20007f, -47.20001f);
                if (items_places[items_places.Count - 1].rectTransform.position != new Vector3(515.0f, 415.0f, 0f))
                {
                    foreach (Image i in items_places)
                    {
                        i.rectTransform.localPosition -= new Vector3(0, -22f, 0);
                    }
                }
            }
            else if(inv_options_holder[2].sprite == Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"))
            {
                if (items_places[items_places.Count - 1].rectTransform.position == new Vector3(515.0f, 415.0f, 0f))
                {
                    foreach (Image i in items_places)
                    {
                        i.rectTransform.localPosition += new Vector3(0, -22f, 0);
                    }
                }

                if (Input.GetKeyDown(KeyCode.D) && !inventory_bool)
                {
                    items_places[0].sprite = Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen");
                    items_places_sprites[0].rectTransform.localPosition -= new Vector3(0, 5.878058f, 0);
                    items_places_sprites[0].tag = "inv_item_chosen";
                    utility.to_change = 0;
                }

                foreach(Image i in items_places)
                {
                    if(i.sprite == Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"))
                    {
                        inventory_bool = true;
                    }
                }

                if (Input.GetKeyDown(KeyCode.F))
                {
                    utility.to_change = 2;
                    inventory_bool = false;
                    foreach(Image i in items_places)
                    {
                        if(i.sprite == Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"))
                        {
                            i.sprite = Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen");
                            i.rectTransform.GetChild(0).localPosition += new Vector3(0, 5.878058f, 0);
                        }
                    }
                }

                if (inventory_bool)
                {
                    if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        foreach(Image i in items_places)
                        {
                            if (i.sprite == Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"))
                            {
                                i.rectTransform.GetChild(0).tag = "prev_inv_item_chosen";
                            }
                        }
                    }

                    utility.choose_buttons(items_places.ToArray(), new Sprite[] { Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen")},
                                                         new Sprite[] { Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen") },
                                                         11, "ver");

                    foreach(Image i in items_places_sprites)
                    {
                        if(i.tag == "prev_inv_item_chosen")
                        {
                            foreach(Image o in items_places)
                            {
                                if(o.sprite == Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"))
                                {
                                    o.tag = "inv_item_chosen";
                                    o.rectTransform.GetChild(0).localPosition -= new Vector3(0, 5.878058f, 0);
                                }
                            }

                            i.rectTransform.localPosition += new Vector3(0, 5.878058f, 0);
                            i.tag = "Untagged";
                        }
                    }
                }

                GameObject.Find("att_stat").GetComponent<Image>().rectTransform.localPosition = new Vector3(-40.20007f, 35.89996f);
                GameObject.Find("def_stat").GetComponent<Image>().rectTransform.localPosition = new Vector3(-40.20007f, -47.20001f);
            }
        }
    }

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        utility = FindObjectOfType<Utility>();
    }

    private void Update()
    {
        use_inv_men();
    }
}