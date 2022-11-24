using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "itemData", menuName = "ITEM/itemData")]
public class itemData : ScriptableObject 
{
    [Header("your item attributes")]
    public int potionPosition; //if it 0 (left) or if it 1 (right)
    public string potionName;
    public GameObject mainPotionItemUI;
    public GameObject potionItemUI;
    [Header("Physical potion item")]
    public GameObject potionItemObject;
    [Header("UI images")]
    public Sprite potionImage;
    [Header("potion Attributes")]
    public float manaAmount;
    public float healthAmount;

    public float manaCooltime;
    public float healthCooltime;

    public float perMana;
    public float perHealth;
    public Sprite defaultEmptyImage;
}
