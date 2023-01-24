using System;
using UnityEngine;

[Serializable]
public class GameData : MonoBehaviour
{
    public string playerName;
    public int health;
    public int objectsHit;
    public float secondsAlive;

    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        ResetStats();
    }

    public void ResetStats()
    {
        health = 5;
        objectsHit = 0;
        secondsAlive = 0.0f;
    }
}
