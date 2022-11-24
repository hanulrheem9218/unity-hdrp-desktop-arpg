using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class systemDataManager
{
    // saving player data system files.
    public static void SavePlayer(InputSystem player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.unvdata";
        FileStream stream = new FileStream(path, FileMode.Create);

        playerData data = new playerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    // loading player data files

    public static playerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.unvdata";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            playerData data = formatter.Deserialize(stream) as playerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not find in " + path);
            return null;
        }
    }

    //more lists.
    
    public static void SaveSettings(SystemGUI_Manager settings)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/settings.unvdata";
        FileStream stream = new FileStream(path, FileMode.Create);

        systemData data = new systemData(settings);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static systemData LoadSetting()
    {
        string path = Application.persistentDataPath + "/settings.unvdata";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            systemData data = formatter.Deserialize(stream) as systemData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not find in " + path);
            return null;
        }
    }
}
