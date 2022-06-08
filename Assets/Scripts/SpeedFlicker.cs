using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedFlicker : MonoBehaviour
{
    float timer = .1f;
    bool show = false;
    float initialZ = 0;

    // Start is called before the first frame update
    void Start()
    {
        timer = .1f;
        initialZ = this.transform.localPosition.z;
    }


    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            show = !show;
            this.gameObject.GetComponent<MeshRenderer>().enabled = show;
            if (!show)
            {
                this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, initialZ);
            }
            timer = Random.Range(.1f, .2f);
        }
        if (show)
        {
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z - Time.deltaTime * 2f);
        }
    }
}
