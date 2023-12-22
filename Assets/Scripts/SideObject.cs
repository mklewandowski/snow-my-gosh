using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideObject : MonoBehaviour
{
    Globals.SideObjectType currentType = Globals.SideObjectType.None;

    [SerializeField]
    GameObject PineTreeSmall;

    [SerializeField]
    GameObject PineTreeBig;
    [SerializeField]
    GameObject OldTree;
    [SerializeField]
    GameObject SnowBank;

    void Start()
    {
        OldTree.transform.localPosition = new Vector3(0, Random.Range(-2f, 0), 0);
        SnowBank.transform.localPosition = new Vector3(0, Random.Range(-1.2f, 0), 0);
        OldTree.transform.localEulerAngles = new Vector3(0, Random.Range(0, 360f), 0);
        PineTreeSmall.transform.localEulerAngles = new Vector3(0, 70f, 0);
        PineTreeBig.transform.localEulerAngles = new Vector3(0, 70f, 0);
    }

    public void SetLeftSide()
    {
        PineTreeSmall.transform.localEulerAngles = new Vector3(0, 110f, 0);
        PineTreeBig.transform.localEulerAngles = new Vector3(0, 110f, 0);
    }

    public void SetType(Globals.SideObjectType newType)
    {
        if (currentType == newType)
            return;
        currentType = newType;
        PineTreeSmall.SetActive(currentType == Globals.SideObjectType.PineTreeSmall);
        PineTreeBig.SetActive(currentType == Globals.SideObjectType.PineTreeBig);
        OldTree.SetActive(currentType == Globals.SideObjectType.OldTree);
        SnowBank.SetActive(currentType == Globals.SideObjectType.SnowBank);
    }
}
