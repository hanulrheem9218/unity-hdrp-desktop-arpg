using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class maskItemObject : MonoBehaviour
{
    // Start is called before the first frame update
    // physical object setting
    [SerializeField] playerStatusData DATA_player;
    [SerializeField] maskData DATA_mask;
    [SerializeField] Transform itemDisplayUI;
    [SerializeField] Transform mainCamera;
    [SerializeField] GameObject displayUIObject;
    [SerializeField] bool isPlayerDetected;
    [SerializeField] Transform itemObject;
    [SerializeField] float itemDetectionRange;
    [SerializeField] LayerMask player;
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
    //[SerializeField] string[] maskItemLists;
    [SerializeField] int maskAmounts;
    [SerializeField] string[] existedMaskItems;
    [SerializeField] int maskPosition; // this is security key
    [SerializeField] string currentMaskName;
    void Start()
    {
        existedMaskItems = new string[] { "a", "b" ,"c","d","e","f","g","h"};
        maskPosition = DATA_mask.maskPosition;
        maskAmounts = existedMaskItems.Length;
        for(int i = 0; i < maskAmounts; i++)
        {
            if (existedMaskItems[i] == DATA_mask.maskName)
            {
                currentMaskName = DATA_mask.maskName;
                itemObject.name = DATA_mask.maskName;
            
                Debug.Log("item existed");

            }
            else Debug.Log("Not Found");
        }
      //  maskItemLists = new string[maskAmounts];
        mainCamera = GameObject.FindWithTag("MainCamera").transform;
        itemDisplayUI = gameObject.transform.Find("Canvas").transform;
        Transform gui_player = GameObject.Find("systemGUI").transform;
        // these are spaown points
        maskPosition = DATA_mask.maskPosition;
        mainMaskItemPosition0 = gui_player.transform.Find("SYSTEM_playerInventory").transform.Find("CharacterView").Find("SYSTEM_mainMask").transform.Find("mask0").transform;
        mainMaskItemPosition1 = gui_player.transform.Find("SYSTEM_playerInventory").transform.Find("CharacterView").Find("SYSTEM_mainMask").transform.Find("mask1").transform;
        mainMaskItemPosition2 = gui_player.transform.Find("SYSTEM_playerInventory").transform.Find("CharacterView").Find("SYSTEM_mainMask").transform.Find("mask2").transform;
        mainMaskItemPosition3 = gui_player.transform.Find("SYSTEM_playerInventory").transform.Find("CharacterView").Find("SYSTEM_mainMask").transform.Find("mask3").transform;
        previewMaskFragment0 = gui_player.transform.Find("maskFragments").transform.Find("mask0").GetChild(0).GetComponent<Image>();
        previewMaskFragment1 = gui_player.transform.Find("maskFragments").transform.Find("mask1").GetChild(0).GetComponent<Image>();
        previewMaskFragment2 = gui_player.transform.Find("maskFragments").transform.Find("mask2").GetChild(0).GetComponent<Image>();
        previewMaskFragment3 = gui_player.transform.Find("maskFragments").transform.Find("mask3").GetChild(0).GetComponent<Image>();
        // just normal item
        itemMaskItemPosition = gui_player.transform.Find("SYSTEM_playerInventory").Find("SPAWN_itemSlots");
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerDetected = Physics.CheckSphere(itemObject.transform.position, itemDetectionRange, player);
        itemDisplayUI.LookAt(itemDisplayUI.transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
        if(isPlayerDetected)
        {
            displayUIObject.SetActive(true);
        }
        else if(!isPlayerDetected)
        {
            displayUIObject.SetActive(false);
        }
    }
    public void OnMouseEnter()
    {
        displayUIObject.SetActive(true);
    }
    public void OnMouseExit()
    {
        displayUIObject.SetActive(false);
    }
    public void createObjectUI()
    {
        // check all positions this is only for main masks. position 0
        if((maskPosition == 0 && currentMaskName != null) && mainMaskItemPosition0.childCount > 0)
        {
            // if it exists. this should go normal item lists
          GameObject objectNamePos0 = Instantiate(DATA_mask.maskItemUI, itemMaskItemPosition) as GameObject;
            objectNamePos0.name = currentMaskName;

            InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();

          //  playerDATA.maskCoolTime0 = DATA_mask.maskCoolTime;
            Destroy(gameObject);
        }
        else if(maskPosition == 0 && currentMaskName != null)
        {
            // if is empty. EQUIPED
            // Check MaskData.
        
            GameObject objectMainName0 = Instantiate(DATA_mask.mainMaskItemUI, mainMaskItemPosition0) as GameObject;
            objectMainName0.name = currentMaskName;

            InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();
   
         //   playerDATA.maskCoolTime0 = DATA_mask.maskCoolTime;
            previewMaskFragment0.sprite = DATA_mask.maskImage;
            Destroy(gameObject);
        }
      
        if ((maskPosition == 1 && currentMaskName != null) && mainMaskItemPosition1.childCount > 0)
        {
            // if it exists. this should go normal item lists
            GameObject objectNamePos1 = Instantiate(DATA_mask.maskItemUI, itemMaskItemPosition) as GameObject;
            objectNamePos1.name = currentMaskName;

            InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();

         //   playerDATA.maskCoolTime1 = DATA_mask.maskCoolTime;
            Destroy(gameObject);
        }
        else if (maskPosition == 1 && currentMaskName != null)
        {
            // if is empty.   // Check MaskData.
      
            GameObject objectMainName1 = Instantiate(DATA_mask.mainMaskItemUI, mainMaskItemPosition1) as GameObject;
            objectMainName1.name = currentMaskName;

            InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();

          //  playerDATA.maskCoolTime1 = DATA_mask.maskCoolTime;
            previewMaskFragment1.sprite = DATA_mask.maskImage;
            Destroy(gameObject);
        }
     
        if ((maskPosition == 2 && currentMaskName != null) && mainMaskItemPosition2.childCount > 0)
        {
            // if it exists. this should go normal item lists
            GameObject objectName2 = Instantiate(DATA_mask.maskItemUI, itemMaskItemPosition) as GameObject;
            objectName2.name = currentMaskName;

            InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();
       
           // playerDATA.maskCoolTime2 = DATA_mask.maskCoolTime;
            Destroy(gameObject);
        }
        else if (maskPosition == 2 && currentMaskName != null)
        {
            // if is empty.
            // Check MaskData.
        
            GameObject objectMainName2 = Instantiate(DATA_mask.mainMaskItemUI, mainMaskItemPosition2) as GameObject;
            objectMainName2.name = currentMaskName;

            InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();
 
          //  playerDATA.maskCoolTime2 = DATA_mask.maskCoolTime;
            previewMaskFragment2.sprite = DATA_mask.maskImage;
            Destroy(gameObject);
        }
    
        if ((maskPosition == 3 && currentMaskName != null) && mainMaskItemPosition3.childCount > 0)
        {
            // if it exists. this should go normal item lists
            GameObject objectName3 = Instantiate(DATA_mask.maskItemUI, itemMaskItemPosition) as GameObject;
            objectName3.name = currentMaskName;

            InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();

         //   playerDATA.maskCoolTime3 = DATA_mask.maskCoolTime;
            Destroy(gameObject);
        }
        else if (maskPosition == 3 && currentMaskName != null)
        {
            // if is empty.
            // Check MaskData.
         
            GameObject objectMainName3 = Instantiate(DATA_mask.mainMaskItemUI, mainMaskItemPosition3) as GameObject;
            objectMainName3.name = currentMaskName;

            InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();

          //  playerDATA.maskCoolTime3 = DATA_mask.maskCoolTime;
            previewMaskFragment3.sprite = DATA_mask.maskImage;
            Destroy(gameObject);
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(itemObject.transform.position, itemDetectionRange);
    }
}
