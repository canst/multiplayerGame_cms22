using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI playerNameText;
    [SerializeField] TextMeshProUGUI timeAliveText;
    [SerializeField] TextMeshProUGUI timeAliveGameOverText;
    [SerializeField] TextMeshProUGUI destroyedObjectsText;
    [SerializeField] GameObject gameOverUIGameObject;

    [SerializeField] Player player;
    [SerializeField] int objectInterval;
    [SerializeField] Sprite fullHpImage;
    [SerializeField] Sprite emptyHpImage;
    [SerializeField] List<Image> healthImages;

    private GameData _gameData;
    private List<CreepyFlyingObject> _creepyFlyingObjects = new List<CreepyFlyingObject>();

    private bool _gameStarted;
    private bool _gameRunning;
    private float _timeAlive;
    private float _timeToNextObject;
    

    private void Awake()
    {
        _creepyFlyingObjects = GameObject.FindObjectsOfType<CreepyFlyingObject>().ToList();
        _gameData = GameObject.FindObjectOfType<GameData>();
        if (!_gameData)
        {
            Debug.LogError("no game data object found!");
        }
    }

    private void Start()
    {
        gameOverUIGameObject.SetActive(false);
        player.OnTakeDamage += PlayerTakesDamage;
        player.OnCreepyObjectDestroyed += CreepyObjectDestroyed;
        destroyedObjectsText.text = _gameData.objectsHit.ToString();
        playerNameText.text = _gameData.playerName;
        foreach (var hpImage in healthImages)
        {
            hpImage.sprite = fullHpImage;
        }
    }

    private void FixedUpdate()
    {
        if (!_gameStarted)
        {
            _timeToNextObject += Time.deltaTime;
            if (_timeToNextObject >= 1)
            {
                _gameStarted = true;
                _gameRunning = true;
                _timeToNextObject = 0.0f;
            }
        }
        else if (_gameRunning)
        {
            _timeAlive += Time.deltaTime;
            _timeToNextObject += Time.deltaTime;
            if (_timeToNextObject >= objectInterval && _creepyFlyingObjects.Count > 0)
            {
                var creepyObject = _creepyFlyingObjects[Random.Range(0, _creepyFlyingObjects.Count)];
                _creepyFlyingObjects.Remove(creepyObject);
                creepyObject.Activate();
                _timeToNextObject = 0.0f;
            }

            timeAliveText.text = _timeAlive.ToString("F2");
        }
    }

    private void OnDestroy()
    {
        player.OnTakeDamage -= PlayerTakesDamage;
        player.OnCreepyObjectDestroyed -= CreepyObjectDestroyed;
    }

    private void PlayerTakesDamage(int damage)
    {
        
        for (int i = 0; i < healthImages.Count; i++)
        {
            healthImages[i].sprite = i < _gameData.health ? fullHpImage : emptyHpImage;
        }
        
        _gameData.health -= damage;
        if (_gameData.health <= 0)
        {
            EndGame();
        }
    }

    private void CreepyObjectDestroyed()
    {
        destroyedObjectsText.text = _gameData.objectsHit.ToString();
        _gameData.objectsHit += 1;
        if (CheckWinCondition())
            EndGame();
            
    }

    private bool CheckWinCondition()
    {
        return _creepyFlyingObjects.Count == 0 && GameObject.FindObjectOfType<CreepyFlyingObject>();
    }

    private void EndGame()
    {
        _gameRunning = false;
        _gameData.secondsAlive = _timeAlive;
        gameOverUIGameObject.SetActive(true);
        timeAliveGameOverText.text = _timeAlive.ToString(CultureInfo.InvariantCulture);
        SaveFileData.WriteDataToFile(_gameData);
    }
    

    public void ResetGame()
    {
        _gameData.ResetStats();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        _gameData.ResetStats();
        AsyncOperation mainMenuSceneLoad = SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive); // load game scene
        mainMenuSceneLoad.completed += operation =>
        {
            Debug.Log("completed loading main menu scene");
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
            SceneManager.UnloadSceneAsync(1);
        };
    }
}
