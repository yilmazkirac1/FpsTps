using UnityEngine;

public interface IInteractable
{
    string GetPrompt();          // ekranda "E: Al" gibi yazý
    void Interact(GameObject interactor);  // player gönderilir
}
