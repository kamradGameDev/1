﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
	private Image joystickBG;
	private Image joystick;
	private Vector2 InputVector;
	
	void Start()
	{
		joystickBG = GetComponent<Image>();
		joystick = transform.GetChild(0).GetComponent<Image>();
	}
	
	public virtual void OnPointerDown(PointerEventData data)
	{
		OnDrag(data);
	}
	
	public virtual void OnPointerUp(PointerEventData data)
	{
		InputVector = Vector2.zero;
		joystick.rectTransform.anchoredPosition = Vector2.zero;
	}
	
	public virtual void OnDrag(PointerEventData data)
	{
		Vector2 pos;
		if(RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG.rectTransform, data.position, data.pressEventCamera, out pos))
		{
			pos.x = (pos.x / joystickBG.rectTransform.sizeDelta.x);
			pos.y = (pos.y / joystickBG.rectTransform.sizeDelta.y);
			
			InputVector = new Vector2(pos.x * 2-1, pos.y * 2-1);
			if(InputVector.magnitude > 1.0f)
			{
				InputVector = InputVector.normalized;
			}
			joystick.rectTransform.anchoredPosition = new Vector2(InputVector.x*(joystickBG.rectTransform.sizeDelta.x/2), InputVector.y*(joystickBG.rectTransform.sizeDelta.y/2));
		}
	}
	
	public float Horizontal()
	{
		if(InputVector.x != 0)
		{
			return InputVector.x;
		}
		else
		{
			return Input.GetAxis("Horizontal");
		}
	}
	
	public float Vertical()
	{
		if(InputVector.y != 0)
		{
			return InputVector.y;
		}
		else
		{
			return Input.GetAxis("Vertical");
		}
	}
}
