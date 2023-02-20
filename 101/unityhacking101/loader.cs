//Boilerplate from https://www.unknowncheats.me/forum/unity/285864-beginners-guide-hacking-unity-games.html

using System;
// We will import this straight from the game files!
using UnityEngine;
// Our namespace, which we will create in another file
using hax;


namespace cheat
{
    public class Loader
    {
        public static GameObject L;
        public static void Load()
        {

            // Create an instance of the Gameobject
            Loader.L = new GameObject();

            // Add our class that will be 
            Loader.L.AddComponent<hax.Hacks>();

            // Telling Unity not to destory our GameObject on level change
            UnityEngine.Object.DontDestroyOnLoad(Loader.L);
        }

        public static void Unload()
        {
            // Destory our GameObject
            UnityEngine.Object.Destroy(L);
        }
    }

}


