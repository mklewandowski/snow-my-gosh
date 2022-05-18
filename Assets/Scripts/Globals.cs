using UnityEngine;

public class Globals
{
    // game scroll speed
    public static Vector3 ScrollSpeed = new Vector3(0, 0, 0);
    public static float minSpeed = 4f;
    public static float maxSpeed = 16f;
    public static float finishLineXPos = 800f;

    // moving direction
    public static Vector3 ScrollDirection = new Vector3(0, 0, -1f);

    public enum GameState {
        TitleScreen,
        Ready,
        Playing,
        ShowScore,
        Restart
    }
    public static GameState CurrentGameState = GameState.TitleScreen;

    public enum VehicleType {
        Default,
        Invincible
    }

    // audio and music
    public static bool AudioOn;
    public static bool MusicOn;

    // keep track of scoring
    public static float BestDistance = 0;
    public static float CurrentDistance = 0;

    public const string AudioPlayerPrefsKey = "Audio";
    public const string MusicPlayerPrefsKey = "Music";
    public const string BestDistancePlayerPrefsKey = "BestDistance";
    public const string VehicleTypePlayerPrefsKey = "VehicleType";

    public static void SaveIntToPlayerPrefs(string key, int val)
    {
        PlayerPrefs.SetInt(key, val);
    }
    public static int LoadIntFromPlayerPrefs(string key, int defaultVal = 0)
    {
        int val = PlayerPrefs.GetInt(key, defaultVal);
        return val;
    }

    public static void SaveFloatToPlayerPrefs(string key, float val)
    {
        PlayerPrefs.SetFloat(key, val);
    }
    public static float LoadFloatFromPlayerPrefs(string key)
    {
        float val = PlayerPrefs.GetFloat(key, 0f);
        return val;
    }

    public static string GetVehicleNameFromType(VehicleType type)
    {
        string name = "";
        if (type == Globals.VehicleType.Default)
            name = "Snow My Gosh";
        return name;
    }
}
