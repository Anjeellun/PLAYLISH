using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class WeatherDragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
{
    public AudioClip soundClip; 
    public AudioSource audioSource;
    private bool isDragging = false;
    private float pressTime = 0f;
    private float longPressThreshold = 0.5f;
    private Vector3 originalPosition;
    private Transform parentTransform;
    public GameObject correctDropZone; 
    private bool hasPlayedSound = false;

    private void Start()
    {
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; 
        audioSource.Stop(); 
    }

    private void OnDestroy()
    {
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();

        correctDropZone = null;
        parentTransform = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pressTime = Time.time;
        originalPosition = transform.position;
        parentTransform = transform.parent;
        isDragging = false;
        hasPlayedSound = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!hasPlayedSound)
        {
            PlaySound();
            hasPlayedSound = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Time.time - pressTime >= longPressThreshold)
        {
            if (!isDragging)
            {
                isDragging = true;
                if (!hasPlayedSound)
                {
                    PlaySound();
                    hasPlayedSound = true;
                }
            }

            Canvas canvas = GetComponentInParent<Canvas>();
            if (canvas != null)
                transform.SetParent(canvas.transform, true);

            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            isDragging = false;
            GameObject dropTarget = GetDropTarget(eventData);

            if (dropTarget != null && dropTarget.CompareTag("DropZone"))
            {
                if (dropTarget.transform.childCount == 0)
                {
                    transform.SetParent(dropTarget.transform);
                    transform.position = dropTarget.transform.position;
                }
                else
                {
                    transform.position = originalPosition;
                    transform.SetParent(parentTransform);
                }
            }
            else
            {
                transform.position = originalPosition;
                transform.SetParent(parentTransform);
            }
        }
    }

    private void PlaySound()
    {
        if (soundClip != null && audioSource != null)
            audioSource.PlayOneShot(soundClip);
    }

    private GameObject GetDropTarget(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("DropZone"))
                return result.gameObject;
        }
        return null;
    }

    public bool IsCorrectlyPlaced()
    {
        if (correctDropZone == null)
            return false;

        return transform.parent != null && transform.parent.gameObject == correctDropZone;
    }
}