using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

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
    [SerializeField] private GameObject[] checkableTasksDisplayContent;
    [SerializeField] private GameObject togglePrefab;
    [SerializeField] private Slider progressBar;
    [SerializeField] private GameObject pageController;
    [SerializeField] private PetListItem currPetListItem;
    [SerializeField] private GameObject petName;
    [SerializeField] private GameObject petAge;
    private List<string> temp = new List<string>();
    

    // Start is called before the first frame update
    void Start()
    {
        habitIndex = 0;
        habits = new List<Habit>();
        habits.Add(new Habit("TestHabit", "jon"));
    }

    // Update is called once per frame
    void Update()
    {
        //dayBtns[dayIndex].Select();
        selectedIndicator.transform.position = dayBtns[dayIndex].transform.position;
        //Debug.Log(selectedIndicator.transform.position);
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
        habits[getHabitIndex()].SetNotificationTimeHour(dayIndex,int.Parse(hours.options[val].text));
    }

    public void HandleInputMinute(int val)
    {
        habits[getHabitIndex()].SetNotificationTimeMinute(dayIndex, int.Parse(minutes.options[val].text));
    }

    public void HandleInputAmPm(int val)
    {
        habits[getHabitIndex()].SetNotificationTimeAmPm(dayIndex, val);
    }

    public void HandleInputBeforeOffset(int val)
    {
        habits[getHabitIndex()].SetBeforeOffset(dayIndex, val);
    }

    public void HandleInputAfterOffset(int val)
    {
        habits[getHabitIndex()].SetAfterOffset(dayIndex, val);
    }

    public void ReadNewTask()
    {
        if(!taskInput.text.Equals("Add Tasks..."))
        {
            
            if (habits[getHabitIndex()].GetDay(dayIndex).isActive)
            {
                habits[getHabitIndex()].GetDay(dayIndex).tasks.Add(taskInput.text);
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
        if (habits[getHabitIndex()].GetDay(dayIndex).isActive)
        {
            List<string> tasks = habits[getHabitIndex()].GetDay(dayIndex).tasks;
            foreach (string task in tasks)
            {
                GameObject text = Instantiate(textPrefab, tasksDisplayContent.transform);
                text.GetComponent<TMP_Text>().text = task;
            }
        }

    }

    public void LoadCheckableTasks()
    {
        for(int i = 0; i < checkableTasksDisplayContent.Length; i++)
        {
            //Clear
            foreach (Transform child in checkableTasksDisplayContent[i].transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            //Add Tasks
            List<string> tasks = habits[getHabitIndex()].GetDay(i).tasks;
            foreach (string task in tasks)
            {

                GameObject toggle = Instantiate(togglePrefab, checkableTasksDisplayContent[i].transform);
                toggle.GetComponentInChildren<Text>().text = task;
                toggle.GetComponent<Toggle>().isOn = false;
                toggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate
                {
                    if (toggle.GetComponent<Toggle>().isOn)
                    {
                        IncreaseProgress(0.1f);
                        Destroy(toggle);
                    }
                    
                });
                
            }
        }
        
        /*
        foreach (Transform child in checkableTasksDisplayContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        if (habits[habitIndex].GetDay(dayIndex).isActive)
        {
            List<string> tasks = habits[habitIndex].GetDay(dayIndex).tasks;
            foreach (string task in tasks)
            {
                GameObject toggle = Instantiate(togglePrefab, checkableTasksDisplayContent.transform);
                toggle.GetComponentInChildren<Text>().text = task;
            }
        }
        */
    }


    public void LoadHabitSchedule()
    {
        //TestCode
        //SetHabitIndex(0);
        LoadMinutesAndSec();
        //Debug.Log(habits[getHabitIndex()].HabitName);
        //Debug.Log(habits[getHabitIndex()].PetName);
        

        //Set up colors 
        for (int i = 0; i < 7; i++)
        {
            //Debug.Log(habits[getHabitIndex()].GetDay(i).isActive);
            Debug.Log("THIS IS IN LOAD HABIT SCHEDULE " + habits[getHabitIndex()].HabitName + " {}{} " + getHabitIndex());
            if (habits[getHabitIndex()].GetDay(i).isActive)
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
        if (habits[getHabitIndex()].GetDay(dayIndex).isActive)
        {
            LoadTasks();
            hours.value = 12 - habits[getHabitIndex()].GetTime(dayIndex).hour;
            minutes.value = habits[getHabitIndex()].GetTime(dayIndex).minute;
            ampm.value = habits[getHabitIndex()].GetTime(dayIndex).ampm;
            beforeOffset.value = habits[getHabitIndex()].GetDay(dayIndex).beforeOffset;
            afterOffset.value = habits[getHabitIndex()].GetDay(dayIndex).afterOffset;


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
        //Debug.Log(selectedIndicator.transform.position);
        LoadTasks();
    }

    public void ToggleDayActive()
    {
        
        if(dayIndex >= 0 && dayIndex < 7)
        {
            habits[getHabitIndex()].SetActiveDay(dayIndex, !habits[getHabitIndex()].GetDay(dayIndex).isActive);
            foreach(string task in temp)
            {
                habits[getHabitIndex()].GetDay(dayIndex).tasks.Add(task);
            }
            UpdateDayButtons();
        }
    }

    public void UpdateDayButtons()
    {
        for (int i = 0; i < 7; i++)
        {
           // Debug.Log(habits[getHabitIndex()].GetDay(i).isActive);
            if (habits[getHabitIndex()].GetDay(i).isActive)
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

    public void IncreaseProgress(float amount)
    {
        habits[getHabitIndex()].IncreaseProgress(amount);
        UpdateProgressSlider();
        //Debug.Log(habits[getHabitIndex()].Progress);
    }

    public void UpdateProgressSlider()
    {
        progressBar.value = habits[getHabitIndex()].Progress;
    }

    public void SetDayIndex(int dayIndex)
    {
        this.dayIndex = dayIndex;
        LoadHabitSchedule();
    }

    public void SetHabitIndex(int habitIndex)
    {
        currPetListItem.GetComponent<PetListItem>().setHabitIndex(habitIndex);
        petName.GetComponent<TextMeshProUGUI>().text = "Name : " + getCurrHabit().Pet.PetName;
        petAge.GetComponent<TextMeshProUGUI>().text = "Age : " + getCurrHabit().Pet.Age;
    }
    
    public int getHabitIndex()
    {
        //Debug.Log("THE CURRENT INDEX IS::   "+currPetListItem.GetComponent<PetListItem>().getHabitIndex()); //switches back to 0 when hitting the premade one
        return currPetListItem.GetComponent<PetListItem>().getHabitIndex();
    }

    public List<Habit> Habits
    {
        get { return habits; }
        set { habits = value; }
    }
    
    public void MakeNewHabit()
    {
        string habitName = habitNameInput.GetComponent<TMP_InputField>().text;
        string petName = petNameInput.GetComponent<TMP_InputField>().text;

        int prevHabitIndex = currPetListItem.getHabitIndex();

        habits.Add(new Habit(habitName, petName));
        GameObject newHabit = Instantiate(petListItemPrefab, petListScrollArea.transform);
        newHabit.transform.parent = contentObject.transform;
        PetListItem petListItemObject = newHabit.GetComponent<PetListItem>();
        petListItemObject.setHabitName(habitName);
        petListItemObject.setHabitIndex(prevHabitIndex + 1);
        currPetListItem = petListItemObject;

        //Set Onclick for the text button
        //petListItemObject.transform.Find("HabitText").GetComponent<Button>().onClick.AddListener(delegate { SetHabitIndex(currPetListItem.getHabitIndex()); });
        petListItemObject.transform.Find("HabitText").GetComponent<Button>().onClick.AddListener(delegate { SetHabitIndex(getClickedHabit(EventSystem.current)); }); // get array pos based on the name of what you clicked on?
        petListItemObject.transform.Find("HabitText").GetComponent<Button>().onClick.AddListener(delegate { pageController.GetComponent<PageController>().ToPage("PetPage"); });
        petListItemObject.transform.Find("HabitText").GetComponent<Button>().onClick.AddListener(delegate { LoadHabitSchedule(); });

        //Set OnClick for the calendar button
        petListItemObject.transform.Find("CalendarButton").GetComponent<Button>().onClick.AddListener(delegate { SetHabitIndex(getClickedHabitFromCalendarButton(EventSystem.current)); });
        petListItemObject.transform.Find("CalendarButton").GetComponent<Button>().onClick.AddListener(delegate { LoadHabitSchedule(); });
        petListItemObject.transform.Find("CalendarButton").GetComponent<Button>().onClick.AddListener(delegate { pageController.GetComponent<PageController>().ToPage("SchedulePage"); });
        petListItemObject.transform.Find("CalendarButton").GetComponent<Button>().onClick.AddListener(delegate { LoadTasks(); });

        petListItemAdd.transform.SetAsLastSibling();
    }

    public Habit getCurrHabit()
    {
        return Habits[getHabitIndex()];
    }

    public int getClickedHabit(EventSystem buttonClicked)
    {
        int index = -1;
        int i = 0;
        Debug.Log("1one: " + index);

        while (!habits[i].HabitName.Equals(buttonClicked.currentSelectedGameObject.GetComponent<TMP_Text>().text))
        {
            Debug.Log(habits[i].HabitName + " ,1,,and,,, "+ buttonClicked.currentSelectedGameObject.GetComponent<TMP_Text>().text);
            i++;   
        }
        Debug.Log(habits[i].HabitName + " ,2,,and,,, " + buttonClicked.currentSelectedGameObject.GetComponent<TMP_Text>().text);
        if (habits[i].HabitName.Equals(buttonClicked.currentSelectedGameObject.GetComponent<TMP_Text>().text)) 
        {
            index = i;
            Debug.Log("1two: " + index);
        }
        Debug.Log("1three: " + index);
        return index;
    }

    public int getClickedHabitFromCalendarButton(EventSystem buttonClicked)
    {
        int index = -1;
        int i = 0;
        Debug.Log("2one: " + index);
        Debug.Log(buttonClicked.currentSelectedGameObject.transform.parent.Find("HabitText").GetComponent<TMP_Text>().text);

        while (!habits[i].HabitName.Equals(buttonClicked.currentSelectedGameObject.transform.parent.Find("HabitText").GetComponent<TMP_Text>().text)) //change made
        {
            Debug.Log(habits[i].HabitName + " ,1,,and,,, " + buttonClicked.currentSelectedGameObject.transform.parent.Find("HabitText").GetComponent<TMP_Text>().text);
            i++;
        }
        Debug.Log(habits[i].HabitName + " ,2,,and,,, " + buttonClicked.currentSelectedGameObject.transform.parent.Find("HabitText").GetComponent<TMP_Text>().text);
        if (habits[i].HabitName.Equals(buttonClicked.currentSelectedGameObject.transform.parent.Find("HabitText").GetComponent<TMP_Text>().text))//change made
        {
            Debug.Log(buttonClicked.currentSelectedGameObject.transform.parent.Find("HabitText").GetComponent<TMP_Text>().text);
            index = i;
            Debug.Log("2two: " + index);
        }
        Debug.Log("2three: " + index);
        return index;
    }
}
