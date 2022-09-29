// *---- Purpose of this file: ----*
// Manage the master audio.

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float masterVolume = 1.0f;

    public void audioChanged (float value)
    {
        masterVolume = value;
    }
}
