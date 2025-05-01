using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugOSMNode : MonoBehaviour
{
    [SerializeField] private Renderer m_Target;

    public void SetColour(Color colour)
    {
        m_Target.material.color = colour;
    }
}
