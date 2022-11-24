using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userMiniMap : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform mainCamera;
    [SerializeField] Transform currentMiniMapObject;
    [SerializeField] int viewDifference;
    void Start()
    {
        currentMiniMapObject = gameObject.transform;
        playerTransform = GameObject.Find("player").transform;
      //  playerTransform.transform.position += new Vector3(0f, 3, 0f);
        mainCamera = GameObject.FindWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        currentMiniMapObject.transform.position = playerTransform.transform.position + new Vector3(0f, viewDifference, 0f);
      //  currentMiniMapObject.LookAt(playerTransform, Vector3.up + mainCamera.localPosition);
    }
}
