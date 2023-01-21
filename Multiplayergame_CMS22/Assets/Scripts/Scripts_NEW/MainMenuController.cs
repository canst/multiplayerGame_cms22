using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject highScoreContainer;
    [SerializeField] GameObject introVideoContainer;
    [SerializeField] Button startGameButton;
    [SerializeField] GameData _gameData;
    
    [Header("HighScore")]
    [SerializeField] HighScoreEntry highScoreEntryPrefab;
    [SerializeField] Transform highScoreEntryParentTransform;

    
    
    private AsyncOperation _gameSceneLoad;
    
    private void Start()
    {
        introVideoContainer.SetActive(true);
        highScoreContainer.SetActive(false);
        startGameButton.interactable = false;
        if (!_gameData)
            _gameData = GameObject.FindObjectOfType<GameData>();
        if (!_gameData)
            Debug.LogError("no game data game object found!");
    }
    
    public void FillHighScoreBoard()
    {
        SaveFileData saveFileData = SaveFileData.GetSaveFileData();
        foreach (Transform childTransform in highScoreEntryParentTransform)
        {
            Destroy(childTransform.gameObject);
        }

        List<SaveFileData.GameDataModel> gameDataModels = saveFileData.saveData.OrderBy(entry => entry.secondsAlive).Reverse().ToList();

        foreach (var gameDataModel in gameDataModels)
        {
            HighScoreEntry entry = Instantiate(highScoreEntryPrefab, highScoreEntryParentTransform);
            entry.Setup(gameDataModel);
        }
    }

    public void ToggleIntroVideo()
    {
        introVideoContainer.SetActive(!introVideoContainer.activeSelf);
    }

    public void StartGame()
    {
        // preload game scene
        _gameSceneLoad = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive); // load game scene
        _gameSceneLoad.completed += operation =>
        {
            Debug.Log("completed loading game scene");
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
            SceneManager.UnloadSceneAsync(0);
        };
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetPlayerName(string playerName)
    {
        if (string.IsNullOrEmpty(playerName) || string.IsNullOrWhiteSpace(playerName))
        {
            startGameButton.interactable = false;
            Debug.LogError("player name can not be empty or just a whitespace!");
            return;
        }

        startGameButton.interactable = true;
        _gameData.playerName = playerName;
    }
    
}
