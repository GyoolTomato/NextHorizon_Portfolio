using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Panel_Slots<T> : Panel_Base where T : Com_Base
{
    //
    [SerializeField] ScrollView    _scrollView = null;
    [SerializeField] RectTransform _container  = null;
    [SerializeField] T             _slotPrefab = null;

    //
    Stack<T> _slotPool = new Stack<T>();

    //
    public List<T> pSlots {private set; get;} = new List<T>();


    /// <summary>
    /// 
    /// </summary>
    public T ActivateSlot()
    {
        //
        T slot = default;
        if (slot != null)
        {
            Debug.LogError("T is Not found 'Com_Base'");
            return slot;
        }

        //
        T temp = null;
        if (_slotPool.Count == 0)
            temp = Instantiate(_slotPrefab, _container);
        else
            temp = _slotPool.Pop();

        temp.gameObject.SetActive(true);
        pSlots.Add(temp);

        //
        return temp;
    }

   /// <summary>
   /// 
   /// </summary>
    void DeactiveSlot(T slot)
    {
        //
        _slotPool.Push(slot);
        
        slot.gameObject.SetActive(false);

        pSlots.Remove(slot);
    }

    /// <summary>
    /// 
    /// </summary>
    public void DeactiveSlots()
    {
        //
        for (int i = pSlots.Count - 1; i >= 0; i--)
        {
            DeactiveSlot(pSlots[i]);
        }
    }
}
