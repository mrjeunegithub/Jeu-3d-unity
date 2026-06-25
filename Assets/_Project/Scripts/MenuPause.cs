using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public static bool EstEnPause = false;

    [Header("Panneau de Menu")]
    public GameObject menuPause;
    public GameObject panneauPrincipal;
    public GameObject panneauOptions;
    public GameObject panneauCredits;

    [Header ("Panneaux Bouton Sélectionné")]
    public GameObject boutonPremierPrincipal;
    public GameObject boutonPremierOptions;
    public GameObject boutonPremierCredits;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //EstEnPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        //on capture si oui ou non le joueur a appuyé sur echap ou start
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Cancel"))
        {
            if (EstEnPause)
            {
                //Si je suis dans un sous menu, crédits ou option, echap nous permet de revenir un menus avant
                if(panneauOptions.activeSelf || panneauCredits.activeSelf)
                {
                    OuvrirMenuPrincipal();
                }
                else
                {  
                    ReprendreJeu();
                }
            }
            else
            {
                MettreEnPause(); 
            }
        }
    }
    
    // --Gestion de la Pause

    public void ReprendreJeu()
    {
        menuPause.SetActive(false);
        Time.timeScale = 1f; // Relance le temps du jeu;
        EstEnPause = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void MettreEnPause()
    {
        menuPause.SetActive(true);
        Time.timeScale = 0f; // Gèle le temps du jeu
        EstEnPause = true;

        OuvrirMenuPrincipal();
    }

    // Ouverture des differents menus

    public void OuvrirMenuPrincipal()
    {
        panneauPrincipal.SetActive(true);
        panneauCredits.SetActive(false);
        panneauOptions.SetActive(false);

        //On sélectionne automatiquement le premier bouton du menu principal
        ChangerBoutonSelectionne(boutonPremierPrincipal);
    }

    public void OuvrirOptions()
    {
        panneauPrincipal.SetActive(false);
        panneauCredits.SetActive(false);
        panneauOptions.SetActive(true);

        //On sélectionne automatiquement le premier bouton du menu option
        ChangerBoutonSelectionne(boutonPremierOptions);
    }

    public void OuvrirCredit()
    {
        panneauPrincipal.SetActive(false);
        panneauCredits.SetActive(true);
        panneauOptions.SetActive(false);

        //On sélectionne automatiquement le premier bouton du menu principal
        ChangerBoutonSelectionne(boutonPremierCredits);        
    }

    // Action des boutons

    public void QuitterJeu()
    {
        Application.Quit();
    }

    public void RetourMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    //méthode interne pour s'assurer que le bon bouton reste sélectionner

    private void ChangerBoutonSelectionne(GameObject cible)
    {
        if(EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null); // on clear pour eviter des bugs
            EventSystem.current.SetSelectedGameObject(cible);
        }
    }

}
