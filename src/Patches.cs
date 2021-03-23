using Harmony;
using Il2CppSystem.Collections.Generic;
using UnityEngine;


namespace WeaponImprovements
{
    //* Hide Ammo Hud
    [HarmonyPatch(typeof(HUDManager), "DisplayAmmoOnHUDForTime")]
    internal class HUDManager_DisplayAmmoOnHUDForTime
    {
        internal static bool Prefix()
        {
            if (Weapon_Settings.Get().hide_ammo_hud)
            {
                InterfaceManager.m_Panel_HUD.m_EquipItemPopup.m_TimeToHideAmmo = Time.time;
                return false;
            }
            return true;
        }
    }
    //* Cancel reload. No fire delay after reload.
    [HarmonyPatch(typeof(vp_FPSPlayer), "Reload")]
    internal class vp_FPSPlayer_Reload
    {
        internal static void Prefix(vp_FPSPlayer __instance)
        {
            if (__instance.FPSCamera.CurrentWeapon.ReloadInProgress() && Weapon_Settings.Get().cancel_reload)
            {
                __instance.FPSCamera.CurrentWeapon.m_BulletsToReload = 0;
            }
            if (Weapon_Settings.Get().no_fire_delay_after_reload)
            {
                __instance.FPSCamera.CurrentWeapon.m_GunItem.m_FireDelayAfterReload = 0;
            }
        }
    }
    //* No More Jam Delay
    [HarmonyPatch(typeof(GunItem), "PlayDryFireAudio")]
    internal class WeaponItem_PlayDryFireAudio
    {
        internal static void Postfix(GunItem __instance)
        {
            if (Weapon_Settings.Get().no_jam_delay)
            {
                GameManager.GetVpFPSCamera().CurrentShooter.NextAllowedReloadTime = Time.time;
            }
        }
    }
}
