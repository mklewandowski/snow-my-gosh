using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    enum LaneTypes {
        SnowBall,
        CoinRun,
        Random
    }

    enum LaneSlotTypes {
        PowerUp,
        Yeti,
        Snowman,
        Empty
    }

    AudioManager audioManager;
    [SerializeField]
    CharacterSelect characterSelect;
    SettingsManager settingsManager;

    [SerializeField]
    GameObject Player;
    [SerializeField]
    SmokeManager smokeManager;
    [SerializeField]
    DebrisManager debrisManager;

    [SerializeField]
    GameObject Level;
    [SerializeField]
    GameObject[] Tracks;
    [SerializeField]
    GameObject SideObjectPrefab;
    [SerializeField]
    GameObject TreesLeftContainer;
    [SerializeField]
    GameObject TreesRightContainer;
    [SerializeField]
    GameObject TreesLeftFarContainer;
    [SerializeField]
    GameObject TreesRightFarContainer;
    [SerializeField]
    GameObject[] TreesLeft;
    [SerializeField]
    GameObject[] TreesRight;
    [SerializeField]
    GameObject[] TreesLeftFar;
    [SerializeField]
    GameObject[] TreesRightFar;

    SideObject[] TreesLeftScripts = new SideObject[18];
    SideObject[] TreesRightScripts = new SideObject[18];
    SideObject[] TreesLeftFarScripts = new SideObject[18];
    SideObject[] TreesRightFarScripts = new SideObject[18];

    // titles and messages
    [SerializeField]
    GameObject HUDFade;
    [SerializeField]
    Image HUDFadeImage;
    [SerializeField]
    GameObject HUDBackground;
    [SerializeField]
    GameObject HUDAbout;
    [SerializeField]
    GameObject HUDSettings;
    [SerializeField]
    GameObject HUDTitle;
    [SerializeField]
    GameObject HUDButtons;
    [SerializeField]
    GameObject HUDSelectVehicle;
    [SerializeField]
    GameObject HUDSnow;

    [SerializeField]
    GameObject HUDQuit;
    [SerializeField]
    GameObject HUDMovementButtons;
    [SerializeField]
    GameObject HUDRaceReady;
    [SerializeField]
    GameObject HUDDistance;
    [SerializeField]
    TextMeshProUGUI HUDDistanceText;
    [SerializeField]
    GameObject HUDHeartContainer;
    [SerializeField]
    TextMeshProUGUI HUDCoinsText;
    [SerializeField]
    GameObject[] HUDHearts;
    [SerializeField]
    GameObject BombFlash;
    [SerializeField]
    GameObject HUDPowerUpImage;
    [SerializeField]
    Sprite PowerUpImageStar;
    [SerializeField]
    Sprite PowerUpImageGhost;
    [SerializeField]
    Sprite PowerUpImageBig;
    [SerializeField]
    Sprite PowerUpImageBomb;
    [SerializeField]
    Sprite PowerUpImageFire;
    [SerializeField]
    Sprite PowerUpImageRacer;
    [SerializeField]
    Sprite PowerUpFlyby;
    int totalHearts = 0;
    float invincibleTimer = 0;
    float invincibleTimerMax = 6f;
    float ghostTimer = 0;
    float ghostTimerMax = 6f;
    float starRacerTimer = 0;
    float starRacerTimerMax = 6f;
    float previousSpeed;
    float planeTimer = 0;
    float planeTimerMax = 6f;
    float bombflashTimer = 0;
    float bombflashTimerMax = .1f;
    float heartTimer = 0;
    float heartTimerMax = 1f;
    float bigifyTimer = 0;
    float bigifyTimerMax = 6f;
    float powerUpImageTimer = 0;
    float powerUpImageTimerMax = 2f;

    [SerializeField]
    TextMeshProUGUI HUDFinalDistance;
    [SerializeField]
    TextMeshProUGUI HUDBestDistance;
    [SerializeField]
    GameObject HUDGameOver;
    [SerializeField]
    GameObject HUDFinalStatsContainer;
    [SerializeField]
    GameObject HUDHighScore;
    float showScoreTimer = 3f;

    // items
    [SerializeField]
	GameObject ItemContainer;
    [SerializeField]
	GameObject YetiPrefab;
    [SerializeField]
	GameObject SnowManPrefab;
    [SerializeField]
	GameObject SnowBallPrefab;
    [SerializeField]
	GameObject CandyCanePrefab;
    [SerializeField]
	GameObject HeartPowerupPrefab;
    [SerializeField]
	GameObject CoinPowerupPrefab;
    [SerializeField]
	GameObject SpeedPointPrefab;
    int[] speedPointWaves = new int[] { 2, 6, 12, 20, 30, 42, 56, 72, 90, 110, 120, 130, 140, 150, 160, 170, 180, 190, 200 };
    int waveNum = 0;

    [SerializeField]
    GameObject SpeedLines;
    float speedLineTimer = 0;
    float speedLineTimerMax = 1f;

    float distance = 0;
    float distanceUntilSpawn = 8f;

    float fadeTimer = .75f;

    Globals.SideObjectType currentInnerSideObjectType = Globals.SideObjectType.PineTreeSmall;
    Globals.SideObjectType currentOuterSideObjectType = Globals.SideObjectType.PineTreeBig;

    void Awake()
    {
        Application.targetFrameRate = 60;

        int useMobileButtons = Globals.LoadIntFromPlayerPrefs(Globals.UseMobileButtonsPlayerPrefsKey, 1);
        Globals.UseMobileButtons = useMobileButtons == 1;

        Globals.LoadVehicleUnlockStatesFromPlayerPrefs();

        Globals.BestDistance = Globals.LoadIntFromPlayerPrefs(Globals.BestDistancePlayerPrefsKey);

        Globals.Coins = Globals.LoadIntFromPlayerPrefs(Globals.CoinsPlayerPrefsKey);
        UpdateCoins();

        int vehicleType = Globals.LoadIntFromPlayerPrefs(Globals.VehicleTypePlayerPrefsKey);
        Player.GetComponent<VehicleTypeManager>().SetVehicleType(vehicleType);

        audioManager = this.GetComponent<AudioManager>();
        settingsManager = this.GetComponent<SettingsManager>();

        int maxSideObjects = 18;
        for (int x = 0; x < maxSideObjects; x++)
        {
            TreesLeft[x] = Instantiate(SideObjectPrefab, Vector3.zero, Quaternion.identity, TreesLeftContainer.transform);
            TreesLeft[x].transform.localPosition = new Vector3(-12f, -3.7f, x * 4f);
            TreesLeft[x].GetComponent<SideObject>().SetType(Globals.SideObjectType.PineTreeSmall);
            TreesLeft[x].GetComponent<SideObject>().SetLeftSide();
            TreesLeftScripts[x] = TreesLeft[x].GetComponent<SideObject>();

            TreesRight[x] = Instantiate(SideObjectPrefab, Vector3.zero, Quaternion.identity, TreesRightContainer.transform);
            TreesRight[x].transform.localPosition = new Vector3(12f, -3.7f, x * 4f);
            TreesRight[x].GetComponent<SideObject>().SetType(Globals.SideObjectType.PineTreeSmall);
            TreesRightScripts[x] = TreesRight[x].GetComponent<SideObject>();

            TreesLeftFar[x] = Instantiate(SideObjectPrefab, Vector3.zero, Quaternion.identity, TreesLeftFarContainer.transform);
            TreesLeftFar[x].transform.localPosition = new Vector3(-20f, -3.6f, x * 4f);
            TreesLeftFar[x].GetComponent<SideObject>().SetType(Globals.SideObjectType.PineTreeBig);
            TreesLeftFar[x].GetComponent<SideObject>().SetLeftSide();
            TreesLeftFarScripts[x] = TreesLeftFar[x].GetComponent<SideObject>();

            TreesRightFar[x] = Instantiate(SideObjectPrefab, Vector3.zero, Quaternion.identity, TreesRightFarContainer.transform);
            TreesRightFar[x].transform.localPosition = new Vector3(20f, -3.6f, x * 4f);
            TreesRightFar[x].GetComponent<SideObject>().SetType(Globals.SideObjectType.PineTreeBig);
            TreesRightFarScripts[x] = TreesRightFar[x].GetComponent<SideObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Globals.CurrentGameState == Globals.GameState.TitleScreen)
        {
            UpdateTitle();
        }
        else if (Globals.CurrentGameState == Globals.GameState.Playing)
        {
            UpdatePlaying();
        }
        else if (Globals.CurrentGameState == Globals.GameState.ShowScore)
        {
            UpdateShowScore();
        }
        HandleInput();
    }

    void FixedUpdate()
    {
        if (Globals.CurrentGameState == Globals.GameState.Ready || Globals.CurrentGameState == Globals.GameState.Playing || Globals.CurrentGameState == Globals.GameState.ShowScore)
        {
            Vector3 trackMovement = new Vector3 (0, 0, Globals.ScrollSpeed.z * Globals.ScrollDirection.z);
            for (int i = 0; i < Tracks.Length; i++)
            {
                Tracks[i].GetComponent<Rigidbody>().velocity = trackMovement;
            }
            Vector3 treeMovement = new Vector3 (0, 0, Globals.ScrollSpeed.z * Globals.ScrollDirection.z);
            for (int i = 0; i < TreesLeft.Length; i++)
            {
                TreesLeft[i].GetComponent<Rigidbody>().velocity = treeMovement;
            }
            for (int i = 0; i < TreesRight.Length; i++)
            {
                TreesRight[i].GetComponent<Rigidbody>().velocity = treeMovement;
            }
            for (int i = 0; i < TreesLeftFar.Length; i++)
            {
                TreesLeftFar[i].GetComponent<Rigidbody>().velocity = treeMovement;
            }
            for (int i = 0; i < TreesRightFar.Length; i++)
            {
                TreesRightFar[i].GetComponent<Rigidbody>().velocity = treeMovement;
            }
        }
    }

    // current button can be
    // start, car, settings, about, music, sound
    enum TitleButtons {
        Play,
        Car,
        Settings,
        About,
        Sound,
        Music,
        MobileInput
    }
    TitleButtons currentTitleButton = TitleButtons.Play;
    bool showVehicles;
    bool showSettings;
    [SerializeField]
    GameObject PlayButton;
    [SerializeField]
    GameObject CarButton;
    [SerializeField]
    GameObject SettingsButton;
    [SerializeField]
    GameObject AboutButton;
    [SerializeField]
    GameObject MusicButton;
    [SerializeField]
    GameObject SoundButton;
    [SerializeField]
    GameObject MobileInputButton;
    void HandleInput()
    {
        bool moveLeft = false;
        bool moveRight = false;
        bool action = false;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveLeft = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveRight = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            action = true;
        }

        if (Globals.CurrentGameState == Globals.GameState.TitleScreen || Globals.CurrentGameState == Globals.GameState.Restart)
        {
            if (moveLeft)
            {
                audioManager.PlayMenuMoveSound();
                if (showVehicles)
                {
                    characterSelect.SelectPrevVehicle();
                }
                else
                {
                    if (currentTitleButton == TitleButtons.Play)
                        currentTitleButton = showSettings ? TitleButtons.MobileInput : TitleButtons.About;
                    else if (currentTitleButton == TitleButtons.Car)
                        currentTitleButton = TitleButtons.Play;
                    else if (currentTitleButton == TitleButtons.Settings)
                        currentTitleButton = TitleButtons.Car;
                    else if (currentTitleButton == TitleButtons.About)
                        currentTitleButton = TitleButtons.Settings;
                    else if (currentTitleButton == TitleButtons.Sound)
                        currentTitleButton = TitleButtons.About;
                    else if (currentTitleButton == TitleButtons.Music)
                        currentTitleButton = TitleButtons.Sound;
                    else if (currentTitleButton == TitleButtons.MobileInput)
                        currentTitleButton = TitleButtons.Music;

                    UpdateTitleButtons();
                }
            }
            else if (moveRight)
            {
                audioManager.PlayMenuMoveSound();
                if (showVehicles)
                {
                    characterSelect.SelectNextVehicle();
                }
                else
                {
                    if (currentTitleButton == TitleButtons.Play)
                        currentTitleButton = TitleButtons.Car;
                    else if (currentTitleButton == TitleButtons.Car)
                        currentTitleButton = TitleButtons.Settings;
                    else if (currentTitleButton == TitleButtons.Settings)
                        currentTitleButton = TitleButtons.About;
                    else if (currentTitleButton == TitleButtons.About)
                        currentTitleButton = showSettings ? TitleButtons.Sound : TitleButtons.Play;
                    else if (currentTitleButton == TitleButtons.Sound)
                        currentTitleButton = TitleButtons.Music;
                    else if (currentTitleButton == TitleButtons.Music)
                        currentTitleButton = TitleButtons.MobileInput;
                    else if (currentTitleButton == TitleButtons.MobileInput)
                        currentTitleButton = TitleButtons.Play;

                    UpdateTitleButtons();
                }
            }
            else if (action)
            {
                if (currentTitleButton == TitleButtons.Play)
                    StartGame();
                else if (currentTitleButton == TitleButtons.Car)
                {
                    if (showVehicles)
                        characterSelect.AttemptVehicleSelection();
                    else
                        SelectVehiclesButton();
                }
                else if (currentTitleButton == TitleButtons.Settings)
                    SelectSettingsButton();
                else if (currentTitleButton == TitleButtons.About)
                    SelectAboutButton();
                else if (currentTitleButton == TitleButtons.Music)
                    settingsManager.SelectMusicButton();
                else if (currentTitleButton == TitleButtons.Sound)
                    settingsManager.SelectAudioButton();
                else if (currentTitleButton == TitleButtons.MobileInput)
                    settingsManager.SelectMobileInputButton();
            }
        }
        else if (Globals.CurrentGameState == Globals.GameState.Ready)
        {
            if (action)
            {
                StartMoving();
            }
        }
        else if (Globals.CurrentGameState == Globals.GameState.ShowScore)
        {
            UpdateShowScore();
        }
    }

    void UpdateTitleButtons()
    {
        PlayButton.transform.localScale = currentTitleButton == TitleButtons.Play ? new Vector3(1.2f, 1.2f, 1.2f) : new Vector3(1f, 1f, 1f);
        CarButton.transform.localScale = currentTitleButton == TitleButtons.Car ? new Vector3(1.2f, 1.2f, 1.2f) : new Vector3(1f, 1f, 1f);
        SettingsButton.transform.localScale = currentTitleButton == TitleButtons.Settings ? new Vector3(1.2f, 1.2f, 1.2f) : new Vector3(1f, 1f, 1f);
        AboutButton.transform.localScale = currentTitleButton == TitleButtons.About ? new Vector3(1.2f, 1.2f, 1.2f) : new Vector3(1f, 1f, 1f);
        MusicButton.transform.localScale = currentTitleButton == TitleButtons.Music ? new Vector3(1.2f, 1.2f, 1.2f) : new Vector3(1f, 1f, 1f);
        SoundButton.transform.localScale = currentTitleButton == TitleButtons.Sound ? new Vector3(1.2f, 1.2f, 1.2f) : new Vector3(1f, 1f, 1f);
        MobileInputButton.transform.localScale = currentTitleButton == TitleButtons.MobileInput ? new Vector3(1.2f, 1.2f, 1.2f) : new Vector3(1f, 1f, 1f);
    }

    void UpdateTitle()
    {
        if (fadeTimer > 0)
        {
            fadeTimer -= Time.deltaTime;
            HUDFadeImage.color = new Color(0, 0, 0, fadeTimer / .75f);
            if (fadeTimer <= 0)
            {
                HUDFade.SetActive(false);
                HUDTitle.SetActive(true);
                HUDTitle.GetComponent<GrowAndShrink>().StartEffect();
                HUDButtons.GetComponent<MoveNormal>().MoveUp();
            }
        }
    }

    void UpdatePlaying()
    {
        float trackMinZ = -10f;
        for (int i = 0; i < Tracks.Length; i++)
        {
            if (Tracks[i].transform.localPosition.z < trackMinZ)
            {
                int abutIndex = i == 0 ? Tracks.Length - 1 : i - 1;
                Renderer renderer = Tracks[abutIndex].GetComponentInChildren(typeof(Renderer)) as Renderer;
                Tracks[i].transform.localPosition = new Vector3(
                        Tracks[i].transform.localPosition.x,
                        Tracks[i].transform.localPosition.y,
                        Tracks[abutIndex].transform.localPosition.z + renderer.bounds.size.z
                    );
            }
        }

        UpdateTrees(TreesLeft, TreesLeftScripts, currentInnerSideObjectType);
        UpdateTrees(TreesRight, TreesRightScripts, currentInnerSideObjectType);
        UpdateTrees(TreesLeftFar, TreesLeftFarScripts, currentOuterSideObjectType);
        UpdateTrees(TreesRightFar, TreesRightFarScripts, currentOuterSideObjectType);

        if (invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 1f)
            {
                bool flash = (Mathf.Floor(invincibleTimer * 10f)) % 2 == 0;
                Player.GetComponent<VehicleTypeManager>().MorphFlash(flash);
            }
            if (invincibleTimer <= 0)
            {
                Player.GetComponent<VehicleTypeManager>().RestoreVehicleType();
            }
        }

        if (starRacerTimer > 0)
        {
            starRacerTimer -= Time.deltaTime;
            if (starRacerTimer < 1f)
            {
                bool flash = (Mathf.Floor(starRacerTimer * 10f)) % 2 == 0;
                Player.GetComponent<VehicleTypeManager>().MorphFlash(flash);
            }
            if (starRacerTimer <= 0)
            {
                SpeedLines.SetActive(false);
                Globals.ScrollSpeed = new Vector3(0, 0, previousSpeed);
                Player.GetComponent<VehicleTypeManager>().RestoreVehicleType();
            }
        }

        if (planeTimer > 0)
        {
            planeTimer -= Time.deltaTime;
            if (planeTimer < 1f)
            {
                bool flash = (Mathf.Floor(planeTimer * 10f)) % 2 == 0;
                Player.GetComponent<VehicleTypeManager>().MorphFlash(flash);
            }
            if (planeTimer <= 0)
            {
                Player.GetComponent<VehicleTypeManager>().RestoreVehicleType();
                Player.GetComponent<MoveNormal>().MoveDown();
                smokeManager.ResumeSmoke(2f);
                Player.GetComponent<VehicleTypeManager>().ResumeCollision(2f);
            }
        }

        if (ghostTimer > 0)
        {
            ghostTimer -= Time.deltaTime;
            if (ghostTimer < 1f)
            {
                bool flash = (Mathf.Floor(ghostTimer * 10f)) % 2 == 0;
                Player.GetComponent<VehicleTypeManager>().GhostFlash(flash);
            }
            if (ghostTimer <= 0)
            {
                Player.GetComponent<VehicleTypeManager>().EndGhost();
            }
        }

        if (bigifyTimer > 0)
        {
            bigifyTimer -= Time.deltaTime;
            if (bigifyTimer < 1f)
            {
                bool flash = (Mathf.Floor(bigifyTimer * 10f)) % 2 == 0;
                Player.GetComponent<VehicleTypeManager>().BigifyFlash(flash);
            }
            if (bigifyTimer <= 0)
            {
                Player.GetComponent<VehicleTypeManager>().EndBigify();
            }
        }

        if (bombflashTimer > 0)
        {
            bombflashTimer -= Time.deltaTime;
            if (bombflashTimer <= 0)
            {
                BombFlash.SetActive(false);
            }
        }

        if (heartTimer > 0)
        {
            heartTimer -= Time.deltaTime;
            if (heartTimer <= 0)
            {
                totalHearts = 0;
                for (int x = 0; x < HUDHearts.Length; x++)
                {
                    HUDHearts[x].SetActive(x < totalHearts);
                }
            }
        }

        if (powerUpImageTimer > 0)
        {
            powerUpImageTimer -= Time.deltaTime;
            if (powerUpImageTimer <= 0)
            {
                HUDPowerUpImage.GetComponent<MoveNormal>().MoveDown();
            }
        }

        if (speedLineTimer > 0)
        {
            speedLineTimer -= Time.deltaTime;
            if (speedLineTimer <= 0 && starRacerTimer <= 0)
            {
                SpeedLines.SetActive(false);
            }
        }

        distance = distance + Time.deltaTime * Globals.ScrollSpeed.z;
        distanceUntilSpawn = distanceUntilSpawn - Time.deltaTime * Globals.ScrollSpeed.z;

        int prevDistance = Globals.CurrentDistance;
        Globals.CurrentDistance = (int)(distance / 10f);
        if (Globals.CurrentDistance != prevDistance && Globals.CurrentDistance % 20 == 0)
        {
            ChangeSideObjectMode();
        }
        HUDDistanceText.text = Globals.CurrentDistance.ToString();

        if (distanceUntilSpawn <= 0)
        {
            SpawnWave();
        }
    }

    void ChangeSideObjectMode()
    {
        Globals.CurrentSideObjectMode = (Globals.SideObjectModes)Random.Range(0, 3);
        if (Globals.CurrentSideObjectMode == Globals.SideObjectModes.PineTree)
        {
            currentInnerSideObjectType = Globals.SideObjectType.PineTreeSmall;
            currentOuterSideObjectType = Globals.SideObjectType.PineTreeBig;
        }
        else if (Globals.CurrentSideObjectMode == Globals.SideObjectModes.OldTree)
        {
            currentInnerSideObjectType = Globals.SideObjectType.OldTree;
            currentOuterSideObjectType = Globals.SideObjectType.OldTree;
        }
        else if (Globals.CurrentSideObjectMode == Globals.SideObjectModes.SnowBank)
        {
            currentInnerSideObjectType = Globals.SideObjectType.SnowBank;
            currentOuterSideObjectType = Globals.SideObjectType.SnowBank;
        }
    }

    bool lastWaveCanes = false;
    void SpawnWave()
    {
        waveNum++;
        bool spawnSpeedPoint = false;
        bool spawnCandyCanes = false;

        for (int a = 0; a < speedPointWaves.Length; a++)
        {
            if (speedPointWaves[a] == waveNum)
                spawnSpeedPoint = true;
        }
        if (!lastWaveCanes && ((waveNum >= 8 && Random.Range(0, 100f) > 94f) || waveNum == 14))
        {
            spawnCandyCanes = true;
            lastWaveCanes = true;
        }
        else
        {
            lastWaveCanes = false;
        }

        if (spawnSpeedPoint)
        {
            // spawn a speed point
            GameObject speedPoint = (GameObject)Instantiate(SpeedPointPrefab,
                new Vector3(0, 0.2f, 64f),
                Quaternion.identity,
                ItemContainer.transform);
            distanceUntilSpawn = 16f;
        }
        else if (spawnCandyCanes)
        {
            int lanes = 5;
            int laneSlots = 20;
            float startX = -6f;
            float xIncrement = 3f;
            distanceUntilSpawn = 160f;
            int openLane = 2;
            int openLaneLength = 3;
            for (int ls = 0; ls < laneSlots; ls++)
            {
                if (ls != (laneSlots - 1))
                {
                    for (int l = 0; l < lanes; l++)
                    {
                        GameObject powerup = (GameObject)Instantiate(l != openLane ? CandyCanePrefab : CoinPowerupPrefab,
                                new Vector3(startX + l * xIncrement, l != openLane ? -1.6f : -3f, 64f + ls * 8f),
                                Quaternion.identity, ItemContainer.transform);
                    }
                }
                openLaneLength--;
                if (openLaneLength == 0)
                {
                    openLaneLength = Random.Range(2, 5);
                    int laneDir = Random.Range(0, 100f) > 50f ? 1 : -1;
                    openLane = Mathf.Min(4, Mathf.Max(0, openLane + laneDir));
                }
            }
        }
        else
        {
            // spawn new items in the 5 slots of the row at z = 64
            int lanes = 5;
            int laneSlots = 8;
            float startX = -6f;
            float xIncrement = 3f;

            int maxCoinRuns = 2;
            int maxSnowBalls = 3;
            int maxYetiPerLaneSlot = 4;

            LaneTypes[] laneTypes = new LaneTypes[lanes];
            int coinRuns = 0;
            int snowBalls = 0;
            for (int x = 0; x < laneTypes.Length; x++)
            {
                LaneTypes laneType = LaneTypes.Random;
                float minRange = snowBalls >= maxSnowBalls ? 5f : 0f;
                float maxRange = coinRuns >= maxCoinRuns ? 95f : 100f;
                float laneRandomVal = Random.Range(minRange, maxRange);
                if (laneRandomVal < 20f) // 20% change of snowball
                {
                    snowBalls++;
                    maxYetiPerLaneSlot--;
                    laneType = LaneTypes.SnowBall;
                }
                else if (laneRandomVal > 95f) // 5% chance of coin run
                {
                    coinRuns++;
                    laneType = LaneTypes.CoinRun;
                }
                laneTypes[x] = laneType;
            }

            // Knuth shuffle algorithm
            for (int i = 0; i < laneTypes.Length; i++ )
            {
                LaneTypes tmp = laneTypes[i];
                int r = Random.Range(i, laneTypes.Length);
                laneTypes[i] = laneTypes[r];
                laneTypes[r] = tmp;
            }

            for (int x = 0; x < lanes; x++)
            {
                if (laneTypes[x] == LaneTypes.SnowBall)
                {
                    int numSnowballs = Random.Range(1, 5);
                    float extraSpeed = Random.Range(10f, 20f);
                    for (int s = 0; s < numSnowballs; s++)
                    {
                        GameObject snowBall = (GameObject)Instantiate(SnowBallPrefab,
                            new Vector3(startX + x * xIncrement, -2.9f, 64f + 7 * 8f + (s * 4f)),
                            Quaternion.identity, ItemContainer.transform);
                        snowBall.GetComponent<Item>().SetExtraSpeed(extraSpeed);
                    }
                }
                else if (laneTypes[x] == LaneTypes.CoinRun)
                {
                    for (int s = 0; s < laneSlots; s++)
                    {
                        GameObject powerup = (GameObject)Instantiate(CoinPowerupPrefab,
                            new Vector3(startX + x * xIncrement, -3f, 64f + s * 8f),
                            Quaternion.identity, ItemContainer.transform);
                    }
                }
                else
                {
                    // random content
                    LaneSlotTypes[] laneSlotTypes = new LaneSlotTypes[laneSlots];
                    int yeti = 0;
                    for (int ls = 0; ls < laneSlotTypes.Length; ls++)
                    {
                        LaneSlotTypes laneSlotType = LaneSlotTypes.Empty;
                        float minRange = yeti >= maxYetiPerLaneSlot ? 20f : 0f;
                        float laneSlotRandomVal = Random.Range(minRange, 100f);
                        if (laneSlotRandomVal < 20f) // 20% change of yeti
                        {
                            yeti++;
                            laneSlotType = LaneSlotTypes.Yeti;
                            if (Globals.CurrentDistance > 50 && Random.Range(0, 3) >= 2) // 33% chance of snowman after 50 distance
                                laneSlotType = LaneSlotTypes.Snowman;
                        }
                        else if (laneSlotRandomVal < 30f) // 10% chance of powerup
                        {
                            laneSlotType = LaneSlotTypes.PowerUp;
                        }
                        laneSlotTypes[ls] = laneSlotType;
                    }
                    // Knuth shuffle algorithm
                    for (int i = 0; i < laneTypes.Length; i++ )
                    {
                        LaneSlotTypes tmp = laneSlotTypes[i];
                        int r = Random.Range(i, laneSlotTypes.Length);
                        laneSlotTypes[i] = laneSlotTypes[r];
                        laneSlotTypes[r] = tmp;
                    }

                    for (int s = 0; s < laneSlots; s++)
                    {
                        if (laneSlotTypes[s] == LaneSlotTypes.Yeti)
                        {
                            GameObject enemy = (GameObject)Instantiate(YetiPrefab,
                                new Vector3(startX + x * xIncrement, -2.5f, 64f + s * 8f + Random.Range(-2f, 2f)),
                                Quaternion.identity, ItemContainer.transform);
                        }
                        else if (laneSlotTypes[s] == LaneSlotTypes.Snowman)
                        {
                            GameObject enemy = (GameObject)Instantiate(SnowManPrefab,
                                new Vector3(startX + x * xIncrement, -2.9f, 64f + s * 8f + Random.Range(-2f, 2f)),
                                Quaternion.identity, ItemContainer.transform);
                        }
                        else if (laneSlotTypes[s] == LaneSlotTypes.PowerUp)
                        {
                            float powerupRandVal = Random.Range(0f, 100.0f);
                            GameObject powerupPrefab = CoinPowerupPrefab;
                            if (powerupRandVal > 70)
                            {
                                powerupPrefab = HeartPowerupPrefab;
                            }
                            GameObject powerup = (GameObject)Instantiate(powerupPrefab,
                                new Vector3(startX + x * xIncrement, -3f, 64f + s * 8f + Random.Range(-2f, 2f)),
                                Quaternion.identity, ItemContainer.transform);
                        }
                    }
                }
            }
            distanceUntilSpawn = 64f;
        }
    }

    void UpdateTrees(GameObject[] trees, SideObject[] treeScripts, Globals.SideObjectType soType)
    {
        float treeMinZ = -4f;
        float treeOffsetZ = 4f;
        for (int i = 0; i < trees.Length; i++)
        {
            if (trees[i].transform.localPosition.z < treeMinZ)
            {
                int abutIndex = i == 0 ? trees.Length - 1 : i - 1;
                trees[i].transform.localPosition = new Vector3(
                        trees[i].transform.localPosition.x,
                        trees[i].transform.localPosition.y,
                        trees[abutIndex].transform.localPosition.z + treeOffsetZ
                    );
                treeScripts[i].SetType(soType);
            }
        }
    }

    void UpdateShowScore()
    {
        showScoreTimer -= Time.deltaTime;
        if (showScoreTimer <= 0)
        {
            HUDFinalStatsContainer.transform.localPosition = new Vector3(0, 0, 0);
            HUDButtons.GetComponent<MoveNormal>().MoveUp();
            HUDGameOver.GetComponent<MoveNormal>().MoveDown();
            Globals.CurrentGameState = Globals.GameState.Restart;
        }
    }

    public void SpeedUp()
    {
        float newSpeed = Mathf.Min(Globals.maxSpeed, Globals.ScrollSpeed.z + 3f);
        Globals.ScrollSpeed = new Vector3(0, 0, newSpeed);
        smokeManager.SpeedUp();
        StartSpeedLines(speedLineTimerMax);
    }

    public void StartSpeedLines(float timerMax)
    {
        speedLineTimer = timerMax;
        SpeedLines.SetActive(true);
    }

    public void StartInvincible()
    {
        invincibleTimer = invincibleTimerMax;
        Player.GetComponent<VehicleTypeManager>().ChangeToInvincible();
    }

    public void StartStarRacer()
    {
        if (starRacerTimer > 0) return;
        previousSpeed = Globals.ScrollSpeed.z;
        Globals.ScrollSpeed = new Vector3(0, 0, Globals.maxSpeed);
        starRacerTimer = starRacerTimerMax;
        Player.GetComponent<VehicleTypeManager>().ChangeToStarRacer();
        SpeedLines.SetActive(true);
    }

    public void StartPlane()
    {
        planeTimer = planeTimerMax;
        Player.GetComponent<VehicleTypeManager>().ChangeToPlane();
        Player.GetComponent<MoveNormal>().MoveUp();
        smokeManager.PauseSmoke();
    }

    public bool IsPlane()
    {
        return planeTimer > 0;
    }

    public bool IsInvincible()
    {
        return invincibleTimer > 0 || bigifyTimer > 0 || starRacerTimer > 0;
    }

    public void StartGhost()
    {
        ghostTimer = ghostTimerMax;
        Player.GetComponent<VehicleTypeManager>().ChangeToGhost();
    }

    public bool IsGhost()
    {
        return ghostTimer > 0;
    }

    public void StartBigify()
    {
        bigifyTimer = bigifyTimerMax;
        Player.GetComponent<VehicleTypeManager>().Bigify();
    }

    public void StartBomb()
    {
        audioManager.PlayBombSound();
        ItemEnemy[] enemies = GameObject.FindObjectsOfType<ItemEnemy>(true);
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.transform.localPosition.x < 20f && enemies[i].gameObject.transform.localPosition.x > -10f)
                enemies[i].BombEnemy();
        }
        BombFlash.SetActive(true);
        bombflashTimer = bombflashTimerMax;
    }

    public void GetHeart()
    {
        if (heartTimer > 0)
        {
            heartTimer = 0;
            totalHearts = 0;
        }
        totalHearts++;
        if (totalHearts >= 3)
        {
            audioManager.PlayPowerupSound();
            heartTimer = heartTimerMax;
            int randVal = Random.Range(0, 6);
            if (IsInvincible() || IsGhost() || IsPlane())
                randVal = 0;
            if (randVal == 0)
            {
                HUDPowerUpImage.GetComponent<Image>().sprite = PowerUpImageBomb;
                StartBomb();
            }
            else if (randVal == 1)
            {
                HUDPowerUpImage.GetComponent<Image>().sprite = PowerUpImageStar;
                StartInvincible();
            }
            else if (randVal == 2)
            {
                HUDPowerUpImage.GetComponent<Image>().sprite = PowerUpImageGhost;
                StartGhost();
            }
            else if (randVal == 3)
            {
                HUDPowerUpImage.GetComponent<Image>().sprite = PowerUpImageBig;
                StartBigify();
            }
            else if (randVal == 4)
            {
                HUDPowerUpImage.GetComponent<Image>().sprite = PowerUpImageRacer;
                StartStarRacer();
            }
            else if (randVal == 5)
            {
                HUDPowerUpImage.GetComponent<Image>().sprite = PowerUpFlyby;
                StartPlane();
            }
            powerUpImageTimer = powerUpImageTimerMax;
            HUDPowerUpImage.GetComponent<MoveNormal>().MoveUp();
        }
        else
        {
            audioManager.PlayHeartSound();
        }
        for (int x = 0; x < HUDHearts.Length; x++)
        {
            HUDHearts[x].SetActive(x < totalHearts);
        }
    }

    public void GetCoin()
    {
        Globals.Coins++;
        HUDCoinsText.text = Globals.Coins.ToString();
    }

    public void UpdateCoins()
    {
        HUDCoinsText.text = Globals.Coins.ToString();
    }

    public void StartGame()
    {
        if (Globals.CurrentGameState != Globals.GameState.TitleScreen && Globals.CurrentGameState != Globals.GameState.Restart)
            return;

        audioManager.PlayStartSound();

        Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y, Camera.main.transform.position.z);
        Player.GetComponent<Player>().Reset();

        currentTitleButton = TitleButtons.Play;
        UpdateTitleButtons();
        HUDGameOver.GetComponent<MoveNormal>().MoveUp();
        HUDSnow.SetActive(false);
        HUDBackground.SetActive(false);
        HUDAbout.GetComponent<MoveNormal>().MoveRight();
        HUDSettings.GetComponent<MoveNormal>().MoveRight();
        showSettings = false;
        HUDTitle.GetComponent<MoveNormal>().MoveLeft();
        HUDButtons.GetComponent<MoveNormal>().MoveDown();
        HUDSelectVehicle.GetComponent<MoveNormal>().MoveDown();
        showVehicles = false;
        Level.SetActive(true);
        Player.SetActive(true);

        Globals.CurrentDistance = 0;
        distance = 0;
        waveNum = 0;
        distanceUntilSpawn = 8f;
        totalHearts = 0;
        for (int x = 0; x < HUDHearts.Length; x++)
        {
            HUDHearts[x].SetActive(false);
        }
        invincibleTimer = 0f;
        starRacerTimer = 0f;
        planeTimer = 0f;
        ghostTimer = 0f;
        bigifyTimer = 0f;
        bombflashTimer = 0f;
        heartTimer = 0f;
        powerUpImageTimer = 0f;
        speedLineTimer = 0f;
        HUDDistance.SetActive(false);
        HUDDistanceText.text = Globals.CurrentDistance.ToString();
        HUDQuit.SetActive(true);
        HUDMovementButtons.SetActive(Globals.UseMobileButtons);

        HUDRaceReady.SetActive(true);
        HUDRaceReady.GetComponent<GrowAndShrink>().StartEffect();

        Globals.CurrentGameState = Globals.GameState.Ready;
    }

    public void StartMoving()
    {
        HUDDistance.SetActive(true);
        HUDHeartContainer.SetActive(true);
        HUDRaceReady.SetActive(false);
        HUDRaceReady.transform.localScale = new Vector3(.1f, .1f, .1f);
        Globals.ScrollSpeed = new Vector3(0, 0, 15f);
        Globals.CurrentGameState = Globals.GameState.Playing;
        Camera.main.GetComponent<CameraTilt>().StartEffect();
        audioManager.PlayStartMovingSound();
    }

    public void EndGame()
    {
        Globals.ScrollSpeed = new Vector3(0, 0, 0);

        if (Globals.CurrentDistance > Globals.BestDistance || Globals.BestDistance == 0f)
        {
            Globals.BestDistance = Globals.CurrentDistance;
            Globals.SaveIntToPlayerPrefs(Globals.BestDistancePlayerPrefsKey, Globals.BestDistance);
            HUDHighScore.SetActive(true);
        }
        else
        {
            HUDHighScore.SetActive(false);
        }

        Globals.SaveIntToPlayerPrefs(Globals.CoinsPlayerPrefsKey, Globals.Coins);

        HUDFinalDistance.text = Globals.CurrentDistance.ToString();
        HUDBestDistance.text = Globals.BestDistance.ToString();

        showScoreTimer = 3f;
        Globals.CurrentGameState = Globals.GameState.ShowScore;

        BombFlash.SetActive(false);
        SpeedLines.SetActive(false);
        Player.GetComponent<Player>().Die();
        Player.GetComponent<VehicleTypeManager>().RestoreVehicleType();
    }

    public void SelectVehiclesButton()
    {
        audioManager.PlayMenuSound();
        currentTitleButton = TitleButtons.Car;
        UpdateTitleButtons();
        HUDButtons.GetComponent<MoveNormal>().MoveDown();
        HUDSelectVehicle.GetComponent<MoveNormal>().MoveUp();
        showVehicles = true;
        HUDAbout.GetComponent<MoveNormal>().MoveRight();
        HUDSettings.GetComponent<MoveNormal>().MoveRight();
        showSettings = false;
        HUDFinalStatsContainer.GetComponent<MoveNormal>().MoveRight();
    }
    public void SelectAboutButton()
    {
        audioManager.PlayMenuSound();
        currentTitleButton = TitleButtons.About;
        UpdateTitleButtons();

        HUDAbout.GetComponent<MoveNormal>().MoveLeft();
        HUDSettings.GetComponent<MoveNormal>().MoveRight();
        showSettings = false;
        HUDFinalStatsContainer.GetComponent<MoveNormal>().MoveRight();
        HUDSelectVehicle.GetComponent<MoveNormal>().MoveDown();
        showVehicles = false;
    }
    public void SelectSettingsButton()
    {
        audioManager.PlayMenuSound();
        currentTitleButton = TitleButtons.Settings;
        UpdateTitleButtons();

        HUDSettings.GetComponent<MoveNormal>().MoveLeft();
        showSettings = true;
        HUDAbout.GetComponent<MoveNormal>().MoveRight();
        HUDFinalStatsContainer.GetComponent<MoveNormal>().MoveRight();
        HUDSelectVehicle.GetComponent<MoveNormal>().MoveDown();
        showVehicles = false;
    }
    public void SelectVehicleButton(int currentVehicleIndex)
    {
        audioManager.PlayMenuSound();

        HUDButtons.GetComponent<MoveNormal>().MoveUp();
        HUDSelectVehicle.GetComponent<MoveNormal>().MoveDown();
        showVehicles = false;

        Player.GetComponent<VehicleTypeManager>().SetVehicleType(currentVehicleIndex);
    }
    public void SelectBuyVehicleButton(int currentVehicleIndex)
    {
        audioManager.PlayBuyCarSound();

        Globals.Coins = Globals.Coins - 100;
        Globals.SaveIntToPlayerPrefs(Globals.CoinsPlayerPrefsKey, Globals.Coins);
        UpdateCoins();
        Globals.UnlockVehicle(currentVehicleIndex);
    }
    public void SelectQuitButton()
    {
        audioManager.PlayMenuSound();

        HUDBackground.SetActive(true);
        HUDSnow.SetActive(true);
        HUDGameOver.GetComponent<MoveNormal>().MoveUp();
        HUDTitle.GetComponent<MoveNormal>().MoveRight();
        HUDButtons.GetComponent<MoveNormal>().MoveUp();
        Level.SetActive(false);
        Player.SetActive(false);
        Player.GetComponent<VehicleTypeManager>().EndGhost();

        HUDQuit.SetActive(false);
        HUDMovementButtons.SetActive(false);
        BombFlash.SetActive(false);
        SpeedLines.SetActive(false);
        HUDRaceReady.SetActive(false);
        HUDDistance.SetActive(false);
        HUDHeartContainer.SetActive(false);
        HUDPowerUpImage.transform.localPosition = new Vector3(HUDPowerUpImage.transform.localPosition.x, -750f, HUDPowerUpImage.transform.localPosition.z);

        Globals.ScrollSpeed = new Vector3(0, 0, 0);
        Player.transform.localPosition = new Vector3(Player.transform.localPosition.x, -3f, Player.transform.localPosition.z);
        Player.GetComponent<VehicleTypeManager>().RestoreVehicleType();

        ItemEnemy[] smashEnemies = GameObject.FindObjectsOfType<ItemEnemy>(true);
        for (int i = 0; i < smashEnemies.Length; i++)
        {
            Destroy(smashEnemies[i].gameObject);
        }
        ItemPowerup[] smashPowerups = GameObject.FindObjectsOfType<ItemPowerup>(true);
        for (int i = 0; i < smashPowerups.Length; i++)
        {
            Destroy(smashPowerups[i].gameObject);
        }

        smokeManager.StopAll();
        debrisManager.StopAll();

        Globals.CurrentGameState = Globals.GameState.TitleScreen;
    }

}
