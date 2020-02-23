
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC_Dialogue_System : MonoBehaviour {

    private PlayerController playerController;
    private Utility utility;

    //variables created for NPC-dialogue-ui-objects
    [HideInInspector] public Image dialogue_box;
    private Image sprite_box;
    private Text dialogue_text;

    public bool NPC_Dialogue_System_bool = true;

    private void run_NPC_dialogue()
    {
        if(NPC_Dialogue_System_bool)
        {
            //getting name and sprites of NPC 
            string NPC_name = playerController.NPC.name;
            Sprite[] NPC_sprites = playerController.NPC.GetComponent<NPC_Attributes>().Sprites;
            FindObjectOfType<Inventory_System>().Inventory_System_bool = false;
            if (playerController.NPC.tag == "NPC_untalkable" && Input.GetKeyDown(KeyCode.E))
            {
                //name of gameobject is box where text is, not textbox itself
                playerController.GetComponent<Animator>().SetBool("isWalking", false);
                playerController.canMove = false;
                playerController.NPC.tag = "NPC_talkable";
            }

            if (playerController.NPC.tag == "NPC_talkable") //said signal received here
            {
                if (NPC_sprites.Length == 0)
                {
                    Destroy(GameObject.Find("sprite_box"));
                }

                //supplies the needed parameters to dialogue_system class
                utility.run_text(utility.split_text(playerController.NPC.GetComponent<NPC_Attributes>().Dialogue.text),
                                                    dialogue_text, 0.07f);

                //when dialogue hits length of lines
                if (utility.current_line == playerController.NPC.GetComponent<NPC_Attributes>().stop_scroll_line)
                {
                    //to revert changes made by the dialogue_system
                    playerController.canMove = true;
                    utility.can_scroll = true;
                    utility.letter = 0;
                    utility.current_line = 0;
                    utility.to_change = -1;
                    if(playerController.NPC.GetComponent<NPC_Attributes>().Sprites.Length != 0)
                    {
                        sprite_box.sprite = null;
                    }
                    playerController.NPC.GetComponent<NPC_Attributes>().oneTime = true;
                    FindObjectOfType<Inventory_System>().Inventory_System_bool = true;

                    //stop repeated execution of same dialogue
                    playerController.NPC.gameObject.tag = "NPC_nottalk";
                    StartCoroutine(set_tag_NPC(0.7f, playerController.NPC.gameObject));

                    //deactivate this script and destroy spawned objects
                    playerController.NPC = null;
                    Destroy(GameObject.Find("dialogue_box"));
                    enabled = false;
                }
            }
        }
    }

    IEnumerator set_tag_NPC(float delay, GameObject NPC)
    {
        yield return new WaitForSeconds(delay);
        NPC.tag = "NPC_untalkable"; //make dialogue able to scroll again
    }

    private void declare_dialogue_attr()
    {
        //create all necessary ui objects for NPC-dialogue
        utility = FindObjectOfType<Utility>();
        playerController = FindObjectOfType<PlayerController>();

        dialogue_box = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 1, new string[] { "dialogue_box" }, new Vector2[] { new Vector2(1800f, 1800f) },
                                new Vector3[] { new Vector3(-98f, 436f) }, new Vector3[] { Vector3.zero });
        dialogue_box.sprite = Resources.Load<Sprite>("Dialogue_System_Graphics/Dialogue_Box");
        
        //only create objects like sprite_box and dialogue_text with (with specified position) if there are more than 0 sprites
        if (playerController.NPC.GetComponent<NPC_Attributes>().Sprites.Length != 0)
        {
            dialogue_text = (Text)utility.create_ui_object(new GameObject().AddComponent<Text>(), new System.Type[] { typeof(Text) }, 1, new string[] { "dialogue_text" }, new Vector2[] { new Vector2(609.13f, 215.41f) },
                                        new Vector3[] { new Vector3(55.64f, -53.86f) }, new Vector3[] { Vector3.zero });

            Image sprite_box_holder = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 1, new string[] { "sprite_box_holder" }, new Vector2[] { new Vector2(1800f, 1800f) },
                                                          new Vector3[] { new Vector3(-342.8f, 0f) }, new Vector3[] { Vector3.zero });
            sprite_box_holder.GetComponent<RectTransform>().transform.SetParent(dialogue_box.GetComponent<RectTransform>(), false);
            sprite_box_holder.sprite = Resources.Load<Sprite>("Dialogue_System_Graphics/Dialogue_Sprite_Frame");

            sprite_box = (Image)utility.create_ui_object(new GameObject().AddComponent<Image>(), new System.Type[] { typeof(Image) }, 1, new string[] { "sprite_box" }, new Vector2[] { new Vector2(215.931f, 216.004f) },
                                                    new Vector3[] { new Vector3(-35.61694f, -54.01702f) }, new Vector3[] { Vector3.zero });
            sprite_box.GetComponent<RectTransform>().transform.SetParent(sprite_box_holder.GetComponent<RectTransform>(), false);
        }
        //not creating any special objects because there are more than 0 sprites
        else if (playerController.NPC.GetComponent<NPC_Attributes>().Sprites.Length == 0)
        {
            dialogue_text = (Text)utility.create_ui_object(new GameObject().AddComponent<Text>(), new System.Type[] { typeof(Text) }, 1, new string[] { "dialogue_text" }, new Vector2[] { new Vector2(827.97f, 215.41f) },
                            new Vector3[] { new Vector3(-51f, -53.86f) }, new Vector3[] { Vector3.zero });
        }
        
        //making dialogue_text-specifications
        dialogue_text.color = new Color(0f, 0f, 0f, 255f);
        dialogue_text.fontSize = 60;
        dialogue_text.font = Resources.Load<Font>("Dialogue_System_Graphics/dpcomic");
        dialogue_text.GetComponent<RectTransform>().transform.SetParent(dialogue_box.GetComponent<RectTransform>(), false);
    }

    private void OnEnable()
    {
        declare_dialogue_attr();
    }

    private void Update()
    {
        run_NPC_dialogue();
    }
}
