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
    [SerializeField] private TMP_Dropdown ampm;
    [SerializeField] private TMP_Dropdown beforeOffset;
    [SerializeField] private TMP_Dropdown afterOffset;
    [SerializeField] private TMP_InputField taskInput;
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
        beforeOffset.ClearOptions();
        afterOffset.ClearOptions();
        List<string> zeroTo59 = new List<string>();
        for(int i = 0; i < 60; i++)
        {
            zeroTo59.Add(i.ToString());
            
        }
        minutes.AddOptions(zeroTo59);
        beforeOffset.AddOptions(zeroTo59);
        afterOffset.AddOptions(zeroTo59);
    }

    public void HandleInputHour(int val)
    {
        habits[habitIndex].SetNotificationTimeHour(dayIndex,int.Parse(hours.options[val].text));
    }

    public void HandleInputMinute(int val)
    {
        habits[habitIndex].SetNotificationTimeMinute(dayIndex, int.Parse(minutes.options[val].text));
    }

    public void HandleInputAmPm(int val)
    {
        habits[habitIndex].SetNotificationTimeAmPm(dayIndex, val);
    }

    public void HandleInputBeforeOffset(int val)
    {
        habits[habitIndex].SetBeforeOffset(dayIndex, val);
    }

    public void HandleInputAfterOffset(int val)
    {
        habits[habitIndex].SetAfterOffset(dayIndex, val);
    }

    public void ReadNewTask()
    {
        if(!taskInput.text.Equals("Add Tasks..."))
        {
            habits[habitIndex].GetDay(dayIndex).tasks.Add(taskInput.text);
            Debug.Log(taskInput.text);
        }
        
        
        taskInput.text = "Add Tasks...";
    }


    public void LoadHabitSchedule()
    {
        //TestCode
        habitIndex = 0;
        LoadMinutesAndSec();
        Debug.Log(habits[habitIndex].HabitName);
        Debug.Log(habits[habitIndex].PetName);
        
        for (int i = 0; i < 7; i++)
        {
            Debug.Log(habits[habitIndex].GetDay(i).isActive);
            if (habits[habitIndex].GetDay(i).isActive)
            {
                Debug.Log(habits[habitIndex].GetTime(dayIndex).minute);
                hours.value = 12 - habits[habitIndex].GetTime(dayIndex).hour;
                minutes.value =  habits[habitIndex].GetTime(dayIndex).minute;
                ampm.value = habits[habitIndex].GetTime(dayIndex).ampm;
                beforeOffset.value = habits[habitIndex].GetDay(dayIndex).beforeOffset;
                afterOffset.value = habits[habitIndex].GetDay(dayIndex).afterOffset;
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
