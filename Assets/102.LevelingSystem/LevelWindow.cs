/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class LevelWindow : MonoBehaviour {

    public Text levelText;
    private Image experienceBarImage;
    private LevelSystem levelSystem;
    //상대의 레퍼런스를 매개변수로 받아가지고 디커플링을 했구나
    private LevelSystemAnimated levelSystemAnimated;

    private void Awake() {
        //levelText = transform.Find("levelText").GetComponent<Text>();
        experienceBarImage = transform.Find("experienceBar").Find("bar").GetComponent<Image>();
        
        Debug.Log("experin"  + experienceBarImage);

    }

    public void SetExperienceBarSize(float experienceNormalized) {
        Debug.Log("되겠지?");
        experienceBarImage.fillAmount = experienceNormalized;
    }

    public void SetLevelNumber(int levelNumber) {
        levelText.text = "LEVEL\n" + (levelNumber + 1);
    }

    public void SetLevelSystem(LevelSystem levelSystem) {
        this.levelSystem = levelSystem;
    }

    //매개변수를 활용한 초기화 좋네

   
    public void SetLevelSystemAnimated(LevelSystemAnimated levelSystemAnimated) {
        // Set the LevelSystemAnimated object
        this.levelSystemAnimated = levelSystemAnimated;

        // Update the starting values
        SetLevelNumber(levelSystemAnimated.GetLevelNumber());
        SetExperienceBarSize(levelSystemAnimated.GetExperienceNormalized());

        // Surbscribe to the changed events
        levelSystemAnimated.OnExperienceChanged += LevelSystemAnimated_OnExperienceChanged;
        levelSystemAnimated.OnLevelChanged += LevelSystemAnimated_OnLevelChanged;
    }

    private void LevelSystemAnimated_OnLevelChanged(object sender, System.EventArgs e) {
        // Level changed, update text
        SetLevelNumber(levelSystemAnimated.GetLevelNumber());
    }

    private void LevelSystemAnimated_OnExperienceChanged(object sender, System.EventArgs e) {
        // Experience changed, update bar size
        SetExperienceBarSize(levelSystemAnimated.GetExperienceNormalized());
    }
}
