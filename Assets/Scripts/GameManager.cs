using System.Collections;
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


    // Start is called before the first frame update
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

    public void TogglePark()
    {
        foreach (var plane in planes)
        {
            plane.TogglePark();
        }
    }

    public void ToggleLights()
    {
        foreach (var plane in planes)
        {
            plane.ToggleLight();
        }
    }

    // Update is called once per frame
    void Update()
    {
        planesParkedText.enabled = AllPlanesParked;
    }
}
