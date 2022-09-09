using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameStarted;
    public GameObject platform_spawner;

    [Header("GameOver")]
    public GameObject gameOverPanel;
    public GameObject highScoreImage;
    public Text lastScoreText;


    [Header("Score")]
    public Text scoreText;
    public Text bestText;
    public Text diamondText;
    public Text starText;

    int score = 0;
    int bestScore, totalDiamond, totalStar;
    bool countScore;

    [Header("for Player")]
    public GameObject[] player;
    Vector3 playerStartPos = new Vector3(0,2,0);
    int selectedCar = 0;

    [Header("for Music")]
    public AudioSource aSource;
    public AudioClip bgMusic;

    private void Awake(){
        if(instance == null){
            instance = this;
        }
         //get selected car
        selectedCar = PlayerPrefs.GetInt("SelectCar");
        Instantiate(player[selectedCar],playerStartPos,Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        aSource.clip = bgMusic;
        aSource.Play();

         //total diamond
        totalDiamond = PlayerPrefs.GetInt("totalDiamond");
        diamondText.text = totalDiamond.ToString();
        
        //total star
        totalStar = PlayerPrefs.GetInt("totalStar");
        starText.text = totalStar.ToString();

        //best score
        bestScore = PlayerPrefs.GetInt("bestScore");
        bestText.text = bestScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameStarted){
            if(Input.GetMouseButtonDown(0)){
                GameStart();
            }
        }
    }

    public void GameStart(){
        isGameStarted = true;
        countScore = true;
        StartCoroutine(UpdateScore());
        platform_spawner.SetActive(true);
    }

    public void GameOver(){
        aSource.Stop();
        lastScoreText.text = score.ToString();
        gameOverPanel.SetActive(true);
        countScore = false;
        platform_spawner.SetActive(false);
        if(score>bestScore){
            PlayerPrefs.SetInt("bestScore",score);
            highScoreImage.SetActive(true);
        }
    }

    IEnumerator UpdateScore(){
        while(countScore){
            yield return new WaitForSeconds(1f);
            score++;
            if(score>bestScore){
                 bestText.text = score.ToString();
                 scoreText.text = "Score: "+score.ToString();
            }
            else{
                scoreText.text = "Score: "+score.ToString();
            }
        }
    }

    public void replayGame(){
        SceneManager.LoadScene("Label");
    }

    public void homeGame(){
        SceneManager.LoadScene("ChooseCar");
    }

    public void GetStar(){
        SoundManager.sm.StarSound();
        int newStar = totalStar++;
        PlayerPrefs.SetInt("totalStar",newStar);
        starText.text = totalStar.ToString();
    }

    public void GetDiamond(){
         SoundManager.sm.DiamondSound();
        int newDiamond = totalDiamond++;
        PlayerPrefs.SetInt("totalDiamond",newDiamond);
        diamondText.text = totalDiamond.ToString();
    }
}
