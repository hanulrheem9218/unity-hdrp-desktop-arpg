using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SystemInventory : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    // Start is called before the first frame update
    public bool isInsideOfInventory;
    public List<string> maskItems;
    public List<string> armorItems;
    public List<string> items;
    public List<string> gunItems;
    // not necessary.
    public GameObject clickedObject;
    public RectTransform clickedRectTransform;
    public Sprite defaultImage;
    [SerializeField] Transform mainMask, mask;
    [SerializeField] Transform ManaShort, HealthShort,viewMask0, viewMask1, viewMask2, viewMask3;
    [SerializeField] Transform maskPartPreview0, maskPartPreview1, maskPartPreview2, maskPartPreview3;
    [SerializeField] Transform playerLevel, playerLife, playerDeffense, playerAttack;
    void Start()
    {
        // sample groups
        maskItems = new List<string> { "A", "B", "C", "D", "E", "F", "G" };
        armorItems = new List<string> { "aA", "aB", "aC", "aD", "aE", "aF", "aG" };
        items = new List<string> { "bA", "bB", "bC", "bD", "bE", "bF", "bG" };
        gunItems = new List<string> { "pistol" };
        clickedObject = null;
        viewMask0 = gameObject.transform.Find("maskFragments").Find("mask0").Find("Image").transform;
        viewMask1 = gameObject.transform.Find("maskFragments").Find("mask1").Find("Image").transform;
        viewMask2 = gameObject.transform.Find("maskFragments").Find("mask2").Find("Image").transform;
        viewMask3 = gameObject.transform.Find("maskFragments").Find("mask3").Find("Image").transform;
        ManaShort = gameObject.transform.Find("playerManaShort").Find("Image").transform;
        HealthShort = gameObject.transform.Find("playerHealthShort").Find("Image").transform;

        maskPartPreview0 = gameObject.transform.Find("SYSTEM_playerInventory").Find("MaskPreview").Find("maskPart01_preview").Find("Image");
        maskPartPreview1 = gameObject.transform.Find("SYSTEM_playerInventory").Find("MaskPreview").Find("maskPart02_preview").Find("Image");
        maskPartPreview2 = gameObject.transform.Find("SYSTEM_playerInventory").Find("MaskPreview").Find("maskPart03_preview").Find("Image");
        maskPartPreview3 = gameObject.transform.Find("SYSTEM_playerInventory").Find("MaskPreview").Find("maskPart04_preview").Find("Image");
        playerLevel = gameObject.transform.Find("SYSTEM_playerInventory").Find("PlayerStatus").Find("PlayerLevel").Find("status");
        playerLife = gameObject.transform.Find("SYSTEM_playerInventory").Find("PlayerStatus").Find("PlayerLife").Find("status");
        playerDeffense = gameObject.transform.Find("SYSTEM_playerInventory").Find("PlayerStatus").Find("PlayerDeffense").Find("status");
        playerAttack = gameObject.transform.Find("SYSTEM_playerInventory").Find("PlayerStatus").Find("PlayerAttack").Find("status");

        playerLevel.GetComponent<Text>().text = FindObjectOfType<InputSystem>().level.ToString(); 
        playerLife.GetComponent<Text>().text = FindObjectOfType<InputSystem>().life.ToString(); 
        playerDeffense.GetComponent<Text>().text = FindObjectOfType<InputSystem>().deffense.ToString(); 
        playerAttack.GetComponent<Text>().text = FindObjectOfType<InputSystem>().attack.ToString(); 
        // sample groups
        // check the first mask.
        if(gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask0").childCount > 0)
        {
            viewMask0.GetComponent<Image>().sprite = gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask0").GetChild(0).GetComponent<Image>().sprite;
            maskPartPreview0.GetComponent<Image>().sprite = gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask0").GetChild(0).GetComponent<Image>().sprite;
        }
        else
        {
            viewMask0.GetComponent<Image>().sprite = defaultImage;
            maskPartPreview0.GetComponent<Image>().sprite = defaultImage;
        }
        if (gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask1").childCount > 0)
        {
            viewMask1.GetComponent<Image>().sprite = gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask1").GetChild(0).GetComponent<Image>().sprite;
            maskPartPreview1.GetComponent<Image>().sprite = gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask1").GetChild(0).GetComponent<Image>().sprite;
        } else { viewMask1.GetComponent<Image>().sprite = defaultImage; maskPartPreview1.GetComponent<Image>().sprite = defaultImage; }
        if (gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask2").childCount > 0)
        {
            viewMask2.GetComponent<Image>().sprite = gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask2").GetChild(0).GetComponent<Image>().sprite;
            maskPartPreview2.GetComponent<Image>().sprite = gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask2").GetChild(0).GetComponent<Image>().sprite;
        } else { viewMask2.GetComponent<Image>().sprite = defaultImage; maskPartPreview2.GetComponent<Image>().sprite = defaultImage; }
        if (gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask3").childCount > 0)
        {
            viewMask3.GetComponent<Image>().sprite = gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask3").GetChild(0).GetComponent<Image>().sprite;
            maskPartPreview3.GetComponent<Image>().sprite = gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask3").GetChild(0).GetComponent<Image>().sprite;
        } else { viewMask3.GetComponent<Image>().sprite = defaultImage; maskPartPreview3.GetComponent<Image>().sprite = defaultImage; }

        if (gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainPotions").Find("mainPotionPart").childCount > 0)
        {
            ManaShort.GetComponent<Image>().sprite = gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainPotions").Find("mainPotionPart").GetChild(0).GetComponent<Image>().sprite;
        }
        else { ManaShort.GetComponent<Image>().sprite = defaultImage; }
        if (gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainPotions").Find("mainPotionHealth").childCount > 0)
        {
            HealthShort.GetComponent<Image>().sprite = gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainPotions").Find("mainPotionHealth").GetChild(0).GetComponent<Image>().sprite;
        }
        else { HealthShort.GetComponent<Image>().sprite = defaultImage; }
    }
    // Update is called once per frame

    public void OnPointerClick(PointerEventData e)
    { // when the object is clicked.
        e.pointerClick.transform.SendMessage("showOptions");
    }
    public void OnPointerEnter(PointerEventData e)
    {
        
        if (e.pointerCurrentRaycast.gameObject.tag == "SystemInventory" ) {  isInsideOfInventory = true;    }
        else if (e.pointerCurrentRaycast.gameObject.transform.tag == "InventorySlots") {  isInsideOfInventory = true; }
        else if (e.pointerCurrentRaycast.gameObject.transform.tag == "Item")  { isInsideOfInventory = true; }
        else if (e.pointerCurrentRaycast.gameObject.transform.tag == "Untagged")  { isInsideOfInventory = true; }
        //   Debug.Log("enter " + e.pointerCurrentRaycast.gameObject.name);
        // entering object ?
    }
    public void OnPointerExit(PointerEventData e)
    { 
        isInsideOfInventory = false;
    }
    public void OnBeginDrag(PointerEventData e)
    {
       Debug.Log("beginDrag");
        Debug.Log(e.pointerId);
        if (e.pointerCurrentRaycast.gameObject.transform.tag == "Item" && e.pointerEnter.gameObject.transform.tag == "Item") 
            {
                clickedObject = e.pointerEnter.transform.gameObject;
                clickedRectTransform = clickedObject.transform.GetComponent<RectTransform>();
                clickedObject.GetComponent<Image>().raycastTarget = true;
        }
            else
            {
                Debug.Log("tag is null");
            }
 
    }

    public void OnDrag(PointerEventData e)
    {

            if (clickedObject.gameObject.transform.tag == "Item" && e.dragging)
            {
               clickedObject.transform.position = e.position;
                 clickedObject.GetComponent<Image>().raycastTarget = false;
          
            }
  

    }

    public void OnEndDrag(PointerEventData e)
    {
        if (e.selectedObject != null)
        {
            if (!isInsideOfInventory && e.selectedObject.transform.tag == "Item")
            {

                clickedObject.transform.SendMessage("dropItem");
                clickedObject.transform.SendMessage("getDestroy");

            }
            else if (isInsideOfInventory && e.pointerCurrentRaycast.gameObject.transform.tag == "InventorySlots") {
                if (clickedObject.GetComponent<SystemItem>().maskPosition == 0 && clickedObject.GetComponent<SystemItem>().enableMaskPart && clickedObject.transform.parent.tag == "MainItems") { viewMask0.GetComponent<Image>().sprite = defaultImage; }
                else if (clickedObject.GetComponent<SystemItem>().maskPosition == 1 && clickedObject.GetComponent<SystemItem>().enableMaskPart && clickedObject.transform.parent.tag == "MainItems") { viewMask1.GetComponent<Image>().sprite = defaultImage; }
                else if (clickedObject.GetComponent<SystemItem>().maskPosition == 2 && clickedObject.GetComponent<SystemItem>().enableMaskPart && clickedObject.transform.parent.tag == "MainItems") { viewMask2.GetComponent<Image>().sprite = defaultImage; }
                else if (clickedObject.GetComponent<SystemItem>().maskPosition == 3 && clickedObject.GetComponent<SystemItem>().enableMaskPart && clickedObject.transform.parent.tag == "MainItems") { viewMask3.GetComponent<Image>().sprite = defaultImage; }
                else if (clickedObject.GetComponent<SystemItem>().itemPosition == 0 && clickedObject.GetComponent<SystemItem>().enableItem && clickedObject.transform.parent.tag == "MainPotions") { HealthShort.GetComponent<Image>().sprite = defaultImage; }
                else if (clickedObject.GetComponent<SystemItem>().itemPosition == 1 && clickedObject.GetComponent<SystemItem>().enableItem && clickedObject.transform.parent.tag == "MainPotions") { ManaShort.GetComponent<Image>().sprite = defaultImage; }
                setCurrentToEnter(e);
            }
            else if (isInsideOfInventory && e.pointerCurrentRaycast.gameObject.transform.name == "mask0" && clickedObject.GetComponent<SystemItem>().maskPosition == 0) { viewMask0.GetComponent<Image>().sprite = clickedObject.GetComponent<Image>().sprite; setCurrentToEnter(e); } // replacing slots
            else if (isInsideOfInventory && e.pointerCurrentRaycast.gameObject.transform.name == "mask1" && clickedObject.GetComponent<SystemItem>().maskPosition == 1) { viewMask1.GetComponent<Image>().sprite = clickedObject.GetComponent<Image>().sprite; setCurrentToEnter(e); }
            else if (isInsideOfInventory && e.pointerCurrentRaycast.gameObject.transform.name == "mask2" && clickedObject.GetComponent<SystemItem>().maskPosition == 2) { viewMask2.GetComponent<Image>().sprite = clickedObject.GetComponent<Image>().sprite; setCurrentToEnter(e); }
            else if (isInsideOfInventory && e.pointerCurrentRaycast.gameObject.transform.name == "mask3" && clickedObject.GetComponent<SystemItem>().maskPosition == 3) { viewMask3.GetComponent<Image>().sprite = clickedObject.GetComponent<Image>().sprite; setCurrentToEnter(e); }
            else if (isInsideOfInventory && e.pointerCurrentRaycast.gameObject.transform.name == "SYSTEM_mainHead" && clickedObject.GetComponent<SystemItem>().armorPosition == 0) { setCurrentToEnter(e); }
            else if (isInsideOfInventory && e.pointerCurrentRaycast.gameObject.transform.name == "SYSTEM_mainGloves" && clickedObject.GetComponent<SystemItem>().armorPosition == 1) { setCurrentToEnter(e); }
            else if (isInsideOfInventory && e.pointerCurrentRaycast.gameObject.transform.name == "SYSTEM_mainChest" && clickedObject.GetComponent<SystemItem>().armorPosition == 2) { setCurrentToEnter(e); }
            else if (isInsideOfInventory && e.pointerCurrentRaycast.gameObject.transform.name == "SYSTEM_mainPant" && clickedObject.GetComponent<SystemItem>().armorPosition == 3) { setCurrentToEnter(e); }
            else if (isInsideOfInventory && e.pointerCurrentRaycast.gameObject.transform.name == "SYSTEM_mainShoes" && clickedObject.GetComponent<SystemItem>().armorPosition == 4) { setCurrentToEnter(e); }
            else if (isInsideOfInventory && e.pointerCurrentRaycast.gameObject.transform.name == "mainPotionHealth" && clickedObject.GetComponent<SystemItem>().itemPosition == 0) { HealthShort.GetComponent<Image>().sprite = clickedObject.GetComponent<Image>().sprite; setCurrentToEnter(e); }
            else if (isInsideOfInventory && e.pointerCurrentRaycast.gameObject.transform.name == "mainPotionPart" && clickedObject.GetComponent<SystemItem>().itemPosition == 1) { ManaShort.GetComponent<Image>().sprite = clickedObject.GetComponent<Image>().sprite; setCurrentToEnter(e); }
            else if (e.pointerEnter.gameObject.transform.tag == "Untagged" || e.pointerEnter.gameObject.transform.tag != clickedObject.transform.gameObject.tag){ resetBack();   }
            else if (isInsideOfInventory && e.pointerCurrentRaycast.gameObject.transform.GetComponent<SystemItem>().maskPosition == clickedObject.GetComponent<SystemItem>().maskPosition && clickedObject.GetComponent<SystemItem>().enableMaskPart
                && e.pointerCurrentRaycast.gameObject.transform.parent.tag == "MainItems")
            {
                Debug.Log("worked??");
                if (e.pointerCurrentRaycast.gameObject.transform.GetComponent<SystemItem>().maskPosition == 0) {
                    viewMask0.GetComponent<Image>().sprite = clickedObject.GetComponent<Image>().sprite;
                    getSwitched(e.pointerEnter.transform, clickedObject.transform, gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask0"));
                  //  viewMask0.GetComponent<Image>().sprite = clickedObject.GetComponent<Image>().sprite;
                }
                else if (e.pointerCurrentRaycast.gameObject.transform.GetComponent<SystemItem>().maskPosition == 1) 
                {
                    viewMask1.GetComponent<Image>().sprite = clickedObject.GetComponent<Image>().sprite;
                    getSwitched(e.pointerEnter.transform, clickedObject.transform, gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask1")); }
                else if (e.pointerCurrentRaycast.gameObject.transform.GetComponent<SystemItem>().maskPosition == 2)
                {
                    viewMask2.GetComponent<Image>().sprite = clickedObject.GetComponent<Image>().sprite;
                    getSwitched(e.pointerEnter.transform, clickedObject.transform, gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask2")); }
                else if (e.pointerCurrentRaycast.gameObject.transform.GetComponent<SystemItem>().maskPosition == 3) 
                {
                    viewMask3.GetComponent<Image>().sprite = clickedObject.GetComponent<Image>().sprite;
                    getSwitched(e.pointerEnter.transform, clickedObject.transform, gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask3")); }
            }
            else if (isInsideOfInventory && e.pointerCurrentRaycast.gameObject.transform.GetComponent<SystemItem>().armorPosition == clickedObject.GetComponent<SystemItem>().armorPosition && clickedObject.GetComponent<SystemItem>().enableArmor
                && e.pointerCurrentRaycast.gameObject.transform.parent.tag == "MainArmorItems")
            {
                if (e.pointerCurrentRaycast.gameObject.transform.GetComponent<SystemItem>().armorPosition == 0) { getSwitched(e.pointerEnter.transform, clickedObject.transform, gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainHead")); } //hat
                else if (e.pointerCurrentRaycast.gameObject.transform.GetComponent<SystemItem>().armorPosition == 1) { getSwitched(e.pointerEnter.transform, clickedObject.transform, gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainGloves")); } // gloves
                else if (e.pointerCurrentRaycast.gameObject.transform.GetComponent<SystemItem>().armorPosition == 2) { getSwitched(e.pointerEnter.transform, clickedObject.transform, gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainChest")); } // shirt
                else if (e.pointerCurrentRaycast.gameObject.transform.GetComponent<SystemItem>().armorPosition == 3) { getSwitched(e.pointerEnter.transform, clickedObject.transform, gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainPant")); } // pants
                else if (e.pointerCurrentRaycast.gameObject.transform.GetComponent<SystemItem>().armorPosition == 4) { getSwitched(e.pointerEnter.transform, clickedObject.transform, gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainShoes")); } // shoes
            }
            else if (isInsideOfInventory && e.pointerCurrentRaycast.gameObject.transform.GetComponent<SystemItem>().itemPosition == clickedObject.GetComponent<SystemItem>().itemPosition && clickedObject.GetComponent<SystemItem>().enableItem
                && e.pointerCurrentRaycast.gameObject.transform.parent.tag == "MainPotions") 
            {
                if (e.pointerCurrentRaycast.gameObject.transform.GetComponent<SystemItem>().itemPosition == 0) 
                {
                    HealthShort.GetComponent<Image>().sprite = clickedObject.GetComponent<Image>().sprite;
                    getSwitched(e.pointerEnter.transform, clickedObject.transform, gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainPotions").Find("mainPotionHealth")); } // Health
                else if (e.pointerCurrentRaycast.gameObject.transform.GetComponent<SystemItem>().itemPosition == 1)
                { 
                    ManaShort.GetComponent<Image>().sprite = clickedObject.GetComponent<Image>().sprite;
                    getSwitched(e.pointerEnter.transform, clickedObject.transform, gameObject.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainPotions").Find("mainPotionPart")); } // Parts
            }
            // we need soemthibng for switch.
            
            else  // if it missese
            {
                Debug.Log("something miising");
                resetBack();
            }
            clickedObject = null;
        }
        else

        {
            resetBack();
        }
     
    }
    void getSwitched(Transform pointerEnter, Transform lastPress, Transform parent) 
    {
        mainMask = pointerEnter;
        mask = lastPress;
        mainMask.transform.SetParent(mask.parent);
        resetRectTrasnform(mainMask.transform.GetComponent<RectTransform>());
        mask.transform.SetParent(parent); // set it to 0 main position
        resetRectTrasnform(mask.transform.GetComponent<RectTransform>());
        mainMask = null;
        mask = null;
    }
    void setCurrentToEnter(PointerEventData e)
    {
        if (e != null)
        {
            clickedObject.transform.SetParent(e.pointerEnter.transform);
            //        RectTransform copyObject = Instantiate(copy.transform.gameObject, e.pointerEnter.transform).transform.GetComponent<RectTransform>();
            clickedRectTransform.anchoredPosition = Vector3.zero;
            clickedRectTransform.offsetMin = new Vector2(0, clickedRectTransform.offsetMin.y);//left
            clickedRectTransform.offsetMax = new Vector2(0, clickedRectTransform.offsetMax.y);
            clickedRectTransform.offsetMin = new Vector2(0, clickedRectTransform.offsetMin.x);
            clickedRectTransform.offsetMax = new Vector2(0, clickedRectTransform.offsetMax.x);
            clickedObject.GetComponent<Image>().raycastTarget = true;
         
        }
    }
    void resetRectTrasnform(RectTransform rectTransform)
    {
        rectTransform.offsetMin = new Vector2(0, rectTransform.offsetMin.y);
        rectTransform.offsetMax = new Vector2(0, rectTransform.offsetMax.y);
        rectTransform.offsetMin = new Vector2(0, rectTransform.offsetMin.x);
        rectTransform.offsetMax = new Vector2(0, rectTransform.offsetMax.x);
        rectTransform.GetComponent<Image>().raycastTarget = true;
        clickedObject = null;
    }
    void resetBack()
    {
        clickedObject.transform.position = Vector3.zero;
        clickedRectTransform.anchoredPosition = Vector3.zero;
        clickedObject.GetComponent<Image>().raycastTarget = true;
      
    }
   
}
