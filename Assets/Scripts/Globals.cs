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

    public enum SideObjectModes {
        PineTree,
        OldTree,
        SnowBank,
    }
    public static SideObjectModes CurrentSideObjectMode = SideObjectModes.PineTree;

    public enum SideObjectType {
        None,
        PineTreeSmall,
        PineTreeBig,
        OldTree,
        SnowBank
    }

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
        SwissMiss,
        WinterBlueJay,
        GaudyLights,
        MoonBoots,
        MoonBoots2,
        CocoaMarshmellow,
        CoolMintCandyCane,
        BigPresent,
        Plaid,
        Moon,
        Plaid2,
        Yeti,
        LogTruck,
        PresentTruck,
        TreeCar,
        Invincible
    }

    // mobile control type
    public static bool UseMobileButtons;

    // audio and music
    public static bool AudioOn;
    public static bool MusicOn;

    // keep track of scoring
    public static int BestDistance = 0;
    public static int CurrentDistance = 0;

    // keep track of coins
    public static int Coins = 0;

    public static int MaxVehicles = 43;
    public static int[] VehicleUnlockStates = new int[MaxVehicles];

    public const string UseMobileButtonsPlayerPrefsKey = "UseMobileButtons";
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
            name = "Spearmint Candy Cane";
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
        else if (type == Globals.VehicleType.SwissMiss)
            name = "Miss Swiss";
        else if (type == Globals.VehicleType.WinterBlueJay)
            name = "Winter Bluejay";
        else if (type == Globals.VehicleType.GaudyLights)
            name = "Gaudy Lights";
        else if (type == Globals.VehicleType.MoonBoots)
            name = "Flashy Moon Boots";
        else if (type == Globals.VehicleType.MoonBoots2)
            name = "Fancy Moon Boots";
        else if (type == Globals.VehicleType.CocoaMarshmellow)
            name = "Marshmallow Cocoa";
        else if (type == Globals.VehicleType.CoolMintCandyCane)
            name = "Cool Mint Candycane";
        else if (type == Globals.VehicleType.BigPresent)
            name = "Under the Tree";
        else if (type == Globals.VehicleType.Plaid)
            name = "Christmas Morning Plaid";
        else if (type == Globals.VehicleType.Moon)
            name = "December Moonlight";
        else if (type == Globals.VehicleType.Plaid2)
            name = "Pine Needle Plaid";
        else if (type == Globals.VehicleType.Yeti)
            name = "Yeti-Mobile";
        else if (type == Globals.VehicleType.LogTruck)
            name = "Bethany's Timber Truck";
        else if (type == Globals.VehicleType.PresentTruck)
            name = "Present Express";
        else if (type == Globals.VehicleType.TreeCar)
            name = "Family Tree";
        return name;
    }

    public static Color GetVehicleDebrisColorFromType(VehicleType type)
    {
        if (type == Globals.VehicleType.Default)
            return (new Color(255f/255f, 106f/255f, 0));
        else if (type == Globals.VehicleType.Blizzard)
            return Color.white;
        else if (type == Globals.VehicleType.PineTree)
            return (new Color(0f, 87f/255f, 0));
        else if (type == Globals.VehicleType.EggNog)
            return (new Color(255f/255f, 209f/255f, 169f/255f));
        else if (type == Globals.VehicleType.FrozenLake)
            return (new Color(53f/255f, 255f/255f, 255f/255f));
        else if (type == Globals.VehicleType.Cardinal)
            return Color.red;
        else if (type == Globals.VehicleType.Cocoa)
            return (new Color(106f/255f, 52f/255f, 0));
        else if (type == Globals.VehicleType.CandyCane)
            return Color.red;
        else if (type == Globals.VehicleType.NorthStar)
            return Color.yellow;
        else if (type == Globals.VehicleType.Sweater)
            return (new Color(255f/255f, 0f, 226f/255f));
        else if (type == Globals.VehicleType.Gingerbread)
            return (new Color(158f/255f, 105f/255f, 0f));
        else if (type == Globals.VehicleType.Tinsel)
            return (new Color(195f/255f, 192f/255f, 207f/255f));
        else if (type == Globals.VehicleType.Santa)
            return Color.red;
        else if (type == Globals.VehicleType.Misletoe)
            return (new Color(0f, 157f/255f, 56f/255f));
        else if (type == Globals.VehicleType.Icicle)
            return (new Color(158f/255f, 255f/255f, 255f/255f));
        else if (type == Globals.VehicleType.CandyCaneMint)
            return (new Color(0f, 255f/255f, 56f/255f));
        else if (type == Globals.VehicleType.Reindeer)
            return (new Color(158f/255f, 105f/255f, 56f/255f));
        else if (type == Globals.VehicleType.Elf)
            return (new Color(0f, 174f/255f, 0f));
        else if (type == Globals.VehicleType.WinterSunset)
            return (new Color(205f/255f, 51f/255f, 108f/255f));
        else if (type == Globals.VehicleType.CandyCaneBerry)
            return (new Color(255f/255f, 0f, 255f/255f));
        else if (type == Globals.VehicleType.SnowCat)
            return (new Color(255f/255f, 106f/255f, 0));
        else if (type == Globals.VehicleType.SnowWagon)
            return (new Color(255f/255f, 106f/255f, 0));
        else if (type == Globals.VehicleType.MoonlessMidnight)
            return (new Color(0, 0, 0));
        else if (type == Globals.VehicleType.SkiTrip)
            return (new Color(158f/255f, 0f, 226f/255f));
        else if (type == Globals.VehicleType.SnowMan)
            return Color.white;
        else if (type == Globals.VehicleType.Penguin)
            return (new Color(0, 0, 0));
        else if (type == Globals.VehicleType.TenBelow)
            return Color.white;
        else if (type == Globals.VehicleType.GiftWrapped)
            return (new Color(0, 157f/255f, 0));
        else if (type == Globals.VehicleType.SwissMiss)
            return Color.red;
        else if (type == Globals.VehicleType.WinterBlueJay)
            return Color.blue;
        else if (type == Globals.VehicleType.GaudyLights)
            return (new Color(255f/255f, 0, 226f/255f));
        else if (type == Globals.VehicleType.MoonBoots)
            return Color.white;
        else if (type == Globals.VehicleType.MoonBoots2)
            return (new Color(255f/255f, 209f/255f, 169f/255f));
        else if (type == Globals.VehicleType.CocoaMarshmellow)
            return (new Color(106f/255f, 52f/255f, 0));
        else if (type == Globals.VehicleType.CoolMintCandyCane)
            return (new Color(0, 157f/255f, 255f/255f));
        else if (type == Globals.VehicleType.BigPresent)
            return Color.yellow;
        else if (type == Globals.VehicleType.Plaid)
            return Color.red;
        else if (type == Globals.VehicleType.Moon)
            return Color.black;
        else if (type == Globals.VehicleType.Plaid2)
            return (new Color(0, 87f/255f, 0));
        else if (type == Globals.VehicleType.Yeti)
            return Color.white;
        else if (type == Globals.VehicleType.LogTruck)
            return Color.yellow;
        else if (type == Globals.VehicleType.PresentTruck)
            return (new Color(0, 209f/255f, 169f/255f));
        else if (type == Globals.VehicleType.TreeCar)
            return (new Color(0f, 87f/255f, 0));
        else
            return (new Color(255f/255f, 106f/255f, 0));
    }
}
