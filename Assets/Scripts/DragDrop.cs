
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    [SerializeField] private GameObject imagePrefab;
    private Image buttonIcon;

    private Image draggedImage;
    private RectTransform canvasRectTransform;
    private RectTransform imageRectTransform;
    private bool dragging;


    void Start()
    {
        buttonIcon = GetComponent<Image>();
        canvas = FindObjectOfType<Canvas>();
        canvasRectTransform = canvas.GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();

        float width = buttonIcon.rectTransform.rect.width;
        float height = buttonIcon.rectTransform.rect.height;

        Debug.Log("Image size: " + width + " x " + height);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        dragging = true;

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
        float width = imageRectTransform.rect.width;
        float height = imageRectTransform.rect.height;

        Debug.Log("big Image size: " + width + " x " + height);
    }

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
        Debug.Log("OnEndDrag");
        dragging = false;

        // Destroy the image when the drag ends
        Destroy(draggedImage.gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    void Update()
    {
        if (dragging)
        {
            // Ensure that the image stays on top of other UI elements
        }
    }

}
