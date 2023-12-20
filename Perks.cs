using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perks : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public int cost = 500;
    public string InteractionPrompt => _prompt;
    public int getCost => cost;

    //public GameObject gun;
    public bool bought = false;

    public Guns gunScript;

    public float reloadTimeM;
    public int magazineSizeM;

    public bool Interact(Interactor interactor)
    {
        gunScript.reloadTime = reloadTimeM*gunScript.reloadTime;
           
        gunScript.magazineSize = magazineSizeM*gunScript.magazineSize;
        Debug.Log(message:"bought perk");
        return true;
    }
}