using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAndHide : MonoBehaviour
{
    [SerializeField]
    float WaitTime = 2f;

    float waitTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
                this.gameObject.SetActive(false);
        }
    }

    public void StartEffect()
    {
        waitTimer = WaitTime;
    }
}
