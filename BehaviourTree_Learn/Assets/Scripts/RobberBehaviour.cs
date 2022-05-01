﻿    using System;
    using UnityEngine;
    using UnityEngine.AI;

    public class RobberBehaviour : MonoBehaviour
    {
        private BehaviourTree _tree;
        
        public GameObject diamond;
        public GameObject van;
        public GameObject backDoor;
        public GameObject frontDoor;

        private NavMeshAgent agent;

        public enum ActionState
        { IDLE, WORKING };
        private ActionState state = ActionState.IDLE;

        private Node.Status treeStatus = Node.Status.RUNNING;

        [Range(0, 1000)] 
        public int money = 800;
        
        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            
            _tree = new BehaviourTree();
            Sequence steal = new Sequence("Steal Something");
            Leaf goToDiamond = new Leaf("Go To Diamond", GoToDiamond);
            Leaf hasGotMoney = new Leaf("Has Got Money", HasMoney);
            Leaf goToBackDoor = new Leaf("Go To BackDoor", GoToBackDoor);
            Leaf goToFrontDoor = new Leaf("Go To BackDoor", GoToFrontDoor);
            Leaf goToVan = new Leaf("Go To Van", GoToVan);
            Selector openDoor = new Selector("Open Door");

            openDoor.AddChild(goToBackDoor);
            openDoor.AddChild(goToFrontDoor);
            
            steal.AddChild(hasGotMoney);
            steal.AddChild(openDoor);
            steal.AddChild(goToDiamond);
            // steal.AddChild(goToBackDoor);
            steal.AddChild(goToVan);
            _tree.AddChild(steal);
            
            _tree.PrintTree();
        }
        
        public Node.Status HasMoney()
        {
            if (money >= 500)
                return Node.Status.FAILURE;
            return Node.Status.SUCCESS;
        }

        public Node.Status GoToDiamond()
        {
            Node.Status s = GoToLocation(diamond.transform.position);
            if (s == Node.Status.SUCCESS)
            {
                diamond.transform.parent = this.gameObject.transform;
            }
            return s;
        }

        public Node.Status GoToVan()
        {
            Node.Status s = GoToLocation(van.transform.position);

            if (s == Node.Status.SUCCESS)
            {
                money += 300;
                diamond.transform.SetParent(null);
            }
            
            
            return s;
        }

        public Node.Status GoToBackDoor()
        {
            return GoToDoor(backDoor);
        }

        public Node.Status GoToFrontDoor()
        {
            return GoToDoor(frontDoor);
        }

        public Node.Status GoToDoor(GameObject door)
        {
            Node.Status s = GoToLocation(door.transform.position);
            if (s == Node.Status.SUCCESS)
            {
                if (!door.GetComponent<Lock>().isLocked)
                {
                    door.SetActive(false);
                    return Node.Status.SUCCESS;
                }

                return Node.Status.FAILURE;
            }
            else
            {
                return s;
            }
        }
        
        Node.Status GoToLocation(Vector3 destination)
        {
            float distanceToTarget = Vector3.Distance(destination, transform.position);
            if (state == ActionState.IDLE)
            {
                agent.SetDestination(destination);
                state = ActionState.WORKING;
            }
            else if (Vector3.Distance(agent.pathEndPosition, destination) >= 2)
            {
                state = ActionState.IDLE;
                return Node.Status.FAILURE;
            }
            else if (distanceToTarget < 2)
            {
                state = ActionState.IDLE;
                return Node.Status.SUCCESS;
            }

            return Node.Status.RUNNING;
        }

        private void Update()
        {
            if (treeStatus != Node.Status.SUCCESS)
            {
                treeStatus = _tree.Process();
            }
        }
    }