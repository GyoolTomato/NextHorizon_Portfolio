using Observer;
using System;
using System.Collections.Generic;
using System.Text;

public class Manager_Observer : Singleton<Manager_Observer>
{
    List<IObserverTracker> _observerTrackers = new List<IObserverTracker>();


    /// <summary>
    /// 
    /// </summary>
    /// <param name="observerTracker"></param>
    public void AddTracker(IObserverTracker observerTracker)
    {
        if (_observerTrackers.Contains(observerTracker) == false)
            _observerTrackers.Add(observerTracker);
    }

    /// <summary>
    /// 
    /// </summary>
    public void ClearAll()
    {
        foreach (var item in _observerTrackers)
        {
            item.Clear();
        }
    }
}
