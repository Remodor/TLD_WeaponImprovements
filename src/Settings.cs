using ModSettings;

namespace WeaponImprovements
{
    internal class WeaponImprovements_Settings : JsonModSettings
    {
        //* ----Guns----
        [Section("Guns")]
        [Name("Hide Ammo Hud")]
        [Description("If you want to hide the ammo hud.\n(Vanilla = false)")]
        public bool hide_ammo_hud = false;

        [Name("Cancel Reload")]
        [Description("If you want to cancel reload with pressing the reload button during reload.\n(Vanilla = false)")]
        public bool cancel_reload = false;

        [Name("No Fire Delay After Reload")]
        [Description("If you want to remove the fire delay after a reload. Particularly usefull with the revolver!\n(Vanilla = false)")]
        public bool no_fire_delay_after_reload = false;

        [Name("No More Jam Delay")]
        [Description("If you want to remove the incredibly long delay until you can remove a jammed bullet after you tried to fire.\n(Vanilla = false)")]
        public bool no_jam_delay = false;
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
