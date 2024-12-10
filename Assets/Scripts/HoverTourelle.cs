using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTourelle : MonoBehaviour
{

    [SerializeField] private GameObject plateauxTourelles;

    [SerializeField] private Material couleurValide;
    [SerializeField] private Material couleurDeBase;
    
    public void AppliquerZoneValide(){
        for(int i = 0; i < plateauxTourelles.transform.childCount; i++)
        {
            plateauxTourelles.transform.GetChild(i).GetComponent<Renderer>().material = couleurValide;
        } 
    }

    public void RetirerZoneValide(){
        for(int i = 0; i < plateauxTourelles.transform.childCount; i++)
        {
            plateauxTourelles.transform.GetChild(i).GetComponent<Renderer>().material = couleurDeBase;
        } 
    }
}
