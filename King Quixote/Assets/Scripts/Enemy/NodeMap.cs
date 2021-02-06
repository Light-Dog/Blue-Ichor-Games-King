using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeMap : MonoBehaviour
{
    //list of all nodes
    public List<Node> nodeList;
    //generates nodes?

    // Start is called before the first frame update
    void Awake()
    {
        Node[] list =  gameObject.GetComponentsInChildren<Node>();
        foreach (Node node in list)
            nodeList.Add(node);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Node FindClosesttNode(Transform enemy)
    {
        Vector2 enemyPos = new Vector2(enemy.position.x, enemy.position.y);
        //print("Enemy Pos: " + enemyPos + " NodeList of size: " + nodeList.Capacity);

        float minDistance = 1000.0f;
        int nodeIndex = -1;

        for(int i = 0; i < nodeList.Capacity; i++)
        {
            float distance = Vector2.Distance(enemyPos, nodeList[i].GetPosition());
            //print("Distance between node " + i + " at " + nodeList[i].position + " and the enemy at " + enemyPos + " is " + distance);

            if (distance < minDistance)
            {
                minDistance = distance;
                nodeIndex = i;
            }
        }

        if (nodeIndex != -1)
        {
            //print("Test #" + nodeIndex);
            return nodeList[nodeIndex];
        }
        else
            return null;
    }

    public Node GeneratePath(Transform goal, Node currentNode, List<Node> path)
    {   
        //set or pass in goal, for now the goal will be to path to the player
        Node goalNode = FindClosesttNode(goal);

        //add starting node to List
        path.Add(currentNode);
        Node cheapestNode = currentNode;
        print("Cheapest Node " + cheapestNode);

       //while list is not empty
       //while(path.Capacity != 0) <-- currently crashing if going right for some reason fml
       {
            path.Remove(cheapestNode);

            //if the cheapest node is goal node
            if (cheapestNode == goalNode)
                return cheapestNode;

            cheapestNode.AddNeighbors(path, goalNode);

            float cost = float.MaxValue;
            foreach(Node node in path)
            {
                if (node.GetCost() < cost)
                {
                    cheapestNode = node;
                    cost = node.GetCost();
                }
            }
       }

        return cheapestNode;

    }
}
