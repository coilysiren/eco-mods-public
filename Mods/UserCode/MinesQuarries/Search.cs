namespace MinesQuarries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Logging;
    using Eco.Shared.Math;
    using Eco.World;
    using Eco.World.Blocks;

    public class Search
    {
        public static bool FindBlock(Vector3 position, string blockType, int radius)
        {
            List<WrappedWorldPosition3i> blockLocations = GeneratePositionsToCheck(
                position,
                radius,
                maximumYAxis: true
            );

            foreach (WrappedWorldPosition3i blockLocation in blockLocations)
            {
                Block block = World.GetBlock(blockLocation);
                if (block != null)
                {
                    string blockName = block.GetType().FullName ?? "";
                    if (blockName == blockType)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static Dictionary<string, float> FindBlockConcentrations(
            Vector3 position,
            int radius
        )
        {
            List<WrappedWorldPosition3i> blockLocations = GeneratePositionsToCheck(
                position,
                radius,
                maximumYAxis: false
            );

            Dictionary<string, float> blockCounts = new();
            Dictionary<string, float> blockPercentages = new();
            int totalBlocks = 0;

            foreach (WrappedWorldPosition3i blockLocation in blockLocations)
            {
                Block block = World.GetBlock(blockLocation);
                if (block != null)
                {
                    string blockName = block.GetType().FullName ?? "";

                    // Skip anything that doesn't have a block name
                    // Unclear when this would happen, but it's better to be safe.
                    if (blockName == "")
                    {
                        continue;
                    }

                    // Skip anything that doesn't have a UI link
                    string itemName = GetDisplayName(blockName);
                    if (itemName == "")
                    {
                        continue;
                    }

                    // Count the number of each block type
                    if (blockCounts.ContainsKey(blockName))
                    {
                        blockCounts[blockName]++;
                    }
                    else
                    {
                        blockCounts[blockName] = 1;
                    }
                    totalBlocks++;
                }
            }

            foreach (KeyValuePair<string, float> blockCount in blockCounts)
            {
                blockPercentages[blockCount.Key] = blockCount.Value / totalBlocks;
            }

            return blockPercentages;
        }

        public static int FindBlockCount(Vector3 position, int radius, string blockType)
        {
            List<WrappedWorldPosition3i> blockLocations = GeneratePositionsToCheck(
                position,
                radius,
                maximumYAxis: false
            );

            int blockCount = 0;
            foreach (WrappedWorldPosition3i blockLocation in blockLocations)
            {
                Block block = World.GetBlock(blockLocation);
                if (block != null)
                {
                    string blockName = block.GetType().FullName ?? "";
                    Log.Debug($"Block name: {blockName}");
                    if (blockName == blockType)
                    {
                        blockCount++;
                    }
                }
            }
            return blockCount;
        }

        private static List<WrappedWorldPosition3i> GeneratePositionsToCheck(
            Vector3 position,
            int radius,
            bool maximumYAxis
        )
        {
            // Get a list of Vector3s that are within a 3d radius of the input position
            List<WrappedWorldPosition3i> positionsToCheck = new();
            WrappedWorldPosition3i wrappedPosition = WrappedWorldPosition3i.Create(
                position.X,
                position.Y,
                position.Z
            );

            for (int x = -radius; x <= radius; x++)
            {
                for (int z = -radius; z <= radius; z++)
                {
                    Vector2i XZ = new(x, z);
                    for (
                        int y = maximumYAxis ? 0 : -radius;
                        y <= (maximumYAxis ? World.GetTopBlockY(XZ) : radius);
                        y++
                    )
                    {
                        WrappedWorldPosition3i positionToCheck = WrappedWorldPosition3i.Create(
                            wrappedPosition.X + x,
                            maximumYAxis ? y : wrappedPosition.Y + y,
                            wrappedPosition.Z + z
                        );
                        positionsToCheck.Add(positionToCheck);
                    }
                }
            }
            return positionsToCheck;
        }

        public static LocString GetDisplayName(string blockName)
        {
            // Skip empty block, specifically. This is a mine / quarry / pit, we don't care about empty space.
            if (blockName.Contains("EmptyBlock"))
            {
                return new LocString("");
            }

            // Turn "Eco.Mods.TechTree.SulfurBlock" into "SulfurItem"
            string itemName = blockName.Split(".")[^1].Replace("Block", "Item");

            // Look for a "rich" UI link (example: ... <icon name="SandstoneItem"> ...)
            Item? item = Item.AllItemsExceptHidden.FirstOrDefault(i =>
                itemName.ToLower() == i.Name.ToString()!.ToLower()
            );
            LocString itemDisplayName = item.UILink();

            return itemDisplayName;
        }
    }
}
