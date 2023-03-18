using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PetListItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI habitText;
    [SerializeField] private int habitIndex;


    public void setHabitName(string habitName)
    {
        habitText.text = habitName;
    }

    public void setHabitIndex(int currHabitIndex)
    {
        habitIndex = currHabitIndex;
    }

    public int getHabitIndex()
    {
        return  habitIndex;
    }
}
