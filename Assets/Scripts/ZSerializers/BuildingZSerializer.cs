[System.Serializable]
public sealed class BuildingZSerializer : ZSerializer.Internal.ZSerializer
{
    public BuildingPreset preset;
    public System.Boolean isNearRoad;
    public UnityEngine.GameObject car;
    public UnityEngine.GameObject human;
    public System.Int32 groupID;
    public System.Boolean autoSync;

    public BuildingZSerializer(string ZUID, string GOZUID) : base(ZUID, GOZUID)
    {       var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID];
         preset = (BuildingPreset)typeof(Building).GetField("preset").GetValue(instance);
         isNearRoad = (System.Boolean)typeof(Building).GetField("isNearRoad").GetValue(instance);
         car = (UnityEngine.GameObject)typeof(Building).GetField("car", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
         human = (UnityEngine.GameObject)typeof(Building).GetField("human", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
         groupID = (System.Int32)typeof(ZSerializer.PersistentMonoBehaviour).GetField("groupID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
         autoSync = (System.Boolean)typeof(ZSerializer.PersistentMonoBehaviour).GetField("autoSync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
    }

    public override void RestoreValues(UnityEngine.Component component)
    {
         typeof(Building).GetField("preset").SetValue(component, preset);
         typeof(Building).GetField("isNearRoad").SetValue(component, isNearRoad);
         typeof(Building).GetField("car", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, car);
         typeof(Building).GetField("human", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, human);
         typeof(ZSerializer.PersistentMonoBehaviour).GetField("groupID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, groupID);
         typeof(ZSerializer.PersistentMonoBehaviour).GetField("autoSync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, autoSync);
    }
}