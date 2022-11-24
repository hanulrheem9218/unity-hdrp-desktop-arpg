using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class consumableMainItem : MonoBehaviour
{
    public itemData DATA_item;
    public Transform itemObject;
    [SerializeField] float dropForce;

    //this must be filled up
    [SerializeField] int itemAmounts;
    [SerializeField]
    string[] existedPotionItems = { "h", "m" };
    [SerializeField] int potionPosition;
    [SerializeField] string currentItemName;
    [SerializeField] int optionToggle;
    [SerializeField] Image previewItem0;
    [SerializeField] Image previewItem1;
    // Start is called before the first frame update
    void Start()
    {
        itemObject = gameObject.transform;
        potionPosition = DATA_item.potionPosition;
        GameObject optionPanel = itemObject.transform.Find("Panel").gameObject;
        optionPanel.SetActive(false);
        // System mask check.
        Transform gui_player = GameObject.Find("GUI_player").transform;
        previewItem0 = gui_player.transform.Find("playerManaPotions").transform.Find("Image").GetComponent<Image>();
        previewItem1 = gui_player.transform.Find("playerHealthPotions").transform.Find("Image").GetComponent<Image>();
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
    // Update is called once per frame
    void Update()
    {

    }

    public void dropMaskItem()
    {
        Transform Player = GameObject.Find("player").transform.Find("itemDrop").transform;
        GameObject maskPrefab = Instantiate(DATA_item.potionItemObject, Player) as GameObject;
        //   Transform maskFragments = GameObject.Find("GUI_player")
        Image playerHealthPotions = GameObject.Find("GUI_player").transform.Find("playerManaPotions").transform.Find("Image").GetComponent<Image>();
        Image playerManaPotions = GameObject.Find("GUI_player").transform.Find("playerHealthPotions").transform.Find("Image").GetComponent<Image>();
        if(potionPosition == 0)
        {
            //  getScript.previewItem0.sprite = DATA_item.defaultEmptyImage;
            playerHealthPotions.sprite = DATA_item.defaultEmptyImage;
        }
        else if(potionPosition == 1)
        {
            playerManaPotions.sprite = DATA_item.defaultEmptyImage;
           // getScript.previewItem1.sprite = DATA_item.defaultEmptyImage;
        }
        Player.DetachChildren();
        Rigidbody maskPrefabRigidBody = maskPrefab.transform.GetComponent<Rigidbody>();
        float randomDropFroce = Random.Range(-20, 20);
        float randomDropFroceMinus = Random.Range(-20, 20);

        maskPrefabRigidBody.AddForce((Vector3.up * 50) + (Vector3.forward * randomDropFroce) + (Vector3.right * randomDropFroceMinus), ForceMode.Acceleration);
        InputSystem playerDATA = GameObject.Find("player").transform.GetComponent<InputSystem>();
        playerDATA.healthName = "";

        Destroy(gameObject);
    }
}
