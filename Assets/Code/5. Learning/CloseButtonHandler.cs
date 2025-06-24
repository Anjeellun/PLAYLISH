using UnityEngine;

public class CloseButtonHandler : MonoBehaviour
{
    [Tooltip("The detail/material panel to be closed")]
    public GameObject detailPanel;

    [Tooltip("The main/list panel to be shown again")]
    public GameObject previousPanel;

    public void CloseDetail()
    {
        if (detailPanel != null)
            detailPanel.SetActive(false);

        if (previousPanel != null)
            previousPanel.SetActive(true);
    }
}
