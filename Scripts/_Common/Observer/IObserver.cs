using UnityEngine;

namespace Observer
{
    /// <summary>
    /// 
    /// </summary>
    public interface IObserverEvent
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IObserver<T>
        where T : struct, IObserverEvent
    {
        void OnEvent(T message);
    }
}