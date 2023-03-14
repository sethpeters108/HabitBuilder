
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class OnDropEnd : MonoBehaviour, IDropHandler
{
    private float AMOUNT = 0.2f;
    private Vector3 previousPosition;
    private RectTransform rectTransform;
    bool setPetText;

    [SerializeField] private ScheduleController scheduleController;
    [SerializeField] private GameObject petName;
    [SerializeField] private GameObject petAge;

    private void Start()
    {
        previousPosition = new Vector3(0,0,0);
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        rectTransform = GetComponent<RectTransform>();
        collider.size = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
        setPetText = false;
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
            currPet.increaseHealth(distanceMoved * 0.0001f);
            // Store the current position for the next frame
            previousPosition = collision.transform.position;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Pet currPet = scheduleController.getCurrHabit().pet;
            currPet.decreaseFun(AMOUNT);
            currPet.decreaseHealth(AMOUNT);
            currPet.decreaseHunger(AMOUNT);
        }
        if (!setPetText)
        {
            petName.GetComponent<TextMeshProUGUI>().text = "Name : " + scheduleController.getCurrHabit().pet.PetName;
            petAge.GetComponent<TextMeshProUGUI>().text = "Age : " + scheduleController.getCurrHabit().pet.Age;
            setPetText = true;
        }
    }

}
