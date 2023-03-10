using System;
using System.Collections.Generic;
using UnityEngine;

public struct Day
{
    public bool isActive;
    public List<string> tasks;
    public Time notificationTime;
    public int beforeOffset;
    public int afterOffset;
    public Day(bool isActive, List<string> tasks, Time notificationTimes, int beforeOffset, int afterOffset)
    {
        this.isActive = isActive;
        this.tasks = tasks;
        this.notificationTime = notificationTimes;
        this.beforeOffset = beforeOffset;
        this.afterOffset = afterOffset;
    }
}

public struct Time
{
    public int hour;
    public int minute;
    public int second;
    public int ampm;
    public Time(int hour, int minute, int second, int ampm)
    {
        this.hour = hour;
        this.minute = minute;
        this.second = second;
        this.ampm = ampm;
    }
}

public class Habit : MonoBehaviour
{
    private string habitName;
    private Day[] activeDays;
    private Pet pet;
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
            activeDays[i] = new Day(false, new List<string>(), new Time(12, 0, 0,0),0,0);
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


    public void SetBeforeOffset(int dayIndex, int offset)
    {
        if (dayIndex >= 0 && dayIndex < 7)
        {
            activeDays[dayIndex].beforeOffset = offset;
        }
    }

    public void SetAfterOffset(int dayIndex, int offset)
    {
        if (dayIndex >= 0 && dayIndex < 7)
        {
            activeDays[dayIndex].afterOffset = offset;
        }
    }

    public void SetNotificationTimeHour (int dayIndex, int time)
    {
        if (dayIndex >= 0 && dayIndex < 7)
        {
            activeDays[dayIndex].notificationTime.hour = time;
        }
    }

    public void SetNotificationTimeMinute(int dayIndex, int time)
    {
        if (dayIndex >= 0 && dayIndex < 7)
        {
            activeDays[dayIndex].notificationTime.minute = time;
        }
    }
    public void SetNotificationTimeAmPm(int dayIndex, int ampm)
    {
        if (dayIndex >= 0 && dayIndex < 7)
        {
            activeDays[dayIndex].notificationTime.ampm = ampm;
        }
    }

    public Day GetDay(int dayIndex)
    {
        if (dayIndex >= 0 && dayIndex < 7)
        {
            return activeDays[dayIndex];
        }
        return new Day(false, null, new Time(12, 59, 59, 0),0,0);
    }

    public Time GetTime(int dayIndex)
    {
        if (dayIndex >= 0 && dayIndex < 7)
        {
            return activeDays[dayIndex].notificationTime;
        }
        return new Time();
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
}
