using UnityEngine;
using TMPro;

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

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
}
