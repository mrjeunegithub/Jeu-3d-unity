using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour
{
    public GameObject PanneauPrincipal;
    public GameObject PanneauOption;
    public GameObject PremierBouttonPrincipal;
    public GameObject PremierBouttonOption;
    public void LoadLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OuvrirPanneauOption()
    {
        PanneauPrincipal.SetActive(false);
        PanneauOption.SetActive(true);

        ChangerBoutonSelectionne(PremierBouttonOption);
    }

    public void OuvrirPanneauPrincipal()
    {
        PanneauOption.SetActive(false);
        PanneauPrincipal.SetActive(true);

        ChangerBoutonSelectionne(PremierBouttonPrincipal);
    }

    private void ChangerBoutonSelectionne(GameObject cible)
    {
        if(EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(cible);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
