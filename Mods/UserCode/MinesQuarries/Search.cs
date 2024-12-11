namespace MinesQuarries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
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
                infiniteYAxis: true
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

        public static Dictionary<string, float> FindBlockCounts(Vector3 position, int radius)
        {
            List<WrappedWorldPosition3i> blockLocations = GeneratePositionsToCheck(
                position,
                radius,
                infiniteYAxis: false
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

                    // Turns "Eco.Mods.TechTree.SulfurBlock" into "SulfurItem"
                    string itemName = blockName.Split(".")[3].Replace("Block", "Item");

                    // Skip anything without a UILink (example: Air)
                    if (GetItemUILink(itemName) == new LocString())
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

        private static List<WrappedWorldPosition3i> GeneratePositionsToCheck(
            Vector3 position,
            int radius,
            bool infiniteYAxis
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
                    for (int y = 0; y <= (infiniteYAxis ? World.GetTopBlockY(XZ) : radius); y++)
                    {
                        WrappedWorldPosition3i positionToCheck = WrappedWorldPosition3i.Create(
                            wrappedPosition.X + x,
                            y,
                            wrappedPosition.Z + z
                        );
                        positionsToCheck.Add(positionToCheck);
                    }
                }
            }
            return positionsToCheck;
        }

        public static LocString GetItemUILink(string itemName)
        {
            Item? item = Item.AllItemsExceptHidden.FirstOrDefault(i =>
                itemName == i.DisplayName.ToString()!.ToLower()
            );
            return item.UILink();
        }
    }
}
