import os
import shutil
import stat

import invoke


AUTOGEN_PATH = os.path.join(
    "C:\\",
    "Program Files (x86)",
    "Steam",
    "steamapps",
    "common",
    "Eco",
    "Eco_Data",
    "Server",
    "Mods",
    "__core__",
    "AutoGen",
)

USERNAME = os.getenv("USERNAME", "")

BUNWULF_CONSTRUCTION_PATH = os.path.join(
    "C:\\", "Users", USERNAME, "projects", "eco-mods-public", "Mods", "UserCode", "BunWulfConstruction", "Recipes"
)

BUNWULF_LIBRARIAN_PATH = os.path.join(
    "C:\\", "Users", USERNAME, "projects", "eco-mods-public", "Mods", "UserCode", "BunWulfLibrarian", "Recipes"
)

BUNWULF_AGRICULTURAL_PATH = os.path.join(
    "C:\\", "Users", USERNAME, "projects", "eco-mods-public", "Mods", "UserCode", "BunWulfAgricultural", "Recipes"
)


class TextProcessingException(Exception):
    pass


class RemovalException(Exception):
    pass


def handleRemoveReadonly(func, path, _):
    if not os.access(path, os.W_OK):
        os.chmod(path, stat.S_IWUSR)
        func(path)
    else:
        raise RemovalException("could not handle path")


def copy_paths(origin_path, target_path):
    if not os.path.isdir(origin_path):
        return
    if os.path.exists(target_path) and os.path.isdir(target_path):
        print(f"\tRemoving {target_path}")
        shutil.rmtree(target_path, ignore_errors=False, onerror=handleRemoveReadonly)
    if os.path.isdir(origin_path):
        print(f"\tCopying {origin_path} to {target_path}")
        shutil.copytree(origin_path, target_path)


@invoke.task
def copy_assets(ctx: invoke.Context, branch=""):
    print("Cleaning out assets folder")
    if os.path.exists("./eco-server/assets"):
        shutil.rmtree("./eco-server/assets", ignore_errors=False, onerror=handleRemoveReadonly)

    # get assets from git
    branch_flag = ""
    if branch != "":
        branch_flag = f"-b {branch}"
    ctx.run(
        f"git clone --depth 1 {branch_flag} -- git@github.com:coilysiren/eco-mods-assets.git ./eco-server/assets",
        echo=True,
    )
    shutil.rmtree("./eco-server/assets/.git", ignore_errors=False, onerror=handleRemoveReadonly)

    for build in os.listdir("./eco-server/assets/Builds/Mods/UserCode/"):
        origin_path = os.path.join("./eco-server/assets/Builds/Mods/UserCode", build, "Assets")
        target_path = os.path.join("./Mods/UserCode", build, "Assets")
        copy_paths(origin_path, target_path)


@invoke.task
def zip_assets(ctx: invoke.Context, mod):
    if os.path.exists(f"{mod}.zip"):
        os.remove(f"{mod}.zip")
    ctx.run(f"zip -r {mod}.zip ./Mods/UserCode/{mod}")


#######################
# SPECIALITY SPECIFIC #
#######################


def process_recipes(recipes_changes, target_path):
    for file, changes in recipes_changes.items():
        print(f"Reading {file}")
        with open(os.path.join(AUTOGEN_PATH, file), "r", encoding=",utf-8") as f:
            recipe_data = f.read()

        for key, configs in changes.items():
            if configs[0] == "REMOVE-CLASS":
                recipe_data = remove_class(recipe_data, file, key, configs)
            elif configs[0] == "REMOVE-CONSTRUCTABLE":
                recipe_data = remove_constructable(recipe_data, file, key, configs)
            elif configs[0] == "REMOVE-LINE":
                recipe_data = remove_line(recipe_data, file, key, configs)
            else:
                recipe_data = replace_key(recipe_data, file, key, configs)

        print(f"\tWriting {file}")
        folder = file.split("\\", maxsplit=1)[0]
        os.makedirs(os.path.join(target_path, folder), exist_ok=True)
        with open(os.path.join(target_path, file), "w", encoding="utf-8") as f:
            f.write(recipe_data)


