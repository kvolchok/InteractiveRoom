using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    [SerializeField]
    private float _throwPower = 300;
    
    [SerializeField]
    private int _highlightIntensity = 4;    
    private Outline _outline;

    private Collider _collider;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void SetFocus()
    {
        _outline.OutlineWidth = _highlightIntensity;
    }
    
    public void RemoveFocus()
    {
        _outline.OutlineWidth = 0;
    }

    public void PickUp(Transform inventory)
    {
        transform.SetParent(inventory);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity; 
        
        _collider.isTrigger = true;
        _rigidbody.isKinematic = true;
    }

    public void Drop()
    {
        _collider.isTrigger = false;
        transform.SetParent(null);
        _rigidbody.isKinematic = false;
    }

    public void Throw(Vector3 direction)
    {
        transform.SetParent(null);
        _collider.isTrigger = false;
        _rigidbody.isKinematic = false;
        
        _rigidbody.AddForce(direction * _throwPower);
    }
}