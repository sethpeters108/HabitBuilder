
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnDropEnd : MonoBehaviour, IDropHandler
{
    private float AMOUNT = 0.2f;
    private Vector3 previousPosition;

    [SerializeField]private ScheduleController scheduleController;

    private void Start()
    {
        previousPosition = new Vector3(0,0,0);
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Pet currPet = scheduleController.getCurrHabit().pet;

            if (eventData.pointerDrag.name.Equals("FoodBtn")){
                currPet.increaseHunger(AMOUNT);
            }else if (eventData.pointerDrag.name.Equals("BallBtn"))
            {
                currPet.increaseFun(AMOUNT);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name.StartsWith("Brush"))
        {
            previousPosition= collision.transform.position;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        // Check if the object is inside a specific collider
        if (collision.gameObject.name.StartsWith("Brush"))
        {
            
            Pet currPet = scheduleController.getCurrHabit().pet;
            
            // Calculate the distance moved since the last frame
            float distanceMoved = Vector3.Distance(collision.transform.position, previousPosition);
            Debug.Log(distanceMoved);
            currPet.increaseHealth(distanceMoved * 0.00001f);
            // Store the current position for the next frame
            previousPosition = collision.transform.position;
        }
    }

}
