namespace MinesQuarries
{
    using System.Collections.Generic;
    using System.Numerics;
    using Eco.Shared.Math;
    using Eco.World;
    using Eco.World.Blocks;

    public class Search
    {
        public static (bool, float) FindBlocks(Vector3 position, string blockType, int radius) =>
            FindBlocks(position, blockType, radius, true);

        public static (bool, float) FindBlocks(
            Vector3 position,
            string blockType,
            int radius,
            bool infiniteYAxis
        )
        {
            List<WrappedWorldPosition3i> blockLocations = GeneratePositionsToCheck(
                position,
                radius,
                infiniteYAxis
            );

            foreach (WrappedWorldPosition3i blockLocation in blockLocations)
            {
                Block block = World.GetBlock(blockLocation);
                if (block != null)
                {
                    if (block.GetType().FullName == blockType)
                    {
                        return (true, 0);
                    }
                }
            }
            return (false, 0);
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
                    if (infiniteYAxis)
                    {
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
                    else
                    {
                        for (int y = 0; y <= radius; y++)
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
            }
            return positionsToCheck;
        }
    }
}
