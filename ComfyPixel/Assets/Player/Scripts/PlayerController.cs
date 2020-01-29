
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private bool onGround = true; //to reactivate jump 

    private Utility utility;

    public string NPC_name;

    [HideInInspector] public bool canMove = true;

    private void player_movement(float moveSpeed, float jumpHeight)
    {
        if (canMove)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Jump");

            transform.Translate(x * moveSpeed * Time.deltaTime, 0, 0f); //moving method based on transform of player

            Animator anim = GetComponent<Animator>();

            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && (!anim.GetBool("isJumping") && !anim.GetBool("isFalling")))
            {
                anim.SetBool("isWalking", true);
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                anim.SetBool("isWalking", false);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }

            if (Input.GetKeyDown(KeyCode.Space) && onGround)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpHeight * y * Time.deltaTime);
                //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
                //jumping method based on rigidbody-object of player

                anim.SetBool("isJumping", true);
                anim.SetBool("isWalking", false);
                onGround = false;
                //take away ability of jumping
            }

            if (GetComponent<Rigidbody2D>().velocity.y < -1)
            {
                anim.SetBool("isFalling", true);
                anim.SetBool("isWalking", false);
                anim.SetBool("isJumping", false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //reactivating jumping based on collisions with "ground"-tagged objects
        if (collision.gameObject.tag == "ground") 
        {
            GetComponent<Animator>().SetBool("isFalling", false);
            GetComponent<Animator>().SetBool("isJumping", false);
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
        #region player_collision_dialogue_handling
        NPC_name = collision.gameObject.name;

        if (collision.gameObject.tag == "NPC_untalkable" && Input.GetKeyDown(KeyCode.E))
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
        #endregion
    }

    #region dialogue_coroutine (ignore)
    IEnumerator set_tag_NPC(float delay, GameObject NPC)
    {
        yield return new WaitForSeconds(delay);
        NPC.tag = "NPC_untalkable"; //make dialogue able to scroll again
    }
    #endregion

    private void Awake()
    {
        utility = FindObjectOfType<Utility>();
    }

    private void FixedUpdate()
    {
        player_movement(10f, 400f);
    }
}
