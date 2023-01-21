using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class SaveFileData
{
    public List<GameDataModel> saveData = new List<GameDataModel>();

    public SaveFileData()
    {
        
    }
    
    [Serializable]
    public class GameDataModel
    {
        public string playerName;
        public int health;
        public int objectsHit;
        public float secondsAlive;

        public GameDataModel()
        {
            
        }
        
        public GameDataModel(GameData gameData)
        {
            playerName = gameData.playerName;
            health = gameData.health;
            objectsHit = gameData.objectsHit;
            secondsAlive = gameData.secondsAlive;
        }
    }
    
    public static void WriteDataToFile(GameData gameData)
    {
        string fileName = Path.Combine(Application.persistentDataPath, "SaveData.json");
        SaveFileData saveFileData = new SaveFileData();
        JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };
        
        if (File.Exists(fileName))
        {
            string fileContent = File.ReadAllText(fileName);
            saveFileData = JsonConvert.DeserializeObject<SaveFileData>(fileContent, jsonSerializerSettings);
        }
        else
        {
            saveFileData = new SaveFileData();
        }
        saveFileData.saveData.Add(new SaveFileData.GameDataModel(gameData));
        File.WriteAllText(fileName, JsonConvert.SerializeObject(saveFileData, jsonSerializerSettings));
    }

    public static SaveFileData GetSaveFileData()
    {
        string fileName = Path.Combine(Application.persistentDataPath, "SaveData.json");
        SaveFileData saveFileData = new SaveFileData();
        JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };
        
        if (File.Exists(fileName))
        {

            string fileContent = File.ReadAllText(fileName);
            saveFileData = JsonConvert.DeserializeObject<SaveFileData>(fileContent, jsonSerializerSettings);
        }

        return saveFileData;
    }
}