def remove_class(recipe_data, file, key, configs):
    recipe_lines = recipe_data.split("\n")
    print(f'\tRemoving "{configs[1]}" from recipe')

    # Step 1: Identify the line where the item starts.
    class_line = 0
    for line, value in enumerate(recipe_lines):
        if configs[1] in value:
            class_line = line
            break
    if class_line == 0:
        raise TextProcessingException(f"\t\tCouldn't find {configs[1]} in {file}:{key}")
    print(f'\t\tFound "{configs[1]}" at line {class_line}')

    # Step 2: Find all of the lines with the serialized attribute.
    if configs[2] == "serialized":
        serialized_lines = []
        for line, value in enumerate(recipe_lines[:class_line]):
            if value.strip().startswith("[Serialized]"):
                serialized_lines.append(line)
        # This should never happen, but just in case.
        if not serialized_lines:
            raise TextProcessingException(f"\t\tCouldn't find [Serialized] in {file}:{key}")
        serialized_line = serialized_lines[-1]
        class_line = serialized_line
        print(f"\t\t[Serialized] attribute line: {serialized_line}")

    # Step 3: Count brackets to find the end of the class.
    bracket_count = 0
    recipe_data = "\n".join(recipe_lines)
    class_start = recipe_data.find(configs[1])
    print(f"\t\tFound start of class at index {class_start}")
    print(f"\t\tLooking from index {class_start} to {len(recipe_data)}")
    backets_started = False

    for index in range(class_start, len(recipe_data)):
        char = recipe_data[index]
        if char == "{":
            bracket_count += 1
            backets_started = True
        elif char == "}":
            bracket_count -= 1
        elif backets_started and bracket_count == 0:
            class_end = index
            break
    if bracket_count != 0:
        raise TextProcessingException(f"\t\tCouldn't find end of class in {file}:{key}")
    print(f"\t\tFound end of class at index {class_end}")

    # Translate the class end index to a line number.
    class_end_line = recipe_data[:class_end].count("\n") + 1
    print(f"\t\tRemoving lines {class_line} to {class_end_line}")
    recipe_lines = recipe_data.split("\n")
    del recipe_lines[class_line:class_end_line]

    # # Step 5: Remove the lines from item_start_line to item_end_line.
    # print(f"\t\tRemoving lines {class_line} to {item_end_line}")
    # del recipe_lines[class_line : item_end_line + 1]

    # Step -1: Join the lines back together to get a single string.
    recipe_data = "\n".join(recipe_lines)
    return recipe_data


def remove_constructable(recipe_data, file, key, configs):
    recipe_lines = recipe_data.split("\n")
    print(f"\tRemoving {configs[1]} from recipe")

    # Step 1: Identify the first Tag line.
    constructable_line = 0
    for line, value in enumerate(recipe_lines):
        if configs[1] in value:
            constructable_line = line
            break
    if constructable_line == 0:
        raise TextProcessingException(f"\t\tCouldn't find {configs[1]} in {file}:{key}")

    # Step 2: This is the end of the file, so remove the lines from constructable_line to the end.
    print(f"\t\tRemoving lines {constructable_line} to {len(recipe_lines) - 2}")
    del recipe_lines[constructable_line : len(recipe_lines) - 2]

    # Step -1: Join the lines back together to get a single string.
    recipe_data = "\n".join(recipe_lines)
    return recipe_data


def remove_line(recipe_data, file, key, configs):
    recipe_lines = recipe_data.split("\n")
    print(f"\tRemoving line near {configs[1]} from recipe")

    # Step 1: Identify the line to remove.
    line_to_remove = 0
    for line, value in enumerate(recipe_lines):
        if configs[1] in value:
            line_to_remove = line
            break
    if line_to_remove == 0:
        raise TextProcessingException(f"\t\tCouldn't find {configs[1]} in {file}:{key}")

    # Step 2: Add offset
    line_to_remove += configs[2]

    # Step 3: Remove the line.
    print(f"\t\tRemoving line {line_to_remove}")
    del recipe_lines[line_to_remove]

    # Step -1: Join the lines back together to get a single string.
    recipe_data = "\n".join(recipe_lines)
    return recipe_data


def replace_key(recipe_data, file, key, configs):
    print(f"\tReplacing {key}")
    if configs[0] not in recipe_data:
        raise TextProcessingException(f"\t\tCouldn't find {configs[0]} in {file}:{key}")
    recipe_data = recipe_data.replace(configs[0], configs[1])
    return recipe_data


