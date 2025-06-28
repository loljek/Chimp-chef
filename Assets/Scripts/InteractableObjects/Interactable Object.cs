using System;
using UnityEngine;

abstract public class InteractableObject : MonoBehaviour
{
    [NonSerialized] public BoxCollider interactSpace; 
    [NonSerialized] public string type;
    
    abstract public void Interaction();
}