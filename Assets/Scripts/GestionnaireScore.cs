using UnityEngine;
using TMPro;

public class GestionnaireScore : MonoBehaviour
{
    [SerializeField]
    private InfosNiveaux so_infosNiveaux;

    [SerializeField]
    private TMP_Text champScore;

    [SerializeField]
    private TMP_Text champVague;

    void Start(){
        so_infosNiveaux.score = 0;
        so_infosNiveaux.vague = 1;
        UpdateText();
    }

    public void EnemyScore()
    {
        so_infosNiveaux.score += 5;
        UpdateText();
    }

    public void UpdateText()
    {
        champScore.text = "Score : " + so_infosNiveaux.score;
        champVague.text = "Vague : " + so_infosNiveaux.vague;
    }
}
