
using UnityEngine;
using UnityEngine.UI;

public class Settings_System : MonoBehaviour {

    private Image options_holder;
    private Image settings, quit;

    private void spawn_settings_menu()
    {
        Utility utility = FindObjectOfType<Utility>();

        options_holder = (Image) utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] {typeof(Image) },
                                                          1, new string[] { "options_holder" }, new Vector2[] { new Vector2(414.7f, 586.6f) }, new Vector3[] { new Vector3(1.2159e-05f, -2.2888e-05f) }, new Vector3[] { Vector3.zero });

        settings = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) },
                                                   1, new string[] { "settings" }, new Vector2[] { new Vector2(365.2f, 210.7f) }, 
                                                   new Vector3[] { new Vector3(options_holder.GetComponent<RectTransform>().anchoredPosition.x, options_holder.GetComponent<RectTransform>().anchoredPosition.y + 100f) }, 
                                                   new Vector3[] { Vector3.zero });

        quit = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) },
                                           1, new string[] { "quit" }, new Vector2[] { new Vector2(365.2f, 210.7f) },
                                           new Vector3[] { new Vector3(options_holder.GetComponent<RectTransform>().anchoredPosition.x, options_holder.GetComponent<RectTransform>().anchoredPosition.y - 100f) },
                                           new Vector3[] { Vector3.zero });

        //for testing
        settings.color = new Color(204f, 21f, 21f, 255f);
        quit.color = new Color(0f, 0f, 0f, 255f);
    }

    private void player_handling()
    {

    }

    private void OnEnable()
    {
        spawn_settings_menu();
    }
}
