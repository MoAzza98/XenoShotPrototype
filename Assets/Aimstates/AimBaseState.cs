using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimBaseState
{
    public abstract void EnterState(AimStateManager aim);
    public abstract void UpdateState(AimStateManager aim);
}
