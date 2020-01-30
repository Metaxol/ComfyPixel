
using UnityEngine;
using UnityEngine.UI;

public class NPC_Dialogue_System : MonoBehaviour {

    private PlayerController playerController;
    private Utility utility;

    private void Awake()
    {
        utility = FindObjectOfType<Utility>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void run_NPC_dialogue()
    {
        string NPC_name = playerController.NPC.name;
        Sprite[] NPC_sprites = playerController.NPC.GetComponent<NPC_Attributes>().Sprites;

        if (playerController.NPC.tag == "NPC_untalkable" && Input.GetKeyDown(KeyCode.E))
        {
            //name of gameobject is box where text is, not textbox itself
            playerController.canMove = false;
            playerController.NPC.tag = "NPC_talkable";
        }

        if (playerController.NPC.tag == "NPC_talkable") //said signal received here
        {
            //turn alpha component on
            GameObject.Find("dialogue_box").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            if (NPC_sprites.Length != 0)
            {
                GameObject.Find("sprite_box").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }

            //supplies the needed parameters to dialogue_system class
            utility.run_text(utility.split_text(playerController.NPC.GetComponent<NPC_Attributes>().Dialogue.text),
                                                GameObject.Find("Text").GetComponent<Text>(),
                                                0.07f);
        }

    /*   if (collision.gameObject.tag == "NPC_untalkable" && Input.GetKeyDown(KeyCode.E))
        {
            //name of gameobject is box where text is, not textbox itself
            canMove = false;
            collision.gameObject.tag = "NPC_talkable";
        }

        if (collision.gameObject.tag == "NPC_talkable") //said signal received here
        {
            Sprite[] NPC_sprites = collision.GetComponent<NPC_Attributes>().Sprites;

            //turn alpha component on
            GameObject.Find("dialogue_box").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            if(NPC_sprites.Length != 0)
            {
                GameObject.Find("sprite_box").GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }

            //supplies the needed parameters to dialogue_system class
            utility.run_text(utility.split_text(collision.gameObject.GetComponent<NPC_Attributes>().Dialogue.text),
                                                GameObject.Find("Text").GetComponent<Text>(),
                                                0.07f);

            //when dialogue hits length of lines
            if(utility.current_line == collision.GetComponent<NPC_Attributes>().stop_scroll_line)
            {
                //to revert changes made by the dialogue_system
                canMove = true;
                utility.can_scroll = true;
                utility.letter = 0;
                utility.current_line = 0;
                utility.to_change = -1;
                GameObject.Find("sprite_box").GetComponent<Image>().sprite = null;
                collision.GetComponent<NPC_Attributes>().oneTime = true;

                //turn alpha component off
                GameObject.Find("dialogue_box").GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                GameObject.Find("sprite_box").GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);

                //stop repeated execution of same dialogue
                collision.gameObject.tag = "NPC_nottalk";
                StartCoroutine(set_tag_NPC(0.7f, collision.gameObject));
            }
        }
     */
}
