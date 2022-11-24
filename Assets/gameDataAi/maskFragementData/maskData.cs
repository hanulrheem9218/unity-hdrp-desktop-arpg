using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="maskData", menuName = "ITEM/maskData")]
public class maskData : ScriptableObject
{
    [Header("Two main UI objects")]
    public int maskPosition;
    public string maskName;
    public GameObject mainMaskItemUI;
    public GameObject maskItemUI;
    [Header("Physical object")]
    public GameObject maskItemObject;
    [Header("UI images")]
    public Sprite maskImage;
    [Header("Mask Attributes")]
    public float maskDamage;
    public string maskAbility;
    public float maskCoolTime;
    public Sprite defaultEmptyImage;
    [Header("VFX Effect")]
    public GameObject maskVFX;
}
