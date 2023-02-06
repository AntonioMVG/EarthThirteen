[System.Serializable]
public sealed class CityZSerializer : ZSerializer.Internal.ZSerializer
{
    public UnityEngine.GameObject sun;
    public System.Single curDayTime;
    public System.Single dayTime;
    public System.Single multiplier;
    public System.Int32 day;
    public System.Int32 money;
    public System.Int32 curPopulation;
    public System.Int32 curJobs;
    public System.Int32 curFood;
    public System.Int32 polution;
    public System.Int32 maxPopulation;
    public System.Int32 maxJobs;
    public System.Int32 incomePerJobs;
    public TMPro.TextMeshProUGUI dayTxt;
    public TMPro.TextMeshProUGUI moneyTxt;
    public TMPro.TextMeshProUGUI populationTxt;
    public TMPro.TextMeshProUGUI jobsTxt;
    public TMPro.TextMeshProUGUI foodTxt;
    public TMPro.TextMeshProUGUI polutionTxt;
    public TMPro.TextMeshProUGUI hourTxt;
    public TMPro.TextMeshProUGUI multiplierTxt;
    public System.Collections.Generic.List<Building> buildings;
    public UnityEngine.GameObject containers;
    public UnityEngine.GameObject housesContainer;
    public UnityEngine.GameObject factoriesContainer;
    public UnityEngine.GameObject farmsContainer;
    public UnityEngine.GameObject roadsContainer;
    public UnityEngine.GameObject treesContainer;
    public UnityEngine.GameObject pipesContainer;
    public UnityEngine.UI.Button houseBt;
    public UnityEngine.UI.Button factoryBt;
    public UnityEngine.GameObject endGameInfo;
    public City instance;
    public System.Int32 groupID;
    public System.Boolean autoSync;

