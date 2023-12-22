using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideObject : MonoBehaviour
{
    Globals.SideObjectType currentType;

    [SerializeField]
    GameObject PineTreeSmall;

    [SerializeField]
    GameObject PineTreeBig;
    [SerializeField]
    GameObject OldTree;

    void Start()
    {
        OldTree.transform.localPosition = new Vector3(0, Random.Range(-2f, 0), 0);
        OldTree.transform.localEulerAngles = new Vector3(0, Random.Range(0, 360f), 0);
    }

    public void SetType(Globals.SideObjectType newType)
    {
        currentType = newType;
        PineTreeSmall.SetActive(currentType == Globals.SideObjectType.PineTreeSmall);
        PineTreeBig.SetActive(currentType == Globals.SideObjectType.PineTreeBig);
        OldTree.SetActive(currentType == Globals.SideObjectType.OldTree);
    }
}
