using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManage : MonoBehaviour
{
    public int nextSceneIndex;
    public int menuIndex;

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void ReturnToMEnu()
    {
        SceneManager.LoadScene(menuIndex);
    }
}
