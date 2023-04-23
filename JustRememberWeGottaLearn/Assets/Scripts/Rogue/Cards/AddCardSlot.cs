using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCardSlot : Card
{
    protected override void PerformUpgrade()
    {
        Progression.Instance.IncrementNumberChoice();
    }
}
