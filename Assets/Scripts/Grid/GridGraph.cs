﻿using System.Collections.Generic;
using UnityEngine;

public class GridGraph
{
    public int Width;
    public int Height;

    public Node[,] Grid;
    public List<Vector2> Walls;
    public List<Vector2> Forests;

    public GridGraph(int w, int h)
    {
        Width = w;
        Height = h;

        Grid = new Node[w, h];

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                Grid[x,y] = new Node(x, y);
                Grid[x, y].tileType = TileType.Empty;
            }
        }
    }

    /// <summary>
    /// Checks whether the neighbouring Node is within the grid bounds or not
    /// </summary>
    public bool InBounds(Vector2 v)
    {
        if (v.x >= 0 && v.x < this.Width &&
            v.y >= 0 && v.y < this.Height)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Checks whether the neighbouring Node is a wall or not
    /// </summary>
    public bool Passable(Node id,Node end)
    {
        
        if (id.tileType==TileType.Empty || id==end) return true;
        else
            return false;
    }

    public bool CheckEndPointDirection(Node id, Node end, Vector2 direction)
    {
        if (id.Position != end.Position)
            return true;
        else
            if (end.currentBus.direction == BusDirection.Vertical &&
               (direction == new Vector2(1, 0) || direction == new Vector2(-1, 0)))
                return true;
            else if (end.currentBus.direction == BusDirection.Horizontal &&
               (direction == new Vector2(0, 1) || direction == new Vector2(0, -1)))
                return true;

        return false;


    }


    /// <summary>
    /// Returns a List of neighbouring Nodes
    /// </summary>
    public List<Node> Neighbours(Node n,Node  end)
    {
        List<Node> results = new List<Node>();

        List<Vector2> directions = new List<Vector2>()
        {
            new Vector2( -1, 0 ), // left
          //  new Vector2(-1, 1 ),  // top-left, comment it out for 4-direction movement
            new Vector2( 0, 1 ),  // top
         //   new Vector2( 1, 1 ),  // top-right, comment it out for 4-direction movement
            new Vector2( 1, 0 ),  // right
        //    new Vector2( 1, -1 ), // bottom-right, comment it out for 4-direction movement
            new Vector2( 0, -1 ), // bottom
        //    new Vector2( -1, -1 ) // bottom-left, comment it out for 4-direction movement
        };

        foreach (Vector2 v in directions)
        {
            Vector2 newVector = v + n.Position;
            if (InBounds(newVector) &&
                Passable(Grid[(int) newVector.x, (int)newVector.y],end) &&
                CheckEndPointDirection(Grid[(int)newVector.x, (int)newVector.y], end,v))
            {
                results.Add(Grid[(int)newVector.x, (int)newVector.y]);
            }
        }

        return results;
    }
    public List<Node> Neighbours(Node n, Node end, Bus bus)
    {
        List<Node> results = new List<Node>();

        List<Vector2> directions = new List<Vector2>()
        {
            new Vector2( -1, 0 ), // left
          //  new Vector2(-1, 1 ),  // top-left, comment it out for 4-direction movement
            new Vector2( 0, 1 ),  // top
         //   new Vector2( 1, 1 ),  // top-right, comment it out for 4-direction movement
            new Vector2( 1, 0 ),  // right
        //    new Vector2( 1, -1 ), // bottom-right, comment it out for 4-direction movement
            new Vector2( 0, -1 ), // bottom
        //    new Vector2( -1, -1 ) // bottom-left, comment it out for 4-direction movement
        };

        foreach (Vector2 v in directions)
        {
            Vector2 newVector = v + n.Position;
            if (InBounds(newVector) &&
                Passable(Grid[(int)newVector.x, (int)newVector.y], end))
            {
                results.Add(Grid[(int)newVector.x, (int)newVector.y]);
            }
        }

        return results;
    }

    public int Cost(Node b)
    {
        // If Node 'b' is a Forest return 2, otherwise 1
       
         return 1;
    }
}
