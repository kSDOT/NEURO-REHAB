using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject rehabMenu;
    [SerializeField] GameObject funMenu;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;

    [SerializeField] GameObject humImg;
    [SerializeField] GameObject bubbleImg;

    [SerializeField] Slider speedSlide;
    [SerializeField] Slider spawnSlide;
    [SerializeField] TMP_Text speedTxt;
    [SerializeField] TMP_Text spawnTxt;

    public GameObject[] human;

    [SerializeField] Toggle rAToggle;
    [SerializeField] Toggle lAToggle;
    [SerializeField] Toggle tToggle;
    [SerializeField] Toggle rLToggle;
    [SerializeField] Toggle lLToggle;

    public static bool isRA;
    public static bool isLA;
    public static bool isT;
    public static bool isRL;
    public static bool isLL;

    public static float speed = 2.0f;// Manuel
    public static float spawnInterval = 1f;// Manuel
    public static Dictionary<Obstacle.BodyPart, bool> parts = new Dictionary<Obstacle.BodyPart, bool>();

    void Awake()
    {
        speedSlide.value = speed;
        spawnSlide.value = spawnInterval;
        bubbleImg.setActive(false);
        humImg.setActive(true);
    }

    void Update()
    {
        speed = Math.Round(speedSlide.value,2);
        spawnInterval = Math.Round(spawnSlide.value,2);
        speedTxt.text = "" + Math.Round(speed,2);
        spawnTxt.text = "" + Math.Round(spawnInterval,2);
    }

    public void Start()
    {
        foreach (Obstacle.BodyPart value in Enum.GetValues(typeof(Obstacle.BodyPart)))
        {
            parts[value] = true;
        }
    }
    void FixedUpdate()
    {
        parts[Obstacle.BodyPart.RightArm] = rAToggle.isOn;
        parts[Obstacle.BodyPart.LeftArm] = lAToggle.isOn;
        parts[Obstacle.BodyPart.Torso] = tToggle.isOn;
        parts[Obstacle.BodyPart.RightLeg] = rLToggle.isOn;
        parts[Obstacle.BodyPart.LeftLeg] = lLToggle.isOn;

        isRA = rAToggle.isOn;
        isLA = lAToggle.isOn;
        isT = tToggle.isOn;
        isRL = rLToggle.isOn;
        isLL = lLToggle.isOn;

        foreach (GameObject hum in human){
                hum.GetComponent<Image>().color = new Color32(239,239,239,255);
            }

        if(isRA){
            human[2].GetComponent<Image>().color = new Color32(255,0,0,255);
        }
        if(isLA){
            human[3].GetComponent<Image>().color = new Color32(0,255,0,255);
        }
        if(isT){
            human[1].GetComponent<Image>().color = new Color32(255,255,0,255);
        }
        if(isLL){
            human[5].GetComponent<Image>().color = new Color32(255,0,255,255);
        }
        if(isRL){
            human[4].GetComponent<Image>().color = new Color32(0,0,255,255);
        }
    }
   public void PlayGame() {

        SceneManager.LoadScene("NeuroRehabGame");
    }
    public void Rehab(){
        rehabMenu.SetActive(true);
        mainMenu.SetActive(false);
        funMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }
    public void Fun(){
        rehabMenu.SetActive(false);
        mainMenu.SetActive(false);
        funMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
    public void Back(){
        rehabMenu.SetActive(false);
        mainMenu.SetActive(true);
        funMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }
    public void Settings(){
        rehabMenu.SetActive(false);
        mainMenu.SetActive(false);
        funMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void Toggle(){

    }
}

