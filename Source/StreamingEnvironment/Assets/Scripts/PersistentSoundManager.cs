using UnityEngine;

public class PersistentSoundManager : MonoBehaviour
{
    private static PersistentSoundManager instance;


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
