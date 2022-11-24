using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class consumableItemObject : MonoBehaviour
{
    // Start is called before the first frame update
    // physical object setting
    [SerializeField] playerStatusData DATA_player;
    [SerializeField] itemData DATA_item;
    [SerializeField] Transform itemDisplayUI;
    [SerializeField] Transform mainCamera;
    [SerializeField] GameObject displayUIObject;
    [SerializeField] bool isPlayerDetected;
    [SerializeField] Transform itemObject;
    [SerializeField] float itemDetectionRange;
    [SerializeField] LayerMask player;
    // Positions this is can be changed due to item positons.
    [SerializeField] Transform mainItemPosition0;
    [SerializeField] Transform mainItemPosition1;

    [SerializeField] Transform ItemPosition;
    public Image previewItem0;
    public Image previewItem1;

    //[SerializeField] string[] maskItemLists;
    [SerializeField] int itemAmounts;
    [SerializeField]
    string[] existedPotionItems = {   "h", "m"  };
    public int itemPosition; // this is security key
    [SerializeField] string currentItemName;

    [SerializeField] bool isMouseOn;
    void Start()
    {
        itemObject = gameObject.transform;
        displayUIObject = itemObject.transform.Find("Canvas").gameObject;
        itemPosition = DATA_item.potionPosition;
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
        //  maskItemLists = new string[maskAmounts];
        mainCamera = GameObject.FindWithTag("MainCamera").transform;
        itemDisplayUI = gameObject.transform.Find("Canvas").transform;
        Transform gui_player = GameObject.Find("systemGUI").transform;
        // these are spaown points
        itemPosition = DATA_item.potionPosition;
        mainItemPosition0 = gui_player.transform.Find("SYSTEM_playerInventory").Find("SYSTEM_mainPotion").transform.Find("mainPotion0").transform;
        mainItemPosition1 = gui_player.transform.Find("SYSTEM_playerInventory").Find("SYSTEM_mainPotion").transform.Find("mainPotion1").transform;
       // mainMaskItemPosition2 = gui_player.transform.Find("SYSTEM_playerInventory").Find("SYSTEM_mainMask").transform.Find("mask2").transform;
       // mainMaskItemPosition3 = gui_player.transform.Find("SYSTEM_playerInventory").Find("SYSTEM_mainMask").transform.Find("mask3").transform;
        previewItem0 = gui_player.transform.Find("playerManaPotions").transform.Find("Image").GetComponent<Image>();
        previewItem1 = gui_player.transform.Find("playerHealthPotions").transform.Find("Image").GetComponent<Image>();


        // just normal item
        ItemPosition = gui_player.transform.Find("SYSTEM_playerInventory").Find("SPAWN_potionSlots");
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerDetected = Physics.CheckSphere(itemObject.transform.position, itemDetectionRange, player);
        itemDisplayUI.LookAt(itemDisplayUI.transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
        if (isPlayerDetected)
        {
            if (!isMouseOn)
            {
                displayUIObject.SetActive(true);
            }
        }
        else if (!isPlayerDetected)
        {
            if (!isMouseOn)
            {
                displayUIObject.SetActive(false);

            }
        }
    }
    public void OnMouseEnter()
    {
        isMouseOn = true;
        displayUIObject.SetActive(true);
    }
    public void OnMouseExit()
    {
        isMouseOn = false;
        displayUIObject.SetActive(false);
    }
    public void createObjectUI()
    {
        // check all positions this is only for main masks. position 0
        if ((itemPosition == 0 && currentItemName != null) && mainItemPosition0.childCount > 0)
        {
            // if it exists. this should go normal item lists
            GameObject objectNamePos0 = Instantiate(DATA_item.potionItemUI, ItemPosition) as GameObject;
            objectNamePos0.name = currentItemName;
            // passing data
            InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();
            playerDATA.perUseHealth = DATA_item.perHealth;
            playerDATA.healthName = DATA_item.potionName;
            Destroy(gameObject);
        }
        else if (itemPosition == 0 && currentItemName != null)
        {
            // if is empty. EQUIPED
            GameObject objectMainName0 = Instantiate(DATA_item.mainPotionItemUI, mainItemPosition0) as GameObject;
            objectMainName0.name = currentItemName;
            previewItem0.sprite = DATA_item.potionImage;
            // passing data
            InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();
            playerDATA.perUseHealth = DATA_item.perHealth;
            playerDATA.healthName = DATA_item.potionName;
            Destroy(gameObject);
        }

        if ((itemPosition == 1 && currentItemName != null) && mainItemPosition1.childCount > 0)
        {
            // if it exists. this should go normal item lists
            GameObject objectNamePos1 = Instantiate(DATA_item.potionItemUI, ItemPosition) as GameObject;
            objectNamePos1.name = currentItemName;
            // passing data
            InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();
            playerDATA.perUseMana = DATA_item.perMana;
            playerDATA.manaName = DATA_item.potionName;
            Destroy(gameObject);
        }
        else if (itemPosition == 1 && currentItemName != null)
        {
            // if is empty.
            GameObject objectMainName1 = Instantiate(DATA_item.mainPotionItemUI, mainItemPosition1) as GameObject;
            objectMainName1.name = currentItemName;
            previewItem1.sprite = DATA_item.potionImage;

            InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();
            playerDATA.perUseMana = DATA_item.perMana;
            playerDATA.manaName = DATA_item.potionName;
            Destroy(gameObject);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(itemObject.transform.position, itemDetectionRange);
    }
}
