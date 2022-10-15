namespace Abstractions
{
    public interface ICommandQueue
    {
        void AddCommandToQueue(object command);
        void Clear();
    }
}