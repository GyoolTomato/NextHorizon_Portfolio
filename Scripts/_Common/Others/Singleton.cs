using UnityEngine;

public class Singleton<T> where T : class, new()
{
    //
    private static T staticInstance;


    /// <summary>
    /// 
    /// </summary>
    protected Singleton() { }

    /// <summary>
    /// 
    /// </summary>
    public static T Instance
    {
        get
        {
            if (staticInstance == null)
            {
                staticInstance = new T();
            }
            return staticInstance;
        }
    }
}
