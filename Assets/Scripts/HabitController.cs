using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class HabitController : MonoBehaviour
{
    [SerializeField] private List<Habit> habits;
    [SerializeField] private Button[] dayBtns;
    [SerializeField] private GameObject selectedIndicator;
    [SerializeField] private int dayIndex;
    [SerializeField] private int habitIndex;
    [SerializeField] private TMP_Dropdown hours;
    [SerializeField] private TMP_Dropdown minutes;
    [SerializeField] private TMP_Dropdown ampm;
    [SerializeField] private TMP_Dropdown beforeOffset;
    [SerializeField] private TMP_Dropdown afterOffset;
    [SerializeField] private TMP_InputField taskInput;
    [SerializeField] private GameObject tasksDisplayContent;
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private TMP_InputField habitNameInput;
    [SerializeField] private TMP_InputField petNameInput;
    [SerializeField] private GameObject petListItemPrefab;
    [SerializeField] private GameObject petListItemAdd;
    [SerializeField] private GameObject petListScrollArea;
    [SerializeField] private GameObject contentObject;
    private List<string> temp = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        habits = new List<Habit>();
        habits.Add(new Habit("TestHabit", "jon"));
    }

    // Update is called once per frame
    void Update()
    {
        //dayBtns[dayIndex].Select();
        selectedIndicator.transform.position = dayBtns[dayIndex].transform.position;
        Debug.Log(selectedIndicator.transform.position);
    }

    private void LoadMinutesAndSec()
    {
        minutes.ClearOptions();
        beforeOffset.ClearOptions();
        afterOffset.ClearOptions();
        List<string> zeroTo59 = new List<string>();
        for(int i = 0; i < 60; i++)
        {
            zeroTo59.Add(i.ToString("D2"));
            
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
            
            if (habits[habitIndex].GetDay(dayIndex).isActive)
            {
                habits[habitIndex].GetDay(dayIndex).tasks.Add(taskInput.text);
            }
            else
            {
                temp.Add(taskInput.text);
            }



        }

        GameObject text = Instantiate(textPrefab, tasksDisplayContent.transform);
        text.GetComponent<TMP_Text>().text = taskInput.text;
        taskInput.text = "Add Tasks...";
    }

    private void LoadTasks()
    {
        temp.Clear();
        foreach(Transform child in tasksDisplayContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        if (habits[habitIndex].GetDay(dayIndex).isActive)
        {
            List<string> tasks = habits[habitIndex].GetDay(dayIndex).tasks;
            foreach (string task in tasks)
            {
                GameObject text = Instantiate(textPrefab, tasksDisplayContent.transform);
                text.GetComponent<TMP_Text>().text = task;
            }
        }

    }


    public void LoadHabitSchedule()
    {
        //TestCode
        habitIndex = 0;
        LoadMinutesAndSec();
        Debug.Log(habits[habitIndex].HabitName);
        Debug.Log(habits[habitIndex].PetName);

        //Set up colors 
        for (int i = 0; i < 7; i++)
        {
            Debug.Log(habits[habitIndex].GetDay(i).isActive);
            if (habits[habitIndex].GetDay(i).isActive)
            {
                ColorBlock cb = dayBtns[dayIndex].colors;
                cb.normalColor = Color.green;
                cb.highlightedColor = Color.green;
                cb.pressedColor = Color.green;
                cb.selectedColor = Color.green;
                dayBtns[i].colors = cb;
                
            }
            else
            {
                ColorBlock cb = dayBtns[dayIndex].colors;
                cb.normalColor = Color.red;
                cb.highlightedColor = Color.red;
                cb.pressedColor = Color.red;
                cb.selectedColor = Color.red;
                dayBtns[i].colors = cb;
            }
        }

        //Load data for current day of habit
        if (habits[habitIndex].GetDay(dayIndex).isActive)
        {
            LoadTasks();
            Debug.Log("WAA");
            hours.value = 12 - habits[habitIndex].GetTime(dayIndex).hour;
            minutes.value = habits[habitIndex].GetTime(dayIndex).minute;
            ampm.value = habits[habitIndex].GetTime(dayIndex).ampm;
            beforeOffset.value = habits[habitIndex].GetDay(dayIndex).beforeOffset;
            afterOffset.value = habits[habitIndex].GetDay(dayIndex).afterOffset;


        }
        else
        {
            ampm.value = 0;
            hours.value = 0;
        }

        OnSelect(dayIndex);
    }

    public void OnSelect(int dayIndex)
    {
        selectedIndicator.transform.position = dayBtns[dayIndex].transform.position;
        Debug.Log(selectedIndicator.transform.position);
        LoadTasks();
    }

    public void ToggleDayActive()
    {
        
        if(dayIndex >= 0 && dayIndex < 7)
        {
            habits[habitIndex].SetActiveDay(dayIndex, !habits[habitIndex].GetDay(dayIndex).isActive);
            foreach(string task in temp)
            {
                habits[habitIndex].GetDay(dayIndex).tasks.Add(task);
            }
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
        LoadHabitSchedule();
    }

    public void SetHabitIndex(int habitIndex)
    {
        this.habitIndex = habitIndex;
    }

    public void MakeNewHabit()
    {
        string habitName = habitNameInput.GetComponent<TMP_InputField>().text;
        string petName = petNameInput.GetComponent<TMP_InputField>().text;

        habits.Add(new Habit(habitName, petName));

        GameObject newHabit = Instantiate(petListItemPrefab, petListScrollArea.transform);
        newHabit.transform.parent = contentObject.transform;
        PetListItem PetListItemObject = newHabit.GetComponent<PetListItem>();
        PetListItemObject.setHabitName(habitName);

        petListItemAdd.transform.SetAsLastSibling();
    }

}