@invoke.task
def bunwulf_agricultural(_: invoke.Context):
    plant_changes = {
        "Ecopedia": ['[Ecopedia("Plants", "Plants", createAsSubPage: true)]', ""],
        "PlantsTag": ['[Tag("Plants")]', ""],
        "Localized": ["[Localized(false, true)]", ""],
        "Species": ["static PlantSpecies species;", ""],
        "PlantLayerSettings": [
            "REMOVE-CLASS",
            "PlantLayerSettings",
            "unserialized",
        ],
        "PlantSpecies": ["REMOVE-CLASS", "public WheatSpecies() : base()", "unserialized"],
        "ModsPostInitBracket": ["REMOVE-LINE", "ModsPostInitialize", 2],
        "Serialized": ["[Serialized]", ""],
    }

    recipe_changes = {
        r"Plant\Wheat.cs": {
            "PlantBlock": ["REMOVE-CLASS", "public partial class WheatBlock", "serialized"],
            "WorldPosition3i": [
                "public Wheat(WorldPosition3i mapPos, PlantPack plantPack) : base(species, mapPos, plantPack) { }",
                "",
            ],
            "PreSpecies": ["public Wheat() { }", ""],
            "ModsPostInit": [
                "partial void ModsPostInitialize() {",
                'partial void ModsPostInitialize() { MaturityAgeDays = MaturityAgeDays / 2; Name = "TOTALY NOT WHEAT"; }',
            ],
            **plant_changes,
        },
        # r"Plant\Tomatoes.cs": {},
        # r"Plant\Taro.cs": {},
        # r"Plant\Beans.cs": {},
        # r"Plant\Rice.cs": {},
        # r"Plant\Camas.cs": {},
        # r"Plant\BoleteMushroom.cs": {},
        # r"Plant\Pumpkin.cs": {},
        # r"Plant\PricklyPear.cs": {},
        # r"Plant\Beets.cs": {},
        # r"Plant\Pineapple.cs": {},
        # r"Plant\Papaya.cs": {},
        # r"Plant\Agave.cs": {},
        # r"Plant\CriminiMushroom.cs": {},
        # r"Plant\Huckleberry.cs": {},
        # r"Plant\Corn.cs": {},
        # r"Plant\CookeinaMushroom.cs": {},
        # r"Plant\Fireweed.cs": {},
        # r"Plant\Fern.cs": {},
    }

    process_recipes(recipe_changes, BUNWULF_AGRICULTURAL_PATH)


