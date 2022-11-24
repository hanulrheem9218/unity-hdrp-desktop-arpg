using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class systemData 
{
    public int level;
    public float life;
    public float deffense;
    public float attack;

    public float[] playerPositions;
    // player data should contain Level , Life, Deffense, Attack.
    // Start is called before the first frame update

    public systemData(SystemGUI_Manager player)
    {
        // only takes give data only.
        level = player.level;
        life = player.life;
        deffense = player.deffense;

        playerPositions[0] = player.transform.position.x;
        playerPositions[1] = player.transform.position.y;
        playerPositions[2] = player.transform.position.z;
    }
}
