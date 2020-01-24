
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory_System : MonoBehaviour {

    private Utility utility;
    private PlayerController playerController;

    public Image inv_menu_button;
    private Image inv_menu_Holder;

    public Sprite inv_button_not_chosen;
    public Sprite inv_button_chosen;

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
        if (Input.GetKeyDown(KeyCode.D) && inv_menu_Holder.color != new Color(1f, 1f, 1f, 1f))
        {
            spawn_inv_menu();
        }else if (Input.GetKeyDown(KeyCode.Escape))
        {
            close_inv_menu();
        }
        
        if(inv_menu_Holder.color == new Color(1f, 1f, 1f, 1f))
        {
            utility.choose_buttons(inv_buttons.ToArray(), inv_button_chosen, inv_button_not_chosen, 1, "hor");

            
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