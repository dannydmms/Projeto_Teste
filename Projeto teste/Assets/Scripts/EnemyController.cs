using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageble, IPickable
{
    Collider[] allColiders;
    Rigidbody[] allRigidbody;
    GameObject parent;
    bool canPick = false;
    private void Start()
    {
        SetRagdoll();
        allColiders = GetComponentsInChildren<Collider>();
        //foreach (Collider value in allColiders)
        //{
        //    value.isTrigger = true;
        //}
        parent = GameObject.Find("Stack");
    }

    private void SetRagdoll()
    {
        allRigidbody = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in allRigidbody)
        {
            rb.isKinematic = true;
        }
    }

    public void TakeDamage(Vector3 impulseDirection, float punchForce)
    {
        allRigidbody = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in allRigidbody)
        {
            rb.isKinematic = false;
            rb.AddForce(impulseDirection * punchForce, ForceMode.Impulse);

        }
        if (!canPick)
        {
            StartCoroutine(ChangePick());
        }
    }
    IEnumerator ChangePick()
    {
        yield return new WaitForSeconds(2f);
        canPick = true;
    }
    public void PickObject(int current, int max)
    {
        if (canPick && current < max)
        {
            current++;          
            GameEvents.instance.UpdateCurrentCarryAmount(current);
            foreach (Rigidbody rb in allRigidbody)
            {
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
            }            
            transform.parent = parent.transform;
            transform.localPosition = parent.transform.position;
        }
    }
    //this.transform.position = new Vector3(-0.6f, 11.6f, 0);
}
