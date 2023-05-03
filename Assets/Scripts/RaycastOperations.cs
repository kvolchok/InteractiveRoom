using UnityEngine;

public static class RaycastOperations
{
    public static T GetSelectedObject<T>() where T : MonoBehaviour
    {
        var mousePosition = Input.mousePosition;
        var ray = Camera.main.ScreenPointToRay(mousePosition);

        if (!Physics.Raycast(ray, out var hitInfo)) return null;
        var selectedObject = hitInfo.collider.GetComponent<T>();
        return selectedObject;
    }
}