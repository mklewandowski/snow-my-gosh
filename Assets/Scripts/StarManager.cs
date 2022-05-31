using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    [SerializeField]
    GameObject StarContainer;
    [SerializeField]
    GameObject StarPrefab;
    GameObject[] starPool = new GameObject[125];

    // Start is called before the first frame update
    void Start()
    {
        AddStars();
    }

    public void AddStars()
    {
        for (int x = 0; x < starPool.Length; x++)
        {
            starPool[x] = (GameObject)Instantiate (StarPrefab, new Vector3 (100f, 100f, 100f), Quaternion.identity, StarContainer.transform);

            // choose color
            int colorNum = Random.Range(0, 4);
            if (colorNum == 0)
                starPool[x].GetComponent<Renderer>().material.color = new Color(19f/255f, 216f/255f, 251f/255f);
            else if (colorNum == 1)
                starPool[x].GetComponent<Renderer>().material.color = new Color(99f/255f, 135f/255f, 223f/255f);
            else if (colorNum == 2)
                starPool[x].GetComponent<Renderer>().material.color = new Color(245f/255f, 219f/255f, 244f/255f);
            else if (colorNum == 3)
                starPool[x].GetComponent<Renderer>().material.color = new Color(255f/255f, 187f/255f, 255f/255f);

            float scale = Random.Range(.05f, .1f);
            starPool[x].transform.localScale = new Vector3(scale, scale, scale);
            starPool[x].transform.localPosition = new Vector3(
                Random.Range(-10f, 10f),
                Random.Range(2f, 5f),
                0
            );

            // twinkle
        }
    }
}
