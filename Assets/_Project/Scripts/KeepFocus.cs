using UnityEngine;
using UnityEngine.EventSystems;

public class KeepFocus : MonoBehaviour
{
    GameObject lastSelected;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // on mémorise le bouton sélectionner comme "First selected
        if (EventSystem.current != null)
        {
            lastSelected = EventSystem.current.firstSelectedGameObject;
        }
        // On rend le curseur invisible
        Cursor.visible = false;
        // On bloque le curseur au centre de l'écran
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // On gere l'erreur de l'inexistance de l'event system
        if (EventSystem.current == null)
        {
            return;            
        }
        // Si un bouton est actuellement sélectionné on met à jour notre mémoire
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            lastSelected = EventSystem.current.currentSelectedGameObject;
        }
         else
        {
            // Sinon on remet le dernier sélectionné
            EventSystem.current.SetSelectedGameObject(lastSelected);
        }
    }
}
