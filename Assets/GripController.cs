using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripController : MonoBehaviour
{
    [SerializeField] private Collider2D[] wheelColliders;

    public void UpdateGrip(float grip)
    {
        foreach (Collider2D wheelCollider in wheelColliders)
        {
            if (wheelCollider.sharedMaterial.friction != grip)
            {
                wheelCollider.sharedMaterial = new PhysicsMaterial2D
                {
                    friction = grip,
                    bounciness = 0.1f
                };
            }
        }
    }
}
