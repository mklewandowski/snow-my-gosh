using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CharacterSelect : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    SceneManager sceneManager;

    [SerializeField]
    GameObject[] Vehicles = new GameObject[1];
    [SerializeField]
    Image CurrentVehicle;
    [SerializeField]
    TextMeshProUGUI VehicleName;
    [SerializeField]
    GameObject SelectButton;
    [SerializeField]
    GameObject BuyButton;
    [SerializeField]
    GameObject NotEnoughCoins;
    [SerializeField]
    GameObject ForSale;

    [SerializeField]
    Transform DragContainer;

    float minX = 0;
    float maxX = 0;
    int maxItems = 28;
    int currentVehicle = 0;
    float vehicleInstanceXInterval = 250f;
    private float lastDragPos;
    float scaleFactor;

    void Start()
    {
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        scaleFactor = this.GetComponent<Canvas>().scaleFactor;
        maxX = Camera.main.scaledPixelWidth / 2 - vehicleInstanceXInterval / 2;
        minX = maxX - vehicleInstanceXInterval * scaleFactor * (maxItems - 1);
        DragContainer.position = new Vector3(maxX, DragContainer.position.y, DragContainer.position.z);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastDragPos = eventData.position.x;
    }

    // Drag the selected item.
    public void OnDrag(PointerEventData data)
    {
        float dragOffset = lastDragPos - data.position.x;
        lastDragPos = data.position.x;
        float newX = DragContainer.position.x - dragOffset;
        newX = Mathf.Max(minX, newX);
        newX = Mathf.Min(maxX, newX);
        DragContainer.position = new Vector3(newX, DragContainer.position.y, DragContainer.position.z);

        float indexDelta = maxX + vehicleInstanceXInterval / 2 - newX;
        int newVehicleIndex = (int)Mathf.Floor(indexDelta / (vehicleInstanceXInterval * scaleFactor));
        updateCurrentVehicle(newVehicleIndex);
    }

    void updateCurrentVehicle(int newVehicleIndex)
    {
        if (newVehicleIndex != currentVehicle && newVehicleIndex < Vehicles.Length)
        {
            bool unlocked = Globals.VehicleUnlockStates[newVehicleIndex] == 1 ? true : false;
            bool enoughCoins = Globals.Coins >= 100;
            SelectButton.SetActive(unlocked);
            BuyButton.SetActive(!unlocked && enoughCoins);
            NotEnoughCoins.SetActive(!unlocked && !enoughCoins);
            ForSale.SetActive(!unlocked);
            Vehicles[currentVehicle].SetActive(true);
            currentVehicle = newVehicleIndex;
            Vehicles[currentVehicle].SetActive(false);
            CurrentVehicle.sprite = Vehicles[currentVehicle].GetComponent<Image>().sprite;
            VehicleName.text = Globals.GetVehicleNameFromType(Vehicles[currentVehicle].GetComponent<CharacterSelectVehicle>().VehicleType);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Debug.Log("OnEndDrag: " + eventData.position.x);
    }

    public void SelectCurrentVehicle()
    {
        sceneManager.SelectVehicleButton((int)Vehicles[currentVehicle].GetComponent<CharacterSelectVehicle>().VehicleType);
    }

    public void BuyCurrentVehicle()
    {
        sceneManager.SelectBuyVehicleButton(currentVehicle);
        SelectButton.SetActive(true);
        BuyButton.SetActive(false);
        NotEnoughCoins.SetActive(false);
        ForSale.SetActive(false);
    }
}
