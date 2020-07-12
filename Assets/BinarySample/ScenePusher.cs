using UnityEngine;

public class ScenePusher : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private float _force;

    private Ray ClickRay => 
        _camera.ScreenPointToRay(Input.mousePosition);

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryHit(ClickRay);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            TryRestore(ClickRay);
        }
    }

    private void TryHit(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, 
            1))
        {
            var interactable = hit.transform.gameObject.
                GetComponent<InteractableObject>();
            if (interactable == null)
            {
                return;
            }
            var direction = hit.point - _camera.transform.position;
            direction += Random.insideUnitSphere;
            interactable.Push(direction * _force);
        }
    }

    private void TryRestore(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue,
            1))
        {
            var interactable = hit.transform.gameObject.
                GetComponent<InteractableObject>();
            if (interactable == null)
            {
                return;
            }
            interactable.RestoreDefault();
        }
    }
}
