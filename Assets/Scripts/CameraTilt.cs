using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTilt : MonoBehaviour
{
    enum JuicyState {
        Normal,
        TiltTo,
        Wait,
        TiltBack,
    };
    JuicyState state = JuicyState.Normal;
    float tiltTo = 18f;
    float initialTilt = 20f;
    float moveTo = -7.6f;
    float initialMove = -8f;
    float waitTimer = 0f;
    float waitTimerMax = .2f;
    float speedStart = 8f;
    float speedEnd = 15f;
    float cameraToCarSpeedRatio = 5f;

    AudioManager audioManager;
    SceneManager sceneManager;

    void Awake()
    {
        audioManager = GameObject.Find("SceneManager").GetComponent<AudioManager>();
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == JuicyState.TiltTo)
        {
            float newTilt = Mathf.Max(tiltTo, this.transform.eulerAngles.x - Time.deltaTime * speedStart);
            this.transform.eulerAngles = new Vector3 (newTilt, 0, 0);
            float newPos = Mathf.Min(moveTo, this.transform.localPosition.z + Time.deltaTime * speedStart / cameraToCarSpeedRatio);
            this.transform.localPosition = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y, newPos);
            if (newTilt == tiltTo)
            {
                waitTimer = waitTimerMax;
                state =  JuicyState.Wait;
            }
        }
        else if (state == JuicyState.Wait)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                state =  JuicyState.TiltBack;
                audioManager.PlaySpeedUpSound();
                audioManager.PlayEngineSound();
                sceneManager.StartSpeedLines(.75f);
            }
        }
        else if (state == JuicyState.TiltBack)
        {
            float newTilt = Mathf.Min(initialTilt, this.transform.eulerAngles.x + Time.deltaTime * speedEnd);
            this.transform.eulerAngles = new Vector3 (newTilt, 0, 0);
            float newPos = Mathf.Max(initialMove, this.transform.localPosition.z - Time.deltaTime * speedEnd / cameraToCarSpeedRatio);
            this.transform.localPosition = new Vector3 (this.transform.localPosition.x, this.transform.localPosition.y, newPos);
            if (newTilt == initialTilt)
            {
                state =  JuicyState.Normal;
            }
        }
    }

    public void StartEffect()
    {
        state = JuicyState.TiltTo;
    }

    public void StopEffect()
    {
        state = JuicyState.Normal;
    }
}
