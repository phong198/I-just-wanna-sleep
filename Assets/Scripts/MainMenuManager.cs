using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PressShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void PressGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
