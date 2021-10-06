using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//!< AR scene states.
public enum AR_Scene_States
{
    AR_SCENE_STATE_SELECT_DISPLACEMENT_TYPE,
    AR_SCENE_HOLD_PLANE_YOUR_TYPE,
    AR_SCENE_SPECIFY_PLANE,
    AR_SCENE_PLAY_THE_GAME
};


//!< We use this enum. for determining the displacement type of AR scene.
public enum AR_Scene_Displacement_Types
{
    AR_SCENE_TYPE_UNDEFINED,
    AR_SCENE_TYPE_WALL,   //!< We use that if scene is established onto wall 
    AR_SCENE_TYPE_GROUND  //!< We use that if scene is established onto ground.
};