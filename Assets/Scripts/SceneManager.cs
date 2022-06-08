using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    AudioManager audioManager;

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
    GameObject[] TreesLeft;
    [SerializeField]
    GameObject[] TreesRight;

    // titles and messages
    [SerializeField]
    GameObject HUDFade;
    [SerializeField]
    Image HUDFadeImage;
    [SerializeField]
    GameObject HUDBackground;
    [SerializeField]
    GameObject HUDPlayer;
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
    GameObject HUDQuit;
    [SerializeField]
    GameObject HUDRaceReady;
    [SerializeField]
    GameObject HUDDistance;
    [SerializeField]
    TextMeshProUGUI HUDDistanceText;
    [SerializeField]
    GameObject HUDItems;
    [SerializeField]
    TextMeshProUGUI HUDCoinsText;
    [SerializeField]
    GameObject[] HUDHearts;
    int totalHearts = 0;
    float invincibleTimer = 0;
    float invincibleTimerMax = 4f;
    [SerializeField]
    GameObject BombFlash;
    float bombflashTimer = 0;
    float bombflashTimerMax = .1f;
    float heartTimer = 0;
    float heartTimerMax = 1f;

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
	GameObject SnowBallPrefab;
    [SerializeField]
	GameObject SpeedPowerupPrefab;
    [SerializeField]
	GameObject StarPowerupPrefab;
    [SerializeField]
	GameObject BombPowerupPrefab;
    [SerializeField]
	GameObject HeartPowerupPrefab;
    [SerializeField]
	GameObject CoinPowerupPrefab;
    [SerializeField]
	GameObject SpeedPointPrefab;
    int[] speedPointWaves = new int[] { 2, 6, 10, 16, 22, 28, 36, 42, 50 };
    int waveNum = 0;

    [SerializeField]
    GameObject SpeedLines;
    float speedLineTimer = 0;
    float speedLineTimerMax = 1f;

    float distance = 0;
    float distanceUntilSpawn = 8f;

    float fadeTimer = .75f;

    void Awake()
    {
        Application.targetFrameRate = 60;

        Globals.BestDistance = Globals.LoadIntFromPlayerPrefs(Globals.BestDistancePlayerPrefsKey);

        Globals.Coins = Globals.LoadIntFromPlayerPrefs(Globals.CoinsPlayerPrefsKey);
        HUDCoinsText.text = Globals.Coins.ToString();

        int vehicleType = Globals.LoadIntFromPlayerPrefs(Globals.VehicleTypePlayerPrefsKey);
        Player.GetComponent<VehicleTypeManager>().SetVehicleType(vehicleType);
        HUDPlayer.GetComponent<VehicleTypeManager>().SetVehicleType(vehicleType);

        audioManager = this.GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
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
        if (Globals.CurrentGameState == Globals.GameState.Ready)
        {
            UpdateReady();
        }
        else if (Globals.CurrentGameState == Globals.GameState.Playing)
        {
            UpdatePlaying();
        }
        else if (Globals.CurrentGameState == Globals.GameState.ShowScore)
        {
            UpdateShowScore();
        }
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
        }
    }

    void UpdateReady()
    {
        if (Input.GetKey ("space") | Input.GetButton ("Fire1") | Input.GetButton ("Fire2"))
        {
            HUDDistance.SetActive(true);
            HUDItems.SetActive(true);
            HUDRaceReady.SetActive(false);
            HUDRaceReady.transform.localScale = new Vector3(.1f, .1f, .1f);
            Globals.ScrollSpeed = new Vector3(0, 0, 15f);
            Globals.CurrentGameState = Globals.GameState.Playing;
            audioManager.PlayStartMovingSound();
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
        float treeMinZ = -4f;
        float treeOffsetZ = 4f;
        for (int i = 0; i < TreesLeft.Length; i++)
        {
            if (TreesLeft[i].transform.localPosition.z < treeMinZ)
            {
                int abutIndex = i == 0 ? TreesLeft.Length - 1 : i - 1;
                TreesLeft[i].transform.localPosition = new Vector3(
                        TreesLeft[i].transform.localPosition.x,
                        TreesLeft[i].transform.localPosition.y,
                        TreesLeft[abutIndex].transform.localPosition.z + treeOffsetZ
                    );
            }
        }
        for (int i = 0; i < TreesRight.Length; i++)
        {
            if (TreesRight[i].transform.localPosition.z < treeMinZ)
            {
                int abutIndex = i == 0 ? TreesRight.Length - 1 : i - 1;
                TreesRight[i].transform.localPosition = new Vector3(
                        TreesRight[i].transform.localPosition.x,
                        TreesRight[i].transform.localPosition.y,
                        TreesRight[abutIndex].transform.localPosition.z + treeOffsetZ
                    );
            }
        }

        if (invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 1f)
            {
                bool flash = (Mathf.Floor(invincibleTimer * 10f)) % 2 == 0;
                Player.GetComponent<VehicleTypeManager>().InvincibleFlash(flash);
            }
            if (invincibleTimer <= 0)
            {
                Player.GetComponent<VehicleTypeManager>().RestoreVehicleType();
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

        if (speedLineTimer > 0)
        {
            speedLineTimer -= Time.deltaTime;
            if (speedLineTimer <= 0)
            {
                SpeedLines.SetActive(false);
            }
        }

        distance = distance + Time.deltaTime * Globals.ScrollSpeed.z;
        distanceUntilSpawn = distanceUntilSpawn - Time.deltaTime * Globals.ScrollSpeed.z;

        Globals.CurrentDistance = (int)(distance / 10f);
        HUDDistanceText.text = Globals.CurrentDistance.ToString();

        if (distanceUntilSpawn <= 0)
        {
            waveNum++;
            bool exists = false;
            for (int a = 0; a < speedPointWaves.Length; a++)
            {
                if (speedPointWaves[a] == waveNum)
                    exists = true;
            }
            if (exists)
            {
                // spawn a speed point
                GameObject speedPoint = (GameObject)Instantiate(SpeedPointPrefab,
                    new Vector3(0, 0.2f, 64f),
                    Quaternion.identity,
                    ItemContainer.transform);
                distanceUntilSpawn = 16f;
            }
            else
            {
                // spawn new things in the 5 slots of the row at z = 60
                int lanes = 5;
                int laneSlots = 8;
                float startX = -6f;
                float xIncrement = 3f;
                for (int x = 0; x < lanes; x++)
                {
                    float laneRandomVal = Random.Range(0f, 100.0f);
                    if (laneRandomVal < 20f)
                    {
                        // snowball
                        int numSnowballs = Random.Range(1, 5);
                        float extraSpeed = Random.Range(10f, 20f);
                        for (int s = 0; s < numSnowballs; s++)
                        {
                            GameObject snowBall = (GameObject)Instantiate(SnowBallPrefab,
                                new Vector3(startX + x * xIncrement, -2.9f, 64f + 7 * 8f + (s * 4f)),
                                Quaternion.identity,
                                ItemContainer.transform);
                            snowBall.GetComponent<Item>().SetExtraSpeed(extraSpeed);
                        }
                    }
                    else if (laneRandomVal < 25f)
                    {
                        // coin run
                        for (int s = 0; s < laneSlots; s++)
                        {
                            GameObject powerup = (GameObject)Instantiate(CoinPowerupPrefab,
                                new Vector3(startX + x * xIncrement, -3f, 64f + s * 8f),
                                Quaternion.identity,
                                ItemContainer.transform);
                        }
                    }
                    else
                    {
                        // random content
                        for (int s = 0; s < laneSlots; s++)
                        {
                            float randomVal = Random.Range(0f, 100.0f);
                            if (randomVal < 20f)
                            {
                                // make a yeti
                                GameObject enemy = (GameObject)Instantiate(YetiPrefab,
                                    new Vector3(startX + x * xIncrement, -2.5f, 64f + s * 8f + Random.Range(-2f, 2f)),
                                    Quaternion.identity,
                                    ItemContainer.transform);
                            }
                            else if (randomVal < 30f)
                            {
                                // make a powerup
                                float powerupRandVal = Random.Range(0f, 100.0f);
                                GameObject powerupPrefab = CoinPowerupPrefab;
                                if (powerupRandVal > 80)
                                {
                                    powerupPrefab = HeartPowerupPrefab;
                                }
                                GameObject powerup = (GameObject)Instantiate(powerupPrefab,
                                    new Vector3(startX + x * xIncrement, -3f, 64f + s * 8f + Random.Range(-2f, 2f)),
                                    Quaternion.identity,
                                    ItemContainer.transform);
                            }
                        }
                    }
                }
                distanceUntilSpawn = 64f;
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
        float newSpeed = Mathf.Min(Globals.maxSpeed, Globals.ScrollSpeed.z + 4f);
        Globals.ScrollSpeed = new Vector3(0, 0, newSpeed);
        smokeManager.SpeedUp();
        speedLineTimer = speedLineTimerMax;
        SpeedLines.SetActive(true);
    }

    public void Invincible()
    {
        invincibleTimer = invincibleTimerMax;
        Player.GetComponent<VehicleTypeManager>().ChangeToInvincible();
    }

    public bool IsInvincible()
    {
        return invincibleTimer > 0;
    }
    public float InvinciblePercent()
    {
        return invincibleTimer / invincibleTimerMax;
    }

    public void Bomb()
    {
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
            heartTimer = heartTimerMax;
            audioManager.PlayBombSound();
            Bomb();
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

    public void StartGame()
    {
        if (Globals.CurrentGameState != Globals.GameState.TitleScreen && Globals.CurrentGameState != Globals.GameState.Restart)
            return;

        audioManager.PlayStartSound();

        Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y, Camera.main.transform.position.z);
        Player.GetComponent<Player>().Reset();

        HUDGameOver.GetComponent<MoveNormal>().MoveUp();
        HUDBackground.SetActive(false);
        HUDPlayer.SetActive(false);
        HUDPlayer.transform.localPosition = new Vector3(20f, HUDPlayer.transform.localPosition.y, HUDPlayer.transform.localPosition.z);
        HUDAbout.GetComponent<MoveNormal>().MoveRight();
        HUDSettings.GetComponent<MoveNormal>().MoveRight();
        HUDTitle.GetComponent<MoveNormal>().MoveLeft();
        HUDButtons.GetComponent<MoveNormal>().MoveDown();
        HUDSelectVehicle.GetComponent<MoveNormal>().MoveDown();
        Level.SetActive(true);
        Player.SetActive(true);

        invincibleTimer = 0f;
        Globals.CurrentDistance = 0;
        distance = 0;
        waveNum = 0;
        distanceUntilSpawn = 8f;
        totalHearts = 0;
        for (int x = 0; x < HUDHearts.Length; x++)
        {
            HUDHearts[x].SetActive(false);
        }
        HUDDistance.SetActive(false);
        HUDDistanceText.text = Globals.CurrentDistance.ToString();
        HUDQuit.SetActive(true);

        CreateCourse();

        HUDRaceReady.SetActive(true);
        HUDRaceReady.GetComponent<GrowAndShrink>().StartEffect();

        Globals.CurrentGameState = Globals.GameState.Ready;
    }

    public void CreateCourse()
    {
        // WTD fill in with default content if wanted
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

        HUDSelectVehicle.GetComponent<MoveNormal>().MoveUp();
        HUDPlayer.GetComponent<MoveNormal>().MoveRight();
        HUDAbout.GetComponent<MoveNormal>().MoveRight();
        HUDSettings.GetComponent<MoveNormal>().MoveRight();
        HUDFinalStatsContainer.GetComponent<MoveNormal>().MoveRight();
    }
    public void SelectAboutButton()
    {
        audioManager.PlayMenuSound();

        HUDAbout.GetComponent<MoveNormal>().MoveLeft();
        HUDSettings.GetComponent<MoveNormal>().MoveRight();
        HUDPlayer.GetComponent<MoveNormal>().MoveRight();
        HUDFinalStatsContainer.GetComponent<MoveNormal>().MoveRight();
        HUDSelectVehicle.GetComponent<MoveNormal>().MoveDown();
    }
    public void SelectSettingsButton()
    {
        audioManager.PlayMenuSound();

        HUDSettings.GetComponent<MoveNormal>().MoveLeft();
        HUDAbout.GetComponent<MoveNormal>().MoveRight();
        HUDPlayer.GetComponent<MoveNormal>().MoveRight();
        HUDFinalStatsContainer.GetComponent<MoveNormal>().MoveRight();
        HUDSelectVehicle.GetComponent<MoveNormal>().MoveDown();
    }
    public void SelectVehicleButton(int currentVehicleIndex)
    {
        audioManager.PlayMenuSound();

        HUDSelectVehicle.GetComponent<MoveNormal>().MoveDown();
        HUDPlayer.GetComponent<MoveNormal>().MoveLeft();

        HUDPlayer.GetComponent<VehicleTypeManager>().SetVehicleType(currentVehicleIndex);
        Player.GetComponent<VehicleTypeManager>().SetVehicleType(currentVehicleIndex);
    }
    public void SelectQuitButton()
    {
        audioManager.PlayMenuSound();

        HUDBackground.SetActive(true);
        HUDGameOver.GetComponent<MoveNormal>().MoveUp();
        HUDPlayer.SetActive(true);
        HUDPlayer.GetComponent<MoveNormal>().MoveLeft();
        HUDTitle.GetComponent<MoveNormal>().MoveRight();
        HUDButtons.GetComponent<MoveNormal>().MoveUp();
        Level.SetActive(false);
        Player.SetActive(false);

        HUDQuit.SetActive(false);
        BombFlash.SetActive(false);
        HUDRaceReady.SetActive(false);
        HUDDistance.SetActive(false);
        HUDItems.SetActive(true);

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
