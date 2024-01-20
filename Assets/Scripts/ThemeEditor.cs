using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ThemeEditor : MonoBehaviour
{
    public GameObject editThemePanel;
    TMP_InputField themeIDInputField;
    public int themeID = 0;
    public GameObject closeButtonObj;
    Button closeButton;
    public void OpenEditThemePanel()
    {
        editThemePanel.SetActive(true);
        themeIDInputField = editThemePanel.transform.GetChild(2).GetComponent<TMP_InputField>();
        themeIDInputField.text = themeID.ToString();
        closeButton = closeButtonObj.GetComponent<Button>();
        closeButton.onClick.AddListener(CloseEditThemePanel);
        LevelEditor le = GameObject.Find("Balus").GetComponent<LevelEditor>();
        le.SetPopupOpen(true);
    }
    public void CloseEditThemePanel()
    {
        themeID = int.Parse(themeIDInputField.text);
        editThemePanel.SetActive(false);
        LevelEditor le = GameObject.Find("Balus").GetComponent<LevelEditor>();
        le.SetPopupOpen(false);
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
