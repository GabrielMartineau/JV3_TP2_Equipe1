using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    [SerializeField] private GestionnaireScene sceneManager;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Détection Trigger");
        if(other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Détection Enemy");
            sceneManager.ChangeScene("GameOver");
        }
    }
}
