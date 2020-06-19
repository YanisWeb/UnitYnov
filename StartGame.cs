using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    //Fonction pour quitter le jeu
    public void QuitGame(){
        // Application.Quit() ne marche que quand le jeu est build.
        Application.Quit();
    }
    //Fonction qui lance la scène contenant le jeu concret
    public void StartSceneGame(){
        //Load la scene
        SceneManager.LoadScene("SampleScene");
    }
    public void StartOptions(){
        SceneManager.LoadScene("OptionsScene");
    }
    public void StartInfos(){
        SceneManager.LoadScene("Infos");
    }
    public void ReturnMenu(){
        SceneManager.LoadScene("MenuDemarage");
    }
}
