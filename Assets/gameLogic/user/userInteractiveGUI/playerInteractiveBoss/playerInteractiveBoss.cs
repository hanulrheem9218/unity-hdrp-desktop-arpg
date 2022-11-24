using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerInteractiveBoss : MonoBehaviour
{
    [SerializeField] private string bossName;
    [SerializeField] private Transform displayBoss;
    [SerializeField] private Text bossText;
    [SerializeField] private Slider bossHealthBar;
    // Image Alpha rate
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image fillImage;
    public float healthMax;
    public float healthMin;
    public float healthValue;
    [SerializeField] bool isPlayerDetected;
    [SerializeField] Transform currentObject;
    [SerializeField] Vector3 playerDetection;
    [SerializeField] LayerMask playerMask;
    [SerializeField] bool isBossInteractive;
    // Start is called before the first frame update
    void Start()
    {
        displayBoss = GameObject.Find("GUI_player").transform.Find("playerBossInfo").transform;
        bossHealthBar = displayBoss.Find("Slider").GetComponent<Slider>();
        backgroundImage = bossHealthBar.transform.Find("Background").transform.GetComponent<Image>();
        fillImage = bossHealthBar.transform.Find("FillArea").transform.Find("Fill").transform.GetComponent<Image>();
        bossText = displayBoss.Find("Text").GetComponent<Text>();
        bossText.text = bossName;
        //Boss display
        bossHealthBar.maxValue = healthMax;
        bossHealthBar.minValue = healthMin;
        bossHealthBar.value = healthValue;
        bossText.CrossFadeAlpha(0, 0, false);
        backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, 0f);
        fillImage.color = new Color(fillImage.color.r, fillImage.color.g, fillImage.color.b, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerDetected = Physics.CheckBox(currentObject.transform.position, playerDetection, Quaternion.identity, playerMask);
        if(isPlayerDetected && !isBossInteractive)
        {
            ShowBossDisplay();
            isBossInteractive = true;
        }
    }
    public void ShowBossDisplay()
    {
        bossText.CrossFadeAlpha(1, 0.1f, false);
        StartCoroutine(ChangeImageAlpha(0.01f));
    }
    public IEnumerator ChangeImageAlpha(float second)
    {
        for(float i = 0; i <= 1f; i+=0.01f)
        {
            backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, i);
            fillImage.color = new Color(fillImage.color.r, fillImage.color.g, fillImage.color.b, i);
            yield return new WaitForSeconds(second);
        }
        StopCoroutine(ChangeImageAlpha(second));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawCube(currentObject.transform.position, playerDetection * 2);
    }
}
