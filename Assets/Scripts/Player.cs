using UnityEngine;

public class Player : MonoBehaviour
{
    AudioManager audioManager;

    Vector3 movement = new Vector3(0, 0, 0);
    float gravity = -10f;
    bool thrust = false;
    bool isGrounded = false;
    float speedRange = 6f;
    float maxAngle = 35f;

    void Awake()
    {
        audioManager = this.GetComponent<AudioManager>();
    }

    void Update() {
        thrust = Input.GetKey ("space") | Input.GetButton ("Fire1") | Input.GetButton ("Fire2");

        movement.y += gravity * Time.deltaTime;
        // user "thrust", give vehicle some upward movement
        if (thrust && Globals.CurrentGameState == Globals.GameState.Playing)
        {
            movement.y += 25f * Time.deltaTime;
            isGrounded = false;
        }
        movement.y = Mathf.Max(movement.y, -6.0f);
        movement.y = Mathf.Min(movement.y, 6.0f);
        this.gameObject.GetComponent<Rigidbody> ().velocity = movement;

        // what is the min position based on the current angle?
        float maxYPos = 3.9f;
        float minYPos = -3.1f;
        float adjustAmount = .45f;
        float anglePercent = (movement.y / speedRange);
        minYPos = minYPos - adjustAmount * anglePercent;

        if(transform.localPosition.y <= minYPos)
        {
            // did we hit the ground? stop it if we did
            isGrounded = true;
            transform.localPosition = new Vector3 (transform.localPosition.x, minYPos, transform.localPosition.z);
            movement.y = 0f;
            this.gameObject.GetComponent<Rigidbody> ().velocity = movement;
        }
        else if (transform.localPosition.y >= maxYPos)
        {
            // don't let the ship go offscreen to the top
            transform.localPosition = new Vector3 (transform.localPosition.x, maxYPos, transform.localPosition.z);
            if (movement.y > 0)
                movement.y -= 30f * Time.deltaTime;
            else
                movement.y -= 10f * Time.deltaTime;
            this.gameObject.GetComponent<Rigidbody> ().velocity = movement;
        }

        // set the rotation of vehicle
        float newRotation = 0f;
        newRotation = maxAngle * (movement.y / speedRange);
        this.transform.eulerAngles = new Vector3 (0, 0, newRotation);
    }
}
