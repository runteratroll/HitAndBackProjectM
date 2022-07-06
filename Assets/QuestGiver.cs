using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;

    //플레이어정보를 스크립터블로 해서 정리할까 
    public GameObject questWindow;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI experienceText;
    public TextMeshProUGUI goldText;
    public PlayerMove playerMove;


    //자기에서만 다르게 표현
    //로직과 보여주는 걸 함께하진 않겠지
    public void OpenQuestWindow()
    {
        questWindow.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        experienceText.text = quest.experienceReward.ToString();
        goldText.text = quest.goldReward.ToString();
    }


    //퀘스트기버는 클래스가 맞지않을때 버튼눌렀을떄 확인해주는 로직인가
    
    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        quest.isActive = true; //수락하기 bool로 UI는 그건가 
        //give to player
        playerMove.quest = quest;

        //다른 클래스에다가 클래스를 직접넣는 이유가 뭐야 
        
    }
}
