using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    
    public static void SaveData(string lvlName, int points)
    {
        GameSave old = LoadData(lvlName);
        if(old == null)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/stage" + lvlName + ".f";
            FileStream stream = new FileStream(path, FileMode.Create);

            GameSave save = new GameSave(points);

            formatter.Serialize(stream, save);
            stream.Close();
        }
        else if(points > old.lvlScore)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/stage" + lvlName + ".f";
            FileStream stream = new FileStream(path, FileMode.Create);

            GameSave save = new GameSave(points);

            formatter.Serialize(stream, save);
            stream.Close();
        }
    }

    public static GameSave LoadData(string lvlName)
    {
        string path = Application.persistentDataPath + "/stage" + lvlName + ".f";
        Debug.Log(path);
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameSave save = formatter.Deserialize(stream) as GameSave;
            stream.Close();

            return save;
        }
        else
        {
            Debug.LogError("Save file missing or corrupted in " + path);
            return null;
        }
    }

}
