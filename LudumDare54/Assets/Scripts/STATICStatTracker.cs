using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class STATICStatTracker
{
    public const int noteCount = 8;
    public static int deaths = 0;
    public static int notesRead;
    public static BitArray noteStatuses = new BitArray(noteCount);
}
