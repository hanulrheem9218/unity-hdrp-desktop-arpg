using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class showDamageStatus : MonoBehaviour
{
    public Transform mainCamera;
    public Transform currentObjectUI;
    public GameObject currentObject;
    public Text currentText;
    public string damageRate;
    public RectTransform currentRectTransform;
    public bool isEffectEnabled01;
    public bool isEffectEnabled02;
    //[SerializeField] Image currentBorder;
    // Start is called before the first frame update
    void Start()
    {
        currentObject = gameObject;
        mainCamera = GameObject.FindWithTag("MainCamera").transform;
        currentObjectUI = GameObject.Find(currentObject.name).transform.Find("Canvas").transform;
        currentText = currentObjectUI.Find("Text").GetComponent<Text>();
        currentRectTransform = currentObjectUI.transform.GetComponent<RectTransform>();
        //currentBorder.sprite = null;
    }

    // Update is called once per frame this is for ui view
    void Update()
    {
        currentObjectUI.LookAt(currentObjectUI.transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
     
    }
    private void LateUpdate()
    {
        if (isEffectEnabled01)
        {
            damageEffect();
        }
        if(isEffectEnabled02)
        {
            damageEffect02();
        }
    }
    //destroy as soon it is activated.
    public IEnumerator destoryDamageStatus01(float destorySecond)
    {
        //  currentText.CrossFadeAlpha(0, 2, false);
        //currentText.text = damageRate;
        isEffectEnabled01 = true;
        yield return new WaitForSeconds(destorySecond);
        Destroy(currentObject);
        StopCoroutine(destoryDamageStatus01(destorySecond));
    }
    public IEnumerator destoryDamageStatus02(float destorySecond)
    {
        //  currentText.CrossFadeAlpha(0, 2, false);
        //currentText.text = damageRate;
        isEffectEnabled02 = true;
        yield return new WaitForSeconds(destorySecond);
        Destroy(currentObject);
        StopCoroutine(destoryDamageStatus02(destorySecond));
    }
    public void damageEffect()
    {
       
        if (currentRectTransform.localScale.x >= 0)
        {
            currentRectTransform.localScale -= new Vector3(0.0001f, 0.0001f, 0.0001f);
        }
    }
    
    public void damageEffect02()
    {
        currentRectTransform.sizeDelta = new Vector2(300, 200);
        if (currentRectTransform.localScale.x >= 0)
        {
            currentRectTransform.localScale -= new Vector3(0.0001f, 0.0001f, 0.0001f);
        }
    }
}
