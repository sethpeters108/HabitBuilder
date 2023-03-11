
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnDropEnd : MonoBehaviour, IDropHandler
{
    private float AMOUNT = 0.2f;
    
    [SerializeField]private ScheduleController scheduleController;
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
            else if (eventData.pointerDrag.name.Equals("BrushBtn"))
            {
                currPet.increaseHealth(AMOUNT);
            }

        }
    }

}
