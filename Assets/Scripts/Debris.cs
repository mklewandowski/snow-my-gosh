using UnityEngine;

public class Debris : MonoBehaviour
{
    public bool InUse = false;
    float lifeTimer = 0f;

    void Update()
    {
        if (lifeTimer > 0)
        {
            lifeTimer -= Time.deltaTime;
            if (lifeTimer <= 0)
            {
                StopUse();
            }
        }
    }

    public void Use(Vector3 position, Color color)
    {
        InUse = true;

        gameObject.GetComponent<Renderer>().material.color = color;

        this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        this.transform.position = position;
        lifeTimer = Random.Range (1f, 1.5f);

        float newscale = Random.Range (.1f, .3f);
        transform.localScale =  new Vector3 (newscale, newscale, newscale);

        // give it some physics
        Vector3 movementVector = new Vector3(0, 0, 0);
        movementVector.x += Random.Range(-5f, 5f) * Time.deltaTime * 100f;
        movementVector.y += Random.Range(-2, 4f) * Time.deltaTime * 100f;
        movementVector.z += Random.Range(-4f, 4f) * Time.deltaTime * 100f;
        this.gameObject.GetComponent<Rigidbody>().velocity = movementVector;

        this.gameObject.SetActive (true);
    }

    public void StopUse()
    {
        InUse = false;
        this.gameObject.SetActive (false);
        this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        this.transform.position = new Vector3 (100f, 100f, 100f);
    }

}
