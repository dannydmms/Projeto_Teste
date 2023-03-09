using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] private GameObject store;
    PlayerMovemente player;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovemente>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenStore();
        }
    }
    private void OpenStore()
    {
        store.SetActive(true);
        Time.timeScale = 0;
    }
    public void CloseStore()
    {
        store.SetActive(false);
        Time.timeScale = 1;        
    }
    public void UpMaxCarryAmount()
    {
        player.UPMaxAmount();
    }
    public void ChangeColor()
    {
        player.ChangeColor();
    }    
}
