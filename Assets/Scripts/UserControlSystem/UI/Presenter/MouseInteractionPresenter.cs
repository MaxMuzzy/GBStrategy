using System.Linq;
using Abstractions;
using UnityEngine;
using UnityEngine.EventSystems;
using UserControlSystem;

public sealed class MouseInteractionPresenter : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private SelectableValue _selectedObject;
    [SerializeField] private EventSystem _eventSystem;

    private ISelectable _previousGameObject;

    [SerializeField] private Vector3Value _groundClicksRMB;
    [SerializeField] private Transform _groundTransform;

    private Plane _groundPlane;

    [SerializeField] private AttackableValue _attackablesRMB;

    private void Start() => _groundPlane = new Plane(_groundTransform.up, 0);

    private void Update()
    {
        if (!Input.GetMouseButtonUp(0) && !Input.GetMouseButton(1))
        {
            return;
        }
        if (_eventSystem.IsPointerOverGameObject())
        {
            return;
        }
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        var hits = Physics.RaycastAll(ray);
        if (Input.GetMouseButtonUp(0))
        {
            _selectedObject.SetValue(null);
            if (ItHit<ISelectable>(hits, out var selectable))
            {
                _previousGameObject?.EnableOutline(false);
                _selectedObject.SetValue(selectable);
                _previousGameObject = selectable;
                selectable?.EnableOutline(true);
            }
        }
        else
        {
            if (ItHit<IAttackable>(hits, out var attackable))
            {
                _attackablesRMB.SetValue(attackable);
            }
            else if (_groundPlane.Raycast(ray, out var enter))
            {
                _groundClicksRMB.SetValue(ray.origin + ray.direction * enter);
            }
        }

    }
    private bool ItHit<T>(RaycastHit[] hits, out T result) where T : class
    {
        result = default;
        if (hits.Length == 0)
        {
            return false;
        }
        result = hits.Select(hit => hit.collider.GetComponentInParent<T>()).Where(c => c != null).FirstOrDefault();
        return result != default;
    }
}