using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum Object_Placement_Types
{
    PLACEMENT_CENTER,
    PLACEMENT_LEFT,
    PLACEMENT_RIGHT
};

public class Signal_Processing_Manager : MonoBehaviour
{
    private Sound_Rhythm_Manager sound_rhythm_manager;
    private bool                 rhythm_genarator_is_active_b = false;
    private int                  audio_source_duration;

    public  List<GameObject>                          notes_type_gameobject_list  =  new List<GameObject>(2);
    private Dictionary<Object_Placement_Types, float> horizontal_notes_placement  = new Dictionary<Object_Placement_Types, float>();
    private float                                     horizontal_y_axis;
    private int                                       previos_object_placement_type = -1;

    private void Start()
    {
        sound_rhythm_manager = gameObject.GetComponent<Sound_Rhythm_Manager>();

        horizontal_notes_placement.Add(Object_Placement_Types.PLACEMENT_CENTER,  0f);
        horizontal_notes_placement.Add(Object_Placement_Types.PLACEMENT_LEFT,   -2f);
        horizontal_notes_placement.Add(Object_Placement_Types.PLACEMENT_RIGHT,   2f);
    }

    public void Generate_Rhythms(Vector3 reference_point_vec3, Quaternion rotation)
    {
        audio_source_duration = sound_rhythm_manager.Get_Duration_Current_Music();
        horizontal_y_axis     = reference_point_vec3.y;

        //!< Give soul to the objects that we crate
        for (int i = 0; i < (audio_source_duration * 2); i++)
        {

            //!< We use horizontal background.
            if (Rhythm_Gripper_Manager.current_AR_orientation_type == AR_Scene_Displacement_Types.AR_SCENE_TYPE_GROUND)
            {
                Object_Placement_Types obj_to_be_placed = (Object_Placement_Types)Random.Range(0, 3);

                Vector3 game_obj_pos = new Vector3(reference_point_vec3.x + horizontal_notes_placement[obj_to_be_placed], horizontal_y_axis, reference_point_vec3.z);

                int prefab_type = Random.Range(0, 2);

                GameObject new_rhythm_object = Instantiate(notes_type_gameobject_list[prefab_type], game_obj_pos, rotation);

                new_rhythm_object.AddComponent<Note_Process_Manager>();

                new_rhythm_object.tag = "Note_Rhythm";

                horizontal_y_axis -= 2;
            }
            else
            {
                //!< We use vertical background.

            }

        }
    }
    
}
