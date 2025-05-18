using UnityEngine;

public class dinoCheck : MonoBehaviour
{
    public UIManager uiManager;

    void Update()
    {
        Vector2 pos = transform.position;

        if (pos.x < -30 || pos.x > 55 || pos.y < -7.5f || pos.y > 12)
        {
            uiManager.ShowGameOverPanel();
            this.enabled = false; // optional: stop further checks
        }
    }
}
