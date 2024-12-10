using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementInvalide : MonoBehaviour
{

    [SerializeField] private Material couleurInvalide;
    [SerializeField] private Material couleurDeBase;
    [SerializeField] private AudioSource audioInvalide;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Tourelle")){
            gameObject.GetComponent<Renderer>().material = couleurInvalide;
            audioInvalide.Play();
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Tourelle")){
            gameObject.GetComponent<Renderer>().material = couleurDeBase;
        }
    }
}
