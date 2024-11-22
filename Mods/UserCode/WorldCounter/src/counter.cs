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
    using Eco.Shared.Utils;
    using Eco.Simulation.Types;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.WorldGenerator;

    public class Counter
    {
        public static SortedDictionary<string, int> GetCounts(Vector3 position, int radius = 100)
        {
            List<Vector3> blockLocations = GeneratePositionsToCheck(position, radius);

            // Count the number of each block type in the area
            SortedDictionary<string, int> blockCount = new();
            foreach (Vector3 blockLocation in blockLocations)
            {
                Block block = World.GetBlock((Eco.Shared.Math.Vector3i)blockLocation);
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
                    if (blockCount.ContainsKey(displayName))
                    {
                        blockCount[displayName] += 1;
                    }
                    else
                    {
                        blockCount[displayName] = 1;
                    }
                }
            }

            // Round block counts to 2 significant figures
            SortedDictionary<string, int> roundedBlockCount = new();
            foreach (KeyValuePair<string, int> kvp in blockCount)
            {
                roundedBlockCount[kvp.Key] = RoundNumber(kvp.Value);
            }
            return roundedBlockCount;
        }

        private static List<Vector3> GeneratePositionsToCheck(Vector3 position, int radius)
        {
            // Get a list of Vector3s that are within a 3d radius of the input position
            List<Vector3> positionsToCheck = new();

            // I assume x is the width and z is the length, but that may be wrong
            int worldWidth = WorldGeneratorPlugin.Settings.Dimensions.WorldWidth;
            int WorldLength = WorldGeneratorPlugin.Settings.Dimensions.WorldLength;

            for (int x = -radius; x <= radius; x++)
            {
                for (int z = -radius; z <= radius; z++)
                {
                    for (int y = 0; y <= 160; y++)
                    {
                        Vector3 offset = new(x, y, z);
                        if (offset.Length() <= radius)
                        {
                            Vector3 newPosition = position + offset;

                            // Wrap around the x-coordinate based on world width
                            if (newPosition.X < 0)
                            {
                                newPosition.X += worldWidth;
                            }
                            else if (newPosition.X >= worldWidth)
                            {
                                newPosition.X -= worldWidth;
                            }

                            // Wrap around the z-coordinate based on world length
                            if (newPosition.Z < 0)
                            {
                                newPosition.Z += WorldLength;
                            }
                            else if (newPosition.Z >= WorldLength)
                            {
                                newPosition.Z -= WorldLength;
                            }

                            positionsToCheck.Add(newPosition);
                        }
                    }
                }
            }
            return positionsToCheck;
        }

        private static int RoundNumber(int number)
        {
            if (number < 100)
            {
                return number;
            }
            int magnitude = (int)Math.Pow(10, (int)Math.Log10(number) - 1);
            return number / magnitude * magnitude;
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
