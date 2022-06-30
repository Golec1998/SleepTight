using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    
    public static void SaveData(int lvlNumber, float points)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/stage" + lvlNumber + ".f";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameSave save = new GameSave(points);

        formatter.Serialize(stream, save);
        stream.Close();
    }
/*
    public static GameSave LoadData(int lvlNumber)
    {

    }
*/
}
