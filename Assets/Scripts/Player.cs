using UnityEngine;

public class Player : MonoBehaviour
{
    AudioManager audioManager;
    protected Color debrisColor = new Color(255f/255f, 106f/255f, 0);
    protected DebrisManager debrisManager;

    Vector3 movement = new Vector3(0, 0, 0);

    float turnSpeed = 450f;
    float desiredXPos = 0f;
    float xIncrement = 3f;
    float minX = -6f;
    float maxX = 6f;
    float currYRotation = 0f;
    float desiredYRotation = 0f;
    float rotateDir = 1f;
    float rotateSpeed = 100f;
    float maxRotation = 15f;
    bool movingLeft = false;
    bool movingRight = false;
    bool requestMoveLeft = false;
    bool requestMoveRight = false;

    void Awake()
    {
        audioManager = GameObject.Find("SceneManager").GetComponent<AudioManager>();
        GameObject dm = GameObject.Find ("DebrisManager");
        debrisManager = dm.GetComponent<DebrisManager> ();
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
    }

    private void SwipeDetector_OnSwipe(SwipeData data)
    {
        if (Globals.CurrentGameState != Globals.GameState.Playing)
            return;
        if (!requestMoveLeft && !requestMoveRight)
        {
            requestMoveLeft = data.Direction == SwipeDirection.Left;
            requestMoveRight = !requestMoveLeft && data.Direction == SwipeDirection.Right;
            if ((requestMoveLeft || requestMoveRight) && !movingLeft && !movingRight)
                audioManager.PlaySwipeSound();
        }
    }

    void Update()
    {
        if (Globals.CurrentGameState != Globals.GameState.Playing)
            return;
        if (!requestMoveLeft && !requestMoveRight)
        {
            requestMoveLeft = Input.GetKeyDown(KeyCode.LeftArrow);
            requestMoveRight = !requestMoveLeft && Input.GetKeyDown(KeyCode.RightArrow);
            if ((requestMoveLeft || requestMoveRight) && !movingLeft && !movingRight)
                audioManager.PlaySwipeSound();
        }
    }

    public void Die()
    {
        this.gameObject.SetActive(false);
        int debrisAmount = 20;
        debrisManager.StartDebris (debrisAmount, this.transform.position, debrisColor);
    }

    public void Reset()
    {
        this.GetComponent<Collider>().enabled = true;
        movingLeft = false;
        movingRight = false;
        requestMoveLeft = false;
        requestMoveRight = false;
        desiredXPos = 0;
        this.gameObject.SetActive(true);
        this.transform.localScale = new Vector3(1f, 1f, 1f);
        this.transform.localPosition = new Vector3(0, -3.05f, this.transform.localPosition.z);
    }

    void FixedUpdate()
    {
        if (requestMoveLeft && !movingLeft && !movingRight)
        {
            movingLeft = true;
            desiredXPos = Mathf.Max(minX, desiredXPos - xIncrement);
            movement.x = turnSpeed * -1f * Time.deltaTime;
            if (Mathf.Round(desiredXPos) != Mathf.Round(transform.localPosition.x))
            {
                desiredYRotation = maxRotation * -1f;
                rotateDir = -1f;
            }
        }
        else if (requestMoveRight && !movingLeft && !movingRight)
        {
            movingRight = true;
            desiredXPos = Mathf.Min(maxX, desiredXPos + xIncrement);
            movement.x = turnSpeed * Time.deltaTime;
            if (Mathf.Round(desiredXPos) != Mathf.Round(transform.localPosition.x))
            {
                desiredYRotation = maxRotation;
                rotateDir = 1f;
            }
        }
        requestMoveLeft = false;
        requestMoveRight = false;

        if (movement.x > 0 && transform.localPosition.x >= desiredXPos)
        {
            transform.localPosition = new Vector3 (desiredXPos, transform.localPosition.y, transform.localPosition.z);
            movement.x = 0;
            movingLeft = false;
            movingRight = false;
        }
        else if (movement.x < 0 && transform.localPosition.x <= desiredXPos)
        {
            transform.localPosition = new Vector3 (desiredXPos, transform.localPosition.y, transform.localPosition.z);
            movement.x = 0;
            movingLeft = false;
            movingRight = false;
        }
        Camera.main.transform.position = new Vector3(transform.localPosition.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        this.gameObject.GetComponent<Rigidbody>().velocity = movement;

        // set the rotation of vehicle
        if (currYRotation != desiredYRotation)
        {
            currYRotation += rotateDir * Time.deltaTime * rotateSpeed;
            if (rotateDir == 1f)
            {
                currYRotation = Mathf.Min(desiredYRotation, currYRotation);
                if (currYRotation == maxRotation)
                {
                    desiredYRotation = 0;
                    rotateDir = -1f;
                }
            }
            else  if (rotateDir == -1f)
            {
                currYRotation = Mathf.Max(desiredYRotation, currYRotation);
                if (currYRotation == maxRotation * -1f)
                {
                    desiredYRotation = 0;
                    rotateDir = 1f;
                }
            }
            this.transform.eulerAngles = new Vector3 (0, currYRotation, 0);
        }
    }
}
