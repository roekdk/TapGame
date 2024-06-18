using System.Collections;
using TMPro;
using UnityEngine;

public class MainUIController : MonoBehaviour
{
    public TextMeshProUGUI PayAutoTxt;
    public TextMeshProUGUI PayHealTxt;
    public TextMeshProUGUI PayAtkUpTxt;
    public TextMeshProUGUI PaySkillUpTxt;
    public TextMeshProUGUI PayMaxHpUpTxt;
    public AudioClip itemClip;  
    public AudioSource audioSound; 

    UserData data;
    float skillTime;

    void Start() 
    {        
        if(GameManager.Instance.playerState!=null)
        {
            data=GameManager.Instance.playerState;   
            //씬을 나눠서 작업해야햇는데.. 나누질 않아서 없는데이터를 불러와서 에러가 남..
        }
        audioSound = GetComponent<AudioSource>();
    }
   
    public void OnAuto()  //인터페이스 어케 썻더라.....
    {   
        skillTime = 3f+data.SkillLv;
        int needGold= 60 + (data.SkillLv*10);
        if(data.Gold>=needGold)
        {
            data.Gold-=needGold;

            StartCoroutine(AutoAtk(skillTime));
            audioSound.PlayOneShot(itemClip);
        }

        PayAutoTxt.text = needGold.ToString()+"G";
    }

    public void OnHeal()
    {
        int needGold= 40 + (data.Lv*20);
        if(data.Gold>=needGold)
        {
            data.Gold-=needGold;

            data.CurHP=data.MaxHP; //시간이 없어서 걍 최대치로 ...
            audioSound.PlayOneShot(itemClip);
        }

        PayHealTxt.text = needGold.ToString()+"G";
    }

    public void OnAtkUp()
    {
        int needGold= 80 + ((int)data.AttackPower * 20);
        if(data.Gold>=needGold)
        {
            data.Gold-=needGold;

            data.AttackPower += 10f; //시간이 없어서 ...
            audioSound.PlayOneShot(itemClip);
        }

        PayAtkUpTxt.text = needGold.ToString()+"G";
    }
    public void OnSkillUp()
    {
        int needGold= 100 + (data.SkillLv * 100);
        if(data.Gold>=needGold)
        {
            data.Gold-=needGold;

            data.SkillLv++; //시간이 없어서 ...
            audioSound.PlayOneShot(itemClip);
        }
        PaySkillUpTxt.text = needGold.ToString()+"G";
    }
    public void OnMaxHpUp()
    {
        int needGold= 100+(((int)data.MaxHP-100)*5);
        if(data.Gold>=needGold)
        {
            data.Gold-=needGold;

            data.MaxHP+=10; //시간이 없어서 ...
            audioSound.PlayOneShot(itemClip);
        }
        PayMaxHpUpTxt.text = needGold.ToString()+"G";
    }

    IEnumerator AutoAtk(float time)
    {   
        GameManager.Instance.isSkill=true;
        for(float i =0; i < time; i += 0.2f)
        {
            GameManager.Instance.OnAttack();
            WaitForSeconds t = new WaitForSeconds(0.2f);
            yield return t;
        } 
        GameManager.Instance.isSkill=false;
        yield return null;
    }
}
