namespace MarchMessageQueue.Consumer
{
    public interface IConsume<T> where T: class
    {
        bool Consume(T message);
    }
}