using Unity.Profiling;

class GameStats
{
    public static readonly ProfilerCategory MyProfilerCategory = ProfilerCategory.Scripts;

    public const string Name = "Platform Speed";
 
    public static readonly ProfilerCounterValue<float> PlatformSpeed =
        new ProfilerCounterValue<float>(MyProfilerCategory, "Speed", ProfilerMarkerDataUnit.Count);

    public static readonly ProfilerCounterValue<float> Distance =
        new ProfilerCounterValue<float>(MyProfilerCategory, "Distance", ProfilerMarkerDataUnit.Count);

    public static readonly ProfilerCounterValue<float> BrakingDistance =
        new ProfilerCounterValue<float>(MyProfilerCategory, "Braking Distance", ProfilerMarkerDataUnit.Count);

}


