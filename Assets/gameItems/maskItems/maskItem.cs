using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class maskItem : MonoBehaviour
{
    [SerializeField] maskData DATA_mask;
    [SerializeField] Transform itemObject;
    [SerializeField] float dropForce;

    //this must be filled up
    [SerializeField] int maskAmounts;
    [SerializeField] string[] existedMaskItems;
    [SerializeField] int maskPosition;
    [SerializeField] string currentMaskName;
    [SerializeField] int optionToggle;

    // Positions
    [SerializeField] Transform mainMaskItemPosition0;
    [SerializeField] Transform mainMaskItemPosition1;
    [SerializeField] Transform mainMaskItemPosition2;
    [SerializeField] Transform mainMaskItemPosition3;
    [SerializeField] Transform itemMaskItemPosition;

    [SerializeField] Image previewMaskFragment0;
    [SerializeField] Image previewMaskFragment1;
    [SerializeField] Image previewMaskFragment2;
    [SerializeField] Image previewMaskFragment3;
    void Start()
    {
        existedMaskItems = new string[] { "a", "b", "c", "d", "e", "f", "g", "h" };
        itemObject = gameObject.transform;
        Transform gui_player = GameObject.Find("GUI_player").transform;
        maskPosition = DATA_mask.maskPosition;
        mainMaskItemPosition0 = gui_player.transform.Find("SYSTEM_playerInventory").Find("SYSTEM_mainMask").transform.Find("mask0").transform;
        mainMaskItemPosition1 = gui_player.transform.Find("SYSTEM_playerInventory").Find("SYSTEM_mainMask").transform.Find("mask1").transform;
        mainMaskItemPosition2 = gui_player.transform.Find("SYSTEM_playerInventory").Find("SYSTEM_mainMask").transform.Find("mask2").transform;
        mainMaskItemPosition3 = gui_player.transform.Find("SYSTEM_playerInventory").Find("SYSTEM_mainMask").transform.Find("mask3").transform;
        previewMaskFragment0 = gui_player.transform.Find("maskFragments").transform.Find("mask0").GetChild(0).GetComponent<Image>();
        previewMaskFragment1 = gui_player.transform.Find("maskFragments").transform.Find("mask1").GetChild(0).GetComponent<Image>();
        previewMaskFragment2 = gui_player.transform.Find("maskFragments").transform.Find("mask2").GetChild(0).GetComponent<Image>();
        previewMaskFragment3 = gui_player.transform.Find("maskFragments").transform.Find("mask3").GetChild(0).GetComponent<Image>();

        itemMaskItemPosition = gui_player.transform.Find("SYSTEM_playerInventory").Find("SPAWN_itemSlots");

        GameObject optionPanel = itemObject.transform.Find("maskOptions").gameObject;
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
        GameObject optionPanel = itemObject.transform.Find("maskOptions").gameObject;
        if (optionToggle == 0)
        {
            optionToggle++;
            optionPanel.SetActive(true);
        }
        else if (optionToggle == 1)
        {
            optionToggle++;
            optionPanel.SetActive(false);
            if (optionToggle == 2)
            {
                optionToggle = 0;
            }
        }

    }
    public void switchMasks()
    {
        if ((maskPosition == 0 && currentMaskName != null) && mainMaskItemPosition0.childCount > 0)
        { // if its exists
          // other object value
            InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();

            mainMaskItem existedItemPos0 = mainMaskItemPosition0.GetChild(0).GetComponent<mainMaskItem>();
            Instantiate(existedItemPos0.DATA_mask.maskItemUI, itemMaskItemPosition);
            Destroy(existedItemPos0.itemObject.gameObject);
        
            // current object value
            Instantiate(DATA_mask.mainMaskItemUI, mainMaskItemPosition0);
            previewMaskFragment0.sprite = DATA_mask.maskImage;


         //   playerDATA.Mask0 = DATA_mask.maskName;
            Destroy(gameObject);
        }
        else if (maskPosition == 0 && currentMaskName != null)
        {
     
            // if object is empty
            Instantiate(DATA_mask.mainMaskItemUI, mainMaskItemPosition0);
            previewMaskFragment0.sprite = DATA_mask.maskImage;

            InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();

         //   playerDATA.Mask0 = DATA_mask.maskName;
            Destroy(gameObject);
        }
        // 2nd options
        if ((maskPosition == 1 && currentMaskName != null) && mainMaskItemPosition1.childCount > 0)
        { // if its exists
            // other object value

            mainMaskItem existedItemPos1 = mainMaskItemPosition1.GetChild(0).GetComponent<mainMaskItem>();
            Instantiate(existedItemPos1.DATA_mask.maskItemUI, itemMaskItemPosition);


            Destroy(existedItemPos1.itemObject.gameObject);

            // current object value
            Instantiate(DATA_mask.mainMaskItemUI, mainMaskItemPosition1);
            previewMaskFragment1.sprite = DATA_mask.maskImage;

            InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();
         //   playerDATA.Mask1 = DATA_mask.maskName;
            Destroy(gameObject);
        }
        else if (maskPosition == 1 && currentMaskName != null)
        {
            // if object is empty
            Instantiate(DATA_mask.mainMaskItemUI, mainMaskItemPosition1);
            previewMaskFragment1.sprite = DATA_mask.maskImage;

            InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();
          //  playerDATA.Mask1 = DATA_mask.maskName;
            Destroy(gameObject);
        }

        if ((maskPosition == 2 && currentMaskName != null) && mainMaskItemPosition2.childCount > 0)
        { // if its exists
            // other object value
            mainMaskItem existedItemPos2 = mainMaskItemPosition2.GetChild(0).GetComponent<mainMaskItem>();
            Instantiate(existedItemPos2.DATA_mask.maskItemUI, itemMaskItemPosition);

  
            Destroy(existedItemPos2.itemObject.gameObject);

            // current object value
            Instantiate(DATA_mask.mainMaskItemUI, mainMaskItemPosition2);
            previewMaskFragment2.sprite = DATA_mask.maskImage;

            InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();
           // playerDATA.Mask2 = DATA_mask.maskName;
            Destroy(gameObject);
        }
        else if (maskPosition == 2 && currentMaskName != null)
        {
            // if object is empty
            Instantiate(DATA_mask.mainMaskItemUI, mainMaskItemPosition2);
            previewMaskFragment2.sprite = DATA_mask.maskImage;

            InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();
         //   playerDATA.Mask2 = DATA_mask.maskName;
            Destroy(gameObject);
        }
        if ((maskPosition == 3 && currentMaskName != null) && mainMaskItemPosition3.childCount > 0)
        { // if its exists
            // other object value
            mainMaskItem existedItemPos3 = mainMaskItemPosition3.GetChild(0).GetComponent<mainMaskItem>();
            Instantiate(existedItemPos3.DATA_mask.maskItemUI, itemMaskItemPosition);

  
            Destroy(existedItemPos3.itemObject.gameObject);

            // current object value
            Instantiate(DATA_mask.mainMaskItemUI, mainMaskItemPosition3);
            previewMaskFragment3.sprite = DATA_mask.maskImage;
            InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();
        //    playerDATA.Mask3 = DATA_mask.maskName;
            Destroy(gameObject);
        }
        else if (maskPosition == 3 && currentMaskName != null)
        {
            // if object is empty
            Instantiate(DATA_mask.mainMaskItemUI, mainMaskItemPosition3);
            previewMaskFragment3.sprite = DATA_mask.maskImage;

            InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();
         //   playerDATA.Mask3 = DATA_mask.maskName;
            Destroy(gameObject);
        }
    }
    public void dropMaskItem()
    {
      

        Transform Player = GameObject.Find("player").transform.Find("itemDrop").transform;
        GameObject maskPrefab = Instantiate(DATA_mask.maskItemObject, Player) as GameObject;
        Player.DetachChildren();
        Rigidbody maskPrefabRigidBody = maskPrefab.transform.GetComponent<Rigidbody>();
        float randomDropFroce = Random.Range(-20, 20);
        float randomDropFroceMinus = Random.Range(-20, 20);

        maskPrefabRigidBody.AddForce((Vector3.up * 50) + (Vector3.forward * randomDropFroce) + (Vector3.right * randomDropFroceMinus), ForceMode.Acceleration);
        //InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();
        ////  playerDATA.CheckAllMasks();
        //if (maskPosition == 0) playerDATA.Mask0 = "";
        //else if (maskPosition == 1) playerDATA.Mask1 = "";
        //else if (maskPosition == 2) playerDATA.Mask2 = "";
        //else if (maskPosition == 3) playerDATA.Mask3 = "";
        Destroy(gameObject);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
