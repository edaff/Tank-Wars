// Source: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnMouseOver.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHighlighter : MonoBehaviour
{
    Color m_MouseOverColor = Color.grey;
    Color m_OriginalColor;
    MeshRenderer m_Renderer;
    int badasslock;

    void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();
        m_OriginalColor = m_Renderer.material.color;
        badasslock = 0;
    }

    void OnMouseOver() {
        // Change the color of the GameObject when the mouse is over GameObject
        if(badasslock < 1) {
            m_Renderer = GetComponent<MeshRenderer>();
            m_OriginalColor = m_Renderer.material.color;
            badasslock++;
        }

        m_Renderer.material.color = m_MouseOverColor;
    }

    void OnMouseExit()
    {
        // Reset the color of the GameObject back to normal
        badasslock = 0;
        m_Renderer.material.color = m_OriginalColor;
    }
}