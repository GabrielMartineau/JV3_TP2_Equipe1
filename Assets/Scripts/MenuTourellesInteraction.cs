using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MenuTourellesInteraction : MonoBehaviour
{

    [SerializeField] private GameObject joueur;
    [SerializeField] private HoverTourelle hoverTourelle;

    private GameObject tourelle;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        joueur = transform.parent.parent.parent.GetComponentInChildren<OVRManager>().gameObject;
    }

    public void SelectionMenuTourelles(GameObject prefabTourelle){
        tourelle = Instantiate(prefabTourelle, prefabTourelle.transform.parent = joueur.transform);
    } 

    public void Update()
    {

        if(OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            Debug.Log("GetUp PrimaryIndexTrigger");
            tourelle.transform.SetParent(null);
        }
        if(OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            Debug.Log($"GetDown SecondaryHandTrigger");
            hoverTourelle.AppliquerZoneValide();
        }
        if(OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
        {
            Debug.Log($"GetUp SecondaryHandTrigger");
            hoverTourelle.RetirerZoneValide();
        }
    }
}
