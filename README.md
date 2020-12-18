# Oni [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
### Resuable scripts and tools for Unity

Oni is a collection of scripts and tools I've created as well as some open source third-party scripts I've integrated into my own workflow.  While working in Unity, I realized there's a lot of common, reusable, functionality that I've reimplemented over and over again and I figured I should take the time to put it all in one place!

## Overview
### Scripting
- Attributes

   Property attributes for readability and control

- Coroutines

   Reusable coroutines for common uses like scheudling and timers, along with CustomYieldInstructions for better control

- Event Helpers

   Helper functions for allowing UnityEvents to execute common tasks like setting UI Text to a value

- Extensions

   Extensions for common Unity types

- Hooks

   UnityEvent based hooks for commonly used MonoBehaviour calls and events like inputs

- Math

   Extended math library supporting additional vector functions, bezier curves, and more

- Patterns

   Common game development design patterns like object pooling

- Transformation

   Simple and reusable object movement scripts e.g. bobbing, rotating, etc.

### Tools
- Editor Hotkeys

   Blender-like numpad hotkeys for manipulating the scene-view camera

- Project Window Details

   Display optional and toggleable info about assets in the Project Window (ex. type, file size, etc.)

## Installation
### Git
This package can be installed with the Unity Package Manager by selecting the add package dropdown, clicking "Add package from git url...", and entering `https://github.com/nmacadam/Oni.git`.

Alternatively the package can be added directly to the Unity project's manifest.json by adding the following line:
```
{
  "dependencies": {
      ...
      "com.daruma-works.oni":"https://github.com/nmacadam/Oni.git"
      ...
  }
}
```
For either option, by appending `#<release>` to the Oni.git url you can specify a specific release (e.g. Oni.git#1.0.0-preview)

### Manual
Download this repository as a .zip file and extract it, open the Unity Package Manager window, and select "Add package from disk...".  Then select the package.json in the extracted folder.
