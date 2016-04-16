# Custom Project Settings

Make your own project settings for your Unity games!

## How to use it

Place **"CustomProjectSettings.cs"** anywhere in your project (under the **"Assets"** folder). Note that a **"Resources**" folder will be created next to it so you might as well keep it inside the **"CustomProjectSettings"** folder.

To create a new type of settings, create a new C# class which inherits from **CustomProjectSettings\<T\>**, **T** being the class you are creating.

You can then create a static member method with the [**[MenuItem]**](http://docs.unity3d.com/ScriptReference/MenuItem.html) attribute to make it selectable from the Unity Editor Menus. You can call **SelectSettings()** inside this function to select the settings file which will be automatically created in the **"Resources"** folder next to **"CustomProjectSettings.cs"** if it does not exist. This method will not compile outside of the Unity Editor (surround it with **#if UNITY_EDITOR / #endif** or implement it in an editor-only script).

An **ExampleSettings** is provided as a reference. Like other Unity Objects, you can create a custom Inspector that will be displayed when the settings file is selected (please refer to the Unity Scripting Reference: http://docs.unity3d.com/ScriptReference/Editor.html).

## Warnings & Guidelines

### File structure restrictions

There are only 2 restrictions regarding file structure:
* The C# file containing the base class **CustomProjectSettings** should be named **"CustomProjectSettings.cs"** and no other files in the project should have the same name.
* The **".asset"** files containg the settings should be in a **"Resources"** folder next to **"CustomProjectSettings.cs"**.

### Resources Loading

Since we cannot use the **"ProjectSettings"** folder where Unity's built-in project settings are stored, custom settings are saved in the **"Resources"** folder next to **"CustomProjectSettings.cs"**. This means that all these files are included in every build as well as any object referenced by these files. So it is recommended to keep object references to a minimum and only hold values as much as possible.

If some of the fields are only meant to be used in the Editor, you can surround them with **#if UNITY_EDITOR / #endif**. In that case, you can reference objects as much as you like since these will not be considered as dependent by Unity when building.

Unlike built-in project settings, custom settings will not be loaded automatically on game start. A settings file will be loaded by the first call to the **"instance"** property of its type. You can call **Load()** to load the settings file at the appropriate moment (e.g. on **Awake()**).
