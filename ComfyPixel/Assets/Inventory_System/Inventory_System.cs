
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory_System : MonoBehaviour {

    private Utility utility;
    private PlayerController playerController;

    public Image inv_menu_button;

    public Sprite inv_button_not_chosen, inv_button_chosen;
    public Sprite[] sound, graphics;
    private int[] sound_settings, graphics_settings;

    private Image inv_menu_Holder;
    private List<GameObject> inv_buttons = new List<GameObject>();

    private void spawn_inv_menu()
    {
        playerController.gameObject.GetComponent<Animator>().SetBool("isWalking", false);
        inv_buttons = utility.spawn_Buttons(inv_menu_button, 1,
                        new Vector3[] { new Vector3(inv_menu_Holder.rectTransform.anchoredPosition.x, inv_menu_Holder.rectTransform.anchoredPosition.y+70f),
                                        new Vector3(inv_menu_Holder.rectTransform.anchoredPosition.x, inv_menu_Holder.rectTransform.anchoredPosition.y-70f)},
                        new Quaternion[] { new Quaternion(), new Quaternion() },
                        new string[] { "inventory", "options" });
        inv_menu_Holder.color = new Color(1f, 1f, 1f, 1f);
        playerController.canMove = false;
    }

    private void close_inv_menu()
    {
        inv_menu_Holder.color = new Color(1f, 1f, 1f, 0f);
        inv_buttons = utility.delete_list_objects(inv_buttons);
        playerController.canMove = true;
        utility.to_change = -1;
    }

    private void use_inv_men()
    {
        Image graphics_sound = GameObject.Find("sound_graphics").GetComponent<Image>();
        Image inventory = GameObject.Find("inventory").GetComponent<Image>();

        if (Input.GetKeyDown(KeyCode.D) && inv_menu_Holder.color != new Color(1f, 1f, 1f, 1f))
        {
            spawn_inv_menu();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && (graphics_sound.color == new Color(1f, 1f, 1f, 0f) && inventory.color == new Color(1f, 1f, 1f, 0f)))
        {
            close_inv_menu();
        }
        
        if(inv_menu_Holder.color == new Color(1f, 1f, 1f, 1f))
        {           
            if (graphics_sound.color == new Color(1f, 1f, 1f, 0f) && inventory.color == new Color(1f, 1f, 1f, 0f))
            {
                utility.choose_buttons(inv_buttons.ToArray(), inv_button_chosen, inv_button_not_chosen, 1, "hor");

                if (inv_buttons[0].GetComponent<Image>().sprite == inv_button_chosen && Input.GetKeyDown(KeyCode.D))
                {
                    inventory.color = new Color(1f, 1f, 1f, 1f);
                }
                else if (inv_buttons[1].GetComponent<Image>().sprite == inv_button_chosen && Input.GetKeyDown(KeyCode.D))
                {
                    graphics_sound.color = new Color(1f, 1f, 1f, 1f);
                }
            }

            if(graphics_sound.color == new Color(1f, 1f, 1f, 1f))
            {
                //List<GameObject> graphics_sound_options = utility.spawn_Buttons()

                /* supply: images for sound_graphics_options, sprites for chosen and no_chosen and sprites for specified settings
                 * 
                 * spawn graphics_sound_options-buttons
                 * let player choose between them
                 * if chosen button is being pressed left_arrow on, add to int-variable of that option
                 * set sprite of that image according to int-variable of option
                 */

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    graphics_sound.color = new Color(1f, 1f, 1f, 0f);
                }
            }
            if(inventory.color == new Color(1f, 1f, 1f, 1f))
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    inventory.color = new Color(1f, 1f, 1f, 0f);
                }
            }
        }
    }

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        inv_menu_Holder = GameObject.Find("Inventory_Holder").GetComponent<Image>();
        utility = FindObjectOfType<Utility>();
    }

    private void Update()
    {
        use_inv_men();
    }
}