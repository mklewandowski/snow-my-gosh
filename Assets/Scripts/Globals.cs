using UnityEngine;

public class Globals
{
    // game scroll speed
    public static Vector3 ScrollSpeed = new Vector3(0, 0, 0);
    public static float minSpeed = 15f;
    public static float maxSpeed = 45f;
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
        Blizzard,
        PineTree,
        EggNog,
        FrozenLake,
        Cardinal,
        Cocoa,
        CandyCane,
        NorthStar,
        Sweater,
        Gingerbread,
        Tinsel,
        Santa,
        Misletoe,
        Icicle,
        CandyCaneMint,
        Reindeer,
        Elf,
        WinterSunset,
        CandyCaneBerry,
        SnowCat,
        SnowWagon,
        Invincible
    }

    // audio and music
    public static bool AudioOn;
    public static bool MusicOn;

    // keep track of scoring
    public static int BestDistance = 0;
    public static int CurrentDistance = 0;

    // keep track of coins
    public static int Coins = 0;

    public const string AudioPlayerPrefsKey = "Audio";
    public const string MusicPlayerPrefsKey = "Music";
    public const string BestDistancePlayerPrefsKey = "BestDistance";
    public const string VehicleTypePlayerPrefsKey = "VehicleType";
    public const string CoinsPlayerPrefsKey = "Coins";

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
        else if (type == Globals.VehicleType.Blizzard)
            name = "Blizzard";
        else if (type == Globals.VehicleType.PineTree)
            name = "Pine Tree";
        else if (type == Globals.VehicleType.EggNog)
            name = "Egg Nog";
        else if (type == Globals.VehicleType.FrozenLake)
            name = "Frozen Lake";
        else if (type == Globals.VehicleType.Cardinal)
            name = "Winter Cardinal";
        else if (type == Globals.VehicleType.Cocoa)
            name = "Hot Cocoa";
        else if (type == Globals.VehicleType.CandyCane)
            name = "Candy Cane";
        else if (type == Globals.VehicleType.NorthStar)
            name = "North Star";
        else if (type == Globals.VehicleType.Sweater)
            name = "Ugly Sweater";
        else if (type == Globals.VehicleType.Gingerbread)
            name = "Gingerbread";
        else if (type == Globals.VehicleType.Tinsel)
            name = "Tinsel";
        else if (type == Globals.VehicleType.Santa)
            name = "Santa's Helper";
        else if (type == Globals.VehicleType.Misletoe)
            name = "Misletoe";
        else if (type == Globals.VehicleType.Icicle)
            name = "Icicle";
        else if (type == Globals.VehicleType.CandyCaneMint)
            name = "Mint Candy Cane";
        else if (type == Globals.VehicleType.Reindeer)
            name = "Reindeer";
        else if (type == Globals.VehicleType.Elf)
            name = "Elf";
        else if (type == Globals.VehicleType.WinterSunset)
            name = "Winter Sunset";
        else if (type == Globals.VehicleType.CandyCaneBerry)
            name = "Strawberry Candy Cane";
        else if (type == Globals.VehicleType.SnowCat)
            name = "Snow Cat";
        else if (type == Globals.VehicleType.SnowWagon)
            name = "Snow Wagon";
        return name;
    }
}
