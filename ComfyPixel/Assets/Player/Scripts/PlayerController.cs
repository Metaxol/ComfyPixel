
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

            //spawn needed objects for dialogue
            Image sprite_box = null;
            Image dialogue_box = ((GameObject)utility.create_object(typeof(Image), 1, new string[] { "dialogue_box" },
                                                              new Vector2[] { new Vector2(478.2f, 77f) },
                                                              new Vector3[] { new Vector3(1.2398e-05f, -260f, 0f) },
                                                              new Quaternion[] { new Quaternion() })).GetComponent<Image>();
            dialogue_box.transform.SetParent(GameObject.Find("Canvas").transform, false);

            Text dialogue_text = ((GameObject)utility.create_object(typeof(Text), 1, new string[] { "Text" },
                                                             new Vector2[] { new Vector2(478.18f, 77f) },
                                                             new Vector3[] { new Vector3(0.012494f, 0.012501f, 0f) },
                                                             new Quaternion[] { new Quaternion() })).GetComponent<Text>();
            dialogue_text.transform.SetParent(dialogue_box.transform, false);

            if (NPC_sprites.Length != 0)
            {
                sprite_box = (Image)utility.create_object(typeof(Image), 1, new string[] { "sprite_box" },
                                                  new Vector2[] { new Vector2(90.3f, 77f) },
                                                  new Vector3[] { new Vector3(194f, -0.0060034f, 0f) },
                                                  new Quaternion[] { new Quaternion() });
                sprite_box.transform.SetParent(dialogue_box.transform, false);
            }

            //supplies the needed parameters to dialogue_system class
            utility.run_text(utility.split_text(collision.gameObject.GetComponent<NPC_Attributes>().Dialogue.text),
                                                dialogue_text, 0.07f);

            //when dialogue hits length of lines
            if(utility.current_line == collision.GetComponent<NPC_Attributes>().stop_scroll_line)
            {
                //to revert changes made by the dialogue_system
                canMove = true;
                utility.can_scroll = true;
                utility.letter = 0;
                utility.current_line = 0;
                utility.to_change = -1;
                sprite_box.sprite = null;
                collision.GetComponent<NPC_Attributes>().oneTime = true;

                //destroy created dialogue-objects
                Destroy(dialogue_box);
                Destroy(sprite_box);

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

        //GameObject[] test = (GameObject[]) utility.create_ui_object(typeof(Image), 
        //                     2, new string[] { "test" , "test1"}, new Vector2[] { new Vector2(10f, 10f), new Vector2(10f, 10f)}, 
        //                     new Vector3[] { new Vector3(10f, 10f, 10f), new Vector3(10f, 10f, 10f) },
        //                     new Quaternion[] { new Quaternion(), new Quaternion() });
    }

    private void FixedUpdate()
    {
        player_movement(10f, 400f);
    }
}
