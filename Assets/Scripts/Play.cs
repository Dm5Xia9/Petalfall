using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public void ABOBA()
    {
        SceneManager.LoadScene(1);
    }

    public void Q()
    {
        Application.Quit();

    }
}
