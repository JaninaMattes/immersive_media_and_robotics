﻿namespace VRTK.Examples
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class StartLevel : MonoBehaviour
    {
        // Select the correct level index for a scene
        public int LevelIndex;
        public VRTK_InteractableObject sceneChange;
        private ManageScenes sceneManagement;
        private GameObject stagingObjects;
        public string sceneLoadingName;

        void Start()
        {
            sceneManagement = GameObject.FindGameObjectWithTag("ScenMgr").GetComponent<ManageScenes>();
            stagingObjects = GameObject.FindGameObjectWithTag("stage");
            sceneChange = this.GetComponent<VRTK_InteractableObject>();       
        }

        protected virtual void OnEnable()
        {
            sceneChange = (sceneChange == null ? GetComponent<VRTK_InteractableObject>() : sceneChange);

            if (sceneChange != null)
            {
                sceneChange.InteractableObjectUsed += InteractableObjectUsed;
                sceneChange.InteractableObjectUnused += InteractableObjectUnused;

                sceneChange.InteractableObjectTouched += InteractableObjectTouched;
                sceneChange.InteractableObjectUntouched += InteractableObjectUntouched;
            }
        }

        protected virtual void OnDisable()
        {
            if (sceneChange != null)
            {
                sceneChange.InteractableObjectUsed -= InteractableObjectUsed;
                sceneChange.InteractableObjectUnused -= InteractableObjectUnused;

                sceneChange.InteractableObjectTouched -= InteractableObjectTouched;
                sceneChange.InteractableObjectUntouched -= InteractableObjectUntouched;
            }
        }

        protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
        {
            //LightmapSettings.lightmaps = null;
            DestroyImmediate(stagingObjects);
            sceneManagement.sceneLoadingName = sceneLoadingName;
            sceneManagement.levelInd = LevelIndex;
            sceneManagement.loadLevel= true;
        }

        protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
        {

        }

        protected virtual void InteractableObjectTouched(object sender, InteractableObjectEventArgs e)
        {

            //gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Shader/Hologram"); //changes shader at runtime. without settings set to a color or rimpower, it gets the invisible effect on touch
            //gameObject.GetComponent<Renderer>().material.SetFloat("_RimPower", 1.0f); //changes rimpower of glowshader to glow on touch
        }

        protected virtual void InteractableObjectUntouched(object sender, InteractableObjectEventArgs e)
        {
            //gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Shader/GlowShader");
            //gameObject.GetComponent<Renderer>().material.SetFloat("_RimPower", 5.0f);
        }
    }
}
