namespace WorldCounter
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;
    using Eco.World;
    using Eco.World.Blocks;

    public class Counter
    {
        public static SortedDictionary<string, int> GetCounts(Vector3 position, int radius = 100)
        {
            {
                IEnumerable<PersistentChunk> Chunks = World.Chunks;
                SortedDictionary<string, int> blockCount = new();
                // Count blocks
                foreach (PersistentChunk chunk in Chunks)
                {
                    if (chunk is null)
                    {
                        continue;
                    }
                    if (chunk.Blocks is null)
                    {
                        continue;
                    }

                    // check if within radius...
                    // ...this should be checking against the block positions, not the chunk position
                    if (
                        Vector3.Distance(
                            new Vector3(position.X, 0, position.Z),
                            new Vector3(chunk.Position.X, 0, chunk.Position.Z)
                        ) > radius
                    )
                    {
                        continue;
                    }

                    foreach (Block? block in chunk.Blocks)
                    {
                        // Get name of block
                        if (block is null)
                        {
                            continue;
                        }

                        // Only count blocks from Eco.Mods.TechTree
                        string fullName = block.GetType().ToString();
                        if (!fullName.StartsWith("Eco.Mods.TechTree"))
                        {
                            continue;
                        }

                        // Remove namespace
                        string shortName = fullName["Eco.Mods.TechTree.".Length..];

                        // Add spaces between capital letters
                        string displayName = "";
                        foreach (char c in shortName)
                        {
                            if (char.IsUpper(c) && displayName.Length > 0)
                            {
                                displayName += " ";
                            }
                            displayName += c;
                        }

                        // Remove " Block"
                        displayName = displayName[0..^6];

                        // Increment count
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
    }
}
