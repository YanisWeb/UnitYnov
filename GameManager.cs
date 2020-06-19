using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int lives;
    public int score;
    public Text livesText;
    public Text scoreText;
    public Text highScoreText;
    public InputField highScoreInput;
    public bool gameOver;
    public GameObject gameOverPanel;
    public GameObject loadLevelPanel;
    public int numberOfBricks;
    public Transform[] levels;
    public int currentLevel = 0;


    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "Lives: "+ lives;
        scoreText.text = "Score: "+ score;
        // Permet de compter le nombre de briques (afin de lancer le passage au second niveau)
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateLives(int changeInLives) {
        lives += changeInLives;
        if(lives <= 0) {
            lives = 0;
            GameOver();
        }
        livesText.text = "Lives: "+ lives;
    }
    public void UpdateScore(int points) {
        score += points;
        scoreText.text = "Score: "+ score;
    }

    public void UpdateBricks(){
        // On retire 1 à numberOfBricks après la destruction d'une brique.
        numberOfBricks--;
        // On verifie si le nombre de briques est <= 0 pour soit lancer GameOver() ou LoadLevel().
        if(numberOfBricks <= 0){
            // On regarde si currentLevel est plus grand que le nombre de niveaux disponibles.
            if(currentLevel >= levels.Length -1){
                GameOver();
            }
            else {
                // On rends visible le panneau de passage du niveau
                loadLevelPanel.SetActive(true);

                loadLevelPanel.GetComponentInChildren<Text>().text = "Level " + (currentLevel + 2);
                // Empêche toute action pendant 3s, pour afficher le message de réussite, avant de lancer LoadLevel().
                gameOver = true;
                Invoke("LoadLevel",3f);
            }
        }
    }
    void LoadLevel() {
        // On incrément currentLevel de 1 et on instancie le niveau portant l'index équivalent à currentLevel.
        currentLevel++;
        Instantiate(levels[currentLevel],Vector2.zero,Quaternion.identity);
        // On recompte le nombre de briques après l'instancation d'un nouveau niveau.
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
        gameOver = false;
        loadLevelPanel.SetActive(false);
    }
    void GameOver() {
        gameOver = true;
        // Permet d'afficher le panneau gameOverPanel
        gameOverPanel.SetActive(true);
        int highScore = PlayerPrefs.GetInt("HIGHSCORE");
        if(score > highScore){
            PlayerPrefs.SetInt("HIGHSCORE", score);
            highScoreText.text = "Nouveau record !" + "\n" + "Entrez votre pseudo ci-dessous";
            highScoreInput.gameObject.SetActive(true);
        } else {
            highScoreText.text = PlayerPrefs.GetString("HIGHSCORENAME") + ", a fait " + highScore + "\n" + "Penses-tu le battre ?";
        }
    }
    public void NewHighsScore(){
        string highScoreName = highScoreInput.text;
        PlayerPrefs.SetString("HIGHSCORENAME",highScoreName);
        highScoreInput.gameObject.SetActive(false);
        highScoreText.text = "Bien joué " + highScoreName + "\n" + "Le nouveau record est " + score;
    }
    public void PlayAgain() {
        SceneManager.LoadScene("SampleScene");
    }
    public void Quit() {
        SceneManager.LoadScene("MenuDemarage");
    }
}
