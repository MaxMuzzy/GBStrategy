using UnityEngine;
using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UniRx;
using Core;
using System.Threading.Tasks;
using Zenject;
using Core.CommandRealizationDupes;

public class ProduceUnitCommandExecutor : CommandExecutorBase<IProduceUnitCommand>, IUnitProducer
{
    public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => _queue;

    [SerializeField] private Transform _unitsParent;
    [SerializeField] private int _maximumUnitsInQueue = 6;

    private ReactiveCollection<IUnitProductionTask> _queue = new();

    [Inject] private DiContainer _diContainer;

    private void Start() => Observable.EveryUpdate().Subscribe(_ => OnUpdate());
    private void OnUpdate()
    {
        if (_queue.Count == 0)
        {
            return;
        }

        var innerTask = (UnitProductionTask)_queue[0];
        innerTask.TimeLeft -= Time.deltaTime;
        if (innerTask.TimeLeft <= 0)
        {
            RemoveTaskAtIndex(0);
            var unit = _diContainer.InstantiatePrefab(innerTask.UnitPrefab, new Vector3(Random.Range(-10, 10), 0,Random.Range(-10, 10)), Quaternion.identity, _unitsParent);
            var queue = unit.GetComponent<ICommandQueue>();
            var mainBuilding = GetComponent<MainBuilding>();
            var factionMember = unit.GetComponent<FactionMember>();
            factionMember.SetFaction(GetComponent<FactionMember>().FactionId);
            queue.AddCommandToQueue(new MoveCommand(mainBuilding.Venue));
        }
    }
    public void Cancel(int index) => RemoveTaskAtIndex(index);

    private void RemoveTaskAtIndex(int index)
    {
        for (int i = index; i < _queue.Count - 1; i++)
        {
            _queue[i] = _queue[i + 1];
        }
        _queue.RemoveAt(_queue.Count - 1);
    }
    public override async Task ExecuteSpecificCommand(IProduceUnitCommand command)
    {
        if(_queue.Count == _maximumUnitsInQueue)
        {
            return;
        }
        _queue.Add(new UnitProductionTask(command.ProductionTime, command.Icon, command.UnitPrefab, command.UnitName)); 
    }
}
