using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType {
        Arrow,
        Star,
        Ghost1,
        Ghost2,
        Ghost3,
        Yeti,
        Ball,
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
        if (itemType == ItemType.Arrow || itemType == ItemType.Star || itemType == ItemType.Star)
        {
            debrisColor = Color.yellow;
        }
        if (itemType == ItemType.Heart)
        {
            debrisColor = Color.red;
        }
        else if (itemType == ItemType.Ghost1)
        {
            debrisColor = Color.black;
        }
        else if (itemType == ItemType.Ghost2)
        {
            debrisColor = Color.red;
        }
        else if (itemType == ItemType.Ghost3)
        {
            debrisColor = Color.yellow;
        }
        else if (itemType == ItemType.Yeti)
        {
            debrisColor = Color.blue;
            extraSpeed = Random.Range(5f, 10f);
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
        if (Globals.CurrentGameState == Globals.GameState.ShowScore)
        {
            int debrisAmount = 10;
            debrisManager.StartDebris (debrisAmount, this.transform.position, debrisColor);
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        if (Globals.CurrentGameState == Globals.GameState.Ready || Globals.CurrentGameState == Globals.GameState.Playing || Globals.CurrentGameState == Globals.GameState.ShowScore)
        {
            Vector3 movement = new Vector3 (0, 0, Globals.ScrollSpeed.z * Globals.ScrollDirection.z + extraSpeed * Globals.ScrollDirection.z);
            this.GetComponent<Rigidbody>().velocity = movement;
        }
    }
}
