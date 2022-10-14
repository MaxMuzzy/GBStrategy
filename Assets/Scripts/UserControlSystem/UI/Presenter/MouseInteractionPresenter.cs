using System.Linq;
using Abstractions;
using UnityEngine;
using UnityEngine.EventSystems;
using UserControlSystem;
using Zenject;
using UniRx;

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

    [Inject]
    private void Init()
    {
        _groundPlane = new Plane(_groundTransform.up, 0);

        var framesArentBlockedByUIStream = Observable.EveryUpdate().Where(_ => !_eventSystem.IsPointerOverGameObject());

        var leftClickStream = framesArentBlockedByUIStream.Where(_ => Input.GetMouseButtonDown(0));
        var rightClickStream = framesArentBlockedByUIStream.Where(_ => Input.GetMouseButtonDown(1));

        var leftRaysStream = leftClickStream.Select(_ => _camera.ScreenPointToRay(Input.mousePosition));
        var rightRaysStream = rightClickStream.Select(_ => _camera.ScreenPointToRay(Input.mousePosition));

        var leftHitsStream = leftRaysStream.Select(ray => Physics.RaycastAll(ray));
        var rightHitsStream = rightRaysStream.Select(ray => (ray, Physics.RaycastAll(ray)));

        leftHitsStream.Subscribe(hits =>
        {
            if (ItHit<ISelectable>(hits, out var selectable))
            {
                _previousGameObject?.EnableOutline(false);
                _selectedObject.SetValue(selectable);
                _previousGameObject = selectable;
                selectable?.EnableOutline(true);
            }
        });

        rightHitsStream.Subscribe((ray, hits) =>
        {
            if (ItHit<IAttackable>(hits, out var attackable))
            {
                _attackablesRMB.SetValue(attackable);
            }
            else if (_groundPlane.Raycast(ray, out var enter))
            {
                _groundClicksRMB.SetValue(ray.origin + ray.direction * enter);
            }
        });
    }

    private bool ItHit<T>(RaycastHit[] hits, out T result) where T : class
    {
        result = default;
        if (hits.Length == 0)
        {
            return false;
        }
        result = hits
            .Select(hit => hit.collider.GetComponentInParent<T>())
            .FirstOrDefault(c => c != null);
        return result != default;
    }
}