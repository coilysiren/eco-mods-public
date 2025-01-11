using Eco.Gameplay.Items;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;

namespace MinesQuarries
{
    public class ExcavatableRestriction : InventoryRestriction
    {
        public override LocString Message =>
            Localizer.DoStr("Inventory only accepts excavatable items.");

        public override int MaxAccepted(Item item, int currentQuantity) =>
            item.GetType().HasTag(TagManager.GetTagOrFail("Excavatable")) ? -1 : 0;
    }

    public class DiggableRestriction : InventoryRestriction
    {
        public override LocString Message =>
            Localizer.DoStr("Inventory only accepts diggable items.");

        public override int MaxAccepted(Item item, int currentQuantity) =>
            item.GetType().HasTag(TagManager.GetTagOrFail("Diggable")) ? -1 : 0;
    }

    public class SandRestriction : InventoryRestriction
    {
        public override LocString Message => Localizer.DoStr("Inventory only accepts sand.");

        public override int MaxAccepted(Item item, int currentQuantity) =>
            item.GetType() == typeof(SandItem) ? -1 : 0;
    }

    public class ClayRestriction : InventoryRestriction
    {
        public override LocString Message => Localizer.DoStr("Inventory only accepts clay.");

        public override int MaxAccepted(Item item, int currentQuantity) =>
            item.GetType() == typeof(ClayItem) ? -1 : 0;
    }

    public class DirtRestriction : InventoryRestriction
    {
        public override LocString Message => Localizer.DoStr("Inventory only accepts dirt.");

        public override int MaxAccepted(Item item, int currentQuantity) =>
            item.GetType() == typeof(DirtItem) ? -1 : 0;
    }
}
