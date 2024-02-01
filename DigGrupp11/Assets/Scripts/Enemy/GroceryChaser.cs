using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class GroceryChaser : EnemyAi
    {
        
        [Header("Grocery chaser exclusives")]
        [SerializeField] Vector3 carryingOffset;
        [SerializeField] GameObject grocery;


        void OnEnable()
        {
            PlayerPickup.PickingUpSomething += ChooseTarget;
        }

        protected override void Movement()
        {

            if (targetToChase == null)
            {
                rb.velocity = Vector3.zero;
                return;
            }
            
            base.Movement();
        }

        protected override bool Check()
        {
            if (!targetToChase) { return false; }
            if (!base.Check())
            {
                return false;
            }
            if (!grocery)
            {
                grocery = targetToChase.gameObject;
                grocery.transform.parent = transform;
                grocery.GetComponent<Groceries>().GetsPickedUp(true);
                grocery.transform.position = transform.position + carryingOffset;
                targetToChase = grocery.GetComponent<Groceries>().spawnPoint;
            }
            else
            {
                grocery.transform.parent = targetToChase;
                grocery.transform.position = grocery.transform.parent.position;
                grocery = null;
                targetToChase = null;
                ChooseTarget(true, null);
            }
            return true;
        }

        void ChooseTarget(bool isPickingUpSomething, Transform holding)
        {
            if ((grocery && grocery.transform != holding)) { return;}
            if (targetToChase && targetToChase != holding && !grocery) { return; }
            
            List<Groceries> availableGroceries = new List<Groceries>();
            availableGroceries.AddRange(FindObjectsOfType<Groceries>()
                .Where(p => p.transform.parent != p.spawnPoint && !p.isPickedUp));
            if (availableGroceries.Count > 0)
            {
                targetToChase = availableGroceries[Random.Range(0, availableGroceries.Count)].transform;
                grocery = null;
            }
            else
            {
                targetToChase = null;
                grocery = null;
            }
        }

        public override void Attacked()
        {
            if (grocery)
            {
                grocery.GetComponent<Groceries>().GetsPickedUp(false);
                grocery = null;
            }
            base.Attacked();
            ChooseTarget(true, null);
        }
    }
}