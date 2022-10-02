using Abstractions;
using UnityEngine;

public sealed class MainBuilding : MonoBehaviour, ISelectable
{
    public float Health => _health;
    public float MaxHealth => _maxHealth;
    public Sprite Icon => _icon;

    [SerializeField] private Transform _unitsParent;

    [SerializeField] private float _maxHealth = 1000;
    [SerializeField] private Sprite _icon;

    private float _health;

    public void Awake()
    {
        _health = _maxHealth;
    }

    public void EnableOutline(bool isEnable)
    {
        var outline = gameObject.GetComponent<Outline>();
        outline.enabled = isEnable;
    }
}