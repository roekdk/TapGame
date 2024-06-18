======필수=======
1) 클릭이벤트 처리를 몰라서 버튼처리으로 했습니다.. 
    캔버스 UI에 body라는 오브젝트를 만들고 
    GameManager.cs 안에 OnAttack() 버튼화했습니다.
   
2) 코루틴으로 구현은 했습니다
    MainUIController.cs 안에 OnAuto() 버튼화로
    StartCoroutine(AutoAtk(skillTime)) 동작시킵니다
   
3) , 5)은 Gold로 같이 구현했고
  
4)는 인벤토리나 아이템형식으로는 구현을 제시간에 못할거 같아서 변형해서 구현했습니다. 

========선택========
1)클릭시 적에게서 파티클적용했습니다.
    GameManager.cs 안에 OnAttack() 동작시 EffectParticle.Play() 작동
2)배경과 공격,아이템 구매시 나오게 구현했습니다.
    한곳에 모으질 못하고 GameManager.cs는 OnAttack() 
    각각 업글버튼은 MainUIController.cs 안에 
    배경은 dataManager.cs에 구현했습니다.
4)저장,불러오기는 구현했는데 데이터 삭제를 구현 못했습니다.
