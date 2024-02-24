using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStick : MonoBehaviour, IDragHandler,IPointerUpHandler, IPointerDownHandler
{
    private Image outerImage, innerImage;
    private Vector3 inputVector;

    private void Start()
    {
        outerImage = GetComponent<Image>();
        innerImage = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
        
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        innerImage.rectTransform.anchoredPosition = Vector3.zero;
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(outerImage.rectTransform,ped.position,ped.pressEventCamera,out pos))
        {
            pos.x = (pos.x/outerImage.rectTransform.sizeDelta.x);
            pos.y = (pos.y/outerImage.rectTransform.sizeDelta.y);

            inputVector = new Vector3(pos.x * 2 + 1, 0, pos.y * 2 - 1);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            //move innerImage
            innerImage.rectTransform.anchoredPosition = new Vector3(inputVector.x * (outerImage.rectTransform.sizeDelta.x / 2.5f), inputVector.z * (outerImage.rectTransform.sizeDelta.y / 2.5f));

        }
    }

    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");


    }
    public float Vertical()
    {
        if (inputVector.z != 0)
            return inputVector.z;
        else
            return Input.GetAxis("Vertical");
    }
}
