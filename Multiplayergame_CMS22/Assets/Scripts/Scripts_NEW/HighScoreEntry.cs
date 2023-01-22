using System.Globalization;
using TMPro;
using UnityEngine;

public class HighScoreEntry : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI objecstHitText;

    public void Setup(SaveFileData.GameDataModel gameDataModel)
    {
        nameText.text = gameDataModel.playerName;
        timeText.text = gameDataModel.secondsAlive.ToString(CultureInfo.InvariantCulture);
        objecstHitText.text = gameDataModel.objectsHit.ToString();
    }
}
