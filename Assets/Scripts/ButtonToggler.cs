using UnityEngine;
using TMPro;

public class ButtonToggler : MonoBehaviour
{
    // An array of strings that contains the text for on and off
    [SerializeField] string[] texts = new string[2];
    private TextMeshProUGUI m_TextMeshPro;
    private int state;

    /// <summary>
    /// Gets the TMP component
    /// </summary>
    private void Awake()
    {
        m_TextMeshPro = GetComponentInChildren<TextMeshProUGUI>();
    }

    /// <summary>
    /// Updates the text on start
    /// </summary>
    void Start()
    {
        UpdateText(); 
    }

    /// <summary>
    /// Toggles the states when the button is pressed
    /// </summary>
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

    /// <summary>
    /// Sets the text to the current state.
    /// </summary>
    private void UpdateText()
    {
        m_TextMeshPro.SetText(texts[state]);
    }
}
