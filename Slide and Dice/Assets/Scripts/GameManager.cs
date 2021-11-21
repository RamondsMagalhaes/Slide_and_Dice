using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public static int exp;
    public static int unlockedLevels, unlockedSwords, unlockedShields;
    public static int selectedSword, selectedShield;
    public static int controller;
    void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            gameManager = this;
            DontDestroyOnLoad(this);
            exp = PlayerPrefs.GetInt("exp");
            unlockedLevels = PlayerPrefs.GetInt("unlockedLevels");
            if (unlockedLevels == 0) unlockedLevels = 10;
            Debug.Log("unlockedLevels: " + unlockedLevels);

            unlockedSwords = PlayerPrefs.GetInt("unlockedSwords");
            if (unlockedSwords == 0) unlockedSwords = 3;
            Debug.Log("unlockedSwords: " + unlockedSwords);

            unlockedShields = PlayerPrefs.GetInt("unlockedShields");
            if (unlockedShields == 0) unlockedShields = 3;
            Debug.Log("unlockedShields: " + unlockedShields);

            selectedSword = PlayerPrefs.GetInt("selectedSword");
            if (selectedSword == 0) selectedSword = 3;
            Debug.Log("selectedSword: " + selectedSword);

            selectedShield = PlayerPrefs.GetInt("selectedShield");
            if (selectedShield == 0) selectedShield = 3;
            Debug.Log("selectedShield: " + selectedShield);

            controller = 0;//PlayerPrefs.GetInt("controller");

            Debug.Log("Current Gamepad: "+Gamepad.all.Count);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
