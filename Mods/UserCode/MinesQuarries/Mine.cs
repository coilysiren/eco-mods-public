namespace Mineshafts
{
    using System.Collections.Generic;
    using System.Numerics;
    using Eco.Shared.Math;
    using Eco.World;
    using Eco.World.Blocks;

    public class Mine
    {
        public static bool FindBlock(Vector3 position, string blockType, int radius)
        {
            List<WrappedWorldPosition3i> blockLocations = GeneratePositionsToCheck(
                position,
                radius
            );

            foreach (WrappedWorldPosition3i blockLocation in blockLocations)
            {
                Block block = World.GetBlock(blockLocation);
                if (block != null)
                {
                    if (block.GetType().FullName == blockType)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static List<WrappedWorldPosition3i> GeneratePositionsToCheck(
            Vector3 position,
            int radius
        )
        {
            // Get a list of Vector3s that are within a 3d radius of the input position
            List<WrappedWorldPosition3i> positionsToCheck = new();
            WrappedWorldPosition3i wrappedPosition = WrappedWorldPosition3iCreate(
                position.X,
                position.Y,
                position.Z
            );

            for (int x = -radius; x <= radius; x++)
            {
                for (int z = -radius; z <= radius; z++)
                {
                    Vector2i XZ = new(x, z);
                    for (int y = 0; y <= GetTopBlockY(XZ); y++)
                    {
                        WrappedWorldPosition3i positionToCheck = WrappedWorldPosition3iCreate(
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

        private static WrappedWorldPosition3i WrappedWorldPosition3iCreate(
            float x,
            float y,
            float z
        ) => WrappedWorldPosition3i.Create(x, y, z);

        private static int GetTopBlockY(Vector2i worldPos) => World.GetTopBlockY(worldPos);
    }
}
