using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class MenuNavigator : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void HandlePlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Nico");
    }

    public void HandleQuitButton()
    {
        Application.Quit();
    }
}
