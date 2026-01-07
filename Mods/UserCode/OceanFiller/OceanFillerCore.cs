namespace OceanFiller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Gameplay.Players;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.States;
    using Eco.Simulation.WorldLayers;

    public class OceanFillerCore
    {
        public static void SetBiome(
            Vector2i worldPos,
            BiomeType previousBiome,
            BiomeType targetBiome
        )
        {
            WorldLayerManager.Obj.GetLayer(previousBiome.GetName()).SetAtWorldPos(worldPos, 0f);

            WorldLayerManager.Obj.GetLayer(targetBiome.GetName()).SetAtWorldPos(worldPos, 1f);
        }

        public static void TurnDeepOceanIntoShallowOcean(Vector2i worldPos)
        {
            SetBiome(worldPos, BiomeType.DeepOceanBiome, BiomeType.OceanBiome);
        }

        public static void SetShallowOceanAroundSelf(User user, int radius = 5)
        {
            Vector2i worldPos = user.Position.XZi();
            int radiusSq = radius * radius;
            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    if ((x * x) + (y * y) <= radiusSq)
                    {
                        Vector2i posToSet = new(worldPos.X + x, worldPos.Y + y);
                        TurnDeepOceanIntoShallowOcean(posToSet);
                    }
                }
            }
        }
    }
}
