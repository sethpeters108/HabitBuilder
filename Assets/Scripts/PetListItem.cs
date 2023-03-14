using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PetListItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI habitText;
    public void setHabitName(string habitName)
    {
        habitText.text = habitName;
    }
}
