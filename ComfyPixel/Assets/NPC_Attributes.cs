
using UnityEngine;
using UnityEngine.UI;

public class NPC_Attributes : MonoBehaviour {

    //NPC attributes, more will be added 
    public TextAsset Dialogue;
    public Sprite[] Sprites;

    private PlayerController playerController;
    private Utility utility;
    private Dialogue_System Dialogue_System;

    private void Update()
    {
        //keep adding changing sprites/other special events in this switch statement for the npc's that need it
        if(playerController.NPC_name == name)
        {
            switch (playerController.NPC_name)
            {
                case "NPC":
                    switch (utility.current_line)
                    {
                        case 0:
                            GameObject.Find("sprite_box").GetComponent<Image>().sprite = Sprites[0];
                            break;
                        case 1:
                            GameObject.Find("sprite_box").GetComponent<Image>().sprite = Sprites[1];
                            break;
                        case 3:
                            break;
                    }
                    break;
            }
        }
    }

    private void Awake()
    {
        utility = FindObjectOfType<Utility>();
        playerController = FindObjectOfType<PlayerController>();
    }
}
