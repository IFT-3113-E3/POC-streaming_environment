using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Transition : MonoBehaviour
{
    public string sceneToLoad;
    private string currentScene;
    private bool canActivateScene = false;
    private bool cancel = false;

    private const float SCENE_LOAD_PROGRESS_THRESHOLD = 0.9f;

    internal void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        StartCoroutine(HandleTransition());
    }

    internal void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[{currentScene}] Player entered trigger");
            canActivateScene = true;
            CancelOtherTransitions();
        }
    }

    private void CancelOtherTransitions()
    {
        var transitions = FindObjectsByType<Transition>(FindObjectsSortMode.None);
        foreach (var transition in transitions)
        {
            if (transition != this)
            {
                transition.CancelTransition();
            }
        }
    }

    public void CancelTransition()
    {
        Debug.Log($"[{currentScene}] Transition cancelled by another transition: {gameObject.name}");
        cancel = true;
    }

    private IEnumerator HandleTransition()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncOperation.allowSceneActivation = false;

        yield return new WaitUntil(() => asyncOperation.progress >= SCENE_LOAD_PROGRESS_THRESHOLD || cancel);

        if (cancel)
        {
            yield return CancelAndUnloadScene(asyncOperation);
            yield break;
        }

        Debug.Log($"[{currentScene}] Scene loaded, waiting for player to enter trigger");
        yield return new WaitUntil(() => canActivateScene || cancel);

        if (cancel)
        {
            yield return CancelAndUnloadScene(asyncOperation);
            yield break;
        }

        Debug.Log($"[{currentScene}] Player entered trigger, activating scene");
        asyncOperation.allowSceneActivation = true;
        yield return new WaitUntil(() => asyncOperation.isDone);

        Debug.Log($"[{currentScene}] Scene loaded and activated");
        SceneManager.UnloadSceneAsync(currentScene);
        Debug.Log($"[{currentScene}] Old scene unloaded");
    }

    private IEnumerator CancelAndUnloadScene(AsyncOperation asyncOperation)
    {
        Debug.Log($"[{currentScene}] Transition cancelled");
        asyncOperation.allowSceneActivation = true;
        yield return new WaitUntil(() => asyncOperation.isDone);
        SceneManager.UnloadSceneAsync(sceneToLoad);
    }
}
