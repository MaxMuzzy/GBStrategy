using Abstractions;
using UnityEngine;

public sealed class MainBuilding : MonoBehaviour, ISelectable, IVenue
{
    public float Health => _health;
    public float MaxHealth => _maxHealth;
    public Sprite Icon => _icon;
    public Transform StartPoint => _startPoint;
    public Vector3 Venue { get; set; }

    [SerializeField] private Transform _unitsParent;

    [SerializeField] private float _maxHealth = 1000;
    [SerializeField] private Sprite _icon;

    private float _health;

    private Transform _startPoint;

    public void Awake()
    {
        _health = _maxHealth;
        _startPoint = transform;
        Venue = transform.position;
    }

    public void EnableOutline(bool isEnable)
    {
        var outline = gameObject.GetComponent<Outline>();
        outline.enabled = isEnable;
    }
}