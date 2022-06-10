using UnityEngine;

public class VehicleTypeManager : MonoBehaviour
{
    [SerializeField]
    GameObject Default;
    [SerializeField]
    MeshRenderer DefaultRenderer;
    Collider carCollider;

    [SerializeField]
    GameObject Invincible;
    [SerializeField]
    MeshRenderer InvincibleRenderer;
    [SerializeField]
    GameObject StarRacer;
    [SerializeField]
    MeshRenderer StarRacerRenderer;
    [SerializeField]
    GameObject Plane;
    [SerializeField]
    MeshRenderer PlaneRenderer;

    [SerializeField]
    Material DefaultMaterial;
    [SerializeField]
    Material FlashMaterial;
    [SerializeField]
    Material GhostMaterial;

    Globals.VehicleType currentVehicleType = Globals.VehicleType.Default;

    float resumeCollisionTimer = 0;

    void Awake()
    {
        carCollider = this.GetComponent<Collider>();
    }

    void Update()
    {
        if (resumeCollisionTimer > 0)
        {
            resumeCollisionTimer -= Time.deltaTime;
            if (resumeCollisionTimer <= 0)
            {
                carCollider.enabled = true;
            }
        }
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
        MorphFlash(false);
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

    public void ChangeToStarRacer()
    {
        MorphFlash(false);
        if (StarRacer.activeSelf && !StarRacer.GetComponent<ShrinkAndHide>().IsShrinking())
            return;

        StarRacer.GetComponent<ShrinkAndHide>().StopEffect();
        StarRacer.transform.localScale = new Vector3(.1f, .1f, .1f);
        StarRacer.SetActive(true);
        StarRacer.GetComponent<GrowAndShrink>().StartEffect();

        GameObject currVehicle = GetCurrentVehicle();
        currVehicle.GetComponent<GrowAndShrink>().StopEffect();
        currVehicle.GetComponent<ShrinkAndHide>().StartEffect();
    }

    public void ChangeToPlane()
    {
        MorphFlash(false);
        if (Plane.activeSelf && !Plane.GetComponent<ShrinkAndHide>().IsShrinking())
            return;

        Plane.GetComponent<ShrinkAndHide>().StopEffect();
        Plane.transform.localScale = new Vector3(.1f, .1f, .1f);
        Plane.SetActive(true);
        Plane.GetComponent<GrowAndShrink>().StartEffect();

        GameObject currVehicle = GetCurrentVehicle();
        currVehicle.GetComponent<GrowAndShrink>().StopEffect();
        currVehicle.GetComponent<ShrinkAndHide>().StartEffect();
        carCollider.enabled = false;
    }

    public void ChangeToGhost()
    {
        GhostFlash(false);
        carCollider.enabled = false;
    }

    public void EndGhost()
    {
        carCollider.enabled = true;
        Material[] materialArray = DefaultRenderer.materials; // WTD expand for more vehicles
        materialArray[0] = DefaultMaterial;
        DefaultRenderer.materials = materialArray;
    }

    public void Bigify()
    {
        this.GetComponent<Bigify>().StartBigify();
    }

    public void EndBigify()
    {
        this.GetComponent<Bigify>().EndBigify();
    }

    public void MorphFlash(bool flash)
    {
        MeshRenderer powerupRenderer = InvincibleRenderer;
        if (StarRacer.activeSelf)
            powerupRenderer = StarRacerRenderer;
        if (Plane.activeSelf)
            powerupRenderer = PlaneRenderer;
        Material[] materialArray = powerupRenderer.materials;
        materialArray[0] = flash ? FlashMaterial : DefaultMaterial;
        powerupRenderer.materials = materialArray;
    }

    public void GhostFlash(bool flash)
    {
        Material[] materialArray = DefaultRenderer.materials; // WTD expand for more vehicles
        materialArray[0] = flash ? DefaultMaterial : GhostMaterial;
        DefaultRenderer.materials = materialArray;
    }

    public void BigifyFlash(bool flash)
    {
        Material[] materialArray = DefaultRenderer.materials; // WTD expand for more vehicles
        materialArray[0] = flash ? FlashMaterial : DefaultMaterial;
        DefaultRenderer.materials = materialArray;
    }

    public void RestoreVehicleType()
    {
        if (!Invincible.activeSelf && !StarRacer.activeSelf && !Plane.activeSelf)
            return;

        GameObject currVehicle = GetCurrentVehicle();
        currVehicle.GetComponent<ShrinkAndHide>().StopEffect();
        currVehicle.transform.localScale = new Vector3(.1f, .1f, .1f);
        currVehicle.SetActive(true);
        currVehicle.GetComponent<GrowAndShrink>().StartEffect();

        GameObject powerupGameObject = Invincible;
        if (StarRacer.activeSelf)
            powerupGameObject = StarRacer;
        if (Plane.activeSelf)
            powerupGameObject = Plane;

        powerupGameObject.GetComponent<GrowAndShrink>().StopEffect();
        powerupGameObject.GetComponent<ShrinkAndHide>().StartEffect();
    }

    GameObject GetCurrentVehicle()
    {
        GameObject currVehicle = Default;
        if (currentVehicleType == Globals.VehicleType.Default)
            currVehicle = Default;

        return currVehicle;
    }

    public void ResumeCollision(float delay)
    {
        resumeCollisionTimer = delay;
    }
}
