using NBTFile;
using NBTFile.tags;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace StructureArt;

internal static class Program
{
    // TODO: This sucks.
    private static readonly List<ColorToBlock> ColorToBlocks = new()
    {
        new ColorToBlock(103, 96, 86, "acacia_log"),
        new ColorToBlock(168, 90, 50, "acacia_planks"),
        new ColorToBlock(136, 136, 136, "andesite"),
        new ColorToBlock(85, 85, 85, "bedrock"),
        new ColorToBlock(216, 215, 210, "birch_log"),
        new ColorToBlock(8, 10, 15, "black_concrete"),
        new ColorToBlock(67, 30, 32, "black_glazed_terracotta"),
        new ColorToBlock(37, 22, 16, "black_terracotta"),
        new ColorToBlock(20, 21, 25, "black_wool"),
        new ColorToBlock(44, 46, 143, "blue_concrete"),
        new ColorToBlock(47, 64, 139, "blue_glazed_terracotta"),
        new ColorToBlock(74, 59, 91, "blue_terracotta"),
        new ColorToBlock(53, 57, 157, "blue_wool"),
        new ColorToBlock(229, 225, 207, "bone_block"),
        new ColorToBlock(117, 94, 59, "bookshelf"),
        new ColorToBlock(150, 97, 83, "bricks"),
        new ColorToBlock(96, 59, 31, "brown_concrete"),
        new ColorToBlock(119, 106, 85, "brown_glazed_terracotta"),
        new ColorToBlock(149, 111, 81, "brown_mushroom_block"),
        new ColorToBlock(77, 51, 35, "brown_terracotta"),
        new ColorToBlock(114, 71, 40, "brown_wool"),
        new ColorToBlock(231, 226, 218, "chiseled_quartz_block"),
        new ColorToBlock(216, 202, 155, "chiseled_sandstone"),
        new ColorToBlock(119, 118, 119, "chiseled_stone_bricks"),
        new ColorToBlock(16, 15, 15, "coal_block"),
        new ColorToBlock(105, 105, 105, "coal_ore"),
        new ColorToBlock(127, 127, 127, "cobblestone"),
        new ColorToBlock(124, 125, 120, "copper_ore"),
        new ColorToBlock(118, 117, 118, "cracked_stone_bricks"),
        new ColorToBlock(128, 102, 63, "crafting_table"),
        new ColorToBlock(101, 48, 70, "crimson_planks"),
        new ColorToBlock(21, 119, 136, "cyan_concrete"),
        new ColorToBlock(52, 118, 125, "cyan_glazed_terracotta"),
        new ColorToBlock(86, 91, 91, "cyan_terracotta"),
        new ColorToBlock(21, 137, 145, "cyan_wool"),
        new ColorToBlock(60, 46, 26, "dark_oak_log"),
        new ColorToBlock(66, 43, 20, "dark_oak_planks"),
        new ColorToBlock(51, 91, 75, "dark_prismarine"),
        new ColorToBlock(98, 237, 228, "diamond_block"),
        new ColorToBlock(121, 141, 140, "diamond_ore"),
        new ColorToBlock(188, 188, 188, "diorite"),
        new ColorToBlock(134, 96, 67, "dirt"),
        new ColorToBlock(42, 203, 87, "emerald_block"),
        new ColorToBlock(108, 136, 115, "emerald_ore"),
        new ColorToBlock(219, 222, 158, "end_stone"),
        new ColorToBlock(218, 224, 162, "end_stone_bricks"),
        new ColorToBlock(171, 131, 84, "glowstone"),
        new ColorToBlock(246, 208, 61, "gold_block"),
        new ColorToBlock(145, 133, 106, "gold_ore"),
        new ColorToBlock(149, 103, 85, "granite"),
        new ColorToBlock(54, 57, 61, "gray_concrete"),
        new ColorToBlock(83, 90, 93, "gray_glazed_terracotta"),
        new ColorToBlock(57, 42, 35, "gray_terracotta"),
        new ColorToBlock(62, 68, 71, "gray_wool"),
        new ColorToBlock(73, 91, 36, "green_concrete"),
        new ColorToBlock(117, 142, 67, "green_glazed_terracotta"),
        new ColorToBlock(84, 109, 27, "green_wool"),
        new ColorToBlock(166, 136, 38, "hay_block"),
        new ColorToBlock(220, 220, 220, "iron_block"),
        new ColorToBlock(136, 129, 122, "iron_ore"),
        new ColorToBlock(85, 67, 25, "jungle_log"),
        new ColorToBlock(160, 115, 80, "jungle_planks"),
        new ColorToBlock(107, 117, 141, "lapis_ore"),
        new ColorToBlock(35, 137, 198, "light_blue_concrete"),
        new ColorToBlock(94, 164, 208, "light_blue_glazed_terracotta"),
        new ColorToBlock(113, 108, 137, "light_blue_terracotta"),
        new ColorToBlock(58, 175, 217, "light_blue_wool"),
        new ColorToBlock(125, 125, 115, "light_gray_concrete"),
        new ColorToBlock(144, 166, 167, "light_gray_glazed_terracotta"),
        new ColorToBlock(135, 106, 97, "light_gray_terracotta"),
        new ColorToBlock(142, 142, 134, "light_gray_wool"),
        new ColorToBlock(94, 168, 24, "lime_concrete"),
        new ColorToBlock(162, 197, 55, "lime_glazed_terracotta"),
        new ColorToBlock(112, 185, 25, "lime_wool"),
        new ColorToBlock(169, 48, 159, "magenta_concrete"),
        new ColorToBlock(208, 100, 191, "magenta_glazed_terracotta"),
        new ColorToBlock(149, 88, 108, "magenta_terracotta"),
        new ColorToBlock(189, 68, 179, "magenta_wool"),
        new ColorToBlock(142, 63, 31, "magma_block"),
        new ColorToBlock(114, 146, 30, "melon"),
        new ColorToBlock(110, 118, 94, "mossy_cobblestone"),
        new ColorToBlock(115, 121, 105, "mossy_stone_bricks"),
        new ColorToBlock(97, 38, 38, "netherrack"),
        new ColorToBlock(44, 21, 26, "nether_bricks"),
        new ColorToBlock(117, 65, 62, "nether_quartz_ore"),
        new ColorToBlock(114, 2, 2, "nether_wart_block"),
        new ColorToBlock(88, 58, 40, "note_block"),
        new ColorToBlock(109, 85, 50, "oak_log"),
        new ColorToBlock(162, 130, 78, "oak_planks"),
        new ColorToBlock(15, 10, 24, "obsidian"),
        new ColorToBlock(224, 97, 0, "orange_concrete"),
        new ColorToBlock(154, 147, 91, "orange_glazed_terracotta"),
        new ColorToBlock(161, 83, 37, "orange_terracotta"),
        new ColorToBlock(240, 118, 19, "orange_wool"),
        new ColorToBlock(213, 101, 142, "pink_concrete"),
        new ColorToBlock(235, 154, 181, "pink_glazed_terracotta"),
        new ColorToBlock(161, 78, 78, "pink_terracotta"),
        new ColorToBlock(237, 141, 172, "pink_wool"),
        new ColorToBlock(132, 134, 133, "polished_andesite"),
        new ColorToBlock(192, 193, 194, "polished_diorite"),
        new ColorToBlock(154, 106, 89, "polished_granite"),
        new ColorToBlock(99, 171, 158, "prismarine_bricks"),
        new ColorToBlock(100, 31, 156, "purple_concrete"),
        new ColorToBlock(109, 48, 152, "purple_glazed_terracotta"),
        new ColorToBlock(121, 42, 172, "purple_wool"),
        new ColorToBlock(169, 125, 169, "purpur_block"),
        new ColorToBlock(171, 129, 171, "purpur_pillar"),
        new ColorToBlock(235, 229, 222, "quartz_block"),
        new ColorToBlock(235, 230, 224, "quartz_pillar"),
        new ColorToBlock(175, 24, 5, "redstone_block"),
        new ColorToBlock(95, 54, 30, "redstone_lamp"),
        new ColorToBlock(140, 109, 109, "redstone_ore"),
        new ColorToBlock(142, 32, 32, "red_concrete"),
        new ColorToBlock(181, 59, 53, "red_glazed_terracotta"),
        new ColorToBlock(200, 46, 45, "red_mushroom_block"),
        new ColorToBlock(69, 7, 9, "red_nether_bricks"),
        new ColorToBlock(186, 99, 29, "red_sandstone"),
        new ColorToBlock(143, 61, 46, "red_terracotta"),
        new ColorToBlock(160, 39, 34, "red_wool"),
        new ColorToBlock(216, 203, 155, "sandstone"),
        new ColorToBlock(172, 199, 190, "sea_lantern"),
        new ColorToBlock(111, 192, 91, "slime_block"),
        new ColorToBlock(81, 62, 50, "soul_sand"),
        new ColorToBlock(195, 192, 74, "sponge"),
        new ColorToBlock(58, 37, 16, "spruce_log"),
        new ColorToBlock(114, 84, 48, "spruce_planks"),
        new ColorToBlock(125, 125, 125, "stone"),
        new ColorToBlock(122, 121, 122, "stone_bricks"),
        new ColorToBlock(152, 94, 67, "terracotta"),
        new ColorToBlock(43, 104, 99, "warped_planks"),
        new ColorToBlock(207, 213, 214, "white_concrete"),
        new ColorToBlock(188, 212, 202, "white_glazed_terracotta"),
        new ColorToBlock(209, 178, 161, "white_terracotta"),
        new ColorToBlock(233, 236, 236, "white_wool"),
        new ColorToBlock(240, 175, 21, "yellow_concrete"),
        new ColorToBlock(234, 192, 88, "yellow_glazed_terracotta"),
        new ColorToBlock(186, 133, 35, "yellow_terracotta"),
        new ColorToBlock(248, 197, 39, "yellow_wool")
    };

