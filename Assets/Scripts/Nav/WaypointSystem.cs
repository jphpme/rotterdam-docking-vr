using System.Collections.Generic;
using UnityEngine;


public class WaypointSystem : MonoBehaviour
{
public List<Waypoint> Points = new();
public Transform Target; // XR Rig or camera parent
public int CurrentIndex = 0;


public void JumpTo(int index)
{
if (index < 0 || index >= Points.Count || Target == null) return;
CurrentIndex = index;
Target.position = Points[index].Position;
}


public void Next() => JumpTo(Mathf.Min(CurrentIndex + 1, Points.Count - 1));
public void Prev() => JumpTo(Mathf.Max(CurrentIndex - 1, 0));
}