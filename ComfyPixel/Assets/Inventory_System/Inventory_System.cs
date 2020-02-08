
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory_System : MonoBehaviour {

    private Utility utility;
    private PlayerController playerController;

    private Image inv_menu_Holder = null;
    public List<Image> inv_menu_buttons = new List<Image>();
    public Sprite inv_button_not_chosen, inv_button_chosen;
    public Sprite[] sound, graphics;

    private void spawn_inv_menu()
    {
        playerController.gameObject.GetComponent<Animator>().SetBool("isWalking", false);
        playerController.canMove = false;

        inv_menu_Holder = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), typeof(Image), 1, new string[] { "inventory_holder" }, new Vector2[] { new Vector2(170.8f, 208.6f) },
                                                           new Vector3[] { new Vector3(241.1f, 230.2f) }, new Vector3[] { Vector3.zero });

        inv_menu_buttons = (List<Image>)utility.create_ui_object(new GameObject().AddComponent<Image>(), typeof(Image), 2, new string[] { "inventory", "options" }, new Vector2[] { new Vector2(100f, 33.9f), new Vector2(100f, 33.9f) },
                                               new Vector3[] {new Vector3(inv_menu_Holder.GetComponent<RectTransform>().anchoredPosition.x, inv_menu_Holder.GetComponent<RectTransform>().anchoredPosition.y + 50f),
                                                              new Vector3(inv_menu_Holder.GetComponent<RectTransform>().anchoredPosition.x, inv_menu_Holder.GetComponent<RectTransform>().anchoredPosition.y - 50f)},
                                               new Vector3[] { Vector3.zero, Vector3.zero });

        List<Text> inv_buttons_text = (List<Text>)utility.create_ui_object(
                                       new GameObject().AddComponent<Text>(), typeof(Text), 2, new string[] { "inventory", "options" }, new Vector2[] { new Vector2(100f, 33.9f),
                                       new Vector2(100f, 33.9f) },
                                       new Vector3[] {new Vector3(-3.0518e-05f, 0),
                                                      new Vector3(-3.0518e-05f, -1.5259e-05f)},
                                       new Vector3[] { Vector3.zero, Vector3.zero });
        inv_buttons_text[0].GetComponent<RectTransform>().SetParent(inv_menu_buttons[0].GetComponent<RectTransform>(), false);
        inv_buttons_text[1].GetComponent<RectTransform>().SetParent(inv_menu_buttons[1].GetComponent<RectTransform>(), false);
        inv_buttons_text[0].font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        inv_buttons_text[1].font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        inv_buttons_text[0].text = "inventory";
        inv_buttons_text[1].text = "options";
    }

    public void close_inv_menu()
    {
        try
        {
            Destroy(GameObject.Find(inv_menu_Holder.name));
        }
        catch
        {

        }
        inv_menu_Holder = null;
        inv_menu_buttons = utility.delete_list_objects(inv_menu_buttons);
        playerController.canMove = true;
        utility.to_change = -1;

        foreach (GameObject i in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (i.name == "New Game Object")
            {
                Destroy(i);
            }
        }
    }

    private void use_inv_men()
    {
        if (Input.GetKeyDown(KeyCode.D) && playerController.NPC == null && inv_menu_Holder == null)
        {
            spawn_inv_menu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && inv_menu_Holder != null)
        {
            close_inv_menu();
        }

        try
        {
            if (inv_menu_Holder.gameObject.activeInHierarchy)
            {
                utility.choose_buttons(inv_menu_buttons.ToArray(), inv_button_chosen, inv_button_not_chosen, 1, "hor");
            }
        }
        catch
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
    }
}