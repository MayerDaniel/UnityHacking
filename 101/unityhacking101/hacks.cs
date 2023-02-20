using System;
using UnityEngine;
using System.Reflection;

namespace hax
{
    public class Hacks : MonoBehaviour
    {
        // Cast a wide net with our BindingFlags to catch most variables we would run into. Scope this down as needed.
        // https://learn.microsoft.com/en-us/dotnet/api/system.reflection.bindingflags?redirectedfrom=MSDN&view=net-7.0
        BindingFlags flags = BindingFlags.Instance
               | BindingFlags.Public
               | BindingFlags.NonPublic
               | BindingFlags.Static;

        // Unity Engine reserved function that handles UI events for all objects
        // Taken pretty much verbatim from https://docs.unity3d.com/ScriptReference/GUI.Window.html
        public void OnGUI()
        {
            // Create a window at the center top of our game screen that will hold our button
            Rect windowRect = new Rect(Screen.width / 2, Screen.height / 8, 120, 50);

            // Register the window. Notice the 3rd parameter is a callback function to make the window, defined below
            windowRect = GUI.Window(0, windowRect, DoMyWindow, "HackBox");

            // Make the contents of the window
            void DoMyWindow(int windowID)
            {
                // Combo line that creates the button and then also will check if it has been pressed
                if (GUI.Button(new Rect(10, 20, 100, 20), "Add Tail"))
                {
                    this.AddTail();
                }
            }
        }

        public void AddTail()
        {
            // Get the instantiated Snake GameObject
            Snake snake = GameObject.FindObjectOfType<Snake>();

            // Create a "Type" object for the type "Snake"
            Type snakeType = snake.GetType();
            // Use System.Reflection.FieldInfo object to discover the attributes of the field and provide access to its metadata
            // https://learn.microsoft.com/en-us/dotnet/api/system.reflection.fieldinfo?view=net-7.0
            FieldInfo ateField = snakeType.GetField("ate", flags);
            // Set the value of the "ate" field within our game's Snake object to true
            ateField.SetValue(snake, true);

            // MethodInfo/GetMethod is pretty much the same as FieldInfo/GetField for targeting methods in instantiated objects
            MethodInfo dynMethod = snakeType.GetMethod("Move", flags);
            // Call the "Move" method in our Snake object with no arguments
            dynMethod.Invoke(snake, null);
        }
    }
}