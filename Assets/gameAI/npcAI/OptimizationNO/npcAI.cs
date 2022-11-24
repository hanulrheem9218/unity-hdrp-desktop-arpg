using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class npcAI : MonoBehaviour
{
    //npc AI DATA SECTION
    // core items
    [Header("you must add this settings")]

    // this is changed.
    [SerializeField] int randomMovingRange;
    [SerializeField] Transform[] coreTargetPath;
    [SerializeField] Transform coreTarget;
    [SerializeField] Transform coreFrontAI;
    public float coreBackRadius;
    [SerializeField] Transform Target;
    [SerializeField] Transform coreRayView;

    // Trasnform Detection SECTION
    public Transform npcObject;
    public float playerDetectionRange;
    [SerializeField] bool isPlayerDetected;
    [SerializeField] LayerMask playerMask;

    // if Corpse founded.

    [SerializeField] float corpseDetectionRange;
    [SerializeField] bool isCorpseDetected;
    [SerializeField] LayerMask corpseMask;

    // UI SECTION
    [SerializeField] bool ignoreUI;
    [SerializeField] Transform damageViewPosition;
    public GameObject damageViewObject;

    public float npcHealth;
    [SerializeField] Slider updated_HealthUIObject;
    [SerializeField] float fixedHealth;
    [SerializeField] float localDamageCount;
    [SerializeField] Transform itemDisplayUI;
    [SerializeField] Transform mainCamera;
    [SerializeField] Text npcText;
    // displaying the gameObject
    [SerializeField] GameObject displayUIObject;
    // Range field properties STATUS properties DETECTION, SHOOT, MELEE
    public float coreDetectionRange;
    public float coreShootRange;
    public float coreMeleeRange;
    public float coreMovementSpeed;
    public float coreTurnSpeed;
    //Animation Section 
    [SerializeField] Animator npcAnimator;
    // Start is called before the first frame update


    //Additional attributes.
    [SerializeField] bool isnpcDead;

    //behaviour scripts
    [SerializeField] bool LookAtObject;
    public bool LookAtBack;
    public bool LookAtFront;
    [SerializeField] bool LookShootPlayer;
    [SerializeField] bool LookAttackPlayer;
    // is it allowed ? relates to animation status.
    [SerializeField] bool attackAllowed;
    [SerializeField] bool shootAllowed;

    // Changing next wave.
    [SerializeField] private int TargetIndex = 0;
    [SerializeField] private int TargetMaxPath = 0;
    [SerializeField] private bool npcFollowPrevious;
    [SerializeField] private bool npcFollowNext;
    [SerializeField] private bool npcFollowReset;
    [Header("Initiate Path follow")]
    [SerializeField] private bool initiateFollowPath;
    //Test outputs



    // Ragdoll fucntion
    [SerializeField] CapsuleCollider mainCollider;
    [SerializeField] Rigidbody mainRigidBody;


    [SerializeField] bool randomMove;
    // new Engine
    [SerializeField] npcBehaviours NPC_SYSTEM;
    void Start()
    {
        NPC_SYSTEM = FindObjectOfType<npcBehaviours>();
        //npcObject = gameObject.transform;
        //mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        //itemDisplayUI = GameObject.Find(npcObject.name).transform.Find("Canvas").transform.Find("Slider").transform;
        //damageViewPosition = GameObject.Find(npcObject.name).transform.Find("damageView").transform;
        //displayUIObject = itemDisplayUI.gameObject;
        //npcText = itemDisplayUI.transform.Find("Text").GetComponent<Text>();
        //
        //// more preferences
        //updated_HealthUIObject = GameObject.Find(npcObject.name).transform.Find("Canvas").transform.Find("Slider").GetComponent<Slider>();
        //npcText.text = npcObject.name;
        //displayUIObject.SetActive(false);

        // temp
        coreFrontAI = GameObject.Find("npcManager").transform;

        npcObject = gameObject.transform;
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().transform;
        itemDisplayUI = GameObject.Find(npcObject.name).transform.Find("Canvas").transform;

        damageViewPosition = GameObject.Find(npcObject.name).transform.Find("damageView").transform;
        displayUIObject = itemDisplayUI.gameObject;
        npcText = itemDisplayUI.transform.Find("Slider").Find("Text").GetComponent<Text>();
        updated_HealthUIObject = GameObject.Find(npcObject.name).transform.Find("Canvas").transform.Find("Slider").GetComponent<Slider>();
        // more preferences
        updated_HealthUIObject.maxValue = npcHealth;
        updated_HealthUIObject.minValue = 0f;
        updated_HealthUIObject.value = npcHealth;
        npcText.text = npcObject.name;
        fixedHealth = npcHealth;
        //  mainCollider = GameObject.Find(npcObject.name).transform.GetComponent<CapsuleCollider>();
        //  mainCollider.enabled = true;
        displayUIObject.SetActive(false);
        NPC_SYSTEM.IdentifyCoreTargets(coreTargetPath,coreTarget,TargetMaxPath);
        NPC_SYSTEM.DeactiveRagdoll(npcObject,mainCollider,mainRigidBody);


    }
   
  
   

    public void OnMouseEnter()
    {
        displayUIObject.SetActive(true);
    }
    public void OnMouseExit()
    {
        if (!ignoreUI)
        {
            displayUIObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {

        // check if any dead corpses are founded
        // isCorpseDetected = Physics.CheckSphere(npcObject.transform.position, corpseDetectionRange, corpseMask);
        // isPlayerDetected = Physics.CheckSphere(npcObject.transform.position, playerDetectionRange, playerMask);
        // sightOnPlayer();
        // sightOnCorpse();
        // Behaviour status
        NPC_SYSTEM.interactiveDisplay(itemDisplayUI,mainCamera);
        if (!isnpcDead) NPC_SYSTEM.getTargetWithTransform(npcObject,coreTarget,
            Target,coreTargetPath,coreDetectionRange,coreShootRange,coreMeleeRange,
            coreBackRadius,coreMovementSpeed,3,TargetIndex,TargetMaxPath,randomMovingRange
            ,LookAttackPlayer,LookAtObject,LookShootPlayer,LookAtBack,LookAtFront,isnpcDead,initiateFollowPath,
            randomMove,npcFollowNext,npcFollowReset,npcFollowPrevious);
        // if it dies

        // Old detection system but it can 
        if (!isnpcDead && LookAtObject) { NPC_SYSTEM.coreLookAtObject(Target,npcObject,coreTurnSpeed); NPC_SYSTEM.coreWalkForward(npcObject,coreMovementSpeed); }
        if (!isnpcDead && LookAtBack) { NPC_SYSTEM.coreLookAtBack(npcObject,coreTarget,coreTurnSpeed); NPC_SYSTEM.coreWalkForward(npcObject,coreMovementSpeed); }
        if (!isnpcDead && LookAtFront) { NPC_SYSTEM.coreLookAtFront(coreFrontAI,npcObject,coreTurnSpeed); }
        // Attacking status.
        if (!isnpcDead && LookShootPlayer) NPC_SYSTEM.coreLookAtObject(Target,npcObject,coreTurnSpeed);

        // this property only happen once.
        if (!isnpcDead && LookShootPlayer) NPC_SYSTEM.coreLookAtObject(Target,npcObject,coreTurnSpeed);

        // animation status SMART AI CONDITIONS NOW
        if (!isnpcDead && LookAtObject || !isnpcDead && LookAtBack) npcAnimator.SetBool("isWalking", true);
        else npcAnimator.SetBool("isWalking", false);
        //  if (!isnpcDead && LookAtBack) npcAnimator.SetBool("isWalking", true);
        //  else npcAnimator.SetBool("isWalking", false);
        //  if (!isnpcDead && LookAtFront) npcAnimator.SetBool("isWalking", true);
        //  else npcAnimator.SetBool("isWalking", false);
        if (!isnpcDead && LookAttackPlayer) npcAnimator.SetBool("isAttacking", true);
        else npcAnimator.SetBool("isAttacking", false);
        //   if (!isnpcDead && LookShootPlayer) npcAnimator.SetBool("isShooting", true);
        //   else npcAnimator.SetBool("isShooting", false);
    }

   
    private void OnDrawGizmos()
    {
        // those are deteciton range fields.
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(npcObject.transform.position, playerDetectionRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(npcObject.transform.position, corpseDetectionRange);
        //shooting attributes
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(npcObject.transform.position, coreDetectionRange);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(npcObject.transform.position, coreShootRange);
        Gizmos.DrawWireSphere(npcObject.transform.position, coreMeleeRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(coreTarget.transform.position, coreBackRadius);
        Gizmos.DrawWireSphere(coreFrontAI.transform.position, coreBackRadius);
    }
}
