using Il2CppSystem;
using Il2CppSystem.Collections.Generic;
using MelonLoader;
using UnityEngine;

namespace WeaponImprovements
{
    using Settings = Weapon_Settings;
    public class Implementation : MelonMod
    {
        public override void OnApplicationStart()
        {
            Debug.Log($"[{Info.Name}] version {Info.Version} loaded!");
            Settings.OnLoad();
        }
    }
}
