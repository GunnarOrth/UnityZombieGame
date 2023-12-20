using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask interactionMask;
    public int points = 1000;
    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int numFound;

    public TextMeshProUGUI pointDisplay;
    public TextMeshProUGUI interactionDisplay;

    [SerializeField] private AudioSource buySoundEffect;
    
    private void Update()
    {
        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, _colliders, interactionMask);

        if(numFound > 0)
        {
            var interactable = _colliders[0].GetComponent<IInteractable>();
            if(interactable != null && Input.GetKeyDown(KeyCode.E) && (points >= interactable.getCost))
            {
                points -= interactable.getCost;
                interactable.Interact(this);
                buySoundEffect.Play();
            }
            
            interactionDisplay.gameObject.SetActive(true);
            interactionDisplay.SetText(interactable.InteractionPrompt);
        }
        else{
            interactionDisplay.gameObject.SetActive(false);
        }

        pointDisplay.SetText(points + " ");
    }
}
