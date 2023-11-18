using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    
    public void SceneChange(string name)
    {
        if(name == "Menu" && ComportamientoMapa.mapasGanados == 1)
        {
            SceneManager.LoadScene("Map");
        }
        else
        {
            SceneManager.LoadScene(name);
        }
        Time.timeScale = 1; //Para que no se quede dura la escena
    }
}
