using System;
using System.Collections;
using System.Collections.Generic;
using Bengal.HealthDamageSystem;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Bengal
{
    public class Portal : MonoBehaviour
    {
        public bool isTeleport;
        public bool willSpawnEnemies;
        public bool isDestroyable;
        
        [SerializeField] GameObject[] enemiesToSpawn;
        [SerializeField] private float spawnRate = 1f;
        [Header("If left \"0\" will spawn endlessly")]
        [SerializeField] int maxEnemiesToSpawn;

        [SerializeField] private GameObject portal_Graphics;
        [SerializeField] private GameObject teleportDestination;
        private bool isActive;


        // Start is called before the first frame update
        void Start()
        {
            ActivatePortal();
            if (!isDestroyable)
            {
                GetComponent<EnemyHealthManager>().enabled = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player") && isTeleport)
            {
                //TODO: Add graphics and delay, make it look cool;
                var target = col.gameObject;
                target.transform.position = teleportDestination.transform.position;
            }
        }
        
        public void ActivatePortal()
        {
            //TODO: Create graphics for portal activation.
            portal_Graphics.SetActive(true);
            isActive = true;
            if (willSpawnEnemies)
            {
                StartCoroutine(HandleSpawning());
            }

        }

        public void DeactivatePortal()
        {
            //TODO: Create graphics for portal deactivation.
            portal_Graphics.SetActive(false);
            isActive = false;
        }
        
        IEnumerator HandleSpawning()
        {
            var spawned = 0;
            while (isActive && (spawned < maxEnemiesToSpawn || maxEnemiesToSpawn == 0))
            {
                yield return new WaitForSeconds(spawnRate);
                var enemy = Random.Range(0, enemiesToSpawn.Length);
                Instantiate(enemiesToSpawn[enemy], transform.position, Quaternion.identity);
                spawned++;
            }
        }
        
   }
    
}
