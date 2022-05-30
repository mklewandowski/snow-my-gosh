using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType {
        Arrow,
        Star,
        Ghost,
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
        else if (itemType == ItemType.Ghost)
        {
            debrisColor = Color.yellow;
        }
        else if (itemType == ItemType.Ball)
        {
            debrisColor = Color.white;
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
            Vector3 movement = new Vector3 (0, 0, Globals.ScrollSpeed.z * Globals.ScrollDirection.z);
            this.GetComponent<Rigidbody>().velocity = movement;
        }
    }
}
