using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
public class SystemGUI_Manager : MonoBehaviour
{
    public GameObject d;
    public Button options, save, exit, playerSaveYes, playerSaveNo, playerExitYes, playerExitNo, savePlayerSettings, revertPlayerSettings;
    public Transform playerSettings, playerSystemOptions, playerInventory, playerCollection, playerSave, playerExit;
    // settings
    public Transform mouseSettings, graphicSettings, inputSettings, soundSettings, customSettings, saveSettings, revertSettings;
    private InputSystem player;

    public KeyCode[] abilityKeys;
    // system datas  we grab it from here.
    public float attack;
    public float deffense;
    public float life;
    public int level;

    private bool settingsToggle, systemOptionsToggle, inventoryToggle, collectionToggle, playerSaveToggle, playerExitToggle;
    private bool confirmmSaveYes, confirmSaveNo, confirmExitYes, confirmExitNo;
    // Start is called before the first frame update
    // Show Options, ShowSave, Show Collection , Show Inventory.
   
    void Start()
    {
        //setting up the keys default keys
        abilityKeys = new KeyCode[10];
        abilityKeys[0] = KeyCode.Q;
        abilityKeys[1] = KeyCode.W;
        abilityKeys[2] = KeyCode.E;
        abilityKeys[3] = KeyCode.R;
        abilityKeys[4] = KeyCode.Alpha1;
        abilityKeys[5] = KeyCode.Alpha2;
        abilityKeys[6] = KeyCode.Space; // for shoot use space for default
        Transform systemGUI = FindObjectOfType<SystemGUI_Manager>().transform;
        player = FindObjectOfType<InputSystem>();
        playerSettings = systemGUI.transform.Find("SYSTEM_playerSettings").transform;
        playerSystemOptions = systemGUI.transform.Find("SYSTEM_option").transform;
        playerSave = playerSettings.Find("SYSTEM_save").transform;
        playerExit = playerSettings.Find("SYSTEM_exit").transform;
        playerInventory = systemGUI.transform.Find("SYSTEM_playerInventory").transform;
        playerCollection = systemGUI.transform.Find("SYSTEM_playerCollection").transform;

        // System Options
        mouseSettings = playerSystemOptions.transform.Find("mouseSettings").transform;
        graphicSettings = playerSystemOptions.transform.Find("graphicSettings").transform;
        inputSettings = playerSystemOptions.transform.Find("inputSettings").transform;
        soundSettings = playerSystemOptions.transform.Find("soundSettings").transform;
        customSettings = playerSystemOptions.transform.Find("customSettings").transform;
        saveSettings = playerSystemOptions.transform.Find("saveSettings").transform;
        revertSettings = playerSystemOptions.transform.Find("revertSettings").transform;

        playerSettings.gameObject.SetActive(settingsToggle);
        playerSystemOptions.gameObject.SetActive(systemOptionsToggle);
        playerInventory.gameObject.SetActive(inventoryToggle);
        playerCollection.gameObject.SetActive(collectionToggle);
        playerSave.gameObject.SetActive(playerSaveToggle);
        playerExit.gameObject.SetActive(playerExitToggle);
        options = playerSettings.transform.Find("playerOptions").transform.GetComponent<Button>();
        save = playerSettings.transform.Find("playerSave").transform.GetComponent<Button>();
        exit = playerSettings.transform.Find("playerExit").transform.GetComponent<Button>();
        savePlayerSettings = playerSystemOptions.transform.Find("saveSettings").GetComponent<Button>();
        revertPlayerSettings = playerSystemOptions.transform.Find("revertSettings").GetComponent<Button>(); 
        // buttons
        playerSaveYes = playerSave.Find("saveYes").transform.GetComponent<Button>();
        playerSaveNo = playerSave.Find("saveNo").transform.GetComponent<Button>();
        playerExitYes = playerExit.Find("exitYes").transform.GetComponent<Button>();
        playerExitNo = playerExit.Find("exitNo").transform.GetComponent<Button>();
        // adding system options.
        if (options != null) options.onClick.AddListener(systemOptions);
        else options.onClick.RemoveListener(systemOptions);
        if (save != null) save.onClick.AddListener(systemSave);
        else save.onClick.AddListener(systemSave);
        if (exit != null) exit.onClick.AddListener(systemExit);
        else exit.onClick.RemoveListener(systemExit);

        if (playerSaveYes != null) playerSaveYes.onClick.AddListener(saveYes);
        else playerSaveYes.onClick.RemoveListener(saveYes);
        if (playerSaveNo != null) playerSaveNo.onClick.AddListener(saveNo);
        else playerSaveNo.onClick.RemoveListener(saveNo);
        if (playerExitYes != null) playerExitYes.onClick.AddListener(exitYes);
        else playerExitYes.onClick.RemoveListener(exitYes);
        if (playerExitNo != null) playerExitNo.onClick.AddListener(exitNo);
        else playerExitNo.onClick.RemoveListener(exitNo);


        // system options.
        if (savePlayerSettings != null) savePlayerSettings.onClick.AddListener(saveSystemSettings);
        else savePlayerSettings.onClick.AddListener(saveSystemSettings);
        if (revertPlayerSettings != null) revertPlayerSettings.onClick.AddListener(revertSystenSettings);
        else revertPlayerSettings.onClick.AddListener(revertSystenSettings);
    }
    private void saveSystemSettings()
    {
        systemDataManager.SaveSettings(this);
    }
    private void revertSystenSettings()
    {
        //write the value here. default values for userInputs.
    }
    private void systemOptions()
    {
        if(systemOptionsToggle) {systemOptionsToggle = false; playerSystemOptions.gameObject.SetActive(systemOptionsToggle);   }
        else   {  systemOptionsToggle = true;  playerSystemOptions.gameObject.SetActive(systemOptionsToggle);  }
    }
    private void systemSave()
    {
        if(playerSaveToggle) { playerSaveToggle = false; playerSave.gameObject.SetActive(playerSaveToggle); }
        else  { playerSaveToggle = true; playerSave.gameObject.SetActive(playerSaveToggle);  systemDataManager.SavePlayer(this.player); }
    }
    private void systemExit()
    {
        if (playerExitToggle)  {    playerExitToggle = false;     playerExit.gameObject.SetActive(playerExitToggle); }
        else {   playerExitToggle = true;    playerExit.gameObject.SetActive(playerExitToggle);  }
    }
    private void saveYes()
    {
        Debug.Log("Saved");
        playerSave.gameObject.SetActive(false);
    }
    private void saveNo()
    {
        playerSave.gameObject.SetActive(false);
    }
    private void exitYes()
    {
        Application.Quit();
        playerExit.gameObject.SetActive(false);
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        playerExit.gameObject.SetActive(false);
#endif
    }
    private void exitNo()
    {
        playerExit.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //System fixed default keys
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(settingsToggle) { settingsToggle = false; playerSettings.gameObject.SetActive(settingsToggle); Time.timeScale = 1; }
            else  {  settingsToggle = true; playerSettings.gameObject.SetActive(settingsToggle); Time.timeScale = 0; }
            // disable all options
            systemOptionsToggle = false;
            playerSystemOptions.gameObject.SetActive(systemOptionsToggle);
            playerSaveToggle = false;
            playerSave.gameObject.SetActive(playerSaveToggle);
            playerExitToggle = false;
            playerExit.gameObject.SetActive(playerExitToggle);

        }
        if(Input.GetKeyDown(KeyCode.I) && !settingsToggle)   {
            if(inventoryToggle) { inventoryToggle = false;playerInventory.gameObject.SetActive(inventoryToggle); }
            else   {  inventoryToggle = true;  playerInventory.gameObject.SetActive(inventoryToggle); }
        }
        if(Input.GetKeyDown(KeyCode.C) && !settingsToggle)
        {
            if(collectionToggle) { collectionToggle = false;playerCollection.gameObject.SetActive(collectionToggle);}
            else  {  collectionToggle = true; playerCollection.gameObject.SetActive(collectionToggle);}
        }
    }
}
