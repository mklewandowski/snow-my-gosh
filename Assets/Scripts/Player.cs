using UnityEngine;

public class Player : MonoBehaviour
{
    AudioManager audioManager;

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

    void Awake()
    {
        audioManager = this.GetComponent<AudioManager>();
    }

    void Update() {
        bool moveLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        bool moveRight = !moveLeft && Input.GetKeyDown(KeyCode.RightArrow);
        if (moveLeft)
        {
            desiredXPos = Mathf.Max(minX, desiredXPos - xIncrement);
            movement.x = turnSpeed * -1f * Time.deltaTime;
            if (Mathf.Round(desiredXPos) != Mathf.Round(transform.localPosition.x))
            {
                desiredYRotation = -20f;
                rotateDir = -1f;
            }
        }
        else if (moveRight)
        {
            desiredXPos = Mathf.Min(maxX, desiredXPos + xIncrement);
            movement.x = turnSpeed * Time.deltaTime;
            if (Mathf.Round(desiredXPos) != Mathf.Round(transform.localPosition.x))
            {
                desiredYRotation = 20f;
                rotateDir = 1f;
            }
        }
        if (movement.x > 0 && transform.localPosition.x >= desiredXPos)
        {
            transform.localPosition = new Vector3 (desiredXPos, transform.localPosition.y, transform.localPosition.z);
            movement.x = 0;
        }
        else if (movement.x < 0 && transform.localPosition.x <= desiredXPos)
        {
            transform.localPosition = new Vector3 (desiredXPos, transform.localPosition.y, transform.localPosition.z);
            movement.x = 0;
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
                if (currYRotation == 20f)
                {
                    desiredYRotation = 0;
                    rotateDir = -1f;
                }
            }
            else  if (rotateDir == -1f)
            {
                currYRotation = Mathf.Max(desiredYRotation, currYRotation);
                if (currYRotation == -20f)
                {
                    desiredYRotation = 0;
                    rotateDir = 1f;
                }
            }
            this.transform.eulerAngles = new Vector3 (0, currYRotation, 0);
        }
    }
}
