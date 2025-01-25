namespace EcoNil
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Math;
    using Eco.Shared.Utils;
    using Eco.Simulation.WorldLayers;
    using Eco.Simulation.WorldLayers.Layers;

    public class SharedComponent : WorldObjectComponent
    {
        public float AverageLayerValue(int radius, string layerName)
        {
            IEnumerable<Vector3i> positions = this.RelevantPositions(radius);
            WorldLayer layer = WorldLayerManager.Obj.GetLayer(layerName);

            float sum = 0;
            foreach (Vector3i position in positions)
            {
                sum += layer.EntryWorldPos(position.XZ);
            }

            return sum / positions.Count();
        }

        public void AdjustLayers(int radius, string layerName, float adjustment, float average)
        {
            WorldLayer layer = WorldLayerManager.Obj.GetLayer(layerName);
            IEnumerable<Vector3i> positions = this.RelevantPositions(radius);
            positions.ForEach(pos => layer.SetAtWorldPos(pos.XZ, average + adjustment));
            layer.Modify();
        }

        public IEnumerable<Vector3i> RelevantPositions(int radius)
        {
            List<WrappedWorldPosition3i> positions = new();
            WrappedWorldPosition3i wrappedPosition = WrappedWorldPosition3i.Create(
                this.Parent.Position3i.X,
                0,
                this.Parent.Position3i.Z
            );

            for (int x = -radius; x <= radius; x++)
            {
                for (int z = -radius; z <= radius; z++)
                {
                    if (Math.Sqrt(Math.Pow(x, 2) + Math.Pow(z, 2)) <= radius)
                    {
                        WrappedWorldPosition3i positionToCheck = WrappedWorldPosition3i.Create(
                            wrappedPosition.X + x,
                            0,
                            wrappedPosition.Z + z
                        );
                        positions.Add(positionToCheck);
                    }
                }
            }

            List<Vector3i> plainPositions = new();
            foreach (WrappedWorldPosition3i position in positions)
            {
                plainPositions.Add(new Vector3i(position.X, position.Y, position.Z));
            }

            return plainPositions.AsEnumerable();
        }
    }
}
