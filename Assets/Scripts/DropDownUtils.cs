using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropDownUtils : MonoBehaviour
{
    private TMP_Dropdown m_Dropdown;
    private GameObject balus;
    private LevelEditor m_LevelEditor;
    // Start is called before the first frame update
    void Start()
    {
        m_Dropdown = GetComponent<TMP_Dropdown>();
        balus = GameObject.Find("Balus");
        m_LevelEditor = balus.GetComponent<LevelEditor>();
        m_Dropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(m_Dropdown); });
    }

    void DropdownValueChanged(TMP_Dropdown change)
    {
        m_LevelEditor.SetObjectLayer(change.value);
    }
}
