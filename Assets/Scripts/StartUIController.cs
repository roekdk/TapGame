using UnityEngine;
using UnityEngine.UI;

public class StartUIController : MonoBehaviour
{       
    public Button loadGame;
    public Button newGame;    

    void Start()
    {
        SaveDateChack();       
    }        
    
    public void BtnLoadDate()
    {
        DataManager.Instance.OnLoadDate();        
    }

    public void BtnNewGameDate()
    {
        DataManager.Instance.OnNewGameDate();        
    }
    
    public void BtnExitGame()
    {
        Application.Quit();
    }

    void SaveDateChack()
    {
        if(DataManager.Instance.saveChack)
        {
            loadGame.gameObject.SetActive(true);
            newGame.gameObject.SetActive(false);
        }
        else
        {
            loadGame.gameObject.SetActive(false);
            newGame.gameObject.SetActive(true);
        }        
    }
    
}
