using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackEnemyController : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x,4, player.transform.position.z);
        
    }
    public void DeleteChild()
    {
        EnemyController[] childs = GetComponentsInChildren<EnemyController>();
        foreach (EnemyController item in childs)
        {
            Destroy(item.gameObject);
        }
    }
}
