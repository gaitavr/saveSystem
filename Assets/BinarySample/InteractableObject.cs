using UnityEngine;

public struct InteractableData
{
    public Vector3 Position;
    public Quaternion Rotation;

    public InteractableData(Transform tr)
    {
        Position = tr.position;
        Rotation = tr.rotation;
    }

    public void Restore(Transform tr)
    {
        tr.position = Position;
        tr.rotation = Rotation;
    }
}

[RequireComponent(typeof(Rigidbody))]
public class InteractableObject : MonoBehaviour
{
    [SerializeField]
    private string _id;

    private Rigidbody _rigidBody;

    public string Id => _id;

    private InteractableData _initialData;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _initialData = new InteractableData(transform);
    }

    public void Push(Vector3 direction)
    {
        _rigidBody.AddForce(direction);
    }

    public void Set(InteractableData data)
    {
        data.Restore(transform);
    }

    public InteractableData Get()
    {
        return new InteractableData(transform);
    }

    public void RestoreDefault()
    {
        _initialData.Restore(transform);
    }
}
