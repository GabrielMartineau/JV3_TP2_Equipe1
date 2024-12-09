using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GestionnaireVagues : MonoBehaviour
{  
    [SerializeField] private InfosNiveaux so_infosNiveaux;
    [SerializeField] private GestionnaireScore gestionnaireScore;

    public void VerifVagueTermine()
    {
        
            //Invoke("AugmenterVague­", 15f);
            so_infosNiveaux.vague++;
            Debug.Log($"la vague a augmenté");
            gestionnaireScore.UpdateText();
        
    }

    private void AugmenterVague(){
        so_infosNiveaux.vague++;
    }

    private void CompterEnnemisRestants(){
        Debug.Log($"allo");
    }
}
