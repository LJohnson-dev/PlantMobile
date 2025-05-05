using System;
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
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds((long)Input.location.lastData.timestamp);
        m_Text.text =
            $"Lat: {pos.x}\n" +
            $"Long: {pos.y}\n" +
            $"Status:{Input.location.status.ToString()}\n" +
            $"Timestamp: {dateTimeOffset.ToString("HH:mm:ss")}\n" +
            $"HAcc: {Input.location.lastData.horizontalAccuracy}\n" +
            $"VAcc: {Input.location.lastData.verticalAccuracy}";
    }
}
