
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
    public bool runJoy = false;
    public bool runBounce = false;
    bool setPetText;
    public Animator animator;

    [SerializeField] private HabitController habitController;
    [SerializeField] private GameObject petName;
    [SerializeField] private GameObject petAge;

    private void Start()
    {
        previousPosition = new Vector3(0,0,0);
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        rectTransform = GetComponent<RectTransform>();
        collider.size = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
        setPetText = false;
        animator = GetComponent<Animator>();
        //animator.runtimeAnimatorController = Resources.Load("Assets/Animations/PlaceholderPet.controller");
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Pet currPet = habitController.getCurrHabit().Pet;

            if (eventData.pointerDrag.name.Equals("FoodBtn")){
                animator.SetBool("runJoy", true);
                currPet.increaseHunger(AMOUNT);
            }else if (eventData.pointerDrag.name.Equals("BallBtn"))
            {
                animator.SetBool("runBounce", true);
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
            
            Pet currPet = habitController.getCurrHabit().Pet;
            
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
            Pet currPet = habitController.getCurrHabit().Pet;
            currPet.decreaseFun(AMOUNT);
            currPet.decreaseHealth(AMOUNT);
            currPet.decreaseHunger(AMOUNT);
        }
        if (!setPetText)
        {
            petName.GetComponent<TextMeshProUGUI>().text = "Name : " + habitController.getCurrHabit().Pet.PetName;
            petAge.GetComponent<TextMeshProUGUI>().text = "Age : " + habitController.getCurrHabit().Pet.Age;
            setPetText = true;
        }
    }

}
