
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private bool onGround = true; //to reactivate jump 
    [HideInInspector] public bool canMove = true;

    public GameObject NPC;

    private void player_movement(float moveSpeed, float jumpHeight)
    {
        if (canMove)
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Jump");

            transform.Translate(x * moveSpeed * Time.deltaTime, 0, 0f); //moving method based on transform of player

            Animator anim = GetComponent<Animator>();

            //handle walking animation of player
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && (!anim.GetBool("isJumping") && !anim.GetBool("isFalling")))
            {
                anim.SetBool("isWalking", true);
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                anim.SetBool("isWalking", false);
            }

            //player can turn left/right
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
                //jumping method based on rigidbody-object of player
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpHeight * y * Time.deltaTime);

                anim.SetBool("isJumping", true);
                anim.SetBool("isWalking", false);
                //take away ability of jumping
                onGround = false;
            }

            if (GetComponent<Rigidbody2D>().velocity.y < -1)
            {
                //when player starts to fall, activate falling animation
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
        //handles NPC-dialogue part of player
        if(collision.tag == "NPC_untalkable" && Input.GetKeyDown(KeyCode.E))
        {
            //get NPC the player is talking to 
            NPC = collision.gameObject;

            FindObjectOfType<NPC_Dialogue_System>().enabled = true;
        }
    }

    private void FixedUpdate()
    {
        player_movement(10f, 400f);
    }
}
