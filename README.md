Repository for Software of Umwelt Case Study in SAARTE project
==============================================================

The apps provided in this repository have been developed using the unity game engine (https://unity.com/) for the HoloLens 2 device (https://www.microsoft.com/en-us/hololens/hardware) and Android-base mobile devices.

Directory Structure
-------------------

- AR-Baumwurzel-HoloLens     -> "Rendering Tree Roots Outdoors" Paper (https://doi.org/10.1007/978-3-030-77599-5_26)
- AR-Baumwurzel-Smartphone/  -> "Rendering Tree Roots Outdoors" Paper (https://doi.org/10.1007/978-3-030-77599-5_26)
- Tree/                      -> "Near and Far Interaction" Paper (submitted)


Software requirements
---------------------

* Unity-Version: see the ProjectSettings/ProjectVersion.txt file of the respective subdirectory
        * Activate all Windows components in installer.
	* Activate all Android components in installer.

* Windows 10 needs to have the Windows 10 SDK installed. This update can be found here:
        * https://developer.microsoft.com/en-us/windows/downloads/windows-10-sdk
        * After the installation the dropdown "SDK" (in build settings) should automatically be set to "Latest Version" by Unity.

* MixedRealityToolkit MRTK as Unity package

* Documentation
        * Doxygen 1.8

* 3D Model conversion
        * Blender (https://www.blender.org/)
        * Blender needs to be installed (for enabling Unity to import models)

Building
--------

1. Build Project in Unity
2. Build resulting project in Visual Studio

Acknowledgements
----------------
This work has been performed in project SAARTE (Spatially-Aware Augmented Real-
ity in Teaching and Education). SAARTE is supported by the European Union (EU)
in the ERDF program P1-SZ2-7 and by the German federal state Rhineland-Palatinate
(Antr.-Nr. 84002945).


Acknowledgements
----------------
This work has been performed in project SAARTE (Spatially-Aware Augmented Real-
ity in Teaching and Education). SAARTE is supported by the European Union (EU)
in the ERDF program P1-SZ2-7 and by the German federal state Rhineland-Palatinate
(Antr.-Nr. 84002945).


