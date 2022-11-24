using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SystemItem : MonoBehaviour
{
    //default system item properties. follow this roles.
    // just items like potion
    [SerializeField] SystemInventory SYSTEM_INVENTORY;
    [Header("Health Units")]
    public bool enableItem;
    public string itemName;
    public int itemPosition;
    public GameObject itemObjectUI;
    public GameObject itemMainObjectUI;
    public GameObject itemObject;
    public Sprite itemImage;
    public float manaAmount;
    public float healthAmount;
    public float manaCallTime;
    public float healthCallTime;
    public GameObject itemEffectVFX;


    // armours
    [Header("Armor Units")]
    public bool enableArmor;
    public string armorName;
    public int armorPosition; // 0 = Hat, 1 = Gloves, 2 = Shirt, 3 = Pants , 4 = Shoes
    public GameObject armorObjectUI;
    public GameObject armorMainObjectUI;
    public GameObject armorObject;
    public Sprite armorImage;
    public float armorDeffense;
    public GameObject armorEffectVFX;

    [Header("Gun Units")]
    public bool enableGun;
    public string gunName;
    public int gunPosition;
    public float shootingPerSecond;
    public GameObject gunObjectUI;
    public GameObject gunMainObjectUI;
    public GameObject gunObject;
    public Sprite gunImage;
    public float damage;
    public int clip;
    public int bullets;
    public int maxBullets;
    public float gunSpray;

    // maskparts
    [Header("Mask part Units")]
    public bool enableMaskPart;
    public string maskPartName;
    public string maskPartDescription;
    public int maskPosition; // 0 = topLeft, 1 = top Right, 2 = bottom Left , 3 = bottom Right
    public GameObject maskItemObjectUI;
    public GameObject maskMainObjectUI;
    public GameObject maskItemObject;
    public Sprite maskImage;
    public float maskDamage;
    public string maskAbilityName;
    public float maskCallTime;
    public float maskManaPerUse; // relating the player mana function
    public GameObject maskEffectVFX;
    // Start is called before the first frame update
    // item utitlities
    [SerializeField] bool isObject;
    [SerializeField] bool isObjectUI;
    [SerializeField] bool isObjectMainUI;
    [SerializeField] bool inventoryShowOptions;
    [SerializeField] Transform systemItemDisplayUI;
    [SerializeField] Transform mainCamera;
    [SerializeField] Canvas displayObjectUI;
    [SerializeField] bool isPlayerDetected;
    [SerializeField] GameObject currentObject;
    [SerializeField] float playerDetectionRange;
    [SerializeField] LayerMask player;
    // positions
    [SerializeField] Transform mainMaskItemPosition0;
    [SerializeField] Transform mainMaskItemPosition1;
    [SerializeField] Transform mainMaskItemPosition2;
    [SerializeField] Transform mainMaskItemPosition3;

    [SerializeField] Transform itemMaskItemPosition;
    [SerializeField] Image previewMaskFragment0;
    [SerializeField] Image previewMaskFragment1;
    [SerializeField] Image previewMaskFragment2;
    [SerializeField] Image previewMaskFragment3;

    [SerializeField] Button inventoryOptions, inventoryDrop, inventorySwitch;
    void Start()
    {

        SYSTEM_INVENTORY = FindObjectOfType<SystemInventory>();
        if (isObjectUI)
        {
          

            inventoryOptions = this.gameObject.transform.GetComponent<Button>();
            inventoryDrop = this.gameObject.transform.Find("options").transform.Find("drop").transform.GetComponent<Button>();
            if(!isObjectMainUI)
            {
                inventorySwitch = this.gameObject.transform.Find("options").transform.Find("switch").transform.GetComponent<Button>();
                if (inventorySwitch != null) inventorySwitch.onClick.AddListener(switchItem);
                else inventorySwitch.onClick.AddListener(switchItem);
            }
            // inventorySwitch = this.gameObject.transform.Find("options").transform.Find("switch").transform.GetComponent<Button>();


            inventoryOptions.transform.Find("options").transform.gameObject.SetActive(false);

            if (inventoryOptions != null) inventoryOptions.onClick.AddListener(showOptions);
            else inventoryOptions.onClick.RemoveListener(showOptions);
            if (inventoryDrop != null) inventoryDrop.onClick.AddListener(dropItem);
            else inventoryDrop.onClick.RemoveListener(dropItem);


            currentObject = this.gameObject;
            Transform gui_player = GameObject.Find("systemGUI").transform;
            mainMaskItemPosition0 = gui_player.transform.Find("SYSTEM_playerInventory").transform.Find("CharacterView").Find("SYSTEM_mainMask").transform.Find("mask0").transform;
            mainMaskItemPosition1 = gui_player.transform.Find("SYSTEM_playerInventory").transform.Find("CharacterView").Find("SYSTEM_mainMask").transform.Find("mask1").transform;
            mainMaskItemPosition2 = gui_player.transform.Find("SYSTEM_playerInventory").transform.Find("CharacterView").Find("SYSTEM_mainMask").transform.Find("mask2").transform;
            mainMaskItemPosition3 = gui_player.transform.Find("SYSTEM_playerInventory").transform.Find("CharacterView").Find("SYSTEM_mainMask").transform.Find("mask3").transform;
            previewMaskFragment0 = gui_player.transform.Find("maskFragments").transform.Find("mask0").GetChild(0).GetComponent<Image>();
            previewMaskFragment1 = gui_player.transform.Find("maskFragments").transform.Find("mask1").GetChild(0).GetComponent<Image>();
            previewMaskFragment2 = gui_player.transform.Find("maskFragments").transform.Find("mask2").GetChild(0).GetComponent<Image>();
            previewMaskFragment3 = gui_player.transform.Find("maskFragments").transform.Find("mask3").GetChild(0).GetComponent<Image>();
            // just normal item
            itemMaskItemPosition = gui_player.transform.Find("SYSTEM_playerInventory").Find("SYSTEM_itemSlots");
        }
        else
        {

            displayObjectUI = this.gameObject.transform.Find("Canvas").transform.GetComponent<Canvas>();
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }
        if(isObject)
        {
            currentObject = this.gameObject;
            Transform gui_player = GameObject.Find("systemGUI").transform;
            mainMaskItemPosition0 = gui_player.transform.Find("SYSTEM_playerInventory").transform.Find("CharacterView").Find("SYSTEM_mainMask").transform.Find("mask0").transform;
            mainMaskItemPosition1 = gui_player.transform.Find("SYSTEM_playerInventory").transform.Find("CharacterView").Find("SYSTEM_mainMask").transform.Find("mask1").transform;
            mainMaskItemPosition2 = gui_player.transform.Find("SYSTEM_playerInventory").transform.Find("CharacterView").Find("SYSTEM_mainMask").transform.Find("mask2").transform;
            mainMaskItemPosition3 = gui_player.transform.Find("SYSTEM_playerInventory").transform.Find("CharacterView").Find("SYSTEM_mainMask").transform.Find("mask3").transform;
            previewMaskFragment0 = gui_player.transform.Find("maskFragments").transform.Find("mask0").GetChild(0).GetComponent<Image>();
            previewMaskFragment1 = gui_player.transform.Find("maskFragments").transform.Find("mask1").GetChild(0).GetComponent<Image>();
            previewMaskFragment2 = gui_player.transform.Find("maskFragments").transform.Find("mask2").GetChild(0).GetComponent<Image>();
            previewMaskFragment3 = gui_player.transform.Find("maskFragments").transform.Find("mask3").GetChild(0).GetComponent<Image>();
            // just normal item
            itemMaskItemPosition = gui_player.transform.Find("SYSTEM_playerInventory").Find("SYSTEM_itemSlots");
        }
  
 
 

     

        Invoke(nameof(getSystemInventory), 1.2f);
    }
    void getSystemInventory()
    {
        
        // check if its physcial object or UI object by itself.
        // Physical objects
        if (enableMaskPart)
        {
            for (int i = 0; i < SYSTEM_INVENTORY.maskItems.Count; i++)
            {
                if (SYSTEM_INVENTORY.maskItems[i] == maskPartName)
                {
                    isObject = true;
                }
            }
        }
        if (enableItem)
        {
            for (int j = 0; j < SYSTEM_INVENTORY.items.Count; j++)
            {
                if (SYSTEM_INVENTORY.items[j] == itemName)
                {
                    isObject = true;
                }
            }
        }
        if (enableArmor)
        {
            for (int l = 0; l < SYSTEM_INVENTORY.armorItems.Count; l++)
            {
                if (SYSTEM_INVENTORY.armorItems[l] == armorName)
                {
                    isObject = true;
                }
            }
        }
      
    }
    void Update()
    {
        isPlayerDetected = Physics.CheckSphere(currentObject.transform.position, playerDetectionRange, player);

        if (!isObjectUI)
        {
            if (isPlayerDetected) { displayObjectUI.gameObject.transform.LookAt(displayObjectUI.gameObject.transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up); displayObjectUI.gameObject.SetActive(true); }
            else if (!isPlayerDetected) { displayObjectUI.gameObject.SetActive(false); }
        }
        }
        public void showOptions()
    {

        if (inventoryShowOptions)
        {
            inventoryShowOptions = false;
            inventoryOptions.transform.Find("options").transform.gameObject.SetActive(inventoryShowOptions);
        }
        else
        {
            inventoryShowOptions = true;
            inventoryOptions.transform.Find("options").transform.gameObject.SetActive(inventoryShowOptions);
        }
    }
  
    public void createObjectUI()
    {
        for (int i = 0; i < itemMaskItemPosition.transform.childCount; i++)
        {
            // check if the main item mask is empty. position 0
            if (maskPosition == 0 && mainMaskItemPosition0.childCount <= 0) { GameObject mainMask0 = Instantiate(maskMainObjectUI, mainMaskItemPosition0); mainMask0.name = "main" + maskPartName; break; }// only maing Object goes here }
            else if (maskPosition == 1 && mainMaskItemPosition1.childCount <= 0) { GameObject mainMask1 = Instantiate(maskMainObjectUI, mainMaskItemPosition1); mainMask1.name = "main" + maskPartName; break; }
            else if (maskPosition == 2 && mainMaskItemPosition2.childCount <= 0) { GameObject mainMask2 = Instantiate(maskMainObjectUI, mainMaskItemPosition2); mainMask2.name = "main" + maskPartName; break; }
            else if (maskPosition == 3 && mainMaskItemPosition3.childCount <= 0) { GameObject mainMask3 = Instantiate(maskMainObjectUI, mainMaskItemPosition3); mainMask3.name = "main" + maskPartName; break; }

            else if (itemMaskItemPosition.transform.GetChild(i).childCount > 0)
            {
                //skip
                Debug.Log("soemthing");
            }
            else
            {
                // instantaite
                if (isObject && enableArmor) { GameObject armorObject = Instantiate(armorObjectUI, itemMaskItemPosition.transform.GetChild(i).transform); armorObject.name = armorName; }
                else if (isObject && enableItem) { GameObject itemObject = Instantiate(itemObjectUI, itemMaskItemPosition.transform.GetChild(i).transform); itemObject.name = itemName; }
                else if (isObject && enableMaskPart) { GameObject maskObject = Instantiate(maskItemObjectUI, itemMaskItemPosition.transform.GetChild(i).transform); maskObject.name = maskPartName; }
                Debug.Log("Empty");
                break;
            }
            
        }
        Destroy(gameObject);
    }
    public void switchItem()
    {
        Debug.Log("switch");
        // just for mask parts
        if(isObjectUI && enableMaskPart && maskPosition == 0 && mainMaskItemPosition0.childCount > 0)
        {
            // put old mask into item litsts.
            Transform currentTrasnform = this.gameObject.transform.parent;
            SystemItem oldMask0 = mainMaskItemPosition0.GetChild(0).transform.GetComponent<SystemItem>();

            GameObject switched = Instantiate(oldMask0.maskItemObjectUI, currentTrasnform);
            switched.name = oldMask0.maskPartName;
            Destroy(oldMask0.gameObject);

            GameObject newMask0 = Instantiate(maskMainObjectUI, mainMaskItemPosition0);
            newMask0.name = "main" + maskPartName;
            Destroy(gameObject);
            //  now add new mask in.
            
        } 
        else if(isObjectUI && enableMaskPart && maskPosition == 0 && mainMaskItemPosition0.childCount <= 0)
        {
            GameObject newMask0 = Instantiate(maskMainObjectUI, mainMaskItemPosition0);
            newMask0.name = "main" + maskPartName;
            Destroy(gameObject);
        }
    }
    public void dropItem()
    {
        Debug.Log("dropItem()");
        Transform Player = GameObject.Find("player").transform.Find("itemDrop").transform;

        if (isObjectUI && enableArmor) 
        {
          
            GameObject maskPrefab = Instantiate(armorObject, Player) as GameObject;
            Player.DetachChildren();
            Rigidbody maskPrefabRigidBody = maskPrefab.transform.GetComponent<Rigidbody>();
            float randomDropFroce = Random.Range(-20, 20);
            float randomDropFroceMinus = Random.Range(-20, 20);

            maskPrefabRigidBody.AddForce((Vector3.up * 50) + (Vector3.forward * randomDropFroce) + (Vector3.right * randomDropFroceMinus), ForceMode.Acceleration);
            Destroy(gameObject);

        }
        else if (isObjectUI && enableItem) 
        {
            GameObject maskPrefab = Instantiate(itemObject, Player) as GameObject;
            Player.DetachChildren();
            Rigidbody maskPrefabRigidBody = maskPrefab.transform.GetComponent<Rigidbody>();
            float randomDropFroce = Random.Range(-20, 20);
            float randomDropFroceMinus = Random.Range(-20, 20);

            maskPrefabRigidBody.AddForce((Vector3.up * 50) + (Vector3.forward * randomDropFroce) + (Vector3.right * randomDropFroceMinus), ForceMode.Acceleration);
            Destroy(gameObject);
        }
        else if (isObjectUI && enableMaskPart || isObjectMainUI && enableMaskPart)
        {
            GameObject maskPrefab = Instantiate(maskItemObject, Player) as GameObject;
            Player.DetachChildren();
            Rigidbody maskPrefabRigidBody = maskPrefab.transform.GetComponent<Rigidbody>();
            float randomDropFroce = Random.Range(-20, 20);
            float randomDropFroceMinus = Random.Range(-20, 20);

            maskPrefabRigidBody.AddForce((Vector3.up * 50) + (Vector3.forward * randomDropFroce) + (Vector3.right * randomDropFroceMinus), ForceMode.Acceleration);
            Destroy(gameObject);
        }
    }
    public void getDestroy()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
      // Gizmos.color = Color.red;
      // Gizmos.DrawWireSphere(currentObject.transform.position, playerDetectionRange);
    }
}
