
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory_System : MonoBehaviour {

    private Utility utility;
    private PlayerController playerController;

    private Image inv_Holder = null;

    private void spawn_inv_menu()
    {
        playerController.gameObject.GetComponent<Animator>().SetBool("isWalking", false);
        playerController.canMove = false;

        inv_Holder = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), typeof(Image), 1, new string[] { "inventory_holder" }, new Vector2[] { new Vector2(430f, 272.6f) },
                                                           new Vector3[] { new Vector3(1.1444e-05f, 1.1444e-05f) }, new Vector3[] { Vector3.zero });
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
        if (Input.GetKeyDown(KeyCode.D) && playerController.NPC == null && inv_Holder == null)
        {
            spawn_inv_menu();
        }
        else if (Input.GetKeyDown(KeyCode.F) && inv_Holder != null)
        {
            close_inv_menu();
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
    }
}