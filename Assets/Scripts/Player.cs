using UnityEngine;

public class Player : MonoBehaviour
{
    
}

[System.Serializable]
public class UserData
{
    public string Name;
    public int Lv;
    public int Exp;
    public int Gold;
    public int SkillLv;
    public float CurHP; //현재
    public float MaxHP; //최대
    public float AttackPower;
}
