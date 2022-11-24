using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseCursor : MonoBehaviour
{
    // Start is called before the first frame update
   public InputSystem playerUserInput;

    public void Start()
    {
        playerUserInput = FindObjectOfType<InputSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("reached");
            playerUserInput.isMouseInCursor = false;
            playerUserInput.isPlayerMoving = false;
          //  playerUserInput.isMouseHold = 0;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        // additional bug fixes
        if (other.tag == "Player")
        {
            Debug.Log("reached");
            playerUserInput.isMouseInCursor = false;
            playerUserInput.isPlayerMoving = false;
            //playerUserInput.isMouseHold = 0;
        }
    }
}
