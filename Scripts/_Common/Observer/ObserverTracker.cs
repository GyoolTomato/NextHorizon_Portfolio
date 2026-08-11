using UnityEngine;
using System.Collections.Generic;

namespace Observer
{
    public class ObserverTracker<T> : Singleton<ObserverTracker<T>> where T : struct, IObserverEvent
    {
        //
        readonly List<IObserver<T>> _list = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="observer"></param>
        public void Subscribe(IObserver<T> observer)
        {
            //
            if (observer == null || _list.Contains(observer))
            {
                return;
            }

            //
            _list.Add(observer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="observer"></param>
        public void Unsubscribe(IObserver<T> observer)
        {
            //
            if (observer == null)
            {
                return;
            }

            //
            _list.Remove(observer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void Broadcast(T msg)
        {
            //
            IObserver<T>[] snapshot = _list.ToArray();

            //
            foreach (var item in snapshot)
            {
                try
                {
                    item.OnEvent(msg);
                }
                catch
                {
                    Debug.LogError("Observer Broadcast Error : " + item.GetType().Name);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            _list.Clear();
        }
    }
}
