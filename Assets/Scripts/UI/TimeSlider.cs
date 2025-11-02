using System;
using UnityEngine;


/// <summary>
/// Bind this to a Unity UI Slider (0..1). Convert normalized value to a DateTime range.
/// Downstream systems (tide/current overlays) can subscribe to OnTimeChanged.
/// </summary>
public class TimeSlider : MonoBehaviour
{
public DateTime StartTime = DateTime.UtcNow.Date;
public DateTime EndTime = DateTime.UtcNow.Date.AddDays(1);


public event Action<DateTime> OnTimeChanged;


[Range(0f,1f)] public float Normalized = 0f;


public void SetNormalized(float t)
{
Normalized = Mathf.Clamp01(t);
var dt = LerpTime(StartTime, EndTime, Normalized);
OnTimeChanged?.Invoke(dt);
}


static DateTime LerpTime(DateTime a, DateTime b, float t)
{
var span = b - a;
return a + TimeSpan.FromTicks((long)(span.Ticks * t));
}
}