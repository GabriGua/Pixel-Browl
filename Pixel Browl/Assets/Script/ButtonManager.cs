using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public void MoveToScene(int sceneId)
    {
        Debug.Log("Loading");
        SceneManager.LoadScene(sceneId);
    }
}