@invoke.task
def bunwulf_librarian(_: invoke.Context):
    recipe_changes = {
        r"Item\GeologyResearchPaperBasic.cs": {
            "item": ["REMOVE-CLASS", "public partial class GeologyResearchPaperBasicItem"],
            "class": ["GeologyResearchPaperBasicRecipe", "LibrarianGeologyResearchPaperBasicRecipe"],
            "level": ["RequiresSkill(typeof(MiningSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 1)"],
            "displayName": [
                'Localizer.DoStr("Geology Research Paper',
                'Localizer.DoStr("Librarian Geology Research Paper',
            ],
            "skill": ["MiningSkill", "LibrarianSkill"],
        },
        r"Item\CulinaryResearchPaperBasic.cs": {
            "item": ["REMOVE-CLASS", "public partial class CulinaryResearchPaperBasicItem"],
            "class": ["CulinaryResearchPaperBasicRecipe", "LibrarianCulinaryResearchPaperBasicRecipe"],
            "level": ["RequiresSkill(typeof(CampfireCookingSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 1)"],
            "displayName": [
                'Localizer.DoStr("Culinary Research Paper',
                'Localizer.DoStr("Librarian Culinary Research Paper',
            ],
            "skill": ["CampfireCookingSkill", "LibrarianSkill"],
        },
        r"Item\GatheringResearchPaperBasic.cs": {
            "item": ["REMOVE-CLASS", "public partial class GatheringResearchPaperBasicItem"],
            "class": ["GatheringResearchPaperBasicRecipe", "LibrarianGatheringResearchPaperBasicRecipe"],
            "level": ["RequiresSkill(typeof(GatheringSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 1)"],
            "displayName": [
                'Localizer.DoStr("Gathering Research Paper',
                'Localizer.DoStr("Librarian Gathering Research Paper',
            ],
            "skill": ["GatheringSkill", "LibrarianSkill"],
        },
        r"Item\DendrologyResearchPaperBasic.cs": {
            "item": ["REMOVE-CLASS", "public partial class DendrologyResearchPaperBasicItem"],
            "class": ["DendrologyResearchPaperBasicRecipe", "LibrarianDendrologyResearchPaperBasicRecipe"],
            "level": ["RequiresSkill(typeof(LoggingSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 1)"],
            "displayName": [
                'Localizer.DoStr("Dendrology Research Paper',
                'Localizer.DoStr("Librarian Dendrology Research Paper',
            ],
            "skill": ["LoggingSkill", "LibrarianSkill"],
        },
        r"Item\MetallurgyResearchPaperBasic.cs": {
            "item": ["REMOVE-CLASS", "public partial class MetallurgyResearchPaperBasicItem"],
            "class": ["MetallurgyResearchPaperBasicRecipe", "LibrarianMetallurgyResearchPaperBasicRecipe"],
            "level": ["RequiresSkill(typeof(MiningSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 1)"],
            "displayName": [
                'Localizer.DoStr("Metallurgy Research Paper',
                'Localizer.DoStr("Librarian Metallurgy Research Paper',
            ],
            "skill": ["MiningSkill", "LibrarianSkill"],
        },
        r"Recipe\CulinaryResearchPaperBasicFish.cs": {
            "class": ["CulinaryResearchPaperBasicFishRecipe", "LibrarianCulinaryResearchPaperBasicFishRecipe"],
            "level": ["RequiresSkill(typeof(HuntingSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 1)"],
            "displayName": [
                'Localizer.DoStr("Culinary Research Paper Basic Fish',
                'Localizer.DoStr("Librarian Culinary Research Paper Basic Fish',
            ],
            "skill": ["HuntingSkill", "LibrarianSkill"],
        },
        r"Recipe\CulinaryResearchPaperBasicMeat.cs": {
            "class": ["CulinaryResearchPaperBasicMeatRecipe", "LibrarianCulinaryResearchPaperBasicMeatRecipe"],
            "level": ["RequiresSkill(typeof(HuntingSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 1)"],
            "displayName": [
                'Localizer.DoStr("Culinary Research Paper Basic Meat',
                'Localizer.DoStr("Librarian Culinary Research Paper Basic Meat',
            ],
            "skill": ["HuntingSkill", "LibrarianSkill"],
        },
        r"Item\GeologyResearchPaperAdvanced.cs": {
            "item": ["REMOVE-CLASS", "public partial class GeologyResearchPaperAdvancedItem"],
            "class": ["GeologyResearchPaperAdvancedRecipe", "LibrarianGeologyResearchPaperAdvancedRecipe"],
            "level": ["RequiresSkill(typeof(MasonrySkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 2)"],
            "displayName": [
                'Localizer.DoStr("Geology Research Paper Advanced',
                'Localizer.DoStr("Librarian Geology Research Paper Advanced',
            ],
            "skill": ["MasonrySkill", "LibrarianSkill"],
        },
        r"Item\CulinaryResearchPaperAdvanced.cs": {
            "item": ["REMOVE-CLASS", "public partial class CulinaryResearchPaperAdvancedItem"],
            "class": ["CulinaryResearchPaperAdvancedRecipe", "LibrarianCulinaryResearchPaperAdvancedRecipe"],
            "level": ["RequiresSkill(typeof(CookingSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 2)"],
            "displayName": [
                'Localizer.DoStr("Culinary Research Paper Advanced',
                'Localizer.DoStr("Librarian Culinary Research Paper Advanced',
            ],
            "skill": ["CookingSkill", "LibrarianSkill"],
        },
        r"Item\DendrologyResearchPaperAdvanced.cs": {
            "item": ["REMOVE-CLASS", "public partial class DendrologyResearchPaperAdvancedItem"],
            "class": ["DendrologyResearchPaperAdvancedRecipe", "LibrarianDendrologyResearchPaperAdvancedRecipe"],
            "level": ["RequiresSkill(typeof(CarpentrySkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 2)"],
            "displayName": [
                'Localizer.DoStr("Dendrology Research Paper Advanced',
                'Localizer.DoStr("Librarian Dendrology Research Paper Advanced',
            ],
            "skill": ["CarpentrySkill", "LibrarianSkill"],
        },
        r"Item\MetallurgyResearchPaperAdvanced.cs": {
            "item": ["REMOVE-CLASS", "public partial class MetallurgyResearchPaperAdvancedItem"],
            "class": ["MetallurgyResearchPaperAdvancedRecipe", "LibrarianMetallurgyResearchPaperAdvancedRecipe"],
            "level": ["RequiresSkill(typeof(SmeltingSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 2)"],
            "displayName": [
                'Localizer.DoStr("Metallurgy Research Paper Advanced',
                'Localizer.DoStr("Librarian Metallurgy Research Paper Advanced',
            ],
            "skill": ["SmeltingSkill", "LibrarianSkill"],
        },
        r"Item\AgricultureResearchPaperAdvanced.cs": {
            "item": ["REMOVE-CLASS", "public partial class AgricultureResearchPaperAdvancedItem"],
            "class": ["AgricultureResearchPaperAdvancedRecipe", "LibrarianAgricultureResearchPaperAdvancedRecipe"],
            "level": ["RequiresSkill(typeof(FarmingSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 2)"],
            "displayName": [
                'Localizer.DoStr("Agriculture Research Paper Advanced',
                'Localizer.DoStr("Librarian Agriculture Research Paper Advanced',
            ],
            "skill": ["FarmingSkill", "LibrarianSkill"],
        },
        r"Item\EngineeringResearchPaperAdvanced.cs": {
            "item": ["REMOVE-CLASS", "public partial class EngineeringResearchPaperAdvancedItem"],
            "class": ["EngineeringResearchPaperAdvancedRecipe", "LibrarianEngineeringResearchPaperAdvancedRecipe"],
            "level": ["RequiresSkill(typeof(BasicEngineeringSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 2)"],
            "displayName": [
                'Localizer.DoStr("Engineering Research Paper Advanced',
                'Localizer.DoStr("Librarian Engineering Research Paper Advanced',
            ],
            "skill": ["BasicEngineeringSkill", "LibrarianSkill"],
        },
        r"Recipe\CulinaryResearchPaperAdvancedMeat.cs": {
            "class": ["CulinaryResearchPaperAdvancedMeatRecipe", "LibrarianCulinaryResearchPaperAdvancedMeatRecipe"],
            "level": ["RequiresSkill(typeof(BakingSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 2)"],
            "displayName": [
                'Localizer.DoStr("Culinary Research Paper Advanced Meat',
                'Localizer.DoStr("Librarian Culinary Research Paper Advanced Meat',
            ],
            "skill": ["BakingSkill", "LibrarianSkill"],
        },
        r"Item\GeologyResearchPaperModern.cs": {
            "item": ["REMOVE-CLASS", "public partial class GeologyResearchPaperModernItem"],
            "class": ["GeologyResearchPaperModernRecipe", "LibrarianGeologyResearchPaperModernRecipe"],
            "level": ["RequiresSkill(typeof(PotterySkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 3)"],
            "displayName": [
                'Localizer.DoStr("Geology Research Paper Modern',
                'Localizer.DoStr("Librarian Geology Research Paper Modern',
            ],
            "skill": ["PotterySkill", "LibrarianSkill"],
        },
        r"Item\CulinaryResearchPaperModern.cs": {
            "item": ["REMOVE-CLASS", "public partial class CulinaryResearchPaperModernItem"],
            "class": ["CulinaryResearchPaperModernRecipe", "LibrarianCulinaryResearchPaperModernRecipe"],
            "level": ["RequiresSkill(typeof(AdvancedCookingSkill), 2)", "RequiresSkill(typeof(LibrarianSkill), 3)"],
            "displayName": [
                'Localizer.DoStr("Culinary Research Paper Modern',
                'Localizer.DoStr("Librarian Culinary Research Paper Modern',
            ],
            "skill": ["AdvancedCookingSkill", "LibrarianSkill"],
        },
        r"Item\DendrologyResearchPaperModern.cs": {
            "item": ["REMOVE-CLASS", "public partial class DendrologyResearchPaperModernItem"],
            "class": ["DendrologyResearchPaperModernRecipe", "LibrarianDendrologyResearchPaperModernRecipe"],
            "level": ["RequiresSkill(typeof(CarpentrySkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 3)"],
            "displayName": [
                'Localizer.DoStr("Dendrology Research Paper Modern',
                'Localizer.DoStr("Librarian Dendrology Research Paper Modern',
            ],
            "skill": ["CarpentrySkill", "LibrarianSkill"],
        },
        r"Item\MetallurgyResearchPaperModern.cs": {
            "item": ["REMOVE-CLASS", "public partial class MetallurgyResearchPaperModernItem"],
            "class": ["MetallurgyResearchPaperModernRecipe", "LibrarianMetallurgyResearchPaperModernRecipe"],
            "level": ["RequiresSkill(typeof(AdvancedSmeltingSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 3)"],
            "displayName": [
                'Localizer.DoStr("Metallurgy Research Paper Modern',
                'Localizer.DoStr("Librarian Metallurgy Research Paper Modern',
            ],
            "skill": ["AdvancedSmeltingSkill", "LibrarianSkill"],
        },
        r"Item\AgricultureResearchPaperModern.cs": {
            "item": ["REMOVE-CLASS", "public partial class AgricultureResearchPaperModernItem"],
            "class": ["AgricultureResearchPaperModernRecipe", "LibrarianAgricultureResearchPaperModernRecipe"],
            "level": ["RequiresSkill(typeof(FertilizersSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 3)"],
            "displayName": [
                'Localizer.DoStr("Agriculture Research Paper Modern',
                'Localizer.DoStr("Librarian Agriculture Research Paper Modern',
            ],
            "skill": ["FertilizersSkill", "LibrarianSkill"],
        },
        r"Item\EngineeringResearchPaperModern.cs": {
            "item": ["REMOVE-CLASS", "public partial class EngineeringResearchPaperModernItem"],
            "class": ["EngineeringResearchPaperModernRecipe", "LibrarianEngineeringResearchPaperModernRecipe"],
            "level": ["RequiresSkill(typeof(MechanicsSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 3)"],
            "displayName": [
                'Localizer.DoStr("Engineering Research Paper Modern',
                'Localizer.DoStr("Librarian Engineering Research Paper Modern',
            ],
            "skill": ["MechanicsSkill", "LibrarianSkill"],
        },
        r"Recipe\GeologyResearchPaperModernGlass.cs": {
            "class": ["GeologyResearchPaperModernGlassRecipe", "LibrarianGeologyResearchPaperModernGlassRecipe"],
            "level": ["RequiresSkill(typeof(GlassworkingSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 3)"],
            "displayName": [
                'Localizer.DoStr("Geology Research Paper Modern Glass',
                'Localizer.DoStr("Librarian Geology Research Paper Modern Glass',
            ],
            "skill": ["GlassworkingSkill", "LibrarianSkill"],
        },
    }

    process_recipes(recipe_changes, BUNWULF_LIBRARIAN_PATH)


