using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScheduleController : MonoBehaviour
{
    [SerializeField] private List<Habit> habits;
    [SerializeField] private Button[] dayBtns;
    [SerializeField] private int dayIndex;
    [SerializeField] private int habitIndex;

    private float timer;
    private float DECREASE_RATE = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        habits = new List<Habit>();
        habits.Add(new Habit("TestHabit", "jon"));
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // increment timer by Time.deltaTime
        timer += Time.deltaTime;

        decreasePetStats();
    }

    private void decreasePetStats()
    {
        // if 0.5 hour has elapsed, decrease var by 0.1 and reset timer
        if (timer >= 1800.0f)
        {

            for (int i = 0; i < habits.Count; i++)
            {
                habits[i].pet.decreaseFun(DECREASE_RATE);
                habits[i].pet.decreaseHealth(DECREASE_RATE);
                habits[i].pet.decreaseHunger(DECREASE_RATE);
                timer = 0.0f;
            } 
        }
    }

    public void LoadHabitSchedule()
    {
        //TestCode
        habitIndex = 0;

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
    
    public int getHabitIndex()
    {
        return habitIndex;
    }
    public List<Habit> Habits
    {
        get { return habits; }
        set { habits = value; }
    }

    public Habit getCurrHabit()
    {
        return Habits[habitIndex];
    }
}
