using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.QC;
public class Dummy : Card
{
    // Start is called before the first frame update
    
     
    protected override void PerformUpgrade()
    {
        //Do nothing
        Debug.Log("Uses a dummy card");
    }
}
