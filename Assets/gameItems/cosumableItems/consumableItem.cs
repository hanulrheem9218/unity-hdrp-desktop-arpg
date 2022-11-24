using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class consumableItem : MonoBehaviour
{
    [SerializeField] itemData DATA_item;
    [SerializeField] Transform itemObject;
    [SerializeField] float dropForce;

    //this must be filled up
    [SerializeField] int itemAmounts;
    [SerializeField]
    string[] existedPotionItems =
        {
        "m", "h"
    };
    [SerializeField] int potionPosition;
    [SerializeField] string currentItemName;
    [SerializeField] int optionToggle;

    // Positions
    [SerializeField] Transform mainItemPosition0;
    [SerializeField] Transform mainItemPosition1;
    //all items
    [SerializeField] Transform ItemPosition;

    [SerializeField] Image previewItem0;
    [SerializeField] Image previewItem1;

    void Start()
    {
        itemObject = gameObject.transform;
        Transform gui_player = GameObject.Find("GUI_player").transform;
        potionPosition = DATA_item.potionPosition;
        mainItemPosition0 = gui_player.transform.Find("SYSTEM_playerInventory").Find("SYSTEM_mainPotion").transform.Find("mainPotion0").transform;
        mainItemPosition1 = gui_player.transform.Find("SYSTEM_playerInventory").Find("SYSTEM_mainPotion").transform.Find("mainPotion1").transform;
        // mainMaskItemPosition2 = gui_player.transform.Find("SYSTEM_playerInventory").Find("SYSTEM_mainMask").transform.Find("mask2").transform;
        // mainMaskItemPosition3 = gui_player.transform.Find("SYSTEM_playerInventory").Find("SYSTEM_mainMask").transform.Find("mask3").transform;
        previewItem0 = gui_player.transform.Find("playerManaPotions").transform.Find("Image").GetComponent<Image>();
        previewItem1 = gui_player.transform.Find("playerHealthPotions").transform.Find("Image").GetComponent<Image>();


        // just normal item
        ItemPosition = gui_player.transform.Find("SYSTEM_playerInventory").Find("SPAWN_potionSlots");

        GameObject optionPanel = itemObject.transform.Find("Panel").gameObject;
        optionPanel.SetActive(false);
        // System mask check.
        itemAmounts = existedPotionItems.Length;
        for (int i = 0; i < itemAmounts; ++i)
        {
            if (existedPotionItems[i] == DATA_item.potionName)
            {
                currentItemName = DATA_item.potionName;
                itemObject.name = DATA_item.potionName;


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
        if ((potionPosition == 0 && currentItemName != null) && mainItemPosition0.childCount > 0)
        { // if its exists
            // other object value
            consumableMainItem existedItemPos0 = mainItemPosition0.GetChild(0).GetComponent<consumableMainItem>();
            Instantiate(existedItemPos0.DATA_item.potionItemUI, ItemPosition);
            Destroy(existedItemPos0.itemObject.gameObject);

            // current object value
            Instantiate(DATA_item.mainPotionItemUI, mainItemPosition0);
            previewItem0.sprite = DATA_item.potionImage;
            Destroy(gameObject);
        }
        else if (potionPosition == 0 && currentItemName != null)
        {
            // if object is empty
            Instantiate(DATA_item.mainPotionItemUI, mainItemPosition0);
            previewItem0.sprite = DATA_item.potionImage;
            Destroy(gameObject);
        }
        // 2nd options
        if ((potionPosition == 1 && currentItemName != null) && mainItemPosition1.childCount > 0)
        { // if its exists
            // other object value
            consumableMainItem existedItemPos1 = mainItemPosition1.GetChild(0).GetComponent<consumableMainItem>();
            Instantiate(existedItemPos1.DATA_item.potionItemUI, ItemPosition);
            Destroy(existedItemPos1.itemObject.gameObject);

            // current object value
            Instantiate(DATA_item.mainPotionItemUI, mainItemPosition1);
            previewItem1.sprite = DATA_item.potionImage;
            Destroy(gameObject);
        }
        else if (potionPosition == 1 && currentItemName != null)
        {
            // if object is empty
            Instantiate(DATA_item.mainPotionItemUI, mainItemPosition1);
            previewItem1.sprite = DATA_item.potionImage;
            Destroy(gameObject);
        }

    }
    public void dropMaskItem()
    {
        Transform Player = GameObject.Find("player").transform.Find("itemDrop").transform;
        GameObject maskPrefab = Instantiate(DATA_item.potionItemObject, Player) as GameObject;
        Player.DetachChildren();
        Rigidbody maskPrefabRigidBody = maskPrefab.transform.GetComponent<Rigidbody>();
        float randomDropFroce = Random.Range(-20, 20);
        float randomDropFroceMinus = Random.Range(-20, 20);
        
        maskPrefabRigidBody.AddForce((Vector3.up * 50) + (Vector3.forward * randomDropFroce) + (Vector3.right * randomDropFroceMinus), ForceMode.Acceleration);

        InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();
        playerDATA.healthName = "";

        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
