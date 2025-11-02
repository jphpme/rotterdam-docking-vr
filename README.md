\# Rotterdam Docking VR (Unity 6000.2.10f1 + Cesium + Quest 3)





Goal: Photorealistic VR demo of a ship docking path in Rotterdam on Quest 3, using Google Photorealistic 3D Tiles via Cesium for Unity, with S-100 overlays added later.





\## Prereqs

\- Unity \*\*6000.2.10f1\*\* (Unity 6)

\- Android Build Support (SDK/NDK), IL2CPP

\- Quest 3 developer mode enabled

\- (Later) Cesium for Unity + Google Map Tiles API key





\## Setup

1\. Clone repo and open with the exact Unity version above.

2\. Window ▶ Package Manager: confirm these packages are present/installed

\- \*\*OpenXR\*\* (com.unity.xr.openxr)

\- \*\*XR Interaction Toolkit\*\* (com.unity.xr.interaction.toolkit)

\- \*\*Input System\*\* (com.unity.inputsystem)

3\. Project Settings ▶ XR Plug‑in Management ▶ \*\*OpenXR\*\* (check for \*\*Android\*\* and \*\*Windows\*\*)

4\. Project Settings ▶ Player ▶ Android

\- Scripting Backend: \*\*IL2CPP\*\*

\- Target Architectures: \*\*ARM64\*\*

\- Minimum API Level: \*\*29+\*\*

5\. Import \*\*Cesium for Unity\*\* (per Cesium docs). Add a \*\*CesiumGeoreference\*\* and a \*\*Cesium3DTileset\*\* to the scene.

6\. Set \*\*Google Map Tiles API key\*\* in the Cesium/Google tileset provider UI and enable attribution.





\## Running the demo

\- Open `Assets/Scenes/DockingDemo.unity` (create if missing)

\- Add `CesiumBootstrap` to an empty GameObject; press Play in editor to validate tile streaming

\- Build \& Run for Android (Quest 3)





\## Next

\- Implement S‑100 overlays (S‑101 features, S‑102 bathy, S‑104 tides, S‑111 currents)

\- Add simple waypoints (Maasvlakte → Eemhaven) and a time slider for tide/current time‑steps

