using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonToggler : MonoBehaviour
{
    [SerializeField] string[] texts = new string[2];
    private TextMeshProUGUI m_TextMeshPro;
    private int state;

    private void Awake()
    {
        m_TextMeshPro = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateText(); 
    }

    public void Toggle()
    {
        if (state == 0)
        {
            state = 1;
        }
        else
        {
            state = 0;
        }
        UpdateText();
    }

    private void UpdateText()
    {
        m_TextMeshPro.SetText(texts[state]);
    }
}
