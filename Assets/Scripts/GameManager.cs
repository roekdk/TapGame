using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int idx = 0;
    public int killCount=0;   
    bool isDead=false; 
    public bool isSkill=false;

    public TextMeshProUGUI LvTxt;
    public TextMeshProUGUI ExpTxt;
    public TextMeshProUGUI AttackTxt;
    public TextMeshProUGUI GoldTxt;
    public SpriteRenderer enemyImage;
    public ParticleSystem EffectParticle;
    public GameObject player; 
    public GameObject enemy;
    public Slider playerHpSlider;
    public Slider enemyHpSlider;
    public AudioClip damageClip;  
    public AudioSource audioSound; 
    
    private void Awake() 
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }    
    UserData data;
    public UserData playerState;
    public EnemyData enemyState;

    public void StartGame()
    {           
        data = DataManager.Instance.userData;
        playerState = new UserData();
        enemyState = new EnemyData();
        PlayerState();
        EnemyState();
        audioSound = GetComponent<AudioSource>();
    }

    void PlayerState() //저장된 값을 실제 플레이어오브젝트에 적용
    {
        playerState.Lv=data.Lv;
        playerState.Exp=data.Exp;
        playerState.CurHP=data.MaxHP;
        playerState.MaxHP=data.MaxHP;
        playerState.Name = data.Name;
        playerState.AttackPower=data.AttackPower;
        playerState.Gold=data.Gold;
        playerState.SkillLv=data.SkillLv;
    }
      
    void EnemyState() 
    {
        enemyState.Lv = 1;
        enemyState.CurHP = 100+(enemyState.Lv*20);
        enemyState.MaxHP = 100+(enemyState.Lv*20);
        enemyState.AttackPower = enemyState.Lv;
        enemyState.Exp = 10+(enemyState.Lv*5);
        enemyState.Gold = 10+(enemyState.Lv*5);
    } 
    public void SaveDate()
    {        
        string saveData = JsonUtility.ToJson(playerState); //직렬화[자료를 jeon화]        
        DataManager.Instance.SavePlayerData(saveData);
    }
    private void OnApplicationQuit() //게임이 끝날때 동작하는 매서드
    {        
        //SaveDate();
    }
    
    public void Setting(int num)
    {
        idx = num;
        enemyImage.sprite = Resources.Load<Sprite>($"enemy{idx}");
    }    

    void Update() 
    {
        if(playerState.MaxHP>=100) //스타트씬만들고 했다면 없어도 되는것을 ...
        {
        UpdateHealthUI();
        LvTxt.text = playerState.Lv.ToString();
        ExpTxt.text = playerState.Exp.ToString();
        AttackTxt.text=playerState.AttackPower.ToString();
        GoldTxt.text=playerState.Gold.ToString();        
        }        
    }  

    private void UpdateHealthUI()
    {   
        playerHpSlider.value = playerState.CurHP / playerState.MaxHP;
        enemyHpSlider.value = enemyState.CurHP / enemyState.MaxHP;
    }    

    public void OnAttack()
    {  
       if(isDead) return; 
       if(!isDead)
       { 
            Debug.Log("Atk");
            enemyState.CurHP -= playerState.AttackPower;
            if(!isSkill) playerState.CurHP -= enemyState.AttackPower;            
            audioSound.PlayOneShot(damageClip);
            EffectParticle.Play();

            if(enemyState.CurHP <= 0)
            { 
                enemyState.CurHP=0;
                isDead=true;
                EnemyDead();
                Debug.Log("Dead");
            }
            else if(playerState.CurHP<=0)
            {
                playerState.CurHP=0;
                isDead=true;
                PlayerDead();
            }
       }
    }
    void EnemyDead()
    {       
        playerState.Exp += enemyState.Exp;
        playerState.Gold += enemyState.Gold;
        
        killCount++;
        if(playerState.Exp >= playerState.Lv*100)
        {
            LavelUp();
        }

        if(killCount ==10)
        {
            enemyState.Lv++;
            killCount=0;
        }
        StartCoroutine(EnemyReset(1f));    
    }
    
    void LavelUp()
    {
        playerState.Lv++;
        playerState.Exp=0;
        playerState.CurHP=playerState.MaxHP;
        playerState.MaxHP += 10;
        playerState.AttackPower += 2;
    }
    void PlayerDead()
    {
        killCount=0;
        enemyState.Lv--;
        playerState.CurHP=playerState.MaxHP;
        playerState.Gold -= playerState.Lv*10;
        StartCoroutine(EnemyReset(1f));
    }

    IEnumerator EnemyReset(float time)
    {   
        enemy.SetActive(false);
        WaitForSeconds t = new WaitForSeconds(time);
        yield return t;
        int r =Random.Range(0,4);
        Setting(r);  
        EnemyState();      
        enemy.SetActive(true);
        isDead=false;
        yield return null;
    }    
}
