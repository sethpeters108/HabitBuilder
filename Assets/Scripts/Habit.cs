using System;
using System.Collections.Generic;
using UnityEngine;

public struct Day
{
    public bool isActive;
    public List<string> tasks;
    public List<DateTime> notificationTimes;
    public Day(bool isActive, List<string> tasks, List<DateTime> notificationTimes)
    {
        this.isActive = isActive;
        this.tasks = tasks;
        this.notificationTimes = notificationTimes;
    }
}

public class Habit : MonoBehaviour
{
    private string habitName;
    private Day[] activeDays;
    public Pet pet;
    private int foodAmount;
    private int ballAmount;
    private int brushAmount;
    

    public Habit(string habitName, string petName)
    {
        this.habitName = habitName;
        activeDays = new Day[7];
        pet = new Pet(petName);
        foodAmount = 0;
        ballAmount = 0;
        brushAmount = 0;
        
        initDays();
        
    }

    private void initDays()
    {
        for(int i = 0; i < activeDays.Length; i++)
        {
            activeDays[i] = new Day(false, new List<string>(), new List<DateTime>());
        }
    }

    public void AddTask(int dayIndex, string taskdesc)
    {
        if(dayIndex >= 0 && dayIndex < 7)
        {
            activeDays[dayIndex].tasks.Add(taskdesc);
        }
        
    }

    public void SetActiveDay(int dayIndex, bool isActive)
    {
        if(dayIndex >= 0 && dayIndex < 7)
        {
            activeDays[dayIndex].isActive = isActive;
        }
    }

    public void AddNotificationTime(int dayIndex, DateTime time)
    {
        if (dayIndex >= 0 && dayIndex < 7)
        {
            activeDays[dayIndex].notificationTimes.Add(time);
        }
    }
    public Day GetDay(int dayIndex)
    {
        if (dayIndex >= 0 && dayIndex < 7)
        {
            return activeDays[dayIndex];
        }
        return new Day(false, null, null); ;
    }

    public string HabitName
    {
        get => habitName;
    }

    public string PetName
    {
        get => pet.PetName;
        set
        {
            pet.PetName = value;
        }
    }

    public int FoodAmount
    {
        get => foodAmount;
        set => foodAmount = value;
    }

    public int BallAmount
    {
        get => ballAmount;
        set => ballAmount = value;
    }

    public int BrushAmount
    {
        get => brushAmount;
        set => brushAmount = value;
    }

    

    private void Update()
    {
        
    }
}
