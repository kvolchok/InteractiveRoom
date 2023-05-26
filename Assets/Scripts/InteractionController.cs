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
        
        InteractWithItems(interactableItem, ref _lastInteractableItem,
            () => interactableItem.SetFocus(), () => _lastInteractableItem.RemoveFocus());

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryOpenTheDoor();
            
            InteractWithItems(interactableItem, ref _lastInteractableItem,
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

    private void InteractWithItems(InteractableItem interactableItem, ref InteractableItem lastInteractableItem,
        Action firstAction, Action secondAction)
    {
        if (lastInteractableItem == interactableItem)
        {
            return;
        }

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