using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class InputSystem : MonoBehaviour
{
    // System files
    [SerializeField] float distance;
    public Animator playerAnimator;

    private int mouseSize;
    public GameObject point;
    public Transform mouseCursor;
    public GameObject mouseEffectTransform;
    public GameObject mouseHoldEffectTransform;
    public Transform playerObject;

    private Transform playerObjectPrefab;
    // is inventory opened 
    [SerializeField] bool isSystemGUI;

    [SerializeField] bool MOUSECLICKED = false;
    [SerializeField] bool MOUSEHOLD = false;
    [SerializeField] bool MOUSERIGHTHOLD = false;
    // attributes value.
    public float playerMovementSpeed;
    [SerializeField] private float playerRunningSpeed;
    [SerializeField] private float playerRunRolledSpeed;
    [SerializeField] private float playerWalkingSpeed;
    [SerializeField] private float playerWalkRolledSpeed;
    [SerializeField] private float playerCrouchSpeed;
    public float playerTurningSpeed;
    [SerializeField] float playerHeight;
    //experimental 

    // Start is called before the first frame update

    public bool isMouseInCursor = false;
    private Text behaviourStatus;
    [SerializeField] bool walkToRun;
    // behaviour statements
    public bool isPlayerMoving;
    [SerializeField] bool isCrouching;
    [SerializeField] bool isWalking;
    [SerializeField] bool isWalkRolled;
    [SerializeField] bool isRunning;
    [SerializeField] bool isRunRolled;
    [SerializeField] bool isPicking;
    [SerializeField] bool isAttacking;
    public int mouseCount;


    // player animation

    // slope Handling
    [Header("Slope Handling")]
    public bool isGrounded;
    [SerializeField] float groundDistance = 1f;
    [SerializeField] LayerMask groundMask;
    [SerializeField] RaycastHit slopeHit;

    [Header("Range detection")]
    [SerializeField] bool isVariableTouched;
    [SerializeField] float variableDistance;
    [SerializeField] LayerMask variableMask;
    [SerializeField] LayerMask playerMask;

    [SerializeField] bool isEnemyDetected;
    [SerializeField] float enemyDetetctionRange;
    [SerializeField] bool isNPCDetected;
    [SerializeField] float npcDetectionRange;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] LayerMask npcMask;
    public bool isEnemyClicked;
    [SerializeField] bool attackAllowed;
    [SerializeField] float attackTime;
    public Transform Target;

    //UI prefabs that displayer Mana and Health
    [Header("UserHealth please set the max and value")]
    [SerializeField] float playerMaxHealth;
    [SerializeField] float playerMaxMana;
    [SerializeField] float playerHealth;
    [SerializeField] float playerMana;
    [SerializeField] Image playerHealthDisplay;

    //Player UserSettings
    [SerializeField] gameSettingsData DATA_game;
    //Abilities


    // public Transform temp; this is for the ability attributes

    [SerializeField] float abilityOriginTurningSpeed;

    [SerializeField] bool isWallTouched;
    // you need this for value.

    // HOLY SHIT SO MUCH SHIT TO FIX
    [SerializeField] Slider userHealth;
    [SerializeField] Slider userMana;
    // this shit is health bar not a potin.

    [SerializeField] Slider manaDisplay;
    [SerializeField] Slider healthDisplay;
    public string manaName;
    public string healthName;

    public float manaColdtime;
    public float healthColdtime;

    public float perUseMana;
    public float perUseHealth;

    public bool isManaColdtimeEnabled;
    public bool isHealthColdtimeEnabled;
    public bool isOneTimeOnly;

    //VFX effects these belows are instantiation points
    //Clicking Interface.
    public bool isObjectClicked;
    [SerializeField] bool isObjectDetected;
    [SerializeField] float itemRange;
    [SerializeField] LayerMask itemMask;

    [SerializeField] bool isDestroyableObjectClicked;
    [SerializeField] bool isDestroyableObjectDetected;
    [SerializeField] float destroyableRange;
    [SerializeField] LayerMask destroyableObjectMask;
    // Different attack properties 
    [SerializeField] bool isDefaultAttack;
    [SerializeField] bool isMaskDefaultAttack;

    // new system involved;
    [SerializeField] private bool MouseClicked;
    [SerializeField] private bool mouseAllowed;

    //Ragdoll System
    [SerializeField] CapsuleCollider mainCollider;
    [SerializeField] Rigidbody mainRigidBody;
    [SerializeField] public bool isPlayerDead;

    // system data Stores.
    public int level;
    public int life;
    public float deffense;
    public float attack;
    public abilitySystem ability;
    public SystemInventory inventory;
    public bool isKeyHold;
    public float maskCallQ, maskCallW, maskCallE, maskCallR, incrementQ, incrementW, incrementE, incrementR;
    public float increamentM, increamentH, healthCall = 0f;
    // gun
    [SerializeField] Transform gunDisplayUI;
    [SerializeField] float increamentG;
    [SerializeField] bool isShootKeyHeld = false;
    //test 
    [SerializeField] float minFov;
    [SerializeField] float maxFov;
    [SerializeField] float sensitivity;
    [SerializeField] LineRenderer smokeRailPrefab;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject clipPrefab;
    private void Awake()
    {



    }

    void Start()
    {
        // GameObject smokeObject = Resources.Load("ShootPrefab/smokeRailPrefab") as GameObject;
        // smokeRailPrefab = smokeObject.GetComponent<LineRenderer>();

        // clipPrefab = Resources.Load("ShootPrefab/Clip") as GameObject;
        // bulletPrefab = Resources.Load("ShootPrefab/BulletShell") as GameObject;
        ability = FindObjectOfType<abilitySystem>();
        inventory = FindObjectOfType<SystemInventory>();
        behaviourStatus = FindObjectOfType<SystemInventory>().transform.Find("playerBehaviour").GetComponent<Text>();
        gunDisplayUI = gameObject.transform.Find("Canvas");
        if (inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask0").childCount > 0)
        {
            inventory.transform.Find("maskFragments").Find("mask0").Find("Slider").Find("Text").GetComponent<Text>().text = "";
            inventory.transform.Find("maskFragments").Find("mask0").Find("Slider").GetComponent<Slider>().maxValue = inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask0").GetChild(0).GetComponent<SystemItem>().maskCallTime;
            inventory.transform.Find("maskFragments").Find("mask0").Find("Slider").GetComponent<Slider>().minValue = 0f;
            inventory.transform.Find("maskFragments").Find("mask0").Find("Slider").GetComponent<Slider>().value = 0f;
        }
        else { inventory.transform.Find("maskFragments").Find("mask0").Find("Slider").Find("Text").GetComponent<Text>().text = ""; }
        if (inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask1").childCount > 0)
        {
            inventory.transform.Find("maskFragments").Find("mask1").Find("Slider").Find("Text").GetComponent<Text>().text = "";
            inventory.transform.Find("maskFragments").Find("mask1").Find("Slider").GetComponent<Slider>().maxValue = inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask1").GetChild(0).GetComponent<SystemItem>().maskCallTime;
            inventory.transform.Find("maskFragments").Find("mask1").Find("Slider").GetComponent<Slider>().minValue = 0f;
            inventory.transform.Find("maskFragments").Find("mask1").Find("Slider").GetComponent<Slider>().value = 0f;
        }
        else { inventory.transform.Find("maskFragments").Find("mask1").Find("Slider").Find("Text").GetComponent<Text>().text = ""; }
        if (inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask2").childCount > 0)
        {
            inventory.transform.Find("maskFragments").Find("mask2").Find("Slider").Find("Text").GetComponent<Text>().text = "";
            inventory.transform.Find("maskFragments").Find("mask2").Find("Slider").GetComponent<Slider>().maxValue = inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask2").GetChild(0).GetComponent<SystemItem>().maskCallTime;
            inventory.transform.Find("maskFragments").Find("mask2").Find("Slider").GetComponent<Slider>().minValue = 0f;
            inventory.transform.Find("maskFragments").Find("mask2").Find("Slider").GetComponent<Slider>().value = 0f;
        }
        else { inventory.transform.Find("maskFragments").Find("mask2").Find("Slider").Find("Text").GetComponent<Text>().text = ""; }
        if (inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask3").childCount > 0)
        {
            inventory.transform.Find("maskFragments").Find("mask3").Find("Slider").Find("Text").GetComponent<Text>().text = "";
            inventory.transform.Find("maskFragments").Find("mask3").Find("Slider").GetComponent<Slider>().maxValue = inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask3").GetChild(0).GetComponent<SystemItem>().maskCallTime;
            inventory.transform.Find("maskFragments").Find("mask3").Find("Slider").GetComponent<Slider>().minValue = 0f;
            inventory.transform.Find("maskFragments").Find("mask3").Find("Slider").GetComponent<Slider>().value = 0f;
        }
        else { inventory.transform.Find("maskFragments").Find("mask3").Find("Slider").Find("Text").GetComponent<Text>().text = ""; }




        gameObject.transform.Find("abilityOrigin").Find("Canvas").Find("Circle").gameObject.SetActive(false);
        gameObject.transform.Find("abilityOrigin").Find("Canvas").Find("Arrow").gameObject.SetActive(false);
        mouseEffectTransform = Instantiate(Resources.Load("EffectPrefab/clickEffect") as GameObject);
        mouseHoldEffectTransform = Instantiate(Resources.Load("EffectPrefab/clickHoldEffect") as GameObject);
        point = Instantiate(Resources.Load("MousePrefab/mousePosition") as GameObject);
        mouseCursor = point.transform;

        mainRigidBody = FindObjectOfType<InputSystem>().transform.GetComponent<Rigidbody>();

        Target = playerObject.transform;
        playerObjectPrefab = GameObject.Find("player").transform.Find("playerPrefab").transform;
        // all of these are GUI Presets
        Transform gui_player = FindObjectOfType<SystemGUI_Manager>().transform;
        // fuck you health
        playerMaxHealth = playerHealth;
        userHealth = gui_player.Find("playerHealthBar").transform.Find("Slider").GetComponent<Slider>();
        userHealth.maxValue = playerHealth;
        userHealth.value = playerHealth;
        userHealth.minValue = 0;

        Text getHealthText = userHealth.transform.Find("Text").GetComponent<Text>();
        getHealthText.text = playerHealth.ToString("0");

        playerMaxMana = playerMana;
        userMana = gui_player.Find("playerManaBar").transform.Find("Slider").GetComponent<Slider>();
        userMana.maxValue = playerMana;
        userMana.value = playerMana;
        userMana.value = 0;

        Text getManaText = userMana.transform.Find("Text").GetComponent<Text>();
        getManaText.text = playerMana.ToString("0");

        playerHealthDisplay = gui_player.transform.Find("playerHealthBar").transform.Find("HealthBar").transform.Find("fillBackground").GetComponent<Image>();
        //Get System references

        // we need to get the mask information before we start adding this functions.

        gui_player.Find("playerBehaviour").GetComponent<Text>().text = "";
        //ITEMS this is wrong.
        healthDisplay = gui_player.Find("playerManaBar").transform.Find("Slider").GetComponent<Slider>();
        manaDisplay = gui_player.Find("playerHealthBar").transform.Find("Slider").GetComponent<Slider>();
        healthDisplay.value = 0;
        manaDisplay.value = 0;
        Text manaText = manaDisplay.transform.Find("Text").GetComponent<Text>();
        manaText.text = "";
        Text healthText = healthDisplay.transform.Find("Text").GetComponent<Text>();
        healthText.text = "";
        isWalking = true;
        userMana.transform.Find("Text").GetComponent<Text>().text = playerMana.ToString("N0");
        userHealth.transform.Find("Text").GetComponent<Text>().text = playerHealth.ToString("N0");
        DeactiveRagdoll();
    }


    // Mouse New method


    void Update()
    {
        //We need to display Fixed numbers
        isSystemGUI = FindObjectOfType<SystemInventory>().isInsideOfInventory;
        if (isSystemGUI) { isPlayerMoving = false; }
        userMana.value = (int)playerMana;
        userHealth.value = (int)playerHealth;
        characterZoomInOut();
        //Check all ability physics. and distance difference Abillity One.

        // checking bool
        isGrounded = Physics.CheckSphere(playerObject.transform.position - new Vector3(0, 1, 0), groundDistance, groundMask);
        isVariableTouched = Physics.CheckSphere(playerObject.transform.position - new Vector3(0, -1, 0), variableDistance, variableMask);
        isNPCDetected = Physics.CheckSphere(playerObject.transform.position - new Vector3(0, -1, 0), npcDetectionRange, npcMask);

        //when the destoryable object reached.
        if (isDestroyableObjectClicked && MOUSECLICKED)
        {
            isDestroyableObjectDetected = Physics.CheckSphere(playerObject.transform.position - new Vector3(0, -1, 0), destroyableRange, destroyableObjectMask);
            if (isDestroyableObjectDetected)
            {
                mouseAllowed = false;
                isPlayerMoving = false;
                if (Target != null) Target.transform.SendMessage("fracturedObject");
            }
        }
        else isDestroyableObjectDetected = false;

        // when the enemy is reached.
        if (isEnemyClicked && MOUSECLICKED)
        {
            isEnemyDetected = Physics.CheckSphere(playerObject.transform.position - new Vector3(0, -1, 0), enemyDetetctionRange, enemyMask);
            if (isEnemyDetected)
            {
                isAttacking = true;
                mouseAllowed = false;
                isPlayerMoving = false;
            }
        }
        else isEnemyDetected = false;

        //when the item object is reached.
        if (isObjectClicked && MOUSECLICKED)
        {
            isObjectDetected = Physics.CheckSphere(playerObject.transform.position - new Vector3(0, -1, 0), itemRange, itemMask);
            if (isObjectDetected)
            {
                mouseAllowed = false;
                isPlayerMoving = false;
                // adding animation function;
                isPicking = true;
                if (Target != null) Target.transform.SendMessage("createObjectUI");
            }
        }
        else isObjectClicked = false;


        //Mouse Input system 

        //Keyboard Input System Q,W,E,R

        //Inventory Input System.



        // Game Logic
        if (!isPlayerDead && !MOUSERIGHTHOLD || !isKeyHold) { mouseInputSystem(); }
        if (!isPlayerDead && isNPCDetected) { isMouseInCursor = false; isPlayerMoving = false; }
        if (!isPlayerDead && isVariableTouched) { isWallTouched = true; }
        else if (!isPlayerDead && !isVariableTouched) isWallTouched = false;
        if (!isPlayerDead && isEnemyDetected) { isMouseInCursor = false; isPlayerMoving = false; }

        if (!isPlayerDead && isMouseInCursor && !isPicking && !isAttacking) oneClickMovement();
        if (!isPlayerDead && isPlayerMoving && isWalking && !isRunning && !isPicking && !isAttacking) { walkTowards(); isWalking = true; }
        if (!isPlayerDead && isPlayerMoving && isRunning && !isWalking && !isPicking && !isAttacking) { walkTowards(); isRunning = true; }


        // Input System. reformed.
        // 
        // player zoom in


        // develop single input system. mask abilitys
        // if (!isPlayerDead && inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask0").GetChild(0).GetComponent<SystemItem>().maskPartName == inventory.maskItems[0]) // A items?
        // {
        //     if (incrementQ <= 0.0f && inventory.transform.Find("playerManaBar").Find("Slider").GetComponent<Slider>().value > 0f) // if mana is not zero
        //     {
        //         useMaskFragment(FindObjectOfType<SystemGUI_Manager>().abilityKeys[0], false, true, callingMaskFragment(0.1f, 0.1f, "mask0"), "mask0");
        //     }
        // }
        // if (!isPlayerDead && inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask1").GetChild(0).GetComponent<SystemItem>().maskPartName == inventory.maskItems[1]) // A items?
        // {
        //     if (incrementW <= 0.0f && inventory.transform.Find("playerManaBar").Find("Slider").GetComponent<Slider>().value > 0f) // if mana is not zero
        //     {
        //         useMaskFragment(FindObjectOfType<SystemGUI_Manager>().abilityKeys[1], true, false, callingMaskFragment(0.1f, 0.1f, "mask1"), "mask1");
        //     }
        // }
        // if (!isPlayerDead && inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask2").GetChild(0).GetComponent<SystemItem>().maskPartName == inventory.maskItems[2]) // A items?
        // {
        //     if (incrementE <= 0.0f && inventory.transform.Find("playerManaBar").Find("Slider").GetComponent<Slider>().value > 0f) // if mana is not zero
        //     {
        //         useMaskFragment(FindObjectOfType<SystemGUI_Manager>().abilityKeys[2], false, true, callingMaskFragment(0.1f, 0.1f, "mask2"), "mask2");
        //     }
        // }
        // if (!isPlayerDead && inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find("mask3").GetChild(0).GetComponent<SystemItem>().maskPartName == inventory.maskItems[3]) // A items?
        // {
        //     if (incrementR <= 0.0f && inventory.transform.Find("playerManaBar").Find("Slider").GetComponent<Slider>().value > 0f) // if mana is not zero
        //     {
        //         useMaskFragment(FindObjectOfType<SystemGUI_Manager>().abilityKeys[3], false, true, callingMaskFragment(0.1f, 0.1f, "mask3"), "mask3");
        //     }
        // }
        // if (!isPlayerDead && inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainPotions").Find("mainPotionHealth").GetChild(0).GetComponent<SystemItem>().itemName == inventory.items[0])
        // {
        //     if (increamentH <= 0.0f)
        //     {
        //         usePotion(FindObjectOfType<SystemGUI_Manager>().abilityKeys[4], partUp(0.01f, inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainPotions").Find("mainPotionHealth").GetChild(0).GetComponent<SystemItem>().healthAmount,
        //           1f, "health"), callingPart(0.1f, 0.1f, "mainPotionHealth"));

        //     }
        // }
        // if (!isPlayerDead && inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainPotions").Find("mainPotionPart").GetChild(0).GetComponent<SystemItem>().itemName == inventory.items[1])
        // {
        //     if (increamentM <= 0.0f)
        //     {
        //         usePotion(FindObjectOfType<SystemGUI_Manager>().abilityKeys[5], partUp(0.01f, inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainPotions").Find("mainPotionPart").GetChild(0).GetComponent<SystemItem>().healthAmount,
        //           1f, "part"), callingPart(0.1f, 0.1f, "mainPotionPart"));

        //     }
        // }

        // implement shoot pistol type.
        if (!isPlayerDead && inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainWeapon").GetChild(0).GetComponent<SystemItem>().gunName == inventory.gunItems[0])
        {
            // if (increamentG <= 0.0f)
            // {
            if (Input.GetKeyDown(FindObjectOfType<SystemGUI_Manager>().abilityKeys[6]) && !isShootKeyHeld && inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainWeapon").GetChild(0).GetComponent<SystemItem>().bullets > 0)
            {
                SystemItem gunItem = inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainWeapon").GetChild(0).GetComponent<SystemItem>();
                StartCoroutine(useShotgun(0.001f, 1f, 5, FindObjectOfType<SystemGUI_Manager>().abilityKeys[6], gunItem));
                //StartCoroutine(useGun(inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainWeapon").GetChild(0).GetComponent<SystemItem>().shootingPerSecond, FindObjectOfType<SystemGUI_Manager>().abilityKeys[6], inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainWeapon").GetChild(0).GetComponent<SystemItem>()));
            }
            else if (Input.GetKeyUp(FindObjectOfType<SystemGUI_Manager>().abilityKeys[6])) { isShootKeyHeld = false; }

        }
        if (!isPlayerDead && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (walkToRun == true) { isWalking = true; isRunning = false; behaviourStatus.text = "Walking"; walkToRun = false; }
            else { isWalking = false; isRunning = true; behaviourStatus.text = "Running"; walkToRun = true; }
        }
        if (!isPlayerDead && Input.GetKey(KeyCode.LeftControl)) { Debug.Log("Control"); isCrouching = true; }
        else if (!isPlayerDead && Input.GetKeyUp(KeyCode.LeftControl)) { Debug.Log("control not hold"); isCrouching = false; }

        if (!isPlayerDead && isWalking && !isRunning && !isWalkRolled && !isCrouching && Input.GetKeyDown(KeyCode.Z)) { isPlayerMoving = true; isWalkRolled = true; Invoke(nameof(isRolledInvoke), 1.5f); }
        else if (!isPlayerDead && isRunning && !isWalking && !isRunRolled && !isCrouching && Input.GetKeyDown(KeyCode.Z)) { isPlayerMoving = true; isRunRolled = true; Invoke(nameof(isRolledInvoke), 1f); }

        if (!isPlayerDead && isEnemyDetected && !attackAllowed && isEnemyClicked) StartCoroutine(attackObject(attackTime));
        // animation status
        // if item Variable touched Animation
        // Walking
        if (this.gameObject.activeInHierarchy && !isPlayerDead && !isCrouching && isVariableTouched) playerAnimator.SetBool("isWalking", false);
        if (this.gameObject.activeInHierarchy && !isPlayerDead && !isCrouching) playerAnimator.SetBool("isWalking", false);
        // walking
        if (this.gameObject.activeInHierarchy && !isPlayerDead && !isCrouching && isPlayerMoving && isWalking && !isRunning && !isPicking) { playerAnimator.SetBool("isWalking", true); playerMovementSpeed = playerWalkingSpeed; }
        else { playerAnimator.SetBool("isWalking", false); }
        //running
        if (this.gameObject.activeInHierarchy && !isPlayerDead && !isCrouching && isPlayerMoving && !isWalking && isRunning && !isPicking) { playerAnimator.SetBool("isRunning", true); playerMovementSpeed = playerRunningSpeed; }
        else { playerAnimator.SetBool("isRunning", false); }
        //Crouch 
        if (this.gameObject.activeInHierarchy && !isPlayerDead && isCrouching && isPlayerMoving) { playerAnimator.SetBool("isCrouchWalk", true); playerMovementSpeed = playerCrouchSpeed; }
        else playerAnimator.SetBool("isCrouchWalk", false);
        if (this.gameObject.activeInHierarchy && !isPlayerDead && isCrouching) playerAnimator.SetBool("isCrouching", true);
        else playerAnimator.SetBool("isCrouching", false);
        // rolling animation
        if (this.gameObject.activeInHierarchy && !isPlayerDead && isWalkRolled) { isPlayerMoving = true; playerAnimator.SetBool("isWalkRoll", true); playerMovementSpeed = playerWalkRolledSpeed; }
        else { playerAnimator.SetBool("isWalkRoll", false); }
        if (this.gameObject.activeInHierarchy && !isPlayerDead && isRunRolled) { isPlayerMoving = true; playerAnimator.SetBool("isRunRoll", true); playerMovementSpeed = playerRunRolledSpeed; }
        else { playerAnimator.SetBool("isRunRoll", false); }
        // Picking up two conditions
        if (this.gameObject.activeInHierarchy && !isPlayerDead && isPicking) { playerAnimator.SetBool("isPicking", true); isWalking = false; isRunning = false; isPicking = true; Invoke(nameof(isRolledInvoke), 1f); }
        else { playerAnimator.SetBool("isPicking", false); }
        //attacking 
        if (this.gameObject.activeInHierarchy && !isPlayerDead && isAttacking) { playerAnimator.SetBool("isAttacking", true); isPlayerMoving = false; isAttacking = true; lookAtMouseTransform(Target.transform); Invoke(nameof(isRolledInvoke), 1f); }
        else { playerAnimator.SetBool("isAttacking", false); }
        // if (this.gameObject.activeInHierarchy && !isPlayerDead && isPicking  && isRunning && !isWalking) { playerAnimator.SetBool("isPicking", true); isRunning = false;  isPicking = true; Invoke(nameof(isRolledInvoke), 1f); }
        // else { playerAnimator.SetBool("isPicking", false);  Debug.Log("runpickup"); }
        // Debug.Log(Input.mousePosition);
    }

    private void characterZoomInOut()
    {
        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }
    IEnumerator useGun(float second, KeyCode userInput, SystemItem gunItem)
    {
        if (Input.GetKeyUp(userInput))
        {
            Debug.Log("stoped");
            StopCoroutine(useGun(second, userInput, gunItem));
            isShootKeyHeld = false;
        }
        else if (Input.GetKey(userInput) && gunItem.bullets > 0)
        {
            isShootKeyHeld = true;
            RaycastHit hit;
            Ray ray = new Ray(gameObject.transform.Find("playerShoot").position, gameObject.transform.Find("playerShoot").forward);
            Debug.DrawRay(gameObject.transform.Find("playerShoot").position, gameObject.transform.Find("playerShoot").forward * Mathf.Infinity);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                switch (hit.collider.tag)
                {
                    case "Enemy":
                        {
                            //   Debug.Log("enemy");
                            hit.transform.SendMessage("enemyTakeDamage", gunItem.damage);
                            LineShootEffect(hit);
                            gunItem.bullets--;
                            break;
                        }
                    case "Untagged":
                        {
                            //hit.transform.SendMessage("enemyTakeDamage", gunItem.damage);
                            LineShootEffect(hit);
                            gunItem.bullets--;
                            break;
                        }

                }
            }
            Debug.Log("working");
            // SystemItem gunItem = inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainWeapon").GetChild(0).GetComponent<SystemItem>();
            gameObject.transform.Find("playerShoot").localEulerAngles = Vector3.zero;
            float x = Random.Range(-gunItem.gunSpray, gunItem.gunSpray);
            float y = Random.Range(-gunItem.gunSpray, gunItem.gunSpray);

            gameObject.transform.Find("playerShoot").transform.rotation *= Quaternion.Euler(x, y, 0f);

            yield return new WaitForSeconds(second);
            StartCoroutine(useGun(second, userInput, gunItem));
        }
    }
    IEnumerator useShotgun(float second, float shotgunSecond, int shotgun, KeyCode userInput, SystemItem gunItem)
    {
        if (Input.GetKeyUp(userInput))
        {
            StopCoroutine(useShotgun(second, shotgunSecond, shotgun, userInput, gunItem));

        }
        else if (Input.GetKey(userInput) && gunItem.bullets >= 0)
        {
            float counter = 0;
            while (counter <= shotgun)
            {
                isShootKeyHeld = true;
                RaycastHit hit;
                Ray ray = new Ray(gameObject.transform.Find("playerShoot").position, gameObject.transform.Find("playerShoot").forward);
                Debug.DrawRay(gameObject.transform.Find("playerShoot").position, gameObject.transform.Find("playerShoot").forward * Mathf.Infinity);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    switch (hit.collider.tag)
                    {
                        case "Enemy":
                            {
                                //   Debug.Log("enemy");
                                hit.transform.SendMessage("enemyTakeDamage", gunItem.damage);
                                LineShootEffect(hit);
                                gunItem.bullets--;
                                break;
                            }
                        case "Untagged":
                            {
                                //hit.transform.SendMessage("enemyTakeDamage", gunItem.damage);
                                LineShootEffect(hit);
                                gunItem.bullets--;
                                break;
                            }

                    }
                }
                Debug.Log("working");
                // SystemItem gunItem = inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainWeapon").GetChild(0).GetComponent<SystemItem>();
                gameObject.transform.Find("playerShoot").localEulerAngles = Vector3.zero;
                float x = Random.Range(-gunItem.gunSpray, gunItem.gunSpray);
                float y = Random.Range(-gunItem.gunSpray, gunItem.gunSpray);

                gameObject.transform.Find("playerShoot").transform.rotation *= Quaternion.Euler(x, y, 0f);
                counter++;
                yield return new WaitForSeconds(second);
                //   StartCoroutine(useShotgun(second, shotgunSecond, shotgun, userInput, gunItem));
            }
            yield return new WaitForSeconds(shotgunSecond);
            StartCoroutine(useShotgun(second, shotgunSecond, shotgun, userInput, gunItem));
        }

    }
    public void LineShootEffect(RaycastHit hit)
    {
        LineRenderer gunShootEffect = (LineRenderer)Instantiate(smokeRailPrefab, gameObject.transform.Find("playerShoot").position, gameObject.transform.Find("playerShoot").rotation);
        //  gunShootEffect.name = gameObject.name + "smokeRail" + bulletCounts;
        AudioSource gunAudioSound = gunShootEffect.GetComponent<AudioSource>();
        gunAudioSound.Play();
        gunShootEffect.transform.LookAt(hit.point);
    }


    void useMaskFragment(KeyCode userInput, bool isCircle, bool isArrow, IEnumerator maskCall, string maskPosition)
    {
        if (!isPlayerDead && Input.GetKey(userInput))
        {
            if (isCircle) { gameObject.transform.Find("abilityOrigin").Find("Canvas").Find("Circle").gameObject.SetActive(true); ability.isSphere = true; }
            else if (isArrow) { gameObject.transform.Find("abilityOrigin").Find("Canvas").Find("Arrow").gameObject.SetActive(true); ability.isCube = true; }
            isKeyHold = true;
            getMouseInput();
            getAbilityOriginTransform(point.transform, gameObject.transform.Find("abilityOrigin").transform);
            distance = 0.3f;
            isPlayerMoving = false;
            mouseAllowed = false;
            if (!isPlayerDead && Input.GetMouseButtonDown(0)) // if right clicked
            {
                if (ability.targets.Length >= 0)
                {
                    ability.transform.SendMessage("damageObject", inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find(maskPosition).GetChild(0).GetComponent<SystemItem>().maskDamage);
                }
                // add mana cost here.
                StartCoroutine(partCost(0.001f, inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find(maskPosition).GetChild(0).GetComponent<SystemItem>().maskManaPerUse, 0.1f, "part"));
                StartCoroutine(maskCall);
                isKeyHold = false;
                if (isCircle) { gameObject.transform.Find("abilityOrigin").Find("Canvas").Find("Circle").gameObject.SetActive(false); ability.isSphere = false; }
                else if (isArrow) { gameObject.transform.Find("abilityOrigin").Find("Canvas").Find("Arrow").gameObject.SetActive(false); ability.isCube = false; }

            }
        }
        else if (!isPlayerDead && Input.GetKeyUp(userInput) && isKeyHold)
        {
            isKeyHold = false;
            if (isCircle) { gameObject.transform.Find("abilityOrigin").Find("Canvas").Find("Circle").gameObject.SetActive(false); ability.isSphere = false; }
            else if (isArrow) { gameObject.transform.Find("abilityOrigin").Find("Canvas").Find("Arrow").gameObject.SetActive(false); ability.isCube = false; }
        }
    }
    void usePotion(KeyCode userInput, IEnumerator partUp, IEnumerator potioncall)
    {
        if (!isPlayerDead && Input.GetKeyDown(userInput))
        {
            Debug.Log("called");

            if (userHealth.value >= userHealth.maxValue)
            {
                userHealth.transform.Find("Text").GetComponent<Text>().text = userHealth.maxValue.ToString("N0");
                userHealth.value = userHealth.maxValue;
            }
            else if (userHealth.value <= -1)
            {
                userHealth.transform.Find("Text").GetComponent<Text>().text = userHealth.minValue.ToString("N0");
                userHealth.value = userHealth.minValue;
            }
            else
            {
                StartCoroutine(partUp);
                StartCoroutine(potioncall);

            }
        }

    }

    void mouseInputSystem()
    {
        if (!isPlayerDead && !isSystemGUI && !MOUSERIGHTHOLD && !isKeyHold) // adding more condition to this.
        {
            if (!isPlayerDead && Input.GetMouseButtonDown(0) && !MouseClicked)
            {
                getMouseInput(); mouseAllowed = true; MOUSECLICKED = true; Invoke("isHeld", 0.2f);
                StartCoroutine(spawnMousePrefabAndDelete(0.5f));
            } // MouseClick Effect
            else if (!isPlayerDead && Input.GetMouseButton(0) && MouseClicked)
            {
                lookAtMouseTransform(point.transform); getMouseInput(); MOUSECLICKED = false; MOUSEHOLD = true; isPlayerMoving = true; mouseAllowed = false;
            }
            else if (!isPlayerDead && Input.GetMouseButtonUp(0))
            { CancelInvoke("isHeld"); isPlayerMoving = false; MouseClicked = false; MOUSECLICKED = false; MOUSEHOLD = false; }
            else
            {
                Debug.Log("bug?");
            }
        }

        if (!isPlayerDead && mouseAllowed)
        {
            lookAtMouseTransform(point.transform);
            distance = Vector3.Distance(playerObjectPrefab.transform.position, point.transform.position);
            if (distance >= 1f) { isPlayerMoving = true; }
            else if (distance <= 0.4f) { isPlayerMoving = false; mouseAllowed = false; }
        }
        // if the mouse White Sphere touched.
        // if (!isPlayerDead && isMouseTouched)
        // {
        //     if (!mouseAllowed) { isMouseInCursor = false; isPlayerMoving = false; }
        // }
        if (!isPlayerDead && Input.GetMouseButton(1))
        {
            MOUSERIGHTHOLD = true;
            getMouseInput();
            distance = 0.3f;
            lookAtMouseTransform(point.transform);
            mouseHoldEffectTransform.transform.GetComponent<ParticleSystem>().Play();
            mouseHoldEffectTransform.transform.position = point.transform.position;

            // ui look at the gun prefab
            SystemItem gunItem = inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainWeapon").GetChild(0).GetComponent<SystemItem>();
            gunDisplayUI.LookAt(gunDisplayUI.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
            gunDisplayUI.gameObject.SetActive(true);
            gunDisplayUI.Find("Text").GetComponent<Text>().text = "Type: " + gunItem.gunName + "\nBullets:" + gunItem.bullets + "/" + gunItem.maxBullets + "\n" + "Clips:" + gunItem.clip;
            isPlayerMoving = false;
            mouseAllowed = false;
        }
        else if (!isPlayerDead && Input.GetMouseButtonUp(1) && MOUSERIGHTHOLD)
        {
            mouseHoldEffectTransform.transform.GetComponent<ParticleSystem>().Stop();
            gunDisplayUI.gameObject.SetActive(false);
            Debug.Log("??");
            MOUSERIGHTHOLD = false;
        }
    }

    public void isHeld()
    {
        MouseClicked = true;
    }

    public void CheckMouseTouched()
    {
        print("initiated");
        mouseAllowed = true;
    }
    void isRolledInvoke()
    {
        if (isWalkRolled)
        {
            isWalkRolled = false;
            isPlayerMoving = false;
            CancelInvoke(nameof(isRolledInvoke));
        }
        if (isRunRolled)
        {
            isRunRolled = false;
            isPlayerMoving = false;
            CancelInvoke(nameof(isRolledInvoke));
        }
        if (isPicking)
        {
            if (!walkToRun) isWalking = true;
            else if (walkToRun) isRunning = true;
            isPicking = false;
            isPlayerMoving = false;
            CancelInvoke(nameof(isRolledInvoke));
        }
        if (isAttacking)
        {
            isAttacking = false;
            //isPlayerMoving = true;
            CancelInvoke(nameof(isRolledInvoke));
        }

    }
    public void ActiveRagdoll()
    {
        playerAnimator.enabled = false;
        mainCollider.enabled = false;

        Collider[] ragDollColliders;
        Rigidbody[] ragDollRigidBodys;

        ragDollColliders = playerObject.transform.GetComponentsInChildren<Collider>();
        ragDollRigidBodys = playerObject.transform.GetComponentsInChildren<Rigidbody>();

        foreach (Collider col in ragDollColliders)
        {
            col.enabled = true;
        }
        foreach (Rigidbody rigid in ragDollRigidBodys)
        {
            rigid.isKinematic = false;
        }
    }
    public void DeactiveRagdoll()
    {

        Collider[] ragDollColliders;
        Rigidbody[] ragDollRigidBodys;

        ragDollColliders = playerObject.transform.GetComponentsInChildren<Collider>();
        ragDollRigidBodys = playerObject.transform.GetComponentsInChildren<Rigidbody>();

        foreach (Collider col in ragDollColliders)
        {
            col.enabled = false;
        }
        foreach (Rigidbody rigid in ragDollRigidBodys)
        {
            rigid.isKinematic = true;
        }
        mainCollider = GameObject.Find(playerObject.name).transform.GetComponent<CapsuleCollider>();
        mainCollider.enabled = true;
        mainRigidBody.isKinematic = false;
        //mainRigidBody = GameObject.Find(playerObject.name).transform.GetComponent<Rigidbody>();
        // mainRigidBody.isKinematic = false;
    }

    public void playerTakeDamage(float damageAmount)
    {
        //displayUIObject.SetActive(true);

        StartCoroutine(partCost(0.01f, damageAmount, 0.5f, "health"));
        if (playerHealth <= 0f)
        {
            // display Start Again screen.
            ActiveRagdoll();
            isPlayerDead = true;
            playerObject.tag = "Untagged";
            Rigidbody playerRigid = playerObject.GetComponent<Rigidbody>();
            playerRigid.isKinematic = true;
            playerRigid.detectCollisions = false;
        }
    }
    IEnumerator partCost(float second, float Amount, float decrement, string type) // new type
    {
        if (type == "health")
        {
            userHealth.transform.Find("Text").GetComponent<Text>().text = playerHealth.ToString("N0");
            userHealth.value -= decrement; // for HUI display
            playerHealth -= decrement; // physcial values.
            Amount -= decrement;
        }
        if (type == "part")
        {
            userMana.transform.Find("Text").GetComponent<Text>().text = playerMana.ToString("N0");
            userMana.value -= decrement; // for HUI display
            playerMana -= decrement; // physcial values.
            Amount -= decrement;
        }
        yield return new WaitForSeconds(second);

        if (decrement >= Amount)
        {
            StopCoroutine(partCost(second, Amount, decrement, type));
        }
        else if (decrement <= Amount)
        {
            StartCoroutine(partCost(second, Amount, decrement, type));
        }

    }
    IEnumerator partUp(float second, float Amount, float increment, string type) // new type
    {
        if (userHealth.value >= userHealth.maxValue)
        {

            StopCoroutine(partUp(second, Amount, increment, type));
            healthCall = 0f;
            userHealth.transform.Find("Text").GetComponent<Text>().text = userHealth.maxValue.ToString("N0");
            userHealth.value = (int)userHealth.maxValue;
        }
        else if (userHealth.value <= -1)
        {

            StopCoroutine(partUp(second, Amount, increment, type));
            healthCall = 0f;
            userHealth.transform.Find("Text").GetComponent<Text>().text = userHealth.minValue.ToString("N0");
            userHealth.value = (int)userHealth.minValue;
        }
        else
        {
            if (type == "health")
            {
                userHealth.transform.Find("Text").GetComponent<Text>().text = playerHealth.ToString("N0");
                userHealth.value += increment; // for HUI display
                playerHealth += increment; // physcial values.
                healthCall += increment;
                //  Amount += decrement;
            }
            if (type == "part")
            {
                userMana.transform.Find("Text").GetComponent<Text>().text = playerMana.ToString("N0");
                userMana.value += increment; // for HUI display
                playerMana += increment; // physcial values.
                                         //   Amount += decrement;
                healthCall += increment;
            }

            yield return new WaitForSeconds(second);
            if (healthCall >= Amount)
            {

                StopCoroutine(partUp(second, Amount, increment, type));
                healthCall = 0f;
            }
            else if (healthCall <= Amount)
            {
                StartCoroutine(partUp(second, Amount, increment, type));
            }

        }



    }

    IEnumerator callingPart(float second, float increment, string potionPosition)
    {
        float perIncrement = 0f;
        if (potionPosition == "mainPotionHealth") { increamentH += increment; perIncrement = increamentH; }
        else if (potionPosition == "mainPotionPart") { increamentM += increment; perIncrement = increamentM; }

        yield return new WaitForSeconds(second);

        if (perIncrement >= inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainPotions").Find(potionPosition).GetChild(0).GetComponent<SystemItem>().healthCallTime)
        {
            if (potionPosition == "mainPotionHealth")
            {
                // add max value here.
                inventory.transform.Find("playerHealthShort").Find("Slider").GetComponent<Slider>().value = 0f;
                inventory.transform.Find("playerHealthShort").Find("Slider").Find("Text").GetComponent<Text>().text = "";
                if (potionPosition == "mainPotionHealth") { increamentH = 0f; }
                else if (potionPosition == "mainPotionPart") { increamentM = 0f; }
                StopCoroutine(callingPart(second, increment, potionPosition));
            }
            else if (potionPosition == "mainPotionPart")
            {
                inventory.transform.Find("playerManaShort").Find("Slider").GetComponent<Slider>().value = 0f;
                inventory.transform.Find("playerManaShort").Find("Slider").Find("Text").GetComponent<Text>().text = "";
                if (potionPosition == "mainPotionHealth") { increamentH = 0f; }
                else if (potionPosition == "mainPotionPart") { increamentM = 0f; }
                StopCoroutine(callingPart(second, increment, potionPosition));

            }
        }
        else if (perIncrement <= inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainPotions").Find(potionPosition).GetChild(0).GetComponent<SystemItem>().healthCallTime)
        {
            if (potionPosition == "mainPotionHealth")
            {
                inventory.transform.Find("playerHealthShort").Find("Slider").GetComponent<Slider>().value = perIncrement;
                inventory.transform.Find("playerHealthShort").Find("Slider").Find("Text").GetComponent<Text>().text = perIncrement.ToString("0");
                StartCoroutine(callingPart(second, increment, potionPosition));
            }
            else if (potionPosition == "mainPotionPart")
            {
                inventory.transform.Find("playerManaShort").Find("Slider").GetComponent<Slider>().value = perIncrement;
                inventory.transform.Find("playerManaShort").Find("Slider").Find("Text").GetComponent<Text>().text = perIncrement.ToString("0");
                StartCoroutine(callingPart(second, increment, potionPosition));
            }
        }
    }

    IEnumerator callingMaskFragment(float second, float increment, string maskPosition) // you need to create a script that identifies all of it.
    {
        float perIncrement = 0f;
        if (maskPosition == "mask0") { incrementQ += increment; perIncrement = incrementQ; }
        else if (maskPosition == "mask1") { incrementW += increment; perIncrement = incrementW; }
        else if (maskPosition == "mask2") { incrementE += increment; perIncrement = incrementE; }
        else if (maskPosition == "mask3") { incrementR += increment; perIncrement = incrementR; }

        yield return new WaitForSeconds(second);

        if (perIncrement >= inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find(maskPosition).GetChild(0).GetComponent<SystemItem>().maskCallTime) // maskCallQ
        {
            Debug.Log("stoped");
            StopCoroutine(callingMaskFragment(second, increment, maskPosition));
            inventory.transform.Find("maskFragments").Find(maskPosition).Find("Slider").GetComponent<Slider>().value = 0f;
            inventory.transform.Find("maskFragments").Find(maskPosition).Find("Slider").Find("Text").GetComponent<Text>().text = "";
            if (maskPosition == "mask0") { incrementQ = 0f; }
            else if (maskPosition == "mask1") { incrementW = 0f; }
            else if (maskPosition == "mask2") { incrementE = 0f; }
            else if (maskPosition == "mask3") { incrementR = 0f; }
        }
        else if (perIncrement <= inventory.transform.Find("SYSTEM_playerInventory").Find("CharacterView").Find("SYSTEM_mainMask").Find(maskPosition).GetChild(0).GetComponent<SystemItem>().maskCallTime) // maskCallQ
        {
            inventory.transform.Find("maskFragments").Find(maskPosition).Find("Slider").GetComponent<Slider>().value = perIncrement;
            inventory.transform.Find("maskFragments").Find(maskPosition).Find("Slider").Find("Text").GetComponent<Text>().text = perIncrement.ToString("0");
            StartCoroutine(callingMaskFragment(second, increment, maskPosition));
        }
    }
    IEnumerator attackObject(float second)
    {
        // needs to be fixed
        attackAllowed = true;
        float damage = Random.Range(1f, 20f);
        Target.SendMessage("enemyTakeDamage", 100);
        yield return new WaitForSeconds(second);
        attackAllowed = false;
        StopCoroutine(attackObject(second));
    }
    void getMouseInput()
    {
        //isMouseHold++;
        RaycastHit hit;
        Ray ray;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            switch (hit.collider.tag)
            {
                case "Ground":
                    {
                        point.transform.position = hit.point;
                        if (!MOUSERIGHTHOLD || !isKeyHold)
                        {
                            isPlayerMoving = true;
                            isEnemyClicked = false;
                        }
                        break;
                    }
                case "Player":
                    {

                        break;
                    }
                case "Enemy":
                    {
                        point.transform.position = hit.point;
                        isPlayerMoving = true;
                        mouseAllowed = true;
                        lookAtMouseCursor(hit);
                        lookAtMouseTransform(hit.transform);
                        // isenemy? 
                        Target = hit.transform;
                        isEnemyClicked = true;
                        break;
                    }
                case "Item":
                    {
                        point.transform.position = hit.point;
                        isPlayerMoving = true;
                        mouseAllowed = true;
                        lookAtMouseCursor(hit);
                        lookAtMouseTransform(hit.transform);
                        // isenemy? 
                        Target = hit.transform;
                        isObjectClicked = true;

                        break;
                    }
                case "NPC":
                    {
                        point.transform.position = hit.point;
                        isPlayerMoving = true;
                        lookAtMouseCursor(hit);
                        lookAtMouseTransform(hit.transform);
                        // isenemy?
                        isEnemyClicked = false;
                        break;
                    }
                case "ITEM_DESTORYABLE":
                    {
                        point.transform.position = hit.point;
                        isPlayerMoving = true;
                        mouseAllowed = true;
                        lookAtMouseCursor(hit);
                        lookAtMouseTransform(hit.transform);
                        // isenemy? 
                        Target = hit.transform;
                        isDestroyableObjectClicked = true;

                        break;
                    }
            }
        }
    }


    void oneClickMovement() { lookAtMouseTransform(mouseCursor); isPlayerMoving = true; }
    void walkTowards()
    {
        if (isGrounded && !OnSlope()) playerObject.transform.position += playerObject.transform.forward * Time.deltaTime * playerMovementSpeed;
        else if (isGrounded && OnSlope()) playerObject.transform.position += playerObject.transform.forward * Time.deltaTime * playerMovementSpeed;
    }

    void getAbilityOriginTransform(Transform mousePosition, Transform abilityOriginTrasnform)
    {
        Vector3 dir = mousePosition.transform.position - abilityOriginTrasnform.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(abilityOriginTrasnform.transform.rotation, lookRotation, abilityOriginTurningSpeed).eulerAngles;
        abilityOriginTrasnform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    void lookAtMouseCursor(RaycastHit hit)
    {
        Vector3 dir = hit.point - playerObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(playerObject.transform.rotation, lookRotation, Time.deltaTime * playerTurningSpeed).eulerAngles;
        playerObject.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    void lookAtMouseTransform(Transform transform)
    {
        Vector3 dir = transform.position - playerObject.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(playerObject.transform.rotation, lookRotation, Time.deltaTime * playerTurningSpeed).eulerAngles;
        playerObject.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    IEnumerator spawnMousePrefabAndDelete(float second)
    {
        // Quaternion.LookRotation(-hit.normal * 2f)
        RaycastHit hit;
        Ray ray;
        // this needs to be customized.
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == "Ground")
            {
                GameObject spawnedMousePrefab = (GameObject)Instantiate(mouseEffectTransform, hit.point + new Vector3(0, 0.1f, 0), Quaternion.LookRotation(-hit.normal * 2f));
                spawnedMousePrefab.name = spawnedMousePrefab.name + mouseCount;
                mouseCount++;
                ParticleSystem spawnedParticleSystem = GameObject.Find(spawnedMousePrefab.name).transform.GetComponent<ParticleSystem>();
                spawnedParticleSystem.Play();

                yield return new WaitForSeconds(second);
                //resets the mouse prefab.
                if (mouseCount >= 10) mouseCount = 0;

                Destroy(spawnedMousePrefab);
                StopCoroutine(spawnMousePrefabAndDelete(second));
            }
        }

        // mouseCursor.transform.DetachChildren();

    }
    bool OnSlope()
    {
        if (Physics.Raycast(playerObject.transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up) return true;
            else return false;
        }
        return false;
    }


    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerObject.transform.position - new Vector3(0, -1, 0), variableDistance);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(playerObject.transform.position - new Vector3(0, -1, 0), enemyDetetctionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(playerObject.transform.position - new Vector3(0, -1, 0), npcDetectionRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(playerObject.transform.position - new Vector3(0, -1, 0), itemRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(playerObject.transform.position - new Vector3(0, -1, 0), destroyableRange);
    }

}