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
        MoonlessMidnight,
        SkiTrip,
        SnowMan,
        Penguin,
        TenBelow,
        GiftWrapped,
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

    public static int MaxVehicles = 40;
    public static int[] VehicleUnlockStates = new int[MaxVehicles];

    public const string AudioPlayerPrefsKey = "Audio";
    public const string MusicPlayerPrefsKey = "Music";
    public const string BestDistancePlayerPrefsKey = "BestDistance";
    public const string VehicleTypePlayerPrefsKey = "VehicleType";
    public const string CoinsPlayerPrefsKey = "Coins";
    public const string VehicleUnlockPlayerPrefsKey = "Vehicle";

    public static void LoadVehicleUnlockStatesFromPlayerPrefs()
    {
        for (int x = 0; x < MaxVehicles; x++)
        {
            int unlock = LoadIntFromPlayerPrefs(VehicleUnlockPlayerPrefsKey + x.ToString());
            VehicleUnlockStates[x] = unlock;
        }
        VehicleUnlockStates[0] = 1;
    }

    public static void UnlockVehicle(int vehicleNum)
    {
        VehicleUnlockStates[vehicleNum] = 1;
        SaveIntToPlayerPrefs(VehicleUnlockPlayerPrefsKey + vehicleNum.ToString(), 1);
    }

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
        else if (type == Globals.VehicleType.MoonlessMidnight)
            name = "Moonless Midnight";
        else if (type == Globals.VehicleType.SkiTrip)
            name = "Ski Trip";
        else if (type == Globals.VehicleType.SnowMan)
            name = "Snow Man";
        else if (type == Globals.VehicleType.Penguin)
            name = "Penguin";
        else if (type == Globals.VehicleType.TenBelow)
            name = "10 Below";
        else if (type == Globals.VehicleType.GiftWrapped)
            name = "Gift Wrapped";
        return name;
    }

    public static Color GetVehicleDebrisColorFromType(VehicleType type)
    {
        if (type == Globals.VehicleType.Default)
            return (new Color(255f/255f, 106f/255f, 0));
        else if (type == Globals.VehicleType.Blizzard)
            return Color.white;
        else if (type == Globals.VehicleType.PineTree)
            return (new Color(0/255f, 87f/255f, 0));
        else if (type == Globals.VehicleType.EggNog)
            return (new Color(255f/255f, 209f/255f, 169f/255f));
        else if (type == Globals.VehicleType.FrozenLake)
            return (new Color(53f/255f, 255f/255f, 255f/255f));
        else if (type == Globals.VehicleType.Cardinal)
            return Color.red;
        else if (type == Globals.VehicleType.Cocoa)
            return (new Color(106f/255f, 52f/255f, 0f/255f));
        else if (type == Globals.VehicleType.CandyCane)
            return Color.red;
        else if (type == Globals.VehicleType.NorthStar)
            return Color.yellow;
        else if (type == Globals.VehicleType.Sweater)
            return (new Color(255f/255f, 0/255f, 226f/255f));
        else if (type == Globals.VehicleType.Gingerbread)
            return (new Color(158f/255f, 105f/255f, 0/255f));
        else if (type == Globals.VehicleType.Tinsel)
            return (new Color(195f/255f, 192f/255f, 207f/255f));
        else if (type == Globals.VehicleType.Santa)
            return Color.red;
        else if (type == Globals.VehicleType.Misletoe)
            return (new Color(0/255f, 157f/255f, 56f/255f));
        else if (type == Globals.VehicleType.Icicle)
            return (new Color(158f/255f, 255f/255f, 255f/255f));
        else if (type == Globals.VehicleType.CandyCaneMint)
            return (new Color(0/255f, 255f/255f, 56f/255f));
        else if (type == Globals.VehicleType.Reindeer)
            return (new Color(158f/255f, 105f/255f, 56f/255f));
        else if (type == Globals.VehicleType.Elf)
            return (new Color(0/255f, 174f/255f, 0/255f));
        else if (type == Globals.VehicleType.WinterSunset)
            return (new Color(205f/255f, 51f/255f, 108f/255f));
        else if (type == Globals.VehicleType.CandyCaneBerry)
            return (new Color(255f/255f, 0/255f, 0));
        else if (type == Globals.VehicleType.SnowCat)
            return (new Color(255f/255f, 106f/255f, 255f/255f));
        else if (type == Globals.VehicleType.SnowWagon)
            return (new Color(255f/255f, 106f/255f, 0));
        else if (type == Globals.VehicleType.MoonlessMidnight)
            return (new Color(0, 0, 0));
        else if (type == Globals.VehicleType.SkiTrip)
            return (new Color(158f/255f, 0/255f, 226f/255f));
        else if (type == Globals.VehicleType.SnowMan)
            return Color.white;
        else if (type == Globals.VehicleType.Penguin)
            return (new Color(0, 0, 0));
        else if (type == Globals.VehicleType.TenBelow)
            return Color.white;
        else if (type == Globals.VehicleType.GiftWrapped)
            return (new Color(0/255f, 157f/255f, 0/255f));
        else
            return (new Color(255f/255f, 106f/255f, 0));
    }
}
