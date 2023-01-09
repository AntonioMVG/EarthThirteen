using UnityEngine;

[CreateAssetMenu(fileName = "Building Preset", menuName = "New Preset")]
public class BuildingPreset : ScriptableObject
{
    [Header("Info")]
    public int cost;
    public int costPerTurn;
    public GameObject prefab;
    public int population;
    public int jobs;
    public int food;
}
