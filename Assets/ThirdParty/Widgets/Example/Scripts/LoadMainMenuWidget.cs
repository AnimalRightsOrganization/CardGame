using UnityEngine;
using eeGames.Widget;

public class LoadMainMenuWidget : MonoBehaviour
{
    public WidgetName widgetName;

    void Start () 
    {
        // Show MainMenu Widget
        WidgetManager.Instance.Push(widgetName);
	}
}
