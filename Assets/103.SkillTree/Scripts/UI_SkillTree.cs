

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class UI_SkillTree : MonoBehaviour {

    [SerializeField] private Material skillLockedMaterial;
    [SerializeField] private Material skillUnlockableMaterial;
    [SerializeField] private SkillUnlockPath[] skillUnlockPathArray;
    [SerializeField] private Sprite lineSprite;
    [SerializeField] private Sprite lineGlowSprite;

    private PlayerSkills playerSkills;
    //스킬버튼을 담는 곳인가
    private List<SkillButton> skillButtonList;
    private TMPro.TextMeshProUGUI skillPointsText;

    private void Awake() {
        //transform.Find("DashBtn").GetComponent<Button_UI>().ClickFunc = () =>
        //{
        //    Debug.Log("Click!");
        //};
        skillPointsText = transform.Find("SkillPointText").GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void SetPlayerSkills(PlayerSkills playerSkills) {
        this.playerSkills = playerSkills;

        skillButtonList = new List<SkillButton>();
        skillButtonList.Add(new SkillButton(transform.Find("DashBtn"), playerSkills, PlayerSkills.SkillType.Dash, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("ChargedAttackBtn"), playerSkills, PlayerSkills.SkillType.ChargedAttack, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("ParringBtn"), playerSkills, PlayerSkills.SkillType.Parring, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("MoveSpeed1Btn"), playerSkills, PlayerSkills.SkillType.MoveSpeed_1, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("MoveSpeed2Btn"), playerSkills, PlayerSkills.SkillType.MoveSpeed_2, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("AttackSpeed1Btn"), playerSkills, PlayerSkills.SkillType.AttackSpeed_1, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("AttackSpeed2Btn"), playerSkills, PlayerSkills.SkillType.AttackSpeed_2, skillLockedMaterial, skillUnlockableMaterial));


        playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
        playerSkills.OnSkillPointsChanged += PlayerSkills_OnSkillPointsChanged;

        UpdateVisuals();
        UpdateSkillPoints();
    }

    private void PlayerSkills_OnSkillPointsChanged(object sender, System.EventArgs e) {
        UpdateSkillPoints();
    }

    private void PlayerSkills_OnSkillUnlocked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e) {
        UpdateVisuals();
    }

    private void UpdateSkillPoints() {
        skillPointsText.SetText(playerSkills.GetSkillPoints().ToString());
    }

    private void UpdateVisuals() {
        foreach (SkillButton skillButton in skillButtonList) {
            skillButton.UpdateVisual();
        }

        // Darken all links
        foreach (SkillUnlockPath skillUnlockPath in skillUnlockPathArray) {
            foreach (Image linkImage in skillUnlockPath.linkImageArray) {
                linkImage.color = new Color(.5f, .5f, .5f);
                linkImage.sprite = lineSprite;
            }
        }
        
        foreach (SkillUnlockPath skillUnlockPath in skillUnlockPathArray) {
            if (playerSkills.IsSkillUnlocked(skillUnlockPath.skillType) || playerSkills.CanUnlock(skillUnlockPath.skillType)) {
                // Skill unlocked or can be unlocked //언락이 됬거나 언락을 시킬수 있을때는
                foreach (Image linkImage in skillUnlockPath.linkImageArray) {
                    linkImage.color = Color.white;
                    linkImage.sprite = lineGlowSprite;
                }
            }
        }
    }

    /*
     * Represents a single Skill Button
     * */
    private class SkillButton {

        private Transform transform;
        private Image image;
        private Image backgroundImage;
        private PlayerSkills playerSkills;
        private PlayerSkills.SkillType skillType;
        private Material skillLockedMaterial;
        private Material skillUnlockableMaterial;

        public SkillButton(Transform transform, PlayerSkills playerSkills, PlayerSkills.SkillType skillType, Material skillLockedMaterial, Material skillUnlockableMaterial) {
            this.transform = transform;
            this.playerSkills = playerSkills;
            this.skillType = skillType;
            this.skillLockedMaterial = skillLockedMaterial;
            this.skillUnlockableMaterial = skillUnlockableMaterial;

            //각 버튼을 이렇게만들수 있구나!
            image = transform.Find("image").GetComponent<Image>();
            backgroundImage = transform.Find("background").GetComponent<Image>();


            //버튼을 눌렀을때
            transform.GetComponent<Button_UI>().ClickFunc = () => {
                if (!playerSkills.IsSkillUnlocked(skillType)) {
                    Debug.Log("스킬언락이 안됬습니다");
                    // Skill not yet unlocked
                    if (!playerSkills.TryUnlockSkill(skillType)) {
                        Debug.Log("스킬언락 불가!");
                        //Tooltip_Warning.ShowTooltip_Static("Cannot unlock " + skillType + "!");
                    }
                }
            };
        }

        public void UpdateVisual() {
            //언락이 됬다면
            //이렇게 바로 시스템을 완성시키는게 아니라 테스트하면서 문제를 하나씩 해결하면서
            //구조화를 시키는 구나
            //이미 스킬이 언락됬다면 
            if (playerSkills.IsSkillUnlocked(skillType)) {
                image.material = null;
                backgroundImage.material = null;
            } else {
                if (playerSkills.CanUnlock(skillType)) {
                    //언락이 가능하다면
                    image.material = skillUnlockableMaterial;
                    backgroundImage.color = UtilsClass.GetColorFromString("4B677D");
                    transform.GetComponent<Button_UI>().enabled = true;
                    //언락이 
                } else {
                    image.material = skillLockedMaterial; //이미지가 잠긴걸론
                    backgroundImage.color = new Color(.3f, .3f, .3f);
                    transform.GetComponent<Button_UI>().enabled = false;
                    //더이상 누를수 없게 
                }
            }
        }

    }


    [System.Serializable]
    public class SkillUnlockPath {
        public PlayerSkills.SkillType skillType;
        public Image[] linkImageArray;
    }

}
