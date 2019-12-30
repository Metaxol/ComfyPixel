
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private bool onGround = true;
    //to reactivate jump 

    private Dialogue_System Dialogue_System;
    private Utility utility;

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
        if (collision.gameObject.tag == "NPC" && Input.GetKeyDown(KeyCode.E))
        {
            GameObject.Find("dialogue_box").GetComponent<Image>().color = new Color(255f, 255f, 255f, 255f);
            Dialogue_System.run_dialogue(utility.split_text(collision.gameObject.GetComponent<NPC_Attributes>().Dialogue.text),
                            collision.gameObject.GetComponent<NPC_Attributes>().Sprites,
                            GameObject.Find("Text").GetComponent<Text>());
        }
    }

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
