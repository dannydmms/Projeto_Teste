using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageble 
{
    public void TakeDamage(Vector3 inpulseDirection, float punchForce);
}
