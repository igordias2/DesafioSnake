﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Controller : MonoBehaviour
{

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
