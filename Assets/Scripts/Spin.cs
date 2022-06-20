using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    float spinSpeed = 300f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.left * Time.deltaTime * spinSpeed);
    }
}
