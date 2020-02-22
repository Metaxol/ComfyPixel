
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Settings_System : MonoBehaviour {

    public Image options_holder = null;
    private List<Image> settings_buttons;
    

    private Utility utility;

    private void spawn_settings_menu()
    {
        Image game_coverup = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) },
                                                               1, new string[] { "cover_game" }, new Vector2[] { new Vector2(1920f, 1080f) }, new Vector3[] { new Vector3(0f, 0f) }, new Vector3[] { Vector3.zero });
        game_coverup.color = new Color(0f, 0f, 0f, 110f / 255f);

        options_holder = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) },
                                                          1, new string[] { "options_holder" }, new Vector2[] { new Vector2(414.7f, 586.6f) }, new Vector3[] { new Vector3(1.2159e-05f, -2.2888e-05f) }, new Vector3[] { Vector3.zero });

        settings_buttons = (List<Image>)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) },
                                                   2, new string[] { "settings", "quit" }, new Vector2[] { new Vector2(365.2f, 210.7f), new Vector2(365.2f, 210.7f) },
                                                   new Vector3[] { new Vector3(options_holder.GetComponent<RectTransform>().anchoredPosition.x, options_holder.GetComponent<RectTransform>().anchoredPosition.y + 140f),
                                                                   new Vector3(options_holder.GetComponent<RectTransform>().anchoredPosition.x, options_holder.GetComponent<RectTransform>().anchoredPosition.y - 140f)},
                                                   new Vector3[] { Vector3.zero, Vector3.zero });
        
        //for testing
        settings_buttons[0].GetComponent<RectTransform>().SetParent(options_holder.GetComponent<RectTransform>());
        settings_buttons[1].GetComponent<RectTransform>().SetParent(options_holder.GetComponent<RectTransform>());
        settings_buttons[0].color = new Color(204f / 255f, 21f / 255f, 21f / 255f, 255f / 255f);
        settings_buttons[1].color = new Color(0f, 0f, 0f, 255f);
        Time.timeScale = 0;
    }

    public void player_handling()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && enabled == false)
        {
            enabled = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && enabled == true)
        {
            Time.timeScale = 1;
            utility.delete_with_names(new string[] { options_holder.name, "cover_game" });
            options_holder = null;
            settings_buttons = utility.delete_list_objects(settings_buttons);
            enabled = false;
        }
    }


    private void Awake()
    {
        utility = FindObjectOfType<Utility>();
    }

    private void OnEnable()
    {
        spawn_settings_menu();
    }

    /*    private void spawn_inv_menu()
    {
        playerController.gameObject.GetComponent<Animator>().SetBool("isWalking", false);
        playerController.canMove = false;

        inv_Holder = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 1, new string[] { "inventory_holder" }, new Vector2[] { new Vector2(1046.8f, 1239f) },
                                                     new Vector3[] { new Vector3(13f, -2.2888e-05f) }, new Vector3[] { Vector3.zero });

        Image settings_sprite = (Image) utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 1, new string[] { "settings_sprite" }, new Vector2[] { new Vector2(650f, 650f) },
                                                             new Vector3[] { new Vector3(0f, 157f) }, new Vector3[] { Vector3.zero });
        settings_sprite.sprite = Resources.Load<Sprite>("Settings_System_Graphics/GameSettings_Symbol");
        settings_sprite.rectTransform.SetParent(inv_Holder.rectTransform);

        Image quit_sprite = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 1, new string[] { "settings_sprite" }, new Vector2[] { new Vector2(650f, 650f) },
                                                            new Vector3[] { new Vector3(0f, -90f) }, new Vector3[] { Vector3.zero });
        quit_sprite.sprite = Resources.Load<Sprite>("Settings_System_Graphics/Quit_Symbol");
        quit_sprite.rectTransform.SetParent(inv_Holder.rectTransform);

        Text settings_text = (Text)utility.create_ui_object(new GameObject().AddComponent<Text>(), new System.Type[] { typeof(Text) }, 1, new string[] { "settings_sprite_text" }, new Vector2[] { new Vector2(301.4f, 92.6f) },
                                                         new Vector3[] { new Vector3(-14.2f, 46.3f) }, new Vector3[] { Vector3.zero });
        settings_text.text = "Settings";
        settings_text.fontSize = 100;
        settings_text.font = Resources.Load<Font>("Dialogue_System_Graphics/dpcomic");
        settings_text.rectTransform.SetParent(settings_sprite.rectTransform);
        settings_text.color = new Color(0f, 0f, 0f, 255f);
        settings_text.alignment = TextAnchor.MiddleCenter;

        Text quit_text = (Text)utility.create_ui_object(new GameObject().AddComponent<Text>(), new System.Type[] { typeof(Text) }, 1, new string[] { "quit_sprite_text" }, new Vector2[] { new Vector2(301.4f, 92.6f) },
                                                 new Vector3[] { new Vector3(-14.2f, -208f) }, new Vector3[] { Vector3.zero });
        quit_text.text = "Quit";
        quit_text.fontSize = 100;
        quit_text.font = Resources.Load<Font>("Dialogue_System_Graphics/dpcomic");
        quit_text.rectTransform.SetParent(settings_sprite.rectTransform);
        quit_text.color = new Color(0f, 0f, 0f, 255f);
        quit_text.alignment = TextAnchor.MiddleCenter;

        inv_Holder.sprite = Resources.Load<Sprite>("Settings_System_Graphics/Settings_Holder");
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
    }*/
}
