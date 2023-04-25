using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour //MonoSingleton<T>
{
    private static T m_Instance;
    public static T Instance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = GameObject.FindObjectOfType(typeof(T)) as T;

            // Object not found, we create a temporary one
            if (m_Instance == null)
            {
                Debug.LogWarning("No instance of " + typeof(T).ToString() + ", a temporary one is created.");

                m_Instance = new GameObject("Temp Instance of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();

                // Problem during the creation, this should not happen
                if (m_Instance == null)
                {
                    Debug.LogError("Problem during the creation of " + typeof(T).ToString());
                }
            }

            return m_Instance;
        }
    }

    public void Awake()
    {
        if (m_Instance == null)
            m_Instance = this as T;
    }

}
