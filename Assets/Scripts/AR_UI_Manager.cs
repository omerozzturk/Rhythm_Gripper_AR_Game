using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AR_UI_Manager : MonoBehaviour
{
    public GameObject AR_type_selection_panel_gameobject;
    public GameObject song_selection_panel_gameobject;
    public Text       AR_type_text;

    private void Start()
    {
        AR_type_text                       = GameObject.Find("AR_Type_Text").GetComponent<Text>();
    }

    public void On_Select_AR_Type(int AR_type) 
    {
        Rhythm_Gripper_Manager.current_AR_orientation_type = (AR_Scene_Displacement_Types)AR_type;

        if ((AR_Scene_Displacement_Types)AR_type == AR_Scene_Displacement_Types.AR_SCENE_TYPE_GROUND)
        {
            AR_type_text.text = "AR Type: Horizontal";
        }
        else 
        {
            AR_type_text.text = "AR Type: Vertical";
        }

        //!< Close AR selection panel and open up song selection panel
        song_selection_panel_gameobject.SetActive(true);
        AR_type_selection_panel_gameobject.SetActive(false);
    }

    public void On_Select_Song(int current_song)
    {
        Rhythm_Gripper_Manager.current_playing_song = (Playing_Songs)current_song;

        //!< Go to main scene
        SceneManager.LoadScene("Main_Scene", LoadSceneMode.Single);
    }

    public void On_Back_Menu() 
    {
        //!< Open AR selection panel and close song selection panel
        AR_type_selection_panel_gameobject.SetActive(true);
        song_selection_panel_gameobject.SetActive(false);
    }

}
