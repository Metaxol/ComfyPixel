
using UnityEngine;

public class Dialogue_System : MonoBehaviour {

    private PlayerController playerController;
    private Utility utility;

    private void Awake()
    {
        utility = FindObjectOfType<Utility>();
        playerController = FindObjectOfType<PlayerController>();
    }
}
