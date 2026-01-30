using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public TileBoard board; //needed so it can interact with the board
    public CanvasGroup gameOver;
    public CanvasGroup debug;
    public PrestigeManager prestigeManager;
    public SoundManager soundManager;
    
    
    

    private void Start()
    {
        NewGame();
    }
    
    public void NewGame() //public so it can be hooked to a UI button
    {
        gameOver.alpha = 0f;
        gameOver.interactable = false;//that way it doesnt get in the way while being invisible

        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;//when a script is disabled, update will not get called
    }

    public void AudioSwitch() //for audio button
    {
        soundManager.Speaker.enabled = !soundManager.Speaker.enabled; //alternates between on and off
    }
  public void GameOver() //handles a lost game and the loss menu
    {
        board.enabled = false;
        gameOver.interactable = true;
        StartCoroutine(Fade(gameOver, 1f, 1f)); //fading in, 1 second delay
    }
    public void QuitGame()
    {
        prestigeManager.Save();
        Application.Quit();
    }

    public void Prestige()
    {
        prestigeManager.AddPrestige();
        prestigeManager.Save();
        prestigeManager.UpdateUI();
        soundManager.PlayAudio("2048");
        NewGame();
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay) //adds a fade effect to canvasgroup, makes it feel better
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha; //aka opacity

        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from,to, elapsed/duration);
            elapsed += Time.deltaTime;
            yield return null;
            //same as animate but instead of changing position its changing opacity
        }
        canvasGroup.alpha = to;
    }
}
