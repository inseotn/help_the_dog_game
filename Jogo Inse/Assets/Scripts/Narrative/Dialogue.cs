using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Each dialogue comprises a character name string along with their speech as string[].
/// </summary>
[System.Serializable]
public class Dialogue
{
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;
}