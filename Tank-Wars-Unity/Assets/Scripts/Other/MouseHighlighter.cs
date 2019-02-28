// Source: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnMouseOver.html

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHighlighter : MonoBehaviour
{
    Color m_MouseOverColor = Color.yellow;
    Color m_OriginalColor;
    MeshRenderer m_Renderer;

    void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();
        m_OriginalColor = m_Renderer.material.color;
    }

    void OnMouseOver() {
        // Change the color of the GameObject when the mouse is over GameObject
        m_Renderer.material.color = m_MouseOverColor;
    }

    void OnMouseExit()
    {
        // Reset the color of the GameObject back to normal
        m_Renderer.material.color = m_OriginalColor;
    }
}