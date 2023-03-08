using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
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
        barContoller = (BarContoller)FindObjectOfType(typeof(BarContoller));
        this.petName = name;
        age = 0;
        Health = 0.5f;
        Hunger = 0.5f;
        Fun = 0.5f;
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

    public void increaseHealth(float value)
    {
        Health += value;
    }

    public void increaseHunger(float value)
    {
        Hunger += value;
    }

    public void increaseFun(float value)
    {
        Fun += value;
    }

    public void decreaseHealth(float value)
    {
        Health -= value;
    }

    public void decreaseHunger(float value)
    {
        Hunger -= value;
    }

    public void decreaseFun(float value)
    {
        Fun -= value;
    }


}
