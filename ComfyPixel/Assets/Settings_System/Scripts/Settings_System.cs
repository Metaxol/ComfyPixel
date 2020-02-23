
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Settings_System : MonoBehaviour {

    private Image settings_holder = null; 
    

    private Utility utility;

    private void spawn_settings_menu()
    {
        Image game_coverup = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) },
                                                               1, new string[] { "cover_game" }, new Vector2[] { new Vector2(1920f, 1080f) }, new Vector3[] { new Vector3(0f, 0f) }, new Vector3[] { Vector3.zero });
        game_coverup.color = new Color(0f, 0f, 0f, 110f / 255f);

        settings_holder = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 1, new string[] { "inventory_holder" }, new Vector2[] { new Vector2(1046.8f, 1239f) },
                                                     new Vector3[] { new Vector3(13f, -2.2888e-05f) }, new Vector3[] { Vector3.zero });

        Image settings_sprite = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 1, new string[] { "settings_sprite" }, new Vector2[] { new Vector2(650f, 650f) },
                                                             new Vector3[] { new Vector3(0f, 157f) }, new Vector3[] { Vector3.zero });
        settings_sprite.sprite = Resources.Load<Sprite>("Settings_System_Graphics/GameSettings_Symbol");
        settings_sprite.rectTransform.SetParent(settings_holder.rectTransform);

        Image quit_sprite = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 1, new string[] { "settings_sprite" }, new Vector2[] { new Vector2(650f, 650f) },
                                                            new Vector3[] { new Vector3(0f, -90f) }, new Vector3[] { Vector3.zero });
        quit_sprite.sprite = Resources.Load<Sprite>("Settings_System_Graphics/Quit_Symbol");
        quit_sprite.rectTransform.SetParent(settings_holder.rectTransform);

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
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && enabled == true)
        {
            Time.timeScale = 1;
            FindObjectOfType<Inventory_System>().Inventory_System_bool = true;
            FindObjectOfType<NPC_Dialogue_System>().NPC_Dialogue_System_bool = true;
            utility.delete_with_names(new string[] { settings_holder.name, "cover_game" });
            settings_holder = null;
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
}
