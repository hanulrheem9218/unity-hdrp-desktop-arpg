using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class destroyableBarrier : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform itemDisplayUI;
    public GameObject displayUIObject;
    public Camera mainCamera;
    public Text objectText;

    [SerializeField] Transform currentObject;
    [SerializeField] float objectHealth;

    [SerializeField] bool ignoreUI;
    [SerializeField] Transform damageViewPosition;
    [SerializeField] int localDamageCount;

    [SerializeField] Slider updated_HealthUIObject;
    [SerializeField] float FixedHealth;

    [SerializeField] GameObject barrierDamageStatus;
    void Start()
    {
        currentObject = gameObject.transform;
        

        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        itemDisplayUI = GameObject.Find(currentObject.name).transform.Find("Canvas").transform;

        damageViewPosition = GameObject.Find(currentObject.name).transform.Find("damageView").transform;
        displayUIObject = itemDisplayUI.gameObject;
        objectText = itemDisplayUI.transform.Find("Slider").Find("Text").GetComponent<Text>();
        objectText.text = "Barrier";
        updated_HealthUIObject = GameObject.Find(currentObject.name).transform.Find("Canvas").transform.Find("Slider").GetComponent<Slider>();

        updated_HealthUIObject.maxValue = objectHealth;
        updated_HealthUIObject.minValue = 0f;
        updated_HealthUIObject.value = objectHealth;
    }

    // Update is called once per frame
    void Update()
    {
        itemDisplayUI.LookAt(itemDisplayUI.transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
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
    public void enemyTakeDamage(float damageAmount)
    {
        localDamageCount++; // this is for local damage count so it doesnt merge.
        GameObject damageView = Instantiate(barrierDamageStatus, damageViewPosition);
        damageView.name = "Barrier" + "_" + "DamageView" + localDamageCount;
        damageViewPosition.DetachChildren();
        showDamageStatus damageScript = GameObject.Find(damageView.name).transform.GetComponent<showDamageStatus>();
        //damageScript.StartCoroutine(damageScript.destoryDamageStatus01(1f));
        Text damageDisplayAmount = GameObject.Find(damageView.name).transform.Find("Canvas").transform.Find("Text").GetComponent<Text>();
        // damageDisplayAmount.color = Color.white; // in default damage
        int divided = ((int)FixedHealth / 2);

        if (damageAmount <= divided)
        {
            damageScript.StartCoroutine(damageScript.destoryDamageStatus01(1f));
            damageDisplayAmount.text = damageAmount.ToString("0");
            damageDisplayAmount.color = Color.white; // small damagge
        }
        else if (damageAmount <= 75f)
        {
            damageScript.StartCoroutine(damageScript.destoryDamageStatus02(1f));
            damageDisplayAmount.text = damageAmount.ToString("0");
            damageDisplayAmount.color = Color.yellow; // Mid Damage

        }
        else if (damageAmount <= FixedHealth)
        {
            damageScript.StartCoroutine(damageScript.destoryDamageStatus02(1f));
            damageDisplayAmount.text = damageAmount.ToString("0");
            damageDisplayAmount.color = Color.red; // in critical damage
        }


        damageDisplayAmount.CrossFadeAlpha(0, 0.3f, false);
        //Access rigidbody system
        // int randomX = Random.Range(-5, 15);
        int randomY = Random.Range(100, 120);
        int randomNX = Random.Range(-100, 120);
        //int randomNY = Random.Range(100, 120);
        Rigidbody damageRigidBody = GameObject.Find(damageView.name).GetComponent<Rigidbody>();
        damageRigidBody.AddForce(Vector3.up * randomY + Vector3.forward + randomNX * Vector3.right, ForceMode.Acceleration);


        // keyword  Bugfix
        //coreDetectionAI = coreBackAI.transform;
        ignoreUI = true;
        displayUIObject.SetActive(true);
        updated_HealthUIObject.value -= damageAmount;
        objectHealth -= damageAmount;
        if (objectHealth <= 0)
        {
            Destroy(gameObject);
            //coreDetectionAI = coreBackAI.transform;
        }
    }
}
