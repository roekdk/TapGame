using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    string savePath;
    public bool saveChack;
    public UserData userData;

    private void Awake() 
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        savePath= Application.persistentDataPath; //저장파일 위치
        saveChack=File.Exists(savePath+"/PlayerData.txt"); //세입파일이 있는지 확인
        userData = new UserData();
    }

    public void SavePlayerData(string json) //[jeon화]한 자료를 파일로 생성 
    {   
        File.WriteAllText(savePath + "/PlayerData.txt",json); 
        Debug.Log("저장완료 : " + savePath + "/PlayerData.txt"); 
    }
    public UserData LoadPlayerData()
    {
        if(!saveChack) return null;
        //저장된파일없으면 null반환 (예외처리)
        
        string loadData =File.ReadAllText(savePath + "/PlayerData.txt");
        //경로의 파일을 읽어옴
        return JsonUtility.FromJson<UserData>(loadData);
        //읽어온 정보를 역직렬화함
    }        
    
    public void OnLoadDate() //버튼 로드하기위해
    {
        userData=LoadPlayerData();
        GameManager.Instance.StartGame();
    }

    public void OnNewGameDate() //저장된정보 없으면 초기화후 바로 저장
    {        
        userData.Lv = 1;
        userData.SkillLv=0;
        userData.Exp = 0;
        userData.CurHP = 100f;
        userData.MaxHP = 100f;
        userData.Name = "Player";
        userData.AttackPower = 10;
        userData.Gold=10;

        string saveData = JsonUtility.ToJson(userData);
        SavePlayerData(saveData);
        GameManager.Instance.StartGame();
    }
}



