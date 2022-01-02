using ModSettings;

namespace WeaponImprovements
{
    internal class WeaponImprovements_Settings : JsonModSettings
    {
        //* ----Guns----
        [Section("Rifle, Revolver and Flare Gun")]
        [Name("Hide Ammo Hud")]
        [Description("If you want to hide the ammo hud.\n(Vanilla = false)")]
        public bool hide_ammo_hud = false;

        [Name("Cancel Reload")]
        [Description("If you want to cancel reload with pressing the reload button during reload.\n(Vanilla = false)")]
        public bool cancel_reload = false;

        [Name("No Fire Delay After Reload")]
        [Description("If you want to remove the fire delay after a reload. Particularly useful with the revolver!\n(Vanilla = false)")]
        public bool no_fire_delay_after_reload = false;

        [Name("No Fire Delay After Aim")]
        [Description("If you want to remove the fire delay after aiming.\n(Vanilla = false)")]
        public bool no_fire_delay_after_aim = false;

        [Name("No More Jam Delay")]
        [Description("If you want to remove the incredibly long delay until you can remove a jammed bullet after you tried to fire.\n(Vanilla = false)")]
        public bool no_jam_delay = false;

        [Section("Crosshair")]
        [Name("Hide Crosshair")]
        [Description("If you want to hide the crosshair (the little point) completely.\n(Vanilla = false)")]
        public bool hide_crosshair = false;

        [Name("Hide Bow Crosshair")]
        [Description("If you like to hide the crosshair when equipping the bow.\n(Vanilla = false)")]
        public bool hide_bow_crosshair = false;
    }
    internal static class Weapon_Settings
    {
        private static WeaponImprovements_Settings settings = new WeaponImprovements_Settings();

        public static void OnLoad()
        {
            settings.AddToModSettings("Weapon Improvements");
        }

        internal static WeaponImprovements_Settings Get()
        {
            return settings;
        }
    }
}
