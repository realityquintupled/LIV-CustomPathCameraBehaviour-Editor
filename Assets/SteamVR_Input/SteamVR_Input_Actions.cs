//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Valve.VR
{
    using System;
    using UnityEngine;
    
    
    public partial class SteamVR_Actions
    {
        
        private static SteamVR_Action_Boolean p_controls_Grab;
        
        private static SteamVR_Action_Vector2 p_controls_Touchpad;
        
        private static SteamVR_Action_Boolean p_controls_CreatePoint;
        
        private static SteamVR_Action_Boolean p_controls_DeletePoint;
        
        private static SteamVR_Action_Pose p_controls_Pose;
        
        private static SteamVR_Action_Vibration p_controls_Haptics;
        
        public static SteamVR_Action_Boolean controls_Grab
        {
            get
            {
                return SteamVR_Actions.p_controls_Grab.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Vector2 controls_Touchpad
        {
            get
            {
                return SteamVR_Actions.p_controls_Touchpad.GetCopy<SteamVR_Action_Vector2>();
            }
        }
        
        public static SteamVR_Action_Boolean controls_CreatePoint
        {
            get
            {
                return SteamVR_Actions.p_controls_CreatePoint.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Boolean controls_DeletePoint
        {
            get
            {
                return SteamVR_Actions.p_controls_DeletePoint.GetCopy<SteamVR_Action_Boolean>();
            }
        }
        
        public static SteamVR_Action_Pose controls_Pose
        {
            get
            {
                return SteamVR_Actions.p_controls_Pose.GetCopy<SteamVR_Action_Pose>();
            }
        }
        
        public static SteamVR_Action_Vibration controls_Haptics
        {
            get
            {
                return SteamVR_Actions.p_controls_Haptics.GetCopy<SteamVR_Action_Vibration>();
            }
        }
        
        private static void InitializeActionArrays()
        {
            Valve.VR.SteamVR_Input.actions = new Valve.VR.SteamVR_Action[] {
                    SteamVR_Actions.controls_Grab,
                    SteamVR_Actions.controls_Touchpad,
                    SteamVR_Actions.controls_CreatePoint,
                    SteamVR_Actions.controls_DeletePoint,
                    SteamVR_Actions.controls_Pose,
                    SteamVR_Actions.controls_Haptics};
            Valve.VR.SteamVR_Input.actionsIn = new Valve.VR.ISteamVR_Action_In[] {
                    SteamVR_Actions.controls_Grab,
                    SteamVR_Actions.controls_Touchpad,
                    SteamVR_Actions.controls_CreatePoint,
                    SteamVR_Actions.controls_DeletePoint,
                    SteamVR_Actions.controls_Pose};
            Valve.VR.SteamVR_Input.actionsOut = new Valve.VR.ISteamVR_Action_Out[] {
                    SteamVR_Actions.controls_Haptics};
            Valve.VR.SteamVR_Input.actionsVibration = new Valve.VR.SteamVR_Action_Vibration[] {
                    SteamVR_Actions.controls_Haptics};
            Valve.VR.SteamVR_Input.actionsPose = new Valve.VR.SteamVR_Action_Pose[] {
                    SteamVR_Actions.controls_Pose};
            Valve.VR.SteamVR_Input.actionsBoolean = new Valve.VR.SteamVR_Action_Boolean[] {
                    SteamVR_Actions.controls_Grab,
                    SteamVR_Actions.controls_CreatePoint,
                    SteamVR_Actions.controls_DeletePoint};
            Valve.VR.SteamVR_Input.actionsSingle = new Valve.VR.SteamVR_Action_Single[0];
            Valve.VR.SteamVR_Input.actionsVector2 = new Valve.VR.SteamVR_Action_Vector2[] {
                    SteamVR_Actions.controls_Touchpad};
            Valve.VR.SteamVR_Input.actionsVector3 = new Valve.VR.SteamVR_Action_Vector3[0];
            Valve.VR.SteamVR_Input.actionsSkeleton = new Valve.VR.SteamVR_Action_Skeleton[0];
            Valve.VR.SteamVR_Input.actionsNonPoseNonSkeletonIn = new Valve.VR.ISteamVR_Action_In[] {
                    SteamVR_Actions.controls_Grab,
                    SteamVR_Actions.controls_Touchpad,
                    SteamVR_Actions.controls_CreatePoint,
                    SteamVR_Actions.controls_DeletePoint};
        }
        
        private static void PreInitActions()
        {
            SteamVR_Actions.p_controls_Grab = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/Controls/in/Grab")));
            SteamVR_Actions.p_controls_Touchpad = ((SteamVR_Action_Vector2)(SteamVR_Action.Create<SteamVR_Action_Vector2>("/actions/Controls/in/Touchpad")));
            SteamVR_Actions.p_controls_CreatePoint = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/Controls/in/CreatePoint")));
            SteamVR_Actions.p_controls_DeletePoint = ((SteamVR_Action_Boolean)(SteamVR_Action.Create<SteamVR_Action_Boolean>("/actions/Controls/in/DeletePoint")));
            SteamVR_Actions.p_controls_Pose = ((SteamVR_Action_Pose)(SteamVR_Action.Create<SteamVR_Action_Pose>("/actions/Controls/in/Pose")));
            SteamVR_Actions.p_controls_Haptics = ((SteamVR_Action_Vibration)(SteamVR_Action.Create<SteamVR_Action_Vibration>("/actions/Controls/out/Haptics")));
        }
    }
}