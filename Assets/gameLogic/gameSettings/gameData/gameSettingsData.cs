using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "gameData", menuName = "GAME/gameData")]
public class gameSettingsData : ScriptableObject
{
    [Header("Game Preference Settings")]
    public float mouseSpeed;
    public KeyCode customAbilityOne;
    public KeyCode customAbilityTwo;
    public KeyCode customAbilityThr;
    public KeyCode customAbilityFou;
    public bool isLowGraphic = true;
    public bool isMediumGraphic;
    public bool isHighGraphic;
    public float masterSound;
    public float vfxSound;
    public float bgmSound;
    // more attributes will be added

}
