using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibleMeter : MonoBehaviour
{
    [SerializeField]
    RectTransform Meter;

    float minMeterSize = 0f;
    float maxMeterSize = 108f;

    SceneManager sceneManager;

    void Awake()
    {
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneManager.IsInvincible() && Globals.CurrentGameState == Globals.GameState.Playing)
        {
            float delta = (maxMeterSize - minMeterSize) * sceneManager.InvinciblePercent();

            Meter.sizeDelta = new Vector2(minMeterSize + delta, Meter.sizeDelta.y);
        }
    }
}
