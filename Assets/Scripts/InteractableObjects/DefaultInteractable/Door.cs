using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class Door : InteractableObject
{
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 2f;
    [SerializeField] private BoxCollider doorColider;
    public bool isOpen = false;
    private bool isOpening = false;
    private Quaternion openDirection;
    

    private void Awake()
    {
        interactSpace = doorColider;
        type = "defaultObject";
        openDirection = transform.rotation;
    }

    private void Update()
    {
        if (isOpening)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, openDirection, openSpeed * Time.deltaTime);
        }
        if (Quaternion.Angle(transform.rotation, openDirection) < 0.1f)
        {
            transform.rotation = openDirection;
            isOpening = false;
        }
    }
    public override void Interaction()
    {
        if (isOpen)
        {
            openDirection *= Quaternion.Euler(0, -openAngle, 0);
        }
        else
        {
            openDirection *= Quaternion.Euler(0, openAngle, 0);
        }
        isOpening = true;
        isOpen = !isOpen;
    }
}
