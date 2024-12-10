using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionnaireFin : MonoBehaviour
{

    [SerializeField]
    private GestionnaireScore gestionnaireScore;

    // Start is called before the first frame update
    void Start()
    {
        gestionnaireScore.UpdateText();
    }

   
}
