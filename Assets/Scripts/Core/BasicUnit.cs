using UnityEngine;
using Abstractions;
using Core.CommandExecutors;
using Core.CommandRealizationDupes;

namespace Core
{
    public class BasicUnit : MonoBehaviour, ISelectable, IAttackable, IDamageDealer
    {
        public float Health => _health;
        public float MaxHealth => _maxHealth;
        public Sprite Icon => _icon;
        public Transform StartPoint => _startPoint;
        public int Damage => _damage;

        [SerializeField] private Animator _animator;
        [SerializeField] private StopCommandExecutor _stopCommand;
        [SerializeField] private float _maxHealth = 1000;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _damage = 25;

        private float _health;

        private Transform _startPoint;

        public void Awake()
        {
            _health = _maxHealth;
            _startPoint = transform;
        }

        public void EnableOutline(bool isEnable)
        {
            var outline = gameObject?.GetComponent<Outline>();
            outline.enabled = isEnable;
        }
        public void ReceiveDamage(int amount)
        {
            if (_health <= 0)
            {
                return;
            }
            _health -= amount;
            if (_health <= 0)
            {
                _animator.SetTrigger("PlayDead");
                Destroy(gameObject, 1f);
            }
        }

        private async void OnDestroy()
        {
            await _stopCommand.ExecuteSpecificCommand(new StopCommand());
        }
    }
}