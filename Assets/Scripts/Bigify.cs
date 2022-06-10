using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bigify : MonoBehaviour
{
    enum JuicyState {
        Normal,
        Grow,
        Shrink,
    };
    JuicyState state = JuicyState.Normal;

    [SerializeField]
    float GrowTo = 2f;
    [SerializeField]
    float ShrinkTo = 1f;
    [SerializeField]
    float Rate = 5f;
    [SerializeField]
    float OriginY = -3.05f;
    [SerializeField]
    float BigifyY = -2.45f;
    [SerializeField]
    GameObject SmokeOrigin;

    void Update ()
    {
        if (state == JuicyState.Grow)
        {
            float newScale = Mathf.Min(GrowTo, this.transform.localScale.x + Time.deltaTime * Rate);
            this.transform.localScale = new Vector3(newScale, newScale, newScale);
            float newY = Mathf.Min(BigifyY, this.transform.localPosition.y + Time.deltaTime * Rate);
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, newY, this.transform.localPosition.z);
            if (newScale == GrowTo)
            {
                SmokeOrigin.transform.localPosition = new Vector3(-0.2f, SmokeOrigin.transform.localPosition.y, SmokeOrigin.transform.localPosition.z);
                state =  JuicyState.Normal;
            }
        }
        else if (state == JuicyState.Shrink)
        {
            float newScale = Mathf.Max(ShrinkTo, this.transform.localScale.x - Time.deltaTime * Rate);
            this.transform.localScale = new Vector3(newScale, newScale, newScale);
            float newY = Mathf.Max(OriginY, this.transform.localPosition.y - Time.deltaTime * Rate);
            this.transform.localPosition = new Vector3(this.transform.localPosition.x, newY, this.transform.localPosition.z);
            if (newScale == ShrinkTo)
            {
                SmokeOrigin.transform.localPosition = new Vector3(-0.085f, SmokeOrigin.transform.localPosition.y, SmokeOrigin.transform.localPosition.z);
                state =  JuicyState.Normal;
            }
        }
    }

    public void StartBigify()
    {
        state = JuicyState.Grow;
    }

    public void EndBigify()
    {
        state = JuicyState.Shrink;
    }
}
