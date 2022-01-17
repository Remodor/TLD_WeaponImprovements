using HarmonyLib;
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

    //* Prevent aim delay.
    [HarmonyPatch(typeof(PlayerAnimation), "IsAllowedToFire")]
    internal class PlayerAnimation_IsAllowedToFire
    {
        internal static bool Prefix(PlayerAnimation __instance, ref bool allowHipFire, ref bool __result)
        {
            if (!Weapon_Settings.Get().no_fire_delay_after_aim  || !GameManager.GetVpFPSCamera().CurrentShooter)
            {
                return true;
            }
            if (GameManager.m_SuppressWeaponAim)
            {
                GameManager.GetVpFPSCamera().CurrentShooter.m_NextAllowedFireTime = Time.time;
                __result = false;
                return false;
            }
            if (!allowHipFire)
            {
                if (__instance.m_State != PlayerAnimation.State.Aiming)
                {
                    GameManager.GetVpFPSCamera().CurrentShooter.m_NextAllowedFireTime = Time.time;
                    __result = false;
                    return false;
                }
            }
            else if (__instance.m_State == PlayerAnimation.State.Equipping || __instance.m_State == PlayerAnimation.State.Dequipping)
            {
                GameManager.GetVpFPSCamera().CurrentShooter.m_NextAllowedFireTime = Time.time;
                __result = false;
                return false;
            }
            __result = true;
            return false;
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
    //* Hide crosshair.
    [HarmonyPatch(typeof(HUDManager), "ShouldHideCrossHairs")]
    internal class HUDManager_ShouldHideCrossHairs
    {
        internal static bool Prefix(ref bool __result)
        {
            if (Weapon_Settings.Get().hide_crosshair
                || (Weapon_Settings.Get().hide_bow_crosshair
                && GameManager.GetPlayerManagerComponent().m_ItemInHands
                && GameManager.GetPlayerManagerComponent().m_ItemInHands.m_BowItem))
            {
                __result = true;
                return false;
            }
            return true;
        }
    }
}