    private static int GetClosestColorIndex(List<ColorToBlock> colors, Rgba32 color)
    {
        var closestIndex = 0;
        var shortestDist = double.MaxValue;

        for (var i = 0; i < colors.Count; i++)
        {
            var current = colors[i];
            var dist = Math.Sqrt(Math.Pow(current.R - color.R, 2) + Math.Pow(current.G - color.G, 2) +
                                 Math.Pow(current.B - color.B, 2));

            if (dist <= shortestDist)
            {
                closestIndex = i;
                shortestDist = dist;
            }
        }

        return closestIndex;
    }

    private static NbtFile GenerateStructureFromImage(Image<Rgba32> image, int width, int height, string outPath,
        string rootName)
    {
        var structureFile = new NbtFile(outPath, rootName);

        // Blocks
        var blocksTag = new ListTag("blocks");

        // Used for when blocks need to find a block id inside of the palette tag.
        var blocksInPalette = new List<string>();

        for (var x = 0; x < width; x++)
        for (var y = 0; y < height; y++)
        {
            var blockContainer = new CompoundTag();

            // Stores info on a block's relative position and block id from State tag.
            var blockPos = new ListTag("pos");

            // height - y otherwise the build will be upside down. Minecraft Y increases as you go up.
            blockPos.AddTag(new IntTag(x)).AddTag(new IntTag(height - 1 - y)).AddTag(new IntTag(0));

            blockContainer.AddTag(blockPos);

            // State (container for block names)
            var chosenBlock = ColorToBlocks[GetClosestColorIndex(ColorToBlocks, image[x, y])].Block;

            if (blocksInPalette.Contains(chosenBlock))
            {
                blockContainer.AddTag(new IntTag(blocksInPalette.IndexOf(chosenBlock), "state"));
            }
            else
            {
                blockContainer.AddTag(new IntTag(blocksInPalette.Count, "state"));
                blocksInPalette.Add(chosenBlock);
            }

            blocksTag.AddTag(blockContainer);
        }

        structureFile.GetRoot().AddTag(blocksTag);

        // Palette
        var paletteTag = new ListTag("palette");

        foreach (var block in
                 blocksInPalette)
        {
            var paletteBlock = new CompoundTag();
            paletteBlock.AddTag(new StringTag(block, "Name"));
            paletteTag.AddTag(paletteBlock);
        }

        structureFile.GetRoot().AddTag(paletteTag);

        // Size (only for bounding box)
        var sizeTag = new ListTag("size");
        sizeTag.AddTag(new IntTag(width));
        sizeTag.AddTag(new IntTag(height));
        sizeTag.AddTag(new IntTag(1));

        structureFile.GetRoot().AddTag(sizeTag);

        return structureFile;
    }

