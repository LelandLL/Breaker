using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

public class AlteredCandy {

    private List<GameObject> newCandy { get; set; }
    public int MaxDistance { get; set; }

    /// <summary>
    /// Returns distinct list of altered candy
    /// </summary>
    public IEnumerable<GameObject> AlteredCandyInfo
    {
        get
        {
            return newCandy.Distinct();
        }
    }

    public void AddCandy(GameObject go)
    {
        if (!newCandy.Contains(go))
            newCandy.Add(go);
    }

    public AlteredCandy()
    {
        newCandy = new List<GameObject>();
    }
}
