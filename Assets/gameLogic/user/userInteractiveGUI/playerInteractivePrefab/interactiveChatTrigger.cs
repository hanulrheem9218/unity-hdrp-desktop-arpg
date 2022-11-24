using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactiveChatTrigger : MonoBehaviour
{
    [SerializeField] string[] eventDialogues;
    [SerializeField] string[] eventDialogues1;
    [SerializeField] string[] eventDialogues2;
    [SerializeField] string[] eventDialogues3;
    [SerializeField] string[] eventDialogues4;
    [SerializeField] string[] eventDialogues5;

    [Header("Hanna, Jack, DeathSolo")]
    [SerializeField] int characterNames;
    [SerializeField] string characterColor;
    [SerializeField] playerDialogue dialogueView;
    [SerializeField] bool isPlayerDetected;
    [SerializeField] Transform currentObject;
    [SerializeField] Vector3 detectionField;
    [SerializeField] LayerMask playerMask;

    [SerializeField] bool isInteractive;
    private void Start()
    {
        dialogueView = GameObject.Find("GUI_player").transform.Find("playerInteractiveChat").transform.Find("characterDialogue").transform.GetComponent<playerDialogue>(); 
    }
    private void Update()
    {
        isPlayerDetected = Physics.CheckBox(currentObject.transform.position, detectionField, Quaternion.identity, playerMask);
        if(isPlayerDetected && !isInteractive)
        {
            StartCoroutine(multipleDialogues(22));
            isInteractive = true;
        }
    }
    public IEnumerator multipleDialogues(float second)
    {
        for(int i = 0; i <= 6; i++)
        {
            
            if (i == 0)dialogueView.DisplayDialogue(eventDialogues.Length, characterColor, characterNames, eventDialogues);
            else if(i == 1) dialogueView.DisplayDialogue(3, characterColor, characterNames, eventDialogues1);
            else if(i == 2) dialogueView.DisplayDialogue(3, characterColor, characterNames, eventDialogues2);
            else if(i == 3) dialogueView.DisplayDialogue(3, characterColor, characterNames, eventDialogues3);
            else if(i == 4) dialogueView.DisplayDialogue(3, characterColor, characterNames, eventDialogues4);
            else if(i == 5) dialogueView.DisplayDialogue(3, characterColor, characterNames, eventDialogues5);
            yield return new WaitForSeconds(second);
        }
        StopCoroutine(multipleDialogues(second));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(currentObject.transform.position, detectionField * 2);
    }
}
