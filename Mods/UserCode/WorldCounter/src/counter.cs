namespace WorldCounter
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Plants;
    using Eco.Gameplay.Systems.NewTooltip;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Math;
    using Eco.Shared.Utils;
    using Eco.Simulation.Types;
    using Eco.World;
    using Eco.World.Blocks;

    public class Counter
    {
        public static Dictionary<WrappedWorldPosition3i, (string, int)> GetCounts(
            Vector3 position,
            int radius = 60
        )
        {
            List<WrappedWorldPosition3i> blockLocations = GeneratePositionsToCheck(
                position,
                radius
            );
            Dictionary<WrappedWorldPosition3i, (string, int)> blockCounts = new();

            // Count the number of each block type in the area
            foreach (WrappedWorldPosition3i blockLocation in blockLocations)
            {
                Block block = World.GetBlock(blockLocation);
                if (block != null)
                {
                    // Skip blocks that should be excluded
                    if (ExcludeBlock(block))
                    {
                        continue;
                    }

                    // Get the display name of the block, skip blocks for which you can't find a name
                    string displayName = GetName(block.GetType());
                    if (displayName == "")
                    {
                        continue;
                    }

                    // Increment the count of the block
                    blockCounts[blockLocation] = (displayName, 1);
                    if (blockCounts.ContainsKey(blockLocation))
                    {
                        blockCounts[blockLocation] = (
                            displayName,
                            blockCounts[blockLocation].Item2 + 1
                        );
                    }
                    else
                    {
                        blockCounts[blockLocation] = (displayName, 1);
                    }
                }
            }
            return blockCounts;
        }

        private static List<WrappedWorldPosition3i> GeneratePositionsToCheck(
            Vector3 position,
            int radius
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
                    for (int y = 0; y <= World.GetTopBlockY(XZ); y++)
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

        private static bool ExcludeBlock(Block block)
        {
            Type blockType = block.GetType();
            if (blockType.DerivesFrom<PlantSpecies>())
            {
                return true;
            }
            else if (blockType.DerivesFrom<WorldObject>())
            {
                return true;
            }
            else if (block is WaterBlock or EncasedWaterBlock or PlantBlock)
            {
                return true;
            }
            return false;
        }

        private static string GetName(Type blockType)
        {
            Item? item;

            item = blockType.TryGetAttribute(false, out Ramp? rampAttr)
                ? Item.Get(rampAttr?.RampType)
                : BlockItem.GetBlockItem(blockType) ?? BlockItem.CreatingItem(blockType);

            if (item == null && blockType.DerivesFrom<WorldObject>())
            {
                item = WorldObjectItem.GetCreatingItemTemplateFromType(blockType);
            }

            if (item != null)
            {
                return item.UILink();
            }

            if (blockType.BaseType != null && blockType.BaseType != typeof(Block))
            {
                return GetName(blockType.BaseType);
            }

            // Return empty if you can't find a UILink
            return "";
        }
    }
}
