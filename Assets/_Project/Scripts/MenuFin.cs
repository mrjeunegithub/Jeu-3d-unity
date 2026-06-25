using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class MenuFin : MonoBehaviour
{
    public TMP_Text TextFin;
    public GameObject CreditPanneau;
    public GameObject PremierBoutonFin;
    public GameObject PremierBoutonCredit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TextFin.text = "Youpiii, Tu y es arrivé !!! \n En plus avec un énorme score de " + PlayerInfos.pi.GetScore().ToString() + " points";
    }

    public void ReloadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OuvrirPanneauCredit()
    {
        CreditPanneau.SetActive(true);

        ChangerBoutonSelectionne(PremierBoutonCredit);
    }

    public void FermerPanneauCredit()
    {
        CreditPanneau.SetActive(false);

        ChangerBoutonSelectionne(PremierBoutonFin);
    }

    private void ChangerBoutonSelectionne(GameObject cible)
    {
        if(EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(cible);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
