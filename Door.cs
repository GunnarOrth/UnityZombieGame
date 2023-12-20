using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{

    [SerializeField] private string _prompt;
    public GameObject doorObject;
    public int cost = 500;
    public string InteractionPrompt => _prompt;
    public int getCost => cost;
    public bool Interact(Interactor interactor)
    {
        doorObject.SetActive(false);
        Debug.Log(message:"OpenDoor");
        return true;
    }
}
