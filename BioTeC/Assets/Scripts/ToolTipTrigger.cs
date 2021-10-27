using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler,  IPointerExitHandler
{
    [SerializeField] string header = null;
    [SerializeField] [TextArea] string content = null;

    public void OnPointerEnter(PointerEventData eventData)
    {
        ToolTipSystem.Show(content, header);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTipSystem.Hide();
    }
}
