 //
 // Adds a custom module to the Profiler
 // 
 // See: https://docs.unity3d.com/Manual/Profiler-customizing-details-view.html
 //
 
 using Unity.Profiling;
 using Unity.Profiling.Editor;

 [System.Serializable]
 [ProfilerModuleMetadata("Platform Speed")] 
 public class GameProfilerModule : ProfilerModule
 {
    static readonly ProfilerCounterDescriptor[] k_Counters = new ProfilerCounterDescriptor[]
    {
        // Note that these name strings have to match those in GameStats
        new ProfilerCounterDescriptor("Speed", GameStats.MyProfilerCategory),
        new ProfilerCounterDescriptor("Distance", GameStats.MyProfilerCategory),
        new ProfilerCounterDescriptor("Braking Distance", GameStats.MyProfilerCategory),
    };

    public GameProfilerModule() : base(k_Counters) { }
}