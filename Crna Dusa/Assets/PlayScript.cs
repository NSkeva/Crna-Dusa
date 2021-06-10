using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScript : MonoBehaviour
{
    public void ucitavanje()
    {

        SceneManager.LoadScene("SampleScene");
    }
    public void doExitGame()
    {
        Application.Quit();
    }
}
