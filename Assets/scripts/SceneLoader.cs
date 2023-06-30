using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    public void LoadSceneByName(string sceneName){
        SceneManager.LoadScene(sceneName);
    }
    public void LoadSceneByIndex(int sceneIndex){
        SceneManager.LoadScene(sceneIndex);
    }
}
