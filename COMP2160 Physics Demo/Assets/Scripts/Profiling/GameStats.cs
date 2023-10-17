//
// Storage for profiling data
//
// See: https://docs.unity3d.com/Manual/Profiler-customizing-details-view.html

using Unity.Profiling;

class GameStats
{
    public static readonly ProfilerCategory MyProfilerCategory = ProfilerCategory.Scripts;

    public const string Name = "Platform Speed";
 
    // Note that these name strings need to match the names used in the ProfilerModule
    public static readonly ProfilerCounterValue<float> PlatformSpeed =
        new ProfilerCounterValue<float>(MyProfilerCategory, "Speed", ProfilerMarkerDataUnit.Count);

    public static readonly ProfilerCounterValue<float> Distance =
        new ProfilerCounterValue<float>(MyProfilerCategory, "Distance", ProfilerMarkerDataUnit.Count);

    public static readonly ProfilerCounterValue<float> BrakingDistance =
        new ProfilerCounterValue<float>(MyProfilerCategory, "Braking Distance", ProfilerMarkerDataUnit.Count);

}


