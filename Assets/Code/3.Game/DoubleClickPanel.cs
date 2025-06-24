using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DoubleClickPanel : MonoBehaviour
{
    public Button[] targetButtons; 
    public GameObject[] panelsToOpen;
    public Button[] closeButtons; 
    public float doubleClickThreshold = 0.5f;

    private float[] pressStartTimes;
    private bool[] isPressing;

    private void Start()
    {
        if (targetButtons.Length != panelsToOpen.Length || panelsToOpen.Length != closeButtons.Length)
            return;

        pressStartTimes = new float[targetButtons.Length];
        isPressing = new bool[targetButtons.Length];

        for (int i = 0; i < targetButtons.Length; i++)
        {
            int index = i;
            if (targetButtons[i] != null)
            {
                EventTrigger trigger = targetButtons[i].gameObject.AddComponent<EventTrigger>();

                EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerDown
                };
                pointerDownEntry.callback.AddListener((_) => OnPointerDown(index));
                trigger.triggers.Add(pointerDownEntry);

                EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry
                {
                    eventID = EventTriggerType.PointerUp
                };
                pointerUpEntry.callback.AddListener((_) => OnPointerUp(index));
                trigger.triggers.Add(pointerUpEntry);
            }

            if (closeButtons[i] != null)
                closeButtons[i].onClick.AddListener(() => ClosePanel(index));

            if (panelsToOpen[i] != null)
                panelsToOpen[i].SetActive(false);
        }
    }

    private void Update()
    {
        for (int i = 0; i < targetButtons.Length; i++)
        {
            if (isPressing[i] && Time.time - pressStartTimes[i] >= doubleClickThreshold)
            {
                OpenPanel(i);
                isPressing[i] = false;
            }
        }
    }

    private void OnPointerDown(int index)
    {
        pressStartTimes[index] = Time.time;
        isPressing[index] = true;
    }

    private void OnPointerUp(int index)
    {
        isPressing[index] = false;
    }

    private void OpenPanel(int index)
    {
        if (panelsToOpen[index] != null)
            panelsToOpen[index].SetActive(true);
    }

    private void ClosePanel(int index)
    {
        if (panelsToOpen[index] != null)
            panelsToOpen[index].SetActive(false);
    }
}