    public CityZSerializer(string ZUID, string GOZUID) : base(ZUID, GOZUID)
    {       var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID];
         sun = (UnityEngine.GameObject)typeof(City).GetField("sun").GetValue(instance);
         curDayTime = (System.Single)typeof(City).GetField("curDayTime").GetValue(instance);
         dayTime = (System.Single)typeof(City).GetField("dayTime").GetValue(instance);
         multiplier = (System.Single)typeof(City).GetField("multiplier").GetValue(instance);
         day = (System.Int32)typeof(City).GetField("day").GetValue(instance);
         money = (System.Int32)typeof(City).GetField("money").GetValue(instance);
         curPopulation = (System.Int32)typeof(City).GetField("curPopulation").GetValue(instance);
         curJobs = (System.Int32)typeof(City).GetField("curJobs").GetValue(instance);
         curFood = (System.Int32)typeof(City).GetField("curFood").GetValue(instance);
         polution = (System.Int32)typeof(City).GetField("polution").GetValue(instance);
         maxPopulation = (System.Int32)typeof(City).GetField("maxPopulation").GetValue(instance);
         maxJobs = (System.Int32)typeof(City).GetField("maxJobs").GetValue(instance);
         incomePerJobs = (System.Int32)typeof(City).GetField("incomePerJobs").GetValue(instance);
         dayTxt = (TMPro.TextMeshProUGUI)typeof(City).GetField("dayTxt").GetValue(instance);
         moneyTxt = (TMPro.TextMeshProUGUI)typeof(City).GetField("moneyTxt").GetValue(instance);
         populationTxt = (TMPro.TextMeshProUGUI)typeof(City).GetField("populationTxt").GetValue(instance);
         jobsTxt = (TMPro.TextMeshProUGUI)typeof(City).GetField("jobsTxt").GetValue(instance);
         foodTxt = (TMPro.TextMeshProUGUI)typeof(City).GetField("foodTxt").GetValue(instance);
         polutionTxt = (TMPro.TextMeshProUGUI)typeof(City).GetField("polutionTxt").GetValue(instance);
         hourTxt = (TMPro.TextMeshProUGUI)typeof(City).GetField("hourTxt").GetValue(instance);
         multiplierTxt = (TMPro.TextMeshProUGUI)typeof(City).GetField("multiplierTxt").GetValue(instance);
         buildings = (System.Collections.Generic.List<Building>)typeof(City).GetField("buildings").GetValue(instance);
         containers = (UnityEngine.GameObject)typeof(City).GetField("containers").GetValue(instance);
         housesContainer = (UnityEngine.GameObject)typeof(City).GetField("housesContainer").GetValue(instance);
         factoriesContainer = (UnityEngine.GameObject)typeof(City).GetField("factoriesContainer").GetValue(instance);
         farmsContainer = (UnityEngine.GameObject)typeof(City).GetField("farmsContainer").GetValue(instance);
         roadsContainer = (UnityEngine.GameObject)typeof(City).GetField("roadsContainer").GetValue(instance);
         treesContainer = (UnityEngine.GameObject)typeof(City).GetField("treesContainer").GetValue(instance);
         pipesContainer = (UnityEngine.GameObject)typeof(City).GetField("pipesContainer").GetValue(instance);
         houseBt = (UnityEngine.UI.Button)typeof(City).GetField("houseBt").GetValue(instance);
         factoryBt = (UnityEngine.UI.Button)typeof(City).GetField("factoryBt").GetValue(instance);
         endGameInfo = (UnityEngine.GameObject)typeof(City).GetField("endGameInfo").GetValue(instance);
         instance = (City)typeof(City).GetField("instance").GetValue(instance);
         groupID = (System.Int32)typeof(ZSerializer.PersistentMonoBehaviour).GetField("groupID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
         autoSync = (System.Boolean)typeof(ZSerializer.PersistentMonoBehaviour).GetField("autoSync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
    }

    public override void RestoreValues(UnityEngine.Component component)
    {
         typeof(City).GetField("sun").SetValue(component, sun);
         typeof(City).GetField("curDayTime").SetValue(component, curDayTime);
         typeof(City).GetField("dayTime").SetValue(component, dayTime);
         typeof(City).GetField("multiplier").SetValue(component, multiplier);
         typeof(City).GetField("day").SetValue(component, day);
         typeof(City).GetField("money").SetValue(component, money);
         typeof(City).GetField("curPopulation").SetValue(component, curPopulation);
         typeof(City).GetField("curJobs").SetValue(component, curJobs);
         typeof(City).GetField("curFood").SetValue(component, curFood);
         typeof(City).GetField("polution").SetValue(component, polution);
         typeof(City).GetField("maxPopulation").SetValue(component, maxPopulation);
         typeof(City).GetField("maxJobs").SetValue(component, maxJobs);
         typeof(City).GetField("incomePerJobs").SetValue(component, incomePerJobs);
         typeof(City).GetField("dayTxt").SetValue(component, dayTxt);
         typeof(City).GetField("moneyTxt").SetValue(component, moneyTxt);
         typeof(City).GetField("populationTxt").SetValue(component, populationTxt);
         typeof(City).GetField("jobsTxt").SetValue(component, jobsTxt);
         typeof(City).GetField("foodTxt").SetValue(component, foodTxt);
         typeof(City).GetField("polutionTxt").SetValue(component, polutionTxt);
         typeof(City).GetField("hourTxt").SetValue(component, hourTxt);
         typeof(City).GetField("multiplierTxt").SetValue(component, multiplierTxt);
         typeof(City).GetField("buildings").SetValue(component, buildings);
         typeof(City).GetField("containers").SetValue(component, containers);
         typeof(City).GetField("housesContainer").SetValue(component, housesContainer);
         typeof(City).GetField("factoriesContainer").SetValue(component, factoriesContainer);
         typeof(City).GetField("farmsContainer").SetValue(component, farmsContainer);
         typeof(City).GetField("roadsContainer").SetValue(component, roadsContainer);
         typeof(City).GetField("treesContainer").SetValue(component, treesContainer);
         typeof(City).GetField("pipesContainer").SetValue(component, pipesContainer);
         typeof(City).GetField("houseBt").SetValue(component, houseBt);
         typeof(City).GetField("factoryBt").SetValue(component, factoryBt);
         typeof(City).GetField("endGameInfo").SetValue(component, endGameInfo);
         typeof(City).GetField("instance").SetValue(component, instance);
         typeof(ZSerializer.PersistentMonoBehaviour).GetField("groupID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, groupID);
         typeof(ZSerializer.PersistentMonoBehaviour).GetField("autoSync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, autoSync);
    }
}