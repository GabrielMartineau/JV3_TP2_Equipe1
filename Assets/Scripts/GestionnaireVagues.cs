using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEditor.Animations;
using UnityEngine;

public class GestionnaireVagues : MonoBehaviour
{  
    [SerializeField] private InfosNiveaux so_infosNiveaux;
    [SerializeField] private GestionnaireScore gestionnaireScore;
    [SerializeField] private GestionnaireScene gestionnaireScene;
    public GameObject ennemis;

    void Start(){
        ennemis.name = "Enemies";
    }

    void Update(){
        VerifVagueTermine();
    }

    public void VerifVagueTermine()
    {        
        //Temporaire, je dois mettre des variables pour le nombre de vagues et trouver une solution pour switcher du niveau 2 à la scène de fin sans qu'à la vague 5, il reload le niveau 2.
            if(so_infosNiveaux.ennemisRestants == 0 && so_infosNiveaux.vague < 5){
                Debug.Log($"vague augmente lol");
                AugmenterVague();
                //Invoke("AugmenterVague­", 15.0f);
            }
            else if(so_infosNiveaux.ennemisRestants == 0 && so_infosNiveaux.vague == 5){
                gestionnaireScene.ChangeScene("Niveau2");
            }
            else{
                CompterEnnemisRestants();
            }             
    }

    private void AugmenterVague(){
        so_infosNiveaux.vague++;
        Debug.Log($"la vague a augmenté");
        gestionnaireScore.UpdateText();
    }

    private void CompterEnnemisRestants(){
        Debug.Log($"Il reste " + ennemis.transform.childCount + "ennemis restants");
       so_infosNiveaux.ennemisRestants = ennemis.transform.childCount;
    }
}
