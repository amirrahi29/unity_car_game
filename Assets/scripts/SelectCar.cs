using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectCar : MonoBehaviour
{
    public static SelectCar instance;
    [SerializeField] Button PrevBtn;
    [SerializeField] Button NextBtn;
    [SerializeField] Button UseBtn;
    [SerializeField] GameObject buyPanel;


    int currentCar;
    string ownCarIndex;
    Color redColor = new Color(1f, 0.1f, 0.1f, 1f);
    Color greenColor = new Color(0.5f, 1f, 0.4f, 1f);
    int haveStars, haveDiamonds;
    int carValue = 700;

    [Header("Buy Panel")]
    public Text haveStarText,haveDiamondText,needMoreText;
    public Button buyCarBtn;
    public Button closePanelBtn;

    private void Awake(){
        if(instance == null){
            instance = this;
        }
        ChangeCar(0);
    }//Awake

    private void Start(){
        haveStars = PlayerPrefs.GetInt("totalStar");
        haveDiamonds = PlayerPrefs.GetInt("totalDiamond");
    }

    void chooseCar(int _index){
        PrevBtn.interactable = (_index != 0);
        NextBtn.interactable = (_index != transform.childCount - 1);
        for(int i = 0; i<transform.childCount; i++){
            string carNo = "CarNo"+i;
            if(i == 0){
                PlayerPrefs.SetInt(carNo,1);
            }
            transform.GetChild(i).gameObject.SetActive(i == _index);
        }
    }

    public void ChangeCar(int _change){
        currentCar += _change;

        chooseCar(currentCar);
        ownCarIndex = "CarNo" + currentCar;
        if(PlayerPrefs.GetInt(ownCarIndex) == 1){
            UseBtn.GetComponent<Image>().color = greenColor;
            UseBtn.GetComponentInChildren<Text>().text = "SELECT";
        }
        else{
            UseBtn.GetComponent<Image>().color = redColor;
            UseBtn.GetComponentInChildren<Text>().text = "BUY";
        }
    }

    public void useBtnClick(){
        if(PlayerPrefs.GetInt(ownCarIndex) == 1){
            PlayerPrefs.SetInt("SelectCar", currentCar);
            SceneManager.LoadScene("Label");
        }
        else{
            buyPanel.SetActive(true);

            haveStarText.text = "You have " + haveStars+" stars";
            haveDiamondText.text = "You have " + haveDiamonds+" diamonds";
            if(haveStars < carValue){
                int needStarInt = carValue - haveStars;
                buyCarBtn.interactable = false;
                needMoreText.text = needStarInt+" more star needed!";
            }
            PrevBtn.interactable = false;
            NextBtn.interactable = false;
            UseBtn.interactable = false;
        }
    }

    public void buyStars(){

    }

    public void closePanel(){
        buyPanel.SetActive(false);
        PrevBtn.interactable = true;
        NextBtn.interactable = true;
        UseBtn.interactable = true;
    }

    public void EarnStar(){
        haveStars += 100;
        PlayerPrefs.SetInt("totalStar", haveStars);
        PlayerPrefs.SetInt("totalDiamond", haveDiamonds);
         useBtnClick();

    }

    public void BuyStar(){
        haveDiamonds -= 1;
        haveStars += 10;
        PlayerPrefs.SetInt("totalStar", haveStars);
        PlayerPrefs.SetInt("totalDiamond", haveDiamonds);
         useBtnClick();

    }

    public void BuyThisCar(){
        PlayerPrefs.SetInt(ownCarIndex, 1);
        haveStars += -carValue;
        PlayerPrefs.SetInt("totalStar", haveStars);
        int currentMinone = currentCar - 1;
        ChangeCar(currentMinone);
        closePanel();

    }
}
