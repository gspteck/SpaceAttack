using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightButtonController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {
    public bool buttonPressed = false;

    void Start() {
        
    }

    void Update() {
        
    }

    public void OnPointerDown(PointerEventData eventData) {
        buttonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        buttonPressed = false;
    }
}
