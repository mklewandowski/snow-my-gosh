using UnityEngine;

public class VehicleTypeManager : MonoBehaviour
{
    [SerializeField]
    GameObject Default;
    [SerializeField]
    MeshRenderer DefaultRenderer;
    Collider collider;

    [SerializeField]
    GameObject Invincible;
    [SerializeField]
    MeshRenderer InvincibleRenderer;

    [SerializeField]
    Material DefaultMaterial;
    [SerializeField]
    Material FlashMaterial;
    [SerializeField]
    Material GhostMaterial;

    Globals.VehicleType currentVehicleType = Globals.VehicleType.Default;

    void Awake()
    {
        collider = this.GetComponent<Collider>();
    }

    public void SetVehicleType(int c)
    {
        Globals.VehicleType vehicleType = (Globals.VehicleType)c;
        if (vehicleType != Globals.VehicleType.Invincible)
            currentVehicleType = vehicleType;

        Default.SetActive(vehicleType == Globals.VehicleType.Default);

        if (Invincible != null) Invincible.SetActive(vehicleType == Globals.VehicleType.Invincible);

        Globals.SaveIntToPlayerPrefs(Globals.VehicleTypePlayerPrefsKey, c);
    }

    public void ChangeToInvincible()
    {
        InvincibleFlash(false);
        if (Invincible.activeSelf && !Invincible.GetComponent<ShrinkAndHide>().IsShrinking())
            return;

        Invincible.GetComponent<ShrinkAndHide>().StopEffect();
        Invincible.transform.localScale = new Vector3(.1f, .1f, .1f);
        Invincible.SetActive(true);
        Invincible.GetComponent<GrowAndShrink>().StartEffect();

        GameObject currVehicle = GetCurrentVehicle();
        currVehicle.GetComponent<GrowAndShrink>().StopEffect();
        currVehicle.GetComponent<ShrinkAndHide>().StartEffect();
    }

    public void ChangeToGhost()
    {
        GhostFlash(false);
        collider.enabled = false;
    }

    public void EndGhost()
    {
        collider.enabled = true;
        Material[] materialArray = DefaultRenderer.materials; // WTD expand for more vehicles
        materialArray[0] = DefaultMaterial;
        DefaultRenderer.materials = materialArray;
    }

    public void InvincibleFlash(bool flash)
    {
        Material[] materialArray = InvincibleRenderer.materials;
        materialArray[0] = flash ? FlashMaterial : DefaultMaterial;
        InvincibleRenderer.materials = materialArray;
    }

    public void GhostFlash(bool flash)
    {
        Material[] materialArray = DefaultRenderer.materials; // WTD expand for more vehicles
        materialArray[0] = flash ? DefaultMaterial : GhostMaterial;
        DefaultRenderer.materials = materialArray;
    }

    public void RestoreVehicleType()
    {
        if (!Invincible.activeSelf)
            return;

        GameObject currVehicle = GetCurrentVehicle();
        currVehicle.GetComponent<ShrinkAndHide>().StopEffect();
        currVehicle.transform.localScale = new Vector3(.1f, .1f, .1f);
        currVehicle.SetActive(true);
        currVehicle.GetComponent<GrowAndShrink>().StartEffect();

        Invincible.GetComponent<GrowAndShrink>().StopEffect();
        Invincible.GetComponent<ShrinkAndHide>().StartEffect();
    }

    GameObject GetCurrentVehicle()
    {
        GameObject currVehicle = Default;
        if (currentVehicleType == Globals.VehicleType.Default)
            currVehicle = Default;

        return currVehicle;
    }
}
