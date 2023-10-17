 using Unity.Profiling;
 using Unity.Profiling.Editor;

 [System.Serializable]
 [ProfilerModuleMetadata("Platform Speed")] 
 public class GameProfilerModule : ProfilerModule
 {
    static readonly ProfilerCounterDescriptor[] k_Counters = new ProfilerCounterDescriptor[]
    {
        new ProfilerCounterDescriptor("Speed", GameStats.MyProfilerCategory),
        new ProfilerCounterDescriptor("Distance", GameStats.MyProfilerCategory),
        new ProfilerCounterDescriptor("Braking Distance", GameStats.MyProfilerCategory),
    };

    // // Ensure that both ProfilerCategory.Scripts and ProfilerCategory.Memory categories are enabled when our module is active.
    // static readonly string[] k_AutoEnabledCategoryNames = new string[]
    // {
    //     ProfilerCategory.Scripts.Name,
    //     ProfilerCategory.Memory.Name
    // };


    public GameProfilerModule() : base(k_Counters) { }
}