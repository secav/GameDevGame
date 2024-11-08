using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private GameObject[] canvasList;

    // Start is called before the first frame update
    void Start()
    {
        Canvas[] canvases = Resources.FindObjectsOfTypeAll<Canvas>();
        canvasList = new GameObject[canvases.Length];

        for (int i = 0; i < canvases.Length; i++)
        {
            canvasList[i] = canvases[i].gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ToggleCanvas(string enabledCanvas)
    {
        foreach (GameObject canvas in canvasList)
        {
            if (canvas.name == enabledCanvas)
            {
                canvas.SetActive(true);
            }
            else
            {
                canvas.SetActive(false);
            }
        }
    }

    public void playSound(GameObject button)
    {
        GetComponent<AudioSource>().Play();
    }
    //TODO in the future, maybe
    //public void OpenSettings()
    //{

    //}

    public void CloseGame()
    {
        Application.Quit();
    }
}

