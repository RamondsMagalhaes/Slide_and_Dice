using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObject : MonoBehaviour
{
    public GameObject unlocked, locked, selected;
    public bool isUnlocked;
    public int objectNumber;
    public Button thisButton;
    // Start is called before the first frame update
    void Awake()
    {
        //Debug.Log("Making it False again");
        //isUnlocked = false;
        thisButton = GetComponent<Button>();
        Debug.Log("Changing Back");
        thisButton.targetGraphic = locked.GetComponent<Image>();
    }
    public void Unlock()
    {
        isUnlocked = true;
        unlocked.SetActive(true);
        locked.SetActive(false);
        thisButton.targetGraphic = unlocked.GetComponent<Image>();

    }
    public void Select(bool select)
    {
        selected.SetActive(select);
    }
}
