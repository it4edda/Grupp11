using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemy
{
    public class GroceryChaser : EnemyAi
    {
        [SerializeField] Vector3 carryingOffset;
        [SerializeField] GameObject grocery;
        

        protected override void Movement()
        {

            if (targetToChase == null)
            {
                targetToChase = ChooseTarget();
                return;
            }
            
            base.Movement();
        }

        protected override bool Check()
        {
            if (!base.Check())
            {
                return false;
            }

            grocery = targetToChase.gameObject;
            grocery.transform.parent = transform;
            grocery.transform.position = 
            return true;
        }

        Transform ChooseTarget()
        {
            List<Groceries> availableGroceries = new List<Groceries>();
            availableGroceries.AddRange(FindObjectsOfType<Groceries>()
                .Where(p => p.transform.parent != p.spawnPoint && !p.isPickedUp));
            if (availableGroceries.Count > 0)
            {
                return availableGroceries[Random.Range(0, availableGroceries.Count)].transform;
            }

            return null;
        }
    }
}