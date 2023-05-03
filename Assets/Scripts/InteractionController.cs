using System;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField]
    private Transform _inventory;

    private InteractableItem _lastInteractableItem;
    private InteractableItem _lastPickedUpItem;

    private void Update()
    {
        var interactableItem = RaycastOperations.GetSelectedObject<InteractableItem>();
        
        InteractWithItems(ref _lastInteractableItem, interactableItem,
            () => interactableItem.SetFocus(), () => _lastInteractableItem.RemoveFocus());

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryOpenTheDoor();
            
            InteractWithItems(ref _lastPickedUpItem, interactableItem,
                () => interactableItem.PickUp(_inventory), () => _lastPickedUpItem.Drop());
        }

        if (Input.GetMouseButtonDown(0) && _lastPickedUpItem != null)
        {
            ThrowItem();
        }
    }

    private void TryOpenTheDoor()
    {
        var door = RaycastOperations.GetSelectedObject<Door>();
        if (door != null)
        {
            door.SwitchDoorState();
        }
    }

    private void InteractWithItems(ref InteractableItem lastInteractableItem, InteractableItem interactableItem,
        Action firstAction, Action secondAction)
    {
        if (lastInteractableItem == interactableItem) return;

        if (lastInteractableItem != null)
        {
            secondAction();
        }

        if (interactableItem != null)
        {
            firstAction();
        }

        lastInteractableItem = interactableItem;
    }

    private void ThrowItem()
    {
        _lastPickedUpItem.Throw(transform.forward);
        _lastPickedUpItem = null;
    }
}