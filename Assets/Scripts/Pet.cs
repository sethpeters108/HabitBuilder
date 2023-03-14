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
        Health = Mathf.Min(Health + value, 1);
    }

    public void increaseHunger(float value)
    {
        Hunger = Mathf.Min(Hunger + value, 1);
    }

    public void increaseFun(float value)
    {
        Fun = Mathf.Min(Fun + value, 1);
    }

    public void decreaseHealth(float value)
    {
        Health = Mathf.Max(Health - value, 0);
    }

    public void decreaseHunger(float value)
    {
        Hunger = Mathf.Max(Hunger - value, 0);
    }

    public void decreaseFun(float value)
    {
        Fun = Mathf.Max(Fun - value, 0);
    }


}
