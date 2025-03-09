using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSText : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text m_Text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = GPSManager.instance.GPSPos;
        m_Text.text = $"Lat: {pos.x}\nLong: {pos.y} ";
    }
}
