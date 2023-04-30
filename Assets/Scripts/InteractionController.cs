using UnityEngine;

public class InteractionController : MonoBehaviour
{
    private const string DOOR_TAG = "Door";

    [SerializeField]
    private float _rayMaxDistance = 100;
    
    [SerializeField]
    private LayerMask _layer;

    [SerializeField]
    private Transform _inventoryHolder;

    [SerializeField]
    private Transform _interactableInventory;

    private bool _isInventoryHolderFull;

    private void Update()
    {
        var ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out var hitInfo, _rayMaxDistance,_layer))
        {
            var gameObject = hitInfo.transform.gameObject;

            var interactableObject = gameObject.GetComponent<InteractableItem>();
            interactableObject.SetFocus();

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_isInventoryHolderFull)
                {
                    var inventory = _inventoryHolder.GetComponentInChildren<Rigidbody>().gameObject;
                    inventory.transform.SetParent(_interactableInventory, true);
                    _isInventoryHolderFull = false;   
                }

                gameObject.transform.SetParent(_inventoryHolder, false);
                _isInventoryHolderFull = true;
            }

            if (Input.GetMouseButtonDown(0) && _isInventoryHolderFull)
            {
                var inventory = _inventoryHolder.GetComponentInChildren<Rigidbody>();
                inventory.AddForce(Vector3.forward, ForceMode.Force);
            }
        }
        
        CheckTheDoor(ray);
    }

    private void CheckTheDoor(Ray ray)
    {
        if (Physics.Raycast(ray, out var hitInfo))
        {
            var gameObject = hitInfo.transform.gameObject;
            
            if (gameObject.CompareTag(DOOR_TAG) && Input.GetKeyDown(KeyCode.E))
            {
                var door = gameObject.GetComponent<Door>();
                door.SwitchDoorState();
            }
        }
    }
}
