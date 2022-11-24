using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerDialogue : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string[] dialogues;
    [SerializeField] private string[] characterName;
    public Text[] characterChats;
    [SerializeField] private Transform CharacterDialogue;

    void Start()
    {
        characterChats = new Text[3];
        CharacterDialogue = GameObject.Find("systemGUI").transform.Find("playerInteractiveChat").transform.Find("characterDialogue").transform;

       // Debug.Log(CharacterDialogue.childCount);
     for(int i = 0; i < CharacterDialogue.childCount; i++)
     {
         characterChats[i] = CharacterDialogue.GetChild(i).transform.GetComponent<Text>();
        //  characterChats[i].text = "<color="+ color +"><b> [" + characterName[i] + "] </b></color> This is functional";
         StartCoroutine(FadeOutTheText(characterChats[i],0f));
      }

       // DisplayDialogue(2,"purple", 0, dialogues);
    }

    // Update is called once per frame
    void Update()
    {
   
    }
    public void DisplayDialogue(int dialogueCounts, string color, int names,string[] dialogues)
    {
        for (int i = (dialogueCounts -1); i >= 0; --i)
        {
           // Debug.Log(i);
            //characterChats[i] = CharacterDialogue.GetChild(i).transform.GetComponent<Text>();
            characterChats[i].text = "<color=" + color + "><b> [" + characterName[names] + "] </b></color> " + dialogues[i];
            if (i == 0) { StartCoroutine(FadeInTheText(characterChats[i], 2f)); StartCoroutine(FadeOutTheText(characterChats[i], 3f * ((i + 1) * 2f))); }
            else if (i == 1) { StartCoroutine(FadeInTheText(characterChats[i], 3f)); StartCoroutine(FadeOutTheText(characterChats[i], 3f * ((i + 1) * 2f))); }
            else if (i == 2) { StartCoroutine(FadeInTheText(characterChats[i], 4f)); StartCoroutine(FadeOutTheText(characterChats[i], 3f * ((i + 1) * 2f))); }
        }
    }

    public IEnumerator FadeOutTheText(Text text, float second)
    {
        yield return new WaitForSeconds(second);
        text.CrossFadeAlpha(0,2f, false);
        StopCoroutine(FadeOutTheText(text, second));
    }
    public IEnumerator FadeInTheText(Text text, float second)
    {
        yield return new WaitForSeconds(second);
        text.CrossFadeAlpha(1, 0.1f, false);
        StopCoroutine(FadeInTheText(text, second));
    }
}
