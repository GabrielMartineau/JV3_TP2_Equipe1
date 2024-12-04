using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MenuTourellesInteraction : MonoBehaviour
{

    [SerializeField] private GameObject joueur;

    private GameObject tourelle;

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
    }
}