    private static NbtFile GenerateStructureFromImage(string imagePath, int width, int height, string outPath,
        string rootName)
    {
        var image = Image.Load<Rgba32>(imagePath);
        image.Mutate(x => x.Resize(width, height));

        var result = GenerateStructureFromImage(image, width, height, outPath, rootName);

        image.Dispose();

        return result;
    }


    /*
     * Image path
     * Structure width
     * Structure height
     */
    public static void Main(string[] args)
    {
        // Arguments
        if (args.Length < 3)
        {
            Console.WriteLine("Not enough arguments: image_path structure_width structure_height");
            return;
        }

        var framesPath = args[0];

        if (!Directory.Exists(framesPath))
        {
            Console.WriteLine("Failed to find folder.");
            return;
        }

        var structureWidth = int.Parse(args[1]);
        var structureHeight = int.Parse(args[2]);

        // Begin generating
        Console.WriteLine($"Generating {framesPath} at {structureWidth}x{structureHeight}...");

        var frames = Directory.GetFiles(framesPath);

        for (var i = 0; i < frames.Length; i++)
        {
            Console.WriteLine($"Frame {i} start.");

            // TODO: Create dirs at start

            // Structure file
            var structureFile = GenerateStructureFromImage(frames[i], structureWidth, structureHeight,
                $"structures/frame{i}.nbt",
                $"frame{i}");
            structureFile.WriteToFile();
            structureFile.Close();

            // Function file
            var functionFile = new StreamWriter(File.Open($"functions/frame{i}.mcfunction", FileMode.Create));
            functionFile.WriteLine(
                $"setblock 0 0 0 minecraft:structure_block{{mode:\"LOAD\",name:\"frame{i}\"}} destroy");
            functionFile.WriteLine("setblock 0 -1 0 minecraft:air replace");
            functionFile.WriteLine("setblock 0 -1 0 minecraft:redstone_block replace");
            functionFile.WriteLine(
                $"schedule function framestoblocks:frame{i + 1} 1t replace");

            functionFile.Close();

            Console.WriteLine($"Frame {i} done.");
        }

        Console.WriteLine("Done!");
    }

    private struct ColorToBlock
    {
        public readonly int R;
        public readonly int G;
        public readonly int B;
        public readonly string Block;


        public ColorToBlock(int red, int green, int blue, string block)
        {
            R = red;
            G = green;
            B = blue;
            Block = block;
        }
    }
}
