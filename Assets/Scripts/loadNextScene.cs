using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadNextScene : MonoBehaviour
{

     bool loadScene = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        loadScene = true;

    }

    private void Update()
    {
        if(loadScene == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
