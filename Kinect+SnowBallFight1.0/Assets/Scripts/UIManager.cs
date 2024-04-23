using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameState gameState;
    public GameObject gameManager;
    public GameObject mainMenu;
    public Animator canvas;
    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        gameState = GameState.MENU;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Menu() {
        gameState = GameState.MENU;
    }

    public void PlayButtonHit() {
        gameState = GameState.WAITINGFORPLAYER;
        gameManager.GetComponent<GameManager>().WaitForPlayer();
    }

    public void Play() {
        canvas.Play("StartFadeMainCanvas");
    }


    IEnumerator PlayAnim()
    {

        yield return new WaitForSeconds(2f);
        
    }
    //FadeToWaitingForPlayer
}
