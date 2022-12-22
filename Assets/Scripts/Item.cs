using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType {
        Yeti,
        Ball,
        Heart,
        Coin,
        Cane
    }
    public ItemType itemType = ItemType.Coin;
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
        if (itemType == ItemType.Coin)
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
        }
        else if (itemType == ItemType.Cane)
        {
            debrisColor = Color.red;
        }

        GameObject dm = GameObject.Find ("DebrisManager");
        debrisManager = dm.GetComponent<DebrisManager> ();
    }

    public void SetExtraSpeed(float speed)
    {
        extraSpeed = speed;
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
