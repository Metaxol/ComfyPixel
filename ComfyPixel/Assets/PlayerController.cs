
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private bool onGround = true; //to reactivate jump 

    private Dialogue_System Dialogue_System;
    private Utility utility;

    public string NPC_name;

    private void player_movement(float moveSpeed, float jumpHeight)
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime;

        transform.Translate(x * moveSpeed, 0, 0f); //moving method based on transform of player

        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
            //jumping method based on rigidbody-object of player

            onGround = false;
            //take away ability of jumping
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //reactivating jumping based on collisions with "ground"-tagged objects
        if (collision.gameObject.tag == "ground") 
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            onGround = false; //to prohibit player from jumping when he fell off an object
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        NPC_name = collision.gameObject.name;

        if (collision.gameObject.tag == "NPC_untalkable" && Input.GetKeyDown(KeyCode.E))
        {
            //name of gameobject is box where text is, not textbox itself
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
            Dialogue_System.run_dialogue(utility.split_text(collision.gameObject.GetComponent<NPC_Attributes>().Dialogue.text),
                                        GameObject.Find("Text").GetComponent<Text>());

            //when dialogue hits length of lines
            if(utility.current_line == utility.split_text(collision.gameObject.GetComponent<NPC_Attributes>().Dialogue.text).Count)
            {
                //to revert changes made by the dialogue_system
                utility.can_scroll = true;
                utility.letter = 0;
                utility.current_line = 0;
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
    }

    #region coroutine for dialogue part of script (ignore)
    IEnumerator set_tag_NPC(float delay, GameObject NPC)
    {
        yield return new WaitForSeconds(delay);
        NPC.tag = "NPC_untalkable"; //make dialogue able to scroll again
    }
    #endregion

    private void Awake()
    {
        utility = FindObjectOfType<Utility>();
        Dialogue_System = FindObjectOfType<Dialogue_System>();
    }

    private void Update()
    {
        player_movement(10f, 10f);
    }
}
