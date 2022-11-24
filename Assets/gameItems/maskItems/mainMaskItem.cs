using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class mainMaskItem : MonoBehaviour
{
    public maskData DATA_mask;
    public Transform itemObject;
    [SerializeField] float dropForce;

    //this must be filled up
    [SerializeField] int maskAmounts;
    [SerializeField] string[] existedMaskItems;
    [SerializeField] int maskPosition;
    [SerializeField] string currentMaskName;
    [SerializeField] int optionToggle;
    // Start is called before the first frame update
    void Start()
    {
        existedMaskItems = new string[] { "a", "b", "c", "d", "e", "f", "g", "h" };
        itemObject = gameObject.transform;
        maskPosition = DATA_mask.maskPosition;
        GameObject optionPanel = itemObject.transform.Find("Panel").gameObject;
        optionPanel.SetActive(false);
        // System mask check.
        maskAmounts = existedMaskItems.Length;
        for (int i = 0; i < maskAmounts; ++i)
        {
            if (existedMaskItems[i] == DATA_mask.maskName)
            {
                currentMaskName = DATA_mask.maskName;
                itemObject.name = DATA_mask.maskName;
                Debug.Log("item existed");

            }
            else Debug.Log("Not Found");
        }

    }
    public void showOptions()
    {
        GameObject optionPanel = itemObject.transform.Find("Panel").gameObject;
        if (optionToggle == 0)
        {
            optionToggle++;
            optionPanel.SetActive(true);
        }
        else if(optionToggle == 1)
        {
            optionToggle++;
            optionPanel.SetActive(false);
            if(optionToggle == 2)
            {
                optionToggle = 0;
            }
        }
     
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void dropMaskItem()
    {
        

        Transform Player = GameObject.Find("player").transform.Find("itemDrop").transform;
        GameObject maskPrefab = Instantiate(DATA_mask.maskItemObject, Player) as GameObject;

        Player.DetachChildren();
        Rigidbody maskPrefabRigidBody = maskPrefab.transform.GetComponent<Rigidbody>();
        float randomDropFroce = Random.Range(-20, 20);
        float randomDropFroceMinus = Random.Range(-70, 70);

        maskPrefabRigidBody.AddForce((Vector3.up * 50) + (Vector3.forward * randomDropFroce) + (Vector3.right * randomDropFroceMinus), ForceMode.Acceleration);
        InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();
        //  playerDATA.CheckAllMasks();
        Transform maskFragments = GameObject.Find("GUI_player").transform.Find("maskFragments").transform;
        Image mask0 = maskFragments.Find("mask0").transform.Find("Image").GetComponent<Image>();
        Image mask1 = maskFragments.Find("mask1").transform.Find("Image").GetComponent<Image>();
        Image mask2 = maskFragments.Find("mask2").transform.Find("Image").GetComponent<Image>();
        Image mask3 = maskFragments.Find("mask3").transform.Find("Image").GetComponent<Image>();
        if (maskPosition == 0)
        {
         //   playerDATA.Mask0 = "";
          //  mask0.sprite = DATA_mask.defaultEmptyImage;
        }
        else if (maskPosition == 1)
        {
         //   playerDATA.Mask1 = "";
         //   mask1.sprite = DATA_mask.defaultEmptyImage;
        }
        else if (maskPosition == 2)
        {
        //    playerDATA.Mask2 = "";
        //    mask2.sprite = DATA_mask.defaultEmptyImage;
        }
        else if (maskPosition == 3)
        {
         //   playerDATA.Mask3 = "";
//mask3.sprite = DATA_mask.defaultEmptyImage;
        }
        Destroy(gameObject);

    }
}
