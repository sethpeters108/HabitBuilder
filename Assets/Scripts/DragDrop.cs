
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

    private Image draggedImage;
    private RectTransform canvasRectTransform;
    private RectTransform imageRectTransform;
    private CanvasGroup canvasGroup;

        void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        canvasRectTransform = canvas.GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

        // Create a new instance of the image prefab
        GameObject imageObject = Instantiate(imagePrefab, canvas.transform);

        // Get the image component from the instantiated object
        draggedImage = imageObject.GetComponent<Image>();

        // Get the RectTransform component of the image
        imageRectTransform = imageObject.GetComponent<RectTransform>();

        // Set the position of the image to match the cursor position
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, eventData.position, eventData.pressEventCamera, out localPoint);
        imageRectTransform.anchoredPosition = localPoint;

        MatchOther(imageRectTransform, rectTransform);

        canvasGroup = imageObject.GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;

        BoxCollider2D collider = imageObject.GetComponent<BoxCollider2D>();
        RectTransform imgRectTransform = imageObject.GetComponent<RectTransform>();
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

        // Destroy the image when the drag ends
        Destroy(draggedImage.gameObject);
    }

    
}
