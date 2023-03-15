
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour,  IBeginDragHandler, IEndDragHandler, IDragHandler
{

    private Canvas canvas;

    private RectTransform rectTransform;
    [SerializeField] private GameObject imagePrefab;
    [SerializeField] private HabitController habitController;

    private Image draggedImage;
    private GameObject draggedImageObject;
    private RectTransform canvasRectTransform;
    private RectTransform imageRectTransform;
    private CanvasGroup canvasGroup;
    private GameObject petObject;
    private float AMOUNT = 0.2f;
    public Animator animator;

    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        canvasRectTransform = canvas.GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
        petObject = GameObject.Find("PlaceholderPet");
        animator = petObject.GetComponent<Animator>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        // Create a new instance of the image prefab
        draggedImageObject = Instantiate(imagePrefab, canvas.transform);

        // Get the image component from the instantiated object
        draggedImage = draggedImageObject.GetComponent<Image>();

        // Get the RectTransform component of the image
        imageRectTransform = draggedImageObject.GetComponent<RectTransform>();

        // Set the position of the image to match the cursor position
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, eventData.position, eventData.pressEventCamera, out localPoint);
        imageRectTransform.anchoredPosition = localPoint;

        MatchOther(imageRectTransform, rectTransform);

        canvasGroup = draggedImageObject.GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;

        BoxCollider2D collider = draggedImageObject.GetComponent<BoxCollider2D>();
        RectTransform imgRectTransform = draggedImageObject.GetComponent<RectTransform>();
        if (collider != null) { collider.size = new Vector2(imgRectTransform.rect.width, imgRectTransform.rect.height); }
        
    }

    //change the size to be the same as another object
    public static void MatchOther(RectTransform rt, RectTransform other)
    {
        Vector2 myPrevPivot = rt.pivot;
        myPrevPivot = other.pivot;
        rt.position = other.position;

        rt.localScale = other.localScale;

        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, other.rect.width);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, other.rect.height);
        //rectTransf.ForceUpdateRectTransforms(); - needed before we adjust pivot a second time?
        rt.pivot = myPrevPivot;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, eventData.position, eventData.pressEventCamera, out localPoint);
        imageRectTransform.anchoredPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if (inside(draggedImageObject, petObject))
        {
            Pet currPet = habitController.getCurrHabit().Pet;
            if (draggedImageObject.name.StartsWith("Food"))
            {
                animator.SetBool("runJoy", true);
                currPet.increaseHunger(AMOUNT);
            }
            else if (draggedImageObject.name.StartsWith("Ball"))
            {
                animator.SetBool("runBounce", true);
                currPet.increaseFun(AMOUNT);
            }
        }
        // Destroy the image when the drag ends
        Destroy(draggedImage.gameObject);
    }

    private bool inside(GameObject gameObjectA, GameObject gameObjectB)
    {
        // Get the colliders of the game objects
        BoxCollider2D colliderA = gameObjectA.GetComponent<BoxCollider2D>();
        BoxCollider2D colliderB = gameObjectB.GetComponent<BoxCollider2D>();

        // Get a point on colliderA that is closest to colliderB
        Vector3 closestPoint = colliderA.ClosestPoint(colliderB.bounds.center);

        // Check if the closest point is inside colliderB
        if (colliderB.bounds.Contains(closestPoint))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
