using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet
{
    [SerializeField] private string petName;
    [SerializeField] private int age;
    [SerializeField] private float health;
    [SerializeField] private float hunger;
    [SerializeField] private float fun;
    [SerializeField] private Sprite image;
    [SerializeField] private BarContoller barContoller;

    public Pet(string name)
    {
        this.petName = name;
        age = 0;
        health = 1;
        hunger = 1;
        fun = 1;
    }


    public string PetName
    {
        get => petName;
        set => petName = value;
    }

    public int Age
    {
        get => age;
        set => age = value;  
    }

    public float Health
    {
        get => health;
        set
        {
            health = value;
            barContoller.SetHealth(health);
        }
    }

    public float Hunger
    {
        get => hunger;
        set
        {
            hunger = value;
            barContoller.SetHunger(hunger);
        }
    }


    public float Fun
    {
        get => fun;
        set
        {
            fun = value;
            barContoller.SetFun(fun);
        }
    }

}
