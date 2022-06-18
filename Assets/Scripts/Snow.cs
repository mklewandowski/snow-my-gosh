using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour
{
    public bool InUse = false;
    float movingDownVelocity = -20f;
    float movingHorizontalVelocity = 0;
    float movingDownEndPos = -400f;

    // Start is called before the first frame update
    void Start()
    {
        movingDownEndPos = Screen.height / -2f - 25f;
    }

    void FixedUpdate()
    {
        if (InUse && Globals.CurrentGameState == Globals.GameState.TitleScreen)
        {
            float newY = transform.localPosition.y + movingDownVelocity * Time.deltaTime;
            float newX = transform.localPosition.x + movingHorizontalVelocity * Time.deltaTime;
            Vector3 newPos = new Vector3(newX, newY, transform.localPosition.z);
            transform.localPosition = newPos;
            if (newPos.y <= movingDownEndPos)
                StopUse();
        }
    }

    public void Use(Vector3 position)
    {
        InUse = true;

        this.transform.position = position;

        float newscale = Random.Range (1f, 2.5f);
        movingDownVelocity = Random.Range(-150f, -300f);
        movingHorizontalVelocity = Random.Range(0f, 10f);
        transform.localScale =  new Vector3 (newscale, newscale, newscale);

        this.gameObject.SetActive (true);
    }

    public void StopUse()
    {
        InUse = false;
        this.gameObject.SetActive (false);
        this.transform.position = new Vector3 (100f, 100f, 100f);
    }
}
