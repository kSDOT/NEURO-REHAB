using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject rehabMenu;
    [SerializeField] GameObject funMenu;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;

    public GameObject[] human;

    [SerializeField] Toggle rAToggle;
    [SerializeField] Toggle lAToggle;
    [SerializeField] Toggle tToggle;
    [SerializeField] Toggle rLToggle;
    [SerializeField] Toggle lLToggle;

    public bool isRA;
    public bool isLA;
    public bool isT;
    public bool isRL;
    public bool isLL;

    void FixedUpdate(){
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
        SceneManager.LoadScene("Scene");  
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