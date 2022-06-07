using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType {
        Yeti,
        Ball,
        Arrow,
        Star,
        Heart,
        Coin,
        Bomb
    }
    public ItemType itemType = ItemType.Arrow;
    protected Color debrisColor;
    protected DebrisManager debrisManager;

    protected AudioManager audioManager;
    protected SceneManager sceneManager;

    protected float extraSpeed = 0f;

    protected bool isActive = true;

    void Awake()
    {
        audioManager = GameObject.Find("SceneManager").GetComponent<AudioManager>();
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (itemType == ItemType.Arrow || itemType == ItemType.Star || itemType == ItemType.Coin)
        {
            debrisColor = Color.yellow;
        }
        else if (itemType == ItemType.Heart)
        {
            debrisColor = Color.red;
        }
        else if (itemType == ItemType.Yeti)
        {
            debrisColor = Color.blue;
        }
        else if (itemType == ItemType.Ball)
        {
            debrisColor = Color.white;
            extraSpeed = Random.Range(10f, 20f);
        }
        else if (itemType == ItemType.Bomb)
        {
            debrisColor = Color.black;
        }

        GameObject dm = GameObject.Find ("DebrisManager");
        debrisManager = dm.GetComponent<DebrisManager> ();
    }

    void Update()
    {
        if (Globals.CurrentGameState == Globals.GameState.Restart || this.transform.position.z < -10f)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        if (Globals.CurrentGameState == Globals.GameState.Ready || Globals.CurrentGameState == Globals.GameState.Playing || Globals.CurrentGameState == Globals.GameState.ShowScore)
        {
            float speedBump = 0;
            if (this.transform.localPosition.z < 60f)
                speedBump = extraSpeed;
            Vector3 movement = new Vector3 (0, 0, Globals.ScrollSpeed.z * Globals.ScrollDirection.z + speedBump * Globals.ScrollDirection.z);
            this.GetComponent<Rigidbody>().velocity = movement;
        }
    }
}
