using UnityEngine;

public class PersistentPlayer : MonoBehaviour
{
    private static PersistentPlayer instance;

    internal void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
