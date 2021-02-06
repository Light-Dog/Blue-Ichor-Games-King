using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // location
    Vector2 position = Vector2.zero;
    // accaptable area
    float area = 2.0f;
    // neighbors
    public Node left  = null;
    public Node right = null;
    public Node up = null;
    
    Node parent = null;
    float costSoFar = 0.0f;
    float cost = 0.0f;
    public float weight = 1.0f;
    bool visited = false;


    private void Awake()
    {
        position = new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y);
        costSoFar = 1.0f;
    }

    private float CalculateHeuristic(Node goal)
    {
        float xDiff = Mathf.Abs(goal.position.x - position.x);
        float yDiff = Mathf.Abs(goal.position.y - position.y);

        return Mathf.Min(xDiff, yDiff) * Mathf.Sqrt(2f) + Mathf.Max(xDiff, yDiff) - Mathf.Min(xDiff, yDiff);
    }

    private float CalculateCostSoFar(Node current)
    {
        costSoFar = current.costSoFar + Vector2.Distance(position, current.position);
        return costSoFar;
    }

    public float CalcuateFinalCost(Node current, Node goal)
    {
        return CalculateCostSoFar(current) + (CalculateHeuristic(goal) * weight);
    }

    public Vector2 GetPosition() { return position; }

    public static bool operator == (Node a, Node other)
    {
        return (a.position.Equals(other.position));
    }

    public static bool operator != (Node a, Node other)
    {
        return !(a.position.Equals(other.position));
    }

    public bool NodeToTransform(Transform other)
    {
        if (Vector2.Distance(position, other.position) <= area)
            return true;

        return false;
    }

    public void AddNeighbors(List<Node> path, Node goal)
    {
        visited = true;
        float tempCost = 0.0f;
        if(left && left.visited == false)
        {
            tempCost = left.CalcuateFinalCost(this, goal);
            print("Has a left and its cost is " + tempCost);

            if(path.Contains(left))
            {
                if(left.cost > tempCost)
                {
                    left.parent = this;
                    left.cost = tempCost;
                }
            }
            else
            {
                left.parent = this;
                path.Add(left);
            }

        }
        if(right && right.visited == false)
        {
            tempCost = right.CalcuateFinalCost(this, goal);
            print("Has a right and its cost is " + tempCost);


            if (path.Contains(right))
            {
                if(right.cost > tempCost)
                {
                    right.parent = this;
                    right.cost = tempCost;
                }
            }
            else
            {
                right.parent = this;
                right.cost = tempCost;
            }
        }
        if(up && up.visited == false)
        {
            tempCost = up.CalcuateFinalCost(this, goal);
            print("Has a up and its cost is " + tempCost);


            if (path.Contains(up))
            {
                if (up.cost > tempCost)
                {
                    up.parent = this;
                    up.cost = tempCost;
                }
            }
            else
            {
                up.parent = this;
                up.cost = tempCost;
            }
        }
    }

    public float GetCost() { return cost; }
}
