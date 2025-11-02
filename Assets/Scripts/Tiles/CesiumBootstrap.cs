using UnityEngine;


/// <summary>
/// Lightweight helper that reminds you to add Cesium components and where to put the API key.
/// Keeps build errors low even before Cesium is imported.
/// </summary>
public class CesiumBootstrap : MonoBehaviour
{
[TextArea] public string Notes =
"Add CesiumGeoreference + Cesium3DTileset via GameObject ▶ Cesium ▶ ...\n" +
"For Google Photorealistic 3D Tiles: set the Map Tiles API key in the provider component and ensure attribution is visible.";


void Start()
{
var hadCesium = GameObject.FindObjectsOfType<Component>(includeInactive: true)
.Exists(c => c.GetType().Name.Contains("Cesium"));
if (!hadCesium)
{
Debug.LogWarning("[CesiumBootstrap] No Cesium components found. Import Cesium for Unity and add a CesiumGeoreference + Tileset.");
}
}
}


static class CesiumExtensions
{
public static bool Exists<T>(this T[] array, System.Predicate<T> pred)
{
foreach (var x in array) if (pred(x)) return true; return false;
}
}