@invoke.task
def bunwulf_construction(_: invoke.Context):
    recipes_changes = {
        r"Block\Brick.cs": {
            "level": ["RequiresSkill(typeof(PotterySkill), 1)", "RequiresSkill(typeof(ConstructionSkill), 2)"],
            "displayName": ['Localizer.DoStr("Brick")', 'Localizer.DoStr("Builder Grade Brick")'],
            "class": ["BrickRecipe", "ConstructionBrickRecipe"],
            "skill": ["PotterySkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class BrickItem"],
            "block": ["REMOVE-CLASS", "public partial class BrickBlock"],
            "constructable": ["REMOVE-CONSTRUCTABLE", '[Tag("Constructable")]'],
        },
        r"Block\CopperPipe.cs": {
            "level": ["RequiresSkill(typeof(SmeltingSkill), 2)", "RequiresSkill(typeof(ConstructionSkill), 3)"],
            "displayName": ['Localizer.DoStr("Copper Pipe")', 'Localizer.DoStr("Builder Grade Copper Pipe")'],
            "class": ["CopperPipeRecipe", "ConstructionCopperPipeRecipe"],
            "skill": ["SmeltingSkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class CopperPipeItem"],
            "block": ["REMOVE-CLASS", "public partial class CopperPipeBlock"],
        },
        r"Item\Dowel.cs": {
            "level": ["RequiresSkill(typeof(LoggingSkill), 1)", "RequiresSkill(typeof(ConstructionSkill), 1)"],
            "displayName": ['Localizer.DoStr("Dowel")', 'Localizer.DoStr("Builder Grade Dowel")'],
            "class": ["DowelRecipe", "ConstructionDowelRecipe"],
            "skill": ["LoggingSkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class DowelItem"],
        },
        r"Block\Glass.cs": {
            "level": ["RequiresSkill(typeof(GlassworkingSkill), 1)", "RequiresSkill(typeof(ConstructionSkill), 2)"],
            "displayName": ['Localizer.DoStr("Glass")', 'Localizer.DoStr("Builder Grade Glass")'],
            "class": ["GlassRecipe", "ConstructionGlassRecipe"],
            "skill": ["GlassworkingSkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class GlassItem"],
            "block": ["REMOVE-CLASS", "public partial class GlassBlock"],
            "constructable": ["REMOVE-CONSTRUCTABLE", '[Tag("Constructable")]'],
        },
        r"Block\HewnLog.cs": {
            "level": ["RequiresSkill(typeof(LoggingSkill), 1)", "RequiresSkill(typeof(ConstructionSkill), 1)"],
            "displayName": ['Localizer.DoStr("Hewn Log")', 'Localizer.DoStr("Builder Grade Hewn Log")'],
            "class": ["HewnLogRecipe", "ConstructionHewnLogRecipe"],
            "skill": ["LoggingSkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class HewnLogItem"],
            "block": ["REMOVE-CLASS", "public partial class HewnLogBlock"],
            "constructable": ["REMOVE-CONSTRUCTABLE", '[Tag("HewnLog")]'],
        },
        r"Block\IronPipe.cs": {
            "level": ["RequiresSkill(typeof(SmeltingSkill), 1)", "RequiresSkill(typeof(ConstructionSkill), 3)"],
            "displayName": ['Localizer.DoStr("Iron Pipe")', 'Localizer.DoStr("Builder Grade Iron Pipe")'],
            "class": ["IronPipeRecipe", "ConstructionIronPipeRecipe"],
            "skill": ["SmeltingSkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class IronPipeItem"],
            "block": ["REMOVE-CLASS", "public partial class IronPipeBlock"],
        },
        r"Block\Lumber.cs": {
            "level": ["RequiresSkill(typeof(CarpentrySkill), 1)", "RequiresSkill(typeof(ConstructionSkill), 2)"],
            "displayName": ['Localizer.DoStr("Lumber")', 'Localizer.DoStr("Builder Grade Lumber")'],
            "class": ["LumberRecipe", "ConstructionLumberRecipe"],
            "skill": ["CarpentrySkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class LumberItem"],
            "block": ["REMOVE-CLASS", "public partial class LumberBlock"],
            "constructable": ["REMOVE-CONSTRUCTABLE", '[Tag("Lumber")]'],
        },
        r"Block\MortaredStone.cs": {
            "level": ["RequiresSkill(typeof(MasonrySkill), 1)", "RequiresSkill(typeof(ConstructionSkill), 1)"],
            "displayName": ['Localizer.DoStr("Mortared Stone")', 'Localizer.DoStr("Builder Grade Mortared Stone")'],
            "class": ["MortaredStoneRecipe", "ConstructionMortaredStoneRecipe"],
            "skill": ["MasonrySkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class MortaredStoneItem"],
            "block": ["REMOVE-CLASS", "public partial class MortaredStoneBlock"],
            "constructable": ["REMOVE-CONSTRUCTABLE", '[Tag("MortaredStone")]'],
        },
        r"Item\WetBrick.cs": {
            "level": ["RequiresSkill(typeof(PotterySkill), 1)", "RequiresSkill(typeof(ConstructionSkill), 2)"],
            "displayName": ['Localizer.DoStr("Wet Brick")', 'Localizer.DoStr("Builder Grade Wet Brick")'],
            "class": ["WetBrickRecipe", "ConstructionWetBrickRecipe"],
            "skill": ["PotterySkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class WetBrickItem"],
        },
    }

    process_recipes(recipes_changes, BUNWULF_CONSTRUCTION_PATH)
