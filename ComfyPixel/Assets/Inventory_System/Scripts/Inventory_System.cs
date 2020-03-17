
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory_System : MonoBehaviour {

    private Utility utility;
    private PlayerController playerController;

    private Image inv_Holder = null;
    private List<Image> inv_options_holder = null;


    public bool Inventory_System_bool = true;

    private void spawn_inv_menu()
    {
        playerController.gameObject.GetComponent<Animator>().SetBool("isWalking", false);
        playerController.canMove = false;

        inv_Holder = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 1, new string[] { "inventory_holder" }, new Vector2[] { new Vector2(1800f, 2000f) },
                                                           new Vector3[] { new Vector3(-556f, -1.2398e-05f) }, new Vector3[] { Vector3.zero });
        inv_Holder.sprite = Resources.Load<Sprite>("Settings_System_Graphics/Settings_Holder");

        inv_options_holder = (List<Image>)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 3, new string[] { "stats_holder", "attacks_holder", "items_holder"}, 
                                                                   new Vector2[] { new Vector2(400f, 400f), new Vector2(600f, 400f), new Vector2(1067.3f, 800f)},
                                                                   new Vector3[] { new Vector3(-751f, -330f), new Vector3(-499f, -330f), new Vector3(-597.4f, -8)}, 
                                                                   new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero, });

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

        List<Text> stats_sprites_texts = (List<Text>)utility.create_ui_object(new GameObject().AddComponent<Text>(), new System.Type[] { typeof(Text) }, 2, new string[] { "att_stat_text", "def_stat_text" },
                                                                              new Vector2[] { new Vector2(70f, 70f), new Vector2(70f, 70f) },
                                                                              new Vector3[] { new Vector3(-719.5f, -296.9f), new Vector3(-719.5f, -377.2f) },
                                                                              new Vector3[] { Vector3.zero, Vector3.zero });

        foreach(Text i in stats_sprites_texts)
        {
            i.font = Resources.Load<Font>("Dialogue_System_Graphics/dpcomic");
            i.fontSize = 40;
            i.color = Color.black;
            i.text = "7";
            i.alignment = TextAnchor.MiddleCenter;
            if(stats_sprites_texts.IndexOf(i) == 0)
            {
                i.rectTransform.SetParent(stats_sprites[0].rectTransform);
            }
            else if(stats_sprites_texts.IndexOf(i) == 1)
            {
                i.rectTransform.SetParent(stats_sprites[1].rectTransform);
            }
        }

        foreach (Image i in inv_options_holder)
        {
            i.sprite = Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen");
            i.rectTransform.SetParent(inv_Holder.rectTransform);
        }
    }

    public void close_inv_menu()
    {
        try
        {
            Destroy(GameObject.Find(inv_Holder.name));
        }
        catch
        {

        }
        inv_Holder = null;
        playerController.canMove = true;
        utility.to_change = -1;
        FindObjectOfType<NPC_Dialogue_System>().NPC_Dialogue_System_bool = true;
    }

    private void use_inv_men()
    {
        if (Inventory_System_bool)
        {
            if (Input.GetKeyDown(KeyCode.D) && inv_Holder == null)
            {
                spawn_inv_menu();
            }
            else if (Input.GetKeyDown(KeyCode.F) && inv_Holder != null)
            {
                close_inv_menu();
            }
        }

        if (inv_Holder != null)
        {
            FindObjectOfType<NPC_Dialogue_System>().NPC_Dialogue_System_bool = false;

            utility.choose_buttons(inv_options_holder.ToArray(), new Sprite[] { Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen") },
                                   new Sprite[] { Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen"), Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Not_Chosen") }, 
                                   2, "ver");

            if(inv_options_holder[0].sprite == Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"))
            {
                GameObject.Find("att_stat").GetComponent<Image>().rectTransform.localPosition = new Vector3(-40.20007f, 22.49997f);
                GameObject.Find("def_stat").GetComponent<Image>().rectTransform.localPosition = new Vector3(-40.20007f, -60.6f);
            }
            else if(inv_options_holder[1].sprite == Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"))
            {
                GameObject.Find("att_stat").GetComponent<Image>().rectTransform.localPosition = new Vector3(-40.20007f, 35.89996f);
                GameObject.Find("def_stat").GetComponent<Image>().rectTransform.localPosition = new Vector3(-40.20007f, -47.20001f);
            }
            else if(inv_options_holder[2].sprite == Resources.Load<Sprite>("Settings_System_Graphics/GSettings_Chosen"))
            {
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