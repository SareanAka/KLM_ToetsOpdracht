using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Hanger[] hangars;
    [SerializeField] private Plane[] planes;
    [SerializeField] private TextMeshProUGUI planesParkedText;
    public bool AllPlanesParked => planes.All(plane => plane.IsParked);


    /// <summary>
    /// Assigns the planes and hangars a random number between 1 and 3.
    /// </summary>
    void Start()
    {
        foreach (Hanger hangar in hangars)
        {
            hangar.SetNumber(NewNumber(hangars.Length));
        }
        numbers.Clear();
        foreach (Plane plane in planes)
        {
            var num = NewNumber(planes.Length);
            plane.SetNumber(num);
            plane.SetHanger(hangars.Single(hanger => hanger.Number == num));
        }
    }

    /// <summary>
    /// Gets a new random number and checks if that number is unique.
    /// </summary>
    private List<int> numbers = new List<int>();
    public int NewNumber(int r)
    {
        
        int a = -1;

        while (a == -1)
        {
            a = Random.Range(0, r);
            if (!numbers.Contains(a))
            {
                numbers.Add(a);
            }
            else
            {
                a = -1;
            }
        }
        return a + 1;
    }

    /// <summary>
    /// Toggles the parking method for each plane
    /// </summary>
    public void TogglePark()
    {
        foreach (var plane in planes)
        {
            plane.TogglePark();
        }
    }

    /// <summary>
    /// Toggles the lights for each plane
    /// </summary>
    public void ToggleLights()
    {
        foreach (var plane in planes)
        {
            plane.ToggleLight();
        }
    }

    /// <summary>
    /// When all planes are parked the update will enable the on screen text.
    /// </summary>
    void Update()
    {
        planesParkedText.enabled = AllPlanesParked;
    }
}
