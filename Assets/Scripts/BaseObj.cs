using TMPro;
using UnityEngine;

public class BaseObj : MonoBehaviour
{
    private int number;
    [SerializeField] private TextMeshPro text;

    public void SetNumber(int newNumber)
    {
        text.SetText($"#{newNumber}");
        number = newNumber;
    }

    public int Number => number;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
