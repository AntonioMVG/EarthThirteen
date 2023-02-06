using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ReferenceHolder : MonoBehaviour
{
    public static ReferenceHolder instance;

    [Header("HUD")]
    public TextMeshProUGUI dayTxt;
    public TextMeshProUGUI moneyTxt;
    public TextMeshProUGUI populationTxt;
    public TextMeshProUGUI jobsTxt;
    public TextMeshProUGUI foodTxt;
    public TextMeshProUGUI polutionTxt;
    public TextMeshProUGUI hourTxt;
    public TextMeshProUGUI multiplierTxt;

    [Header("HUD Buttons")]
    public Button houseBt;
    public Button factoryBt;

    [Header("End Game")]
    public GameObject endGameInfo;

    [Header("Day")]
    public GameObject sun;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
}
