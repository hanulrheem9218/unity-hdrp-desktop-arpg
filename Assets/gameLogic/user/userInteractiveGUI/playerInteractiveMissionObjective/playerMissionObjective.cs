using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMissionObjective : MonoBehaviour
{
    [SerializeField] private string missionObjectiveName;
    [SerializeField] private string missionDescription;
    [SerializeField] Text missionObjectNameText;
    [SerializeField] Text missionDescriptionText;

    [SerializeField] bool isPlayerDetected;
    [SerializeField] Transform currentObject;
    [SerializeField] Vector3 playerDetection;
    [SerializeField] LayerMask playerMask;
    [SerializeField] bool isMissionInteractive;
    // Start is called before the first frame update
    void Start()
    {
        missionObjectNameText = GameObject.Find("GUI_player").transform.Find("playerMissionObjective").transform.Find("playerObjective").transform.GetComponent<Text>();
        missionDescriptionText = GameObject.Find("GUI_player").transform.Find("playerMissionObjective").transform.Find("ObjectiveDescription").transform.GetComponent<Text>();
        missionDescriptionText.CrossFadeAlpha(0, 0, false);
        missionObjectNameText.CrossFadeAlpha(0, 0, false);
        missionObjectNameText.text = missionObjectiveName;
        missionDescriptionText.text = missionDescription;
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerDetected = Physics.CheckBox(currentObject.transform.position, playerDetection, Quaternion.identity, playerMask);
        if (isPlayerDetected && !isMissionInteractive)
        {
            ShowMissionObjective();
            isMissionInteractive = true;
        }
    }
    public void ShowMissionObjective()
    {
        missionObjectNameText.CrossFadeAlpha(1, 1f, false);
        missionDescriptionText.CrossFadeAlpha(1, 1f, false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(currentObject.transform.position, playerDetection * 2);
    }
}
