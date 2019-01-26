using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    // Do something when button is highlighted with mouse.
    public void OnPointerEnter(PointerEventData eventData)
    {
        //ButtonSounds.instance.PlayHighlight();
        // TODO Replace with a SoundController call
    }

    // Do something when button is selected.
    public void OnSelect(BaseEventData eventData)
    {
        //ButtonSounds.instance.PlayConfirm();
        // TODO Replace with a SoundController call
    }

}
