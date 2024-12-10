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
    [SerializeField] private GameObject ennemis;

    private bool waveOver;
    private bool paused;

    void Start(){
        waveOver = false;
        so_infosNiveaux.ennemisRestants = 0;

        // Ligne de code temporaire: Demarrer avec un autre script.
        DemarrerSpawners();

        ennemis.name = "Enemies";
    }

    //void Update(){
        //VerifVagueTermine();
    //}

    public void VerifVagueTermine()
    {    
        if(!waveOver && !paused)
        {
            CompterEnnemisRestants();
            switch(so_infosNiveaux.ennemisRestants)
            {
                case 0 : waveOver = true; break;
                default : waveOver = false; break;
            }
            
            for(int i = 0; i < gameObject.transform.childCount; i++)
            {
                if(gameObject.transform.GetChild(i).GetComponent<EnemySpawnpoint>().isActive)
                {
                    waveOver = false;
                }
            }
        }    
        
        //Temporaire, je dois mettre des variables pour le nombre de vagues et trouver une solution pour switcher du niveau 2 à la scène de fin sans qu'à la vague 5, il reload le niveau 2.
            if(waveOver && !paused && so_infosNiveaux.vague < 5){
                Debug.Log($"vague augmente lol");
                Invoke("AugmenterVague", 5.0f);
                
                paused = true;
                
            }
            else if(waveOver && !paused && so_infosNiveaux.vague == 5){
                Invoke("NiveauSuivant", 5.0f);
            }        
    }

    private bool DoubleCheck()
    {
        CompterEnnemisRestants();
        if(so_infosNiveaux.ennemisRestants == 0) return true; else return false;
    }

    private void AugmenterVague(){
        if(DoubleCheck())
        {
            so_infosNiveaux.vague++;
            Debug.Log($"la vague a augmenté");
            gestionnaireScore.UpdateText();
            DemarrerSpawners();
            
        }
        waveOver = false;
        paused = false;
        

        
    }

    private void NiveauSuivant()
    {
        if(DoubleCheck())
        {
            gestionnaireScene.ChangeScene("Niveau2");
        }
    }

    public void DemarrerSpawners()
    {
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).GetComponent<EnemySpawnpoint>().isActive = true;
            gameObject.transform.GetChild(i).GetComponent<EnemySpawnpoint>().StartNewWave();
        }
    }

    private void CompterEnnemisRestants(){
        int ennemisRestants = 0;
        for(int i = 0; i < ennemis.transform.childCount; i++)
        {
            if(ennemis.transform.GetChild(0).GetComponent<BasicEnemyAI>().isAlive)
            {
                ennemisRestants++;
            }
        }
        Debug.Log($"Il reste " + ennemisRestants + "ennemis restants");
        so_infosNiveaux.ennemisRestants = ennemisRestants;
    }
}
