using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIController : MonoBehaviour
{
    public static UIController uiController;
    public List<GameObject> screens;
    public List<TextMeshProUGUI> texts;
    public List<Button> defaultButtons;
    public List<UIObject> playObjects, swordObjects, shieldObjects, controlObjects;
    public List<Slider> sliders;
    public AudioMixer audioMixer;
    public List<AudioClip> audioClips;
    public AudioSource sliderAudioSource;

    int selectedLevel;

    void Awake()
    {
        if (uiController != null && uiController != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            uiController = this;
            DontDestroyOnLoad(this);

        }
    }
    private void Start()
    {
        MusicSlider(PlayerPrefs.GetFloat("musicLevel"));
        SFXSlider(PlayerPrefs.GetFloat("sfxLevel"));
    }

    public void ClickStageSelectionObject(GameObject button)
    {
        foreach (UIObject playObject in playObjects)
        {
            playObject.Select(false);
        }
        UIObject ui = button.GetComponent<UIObject>();
        if (ui.isUnlocked)
        {
            ui.Select(true);
            selectedLevel = ui.objectNumber;
        }
        else
        {
            selectedLevel = 0;
        }

        Debug.Log("Level Selected: "+ selectedLevel);
    }
    public void ClickStoreSwordObject(GameObject button)
    {
        UIObject ui = button.GetComponent<UIObject>();
        if (ui.isUnlocked)
        {
            foreach (UIObject swordObject in swordObjects)
            {
                swordObject.Select(false);
            }
            ui.Select(true);
            GameManager.selectedSword = ui.objectNumber;
        }


    }
    public void ClickStoreShieldObject(GameObject button)
    {
        UIObject ui = button.GetComponent<UIObject>();
        if (ui.isUnlocked)
        {
            foreach (UIObject shieldObject in shieldObjects)
            {
                shieldObject.Select(false);
            }
            ui.Select(true);
            GameManager.selectedShield = ui.objectNumber;
        }

    }

    public void ClickControllerObject(GameObject button)
    {
        UIObject ui = button.GetComponent<UIObject>();
        foreach (UIObject control in controlObjects)
        {
            control.Select(false);
        }
        if (ui.objectNumber == 0)
        {
            GameManager.controller = 0;
            controlObjects[0].Select(true);
            PlayerPrefs.SetInt("controller", 0);
        }
        else if (ui.objectNumber == 1)
        {
            GameManager.controller = 1;
            controlObjects[1].Select(true);
            PlayerPrefs.SetInt("controller", 1);
        }
        else if (ui.objectNumber == 2)
        {
            GameManager.controller = 2;
            controlObjects[2].Select(true);
            PlayerPrefs.SetInt("controller", 2);
        }
    }


    public void ActivateScreen(string screenName)
    {
        foreach (GameObject screen in screens){
            screen.SetActive(false);
        }
        switch (screenName)
        {
            case "Title":
                defaultButtons[0].Select();
                screens[0].SetActive(true);
                break;
            case "Play":
                defaultButtons[1].Select();
                screens[1].SetActive(true);
                texts[0].text = "Exp: " + GameManager.exp;
                for (int i = 0; i < playObjects.Count; i++)
                {
                    if (i+1 <= GameManager.unlockedLevels)
                    {
                        playObjects[i].Unlock();
                    }
                }
                break;
            case "Store":
                defaultButtons[2].Select();
                screens[2].SetActive(true);
                texts[1].text = "Exp: " + GameManager.exp;
                for (int i = 0; i < swordObjects.Count; i++)
                {
                    Debug.Log("i+1 = " + (i + 1) + " Unlock Swords = " + GameManager.unlockedSwords);
                    if (i + 1 <= GameManager.unlockedSwords)
                    {
                        swordObjects[i].Unlock();
                        if ( i + 1 == GameManager.selectedSword)
                        {
                            swordObjects[i].Select(true);
                        }
                    }
                }
                for (int i = 0; i < shieldObjects.Count; i++)
                {
                    if (i + 1 <= GameManager.unlockedShields)
                    {
                        shieldObjects[i].Unlock();
                        if (i + 1 == GameManager.selectedShield)
                        {
                            shieldObjects[i].Select(true);
                        }
                    }
                }
                break;
            case "Settings":
                defaultButtons[3].Select();
                screens[3].SetActive(true);
                float value = PlayerPrefs.GetFloat("musicLevel");
                if (value == 0) value = 1;
                sliders[0].value = value;
                value = PlayerPrefs.GetFloat("sfxLevel");
                if (value == 0) value = 1;
                sliders[1].value = value;
                foreach (UIObject control in controlObjects)
                {
                    control.Select(false);
                    Debug.Log("Changing in");
                    control.Unlock();
                }

                controlObjects[PlayerPrefs.GetInt("controller")].Select(true);
                break;
            case "Credits":
                defaultButtons[4].Select();
                screens[4].SetActive(true);
                break;
        }
    }

    public void MusicSlider(float sliderValue)
    {
        if (Mathf.Ceil(sliderValue) <= 0) sliderValue = 1;
        audioMixer.SetFloat("musicVolume", Mathf.Log10(sliderValue) * 20 );
        PlayerPrefs.SetFloat("musicLevel", sliderValue);
    }    
    public void SFXSlider(float sliderValue)
    {
        if (Mathf.Ceil(sliderValue) <= 0) sliderValue = 1;
        audioMixer.SetFloat("sfxVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("sfxLevel", sliderValue);
        if(!sliderAudioSource.isPlaying) sliderAudioSource.PlayOneShot( audioClips[Random.Range(0,audioClips.Count)] );
    }



}
