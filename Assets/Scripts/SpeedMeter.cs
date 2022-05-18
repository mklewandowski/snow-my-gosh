using UnityEngine;

public class SpeedMeter : MonoBehaviour
{
    [SerializeField]
    RectTransform Meter;

    float minMeterSize = 10f;
    float maxMeterSize = 108f;

    float speed;

    // Update is called once per frame
    void Update()
    {
        if (speed != Globals.ScrollSpeed.z && Globals.CurrentGameState == Globals.GameState.Playing)
        {
            speed = Globals.ScrollSpeed.z;
            float delta = (maxMeterSize - minMeterSize) * ((speed - Globals.minSpeed) / (Globals.maxSpeed - Globals.minSpeed));

            Meter.sizeDelta = new Vector2(minMeterSize + delta, Meter.sizeDelta.y);
        }
    }
}
