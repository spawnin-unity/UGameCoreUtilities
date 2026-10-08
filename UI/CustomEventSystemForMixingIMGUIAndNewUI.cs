using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UGameCore.Utilities
{

    public class CustomEventSystemForMixingIMGUIAndNewUI : EventSystem
    {
        bool m_wasAnyElementActiveLastFrame = false;


        protected override void Start()
        {
            base.Start();

#if !UNITY_SERVER
            // Dedicated Server remove o IMGUI (Dedicated Server Optimizations) e
            // GUIUtility.hotControl vira SIGSEGV nativo. Sem UI no servidor, nao ha
            // IMGUI para arbitrar com o EventSystem.
            StartCoroutine(this.Coroutine());
#endif
        }

        IEnumerator Coroutine()
        {
            while (true)
            {
                yield return null;
                m_wasAnyElementActiveLastFrame = GUIUtility.hotControl != 0;
            }
        }

        protected override void Update()
        {
            if (!m_wasAnyElementActiveLastFrame)
                base.Update();
        }

    }

}