
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
}
