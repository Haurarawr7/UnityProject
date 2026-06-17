using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    //The land the player is currently selecting
    private Land selectedLand = null;
    private InputManager inputManager;

    [SerializeField]
    private float checkDistance = 1f;

    void Start()
    {
        inputManager = GetComponentInParent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, checkDistance))
        {
            OnInteractableHit(hit);
        }
        else
        {
            DeselectCurrentLand();
        }

        if (inputManager != null && inputManager.OnFoot.Interact.triggered)
        {
            Interact();
        }
    }

    //Handles what happens when the interaction raycast hits something interactable
    void OnInteractableHit(RaycastHit hit)
    {
        Collider other = hit.collider;

        //Check if the player is going to interact with land
        if (other.CompareTag("Land"))
        {
            Land land = other.GetComponent<Land>();
            if (land != null)
            {
                SelectLand(land);
                return;
            }
        }

        DeselectCurrentLand();
    }

    //Handles the selection process of the land
    void SelectLand(Land land)
    {
        if (selectedLand != null && selectedLand != land)
        {
            selectedLand.Select(false);
        }

        selectedLand = land;
        selectedLand.Select(true);

        if (UIManager.Instance != null)
        {
            UIManager.Instance.SetSelectedLand(selectedLand);
        }
    }

    void DeselectCurrentLand()
    {
        if (selectedLand != null)
        {
            selectedLand.Select(false);
            selectedLand = null;

            if (UIManager.Instance != null)
            {
                UIManager.Instance.SetSelectedLand(null);
            }
        }
    }

    //Triggered when the player presses the tool button
    public void Interact()
    {
        if (selectedLand != null)
        {
            selectedLand.Interact();
            return;
        }

        Debug.Log("Not on any land!");
    }
}