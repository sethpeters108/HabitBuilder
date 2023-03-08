using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScheduleController : MonoBehaviour
{
    [SerializeField] private List<Habit> habits;
    [SerializeField] private Button[] dayBtns;
    [SerializeField] private int dayIndex;
    [SerializeField] private int habitIndex;
    [SerializeField] private TMP_Dropdown hours;
    [SerializeField] private TMP_Dropdown minutes;
    [SerializeField] private TMP_Dropdown seconds;
    // Start is called before the first frame update
    void Start()
    {
        habits = new List<Habit>();
        habits.Add(new Habit("TestHabit", "jon"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadMinutesAndSec()
    {
        minutes.ClearOptions();
        seconds.ClearOptions();
        List<string> zeroTo59 = new List<string>();
        for(int i = 59; i >= 0; i--)
        {
            zeroTo59.Add(i.ToString());
            
        }
        minutes.AddOptions(zeroTo59);
        seconds.AddOptions(zeroTo59);
    }

    public void HandleInputHour(int val)
    {
        Debug.Log(hours.value);
        Debug.Log(hours.options[val].text);
    }

    public void LoadHabitSchedule()
    {
        //TestCode
        habitIndex = 0;
        LoadMinutesAndSec();
        Debug.Log(habits[habitIndex].HabitName);
        Debug.Log(habits[habitIndex].PetName);
        for(int i = 0; i < 7; i++)
        {
            Debug.Log(habits[habitIndex].GetDay(i).isActive);
            if (habits[habitIndex].GetDay(i).isActive)
            {
                
                ColorBlock cb = dayBtns[i].colors;
                cb.normalColor = Color.green;
                cb.highlightedColor = Color.green;
                cb.pressedColor = Color.green;
                dayBtns[i].colors = cb;
            }
            else
            {
                ColorBlock cb = dayBtns[i].colors;
                cb.normalColor = Color.red;
                cb.highlightedColor = Color.red;
                cb.pressedColor = Color.red;
                dayBtns[i].colors = cb;
            }
        }
    }

    public void SetDayActive(bool isActive)
    {
        
        if(dayIndex >= 0 && dayIndex < 7)
        {
            habits[habitIndex].SetActiveDay(dayIndex, isActive);
            UpdateDayButtons();
        }
    }


    public void UpdateDayButtons()
    {
        for (int i = 0; i < 7; i++)
        {
            Debug.Log(habits[habitIndex].GetDay(i).isActive);
            if (habits[habitIndex].GetDay(i).isActive)
            {

                ColorBlock cb = dayBtns[i].colors;
                cb.normalColor = Color.green;
                cb.highlightedColor = Color.green;
                cb.pressedColor = Color.green;
                dayBtns[i].colors = cb;
            }
            else
            {
                ColorBlock cb = dayBtns[i].colors;
                cb.normalColor = Color.red;
                cb.highlightedColor = Color.red;
                cb.pressedColor = Color.red;
                dayBtns[i].colors = cb;
            }
        }
    }

    public void SetDayIndex(int dayIndex)
    {
        this.dayIndex = dayIndex;
    }

    public void SetHabitIndex(int habitIndex)
    {
        this.habitIndex = habitIndex;
    }

}
