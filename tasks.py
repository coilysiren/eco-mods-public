import os
import shutil
import stat

import invoke
import yaml

import util


USERNAME = os.getenv("USERNAME", "")
USERCODE_PATH = os.path.join("C:\\", "Users", USERNAME, "projects", "eco-mods-public", "Mods", "UserCode")

BUNWULF_CONSTRUCTION_PATH = os.path.join(
    "C:\\", "Users", USERNAME, "projects", "eco-mods-public", "Mods", "UserCode", "BunWulfConstruction", "Recipes"
)

BUNWULF_EDUCATIONAL_PATH = os.path.join(
    "C:\\", "Users", USERNAME, "projects", "eco-mods-public", "Mods", "UserCode", "BunWulfEducational", "Recipes"
)


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


@invoke.task
def push_asset(ctx: invoke.Context, mod):
    remote_path = "/home/kai/.local/share/Steam/steamapps/common/Eco/Eco_Data/Server"
    ctx.run(f"scp {mod}.zip kai@kai-server:{remote_path}")
    ctx.run(f'ssh -t kai@kai-server "cd {remote_path} && unzip -o {mod}.zip"')


#######################
# SPECIALITY SPECIFIC #
#######################


@invoke.task
def bunwulf_agricultural(_: invoke.Context):
    with open("recipes.yml", "r", encoding="utf-8") as recipes:
        recipe_data = yaml.safe_load(recipes)["BunWulfAgricultural"]

    recipes_templates = {
        template: {
            "plant": template.split("\\")[-1].split(".")[0],
            "species": template.split("\\")[-1].split(".")[0] + "Species",
        }
        for template in recipe_data["recipes"]
    }

    util.template_recipes(
        recipes_templates, recipe_data["template"], os.path.join(USERCODE_PATH, "BunWulfAgricultural", "Recipes")
    )


@invoke.task
def bunwulf_educational(_: invoke.Context):
    recipe_changes = {
        r"Item\GeologyResearchPaperBasic.cs": {
            "item": ["REMOVE-CLASS", "class GeologyResearchPaperBasicItem"],
            "class": ["GeologyResearchPaperBasicRecipe", "LibrarianGeologyResearchPaperBasicRecipe"],
            "level": ["RequiresSkill(typeof(MiningSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 1)"],
            "displayName": [
                "Geology Research Paper",
                "Librarian Geology Research Paper",
            ],
            "skill": ["MiningSkill", "LibrarianSkill"],
        },
        r"Item\CulinaryResearchPaperBasic.cs": {
            "item": ["REMOVE-CLASS", " class CulinaryResearchPaperBasicItem"],
            "class": ["CulinaryResearchPaperBasicRecipe", "LibrarianCulinaryResearchPaperBasicRecipe"],
            "level": ["RequiresSkill(typeof(CampfireCookingSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 1)"],
            "displayName": [
                "Culinary Research Paper",
                "Librarian Culinary Research Paper",
            ],
            "skill": ["CampfireCookingSkill", "LibrarianSkill"],
        },
        r"Item\GatheringResearchPaperBasic.cs": {
            "item": ["REMOVE-CLASS", "class GatheringResearchPaperBasicItem"],
            "class": ["GatheringResearchPaperBasicRecipe", "LibrarianGatheringResearchPaperBasicRecipe"],
            "level": ["RequiresSkill(typeof(GatheringSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 1)"],
            "displayName": [
                "Gathering Research Paper",
                "Librarian Gathering Research Paper",
            ],
            "skill": ["GatheringSkill", "LibrarianSkill"],
        },
        r"Item\DendrologyResearchPaperBasic.cs": {
            "item": ["REMOVE-CLASS", "class DendrologyResearchPaperBasicItem"],
            "class": ["DendrologyResearchPaperBasicRecipe", "LibrarianDendrologyResearchPaperBasicRecipe"],
            "level": ["RequiresSkill(typeof(LoggingSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 1)"],
            "displayName": [
                "Dendrology Research Paper",
                "Librarian Dendrology Research Paper",
            ],
            "skill": ["LoggingSkill", "LibrarianSkill"],
        },
        r"Item\MetallurgyResearchPaperBasic.cs": {
            "item": ["REMOVE-CLASS", "class MetallurgyResearchPaperBasicItem"],
            "class": ["MetallurgyResearchPaperBasicRecipe", "LibrarianMetallurgyResearchPaperBasicRecipe"],
            "level": ["RequiresSkill(typeof(MiningSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 1)"],
            "displayName": [
                "Metallurgy Research Paper",
                "Librarian Metallurgy Research Paper",
            ],
            "skill": ["MiningSkill", "LibrarianSkill"],
        },
        r"Recipe\CulinaryResearchPaperBasicFish.cs": {
            "class": ["CulinaryResearchPaperBasicFishRecipe", "LibrarianCulinaryResearchPaperBasicFishRecipe"],
            "level": ["RequiresSkill(typeof(HuntingSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 1)"],
            "displayName": [
                "Culinary Research Paper Basic Fish",
                "Librarian Culinary Research Paper Basic Fish",
            ],
            "skill": ["HuntingSkill", "LibrarianSkill"],
        },
        r"Recipe\CulinaryResearchPaperBasicMeat.cs": {
            "class": ["CulinaryResearchPaperBasicMeatRecipe", "LibrarianCulinaryResearchPaperBasicMeatRecipe"],
            "level": ["RequiresSkill(typeof(HuntingSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 1)"],
            "displayName": [
                "Culinary Research Paper Basic Meat",
                "Librarian Culinary Research Paper Basic Meat",
            ],
            "skill": ["HuntingSkill", "LibrarianSkill"],
        },
        r"Item\GeologyResearchPaperAdvanced.cs": {
            "item": ["REMOVE-CLASS", "class GeologyResearchPaperAdvancedItem"],
            "class": ["GeologyResearchPaperAdvancedRecipe", "LibrarianGeologyResearchPaperAdvancedRecipe"],
            "level": ["RequiresSkill(typeof(MasonrySkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 2)"],
            "displayName": [
                "Geology Research Paper Advanced",
                "Librarian Geology Research Paper Advanced",
            ],
            "skill": ["MasonrySkill", "LibrarianSkill"],
        },
        r"Item\CulinaryResearchPaperAdvanced.cs": {
            "item": ["REMOVE-CLASS", "class CulinaryResearchPaperAdvancedItem"],
            "class": ["CulinaryResearchPaperAdvancedRecipe", "LibrarianCulinaryResearchPaperAdvancedRecipe"],
            "level": ["RequiresSkill(typeof(CookingSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 2)"],
            "displayName": [
                "Culinary Research Paper Advanced",
                "Librarian Culinary Research Paper Advanced",
            ],
            "skill": ["CookingSkill", "LibrarianSkill"],
        },
        r"Item\DendrologyResearchPaperAdvanced.cs": {
            "item": ["REMOVE-CLASS", "class DendrologyResearchPaperAdvancedItem"],
            "class": ["DendrologyResearchPaperAdvancedRecipe", "LibrarianDendrologyResearchPaperAdvancedRecipe"],
            "level": ["RequiresSkill(typeof(CarpentrySkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 2)"],
            "displayName": [
                "Dendrology Research Paper Advanced",
                "Librarian Dendrology Research Paper Advanced",
            ],
            "skill": ["CarpentrySkill", "LibrarianSkill"],
        },
        r"Item\MetallurgyResearchPaperAdvanced.cs": {
            "item": ["REMOVE-CLASS", "class MetallurgyResearchPaperAdvancedItem"],
            "class": ["MetallurgyResearchPaperAdvancedRecipe", "LibrarianMetallurgyResearchPaperAdvancedRecipe"],
            "level": ["RequiresSkill(typeof(SmeltingSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 2)"],
            "displayName": [
                "Metallurgy Research Paper Advanced",
                "Librarian Metallurgy Research Paper Advanced",
            ],
            "skill": ["SmeltingSkill", "LibrarianSkill"],
        },
        r"Item\AgricultureResearchPaperAdvanced.cs": {
            "item": ["REMOVE-CLASS", "class AgricultureResearchPaperAdvancedItem"],
            "class": ["AgricultureResearchPaperAdvancedRecipe", "LibrarianAgricultureResearchPaperAdvancedRecipe"],
            "level": ["RequiresSkill(typeof(FarmingSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 2)"],
            "displayName": [
                "Agriculture Research Paper Advanced",
                "Librarian Agriculture Research Paper Advanced",
            ],
            "skill": ["FarmingSkill", "LibrarianSkill"],
        },
        r"Item\EngineeringResearchPaperAdvanced.cs": {
            "item": ["REMOVE-CLASS", "class EngineeringResearchPaperAdvancedItem"],
            "class": ["EngineeringResearchPaperAdvancedRecipe", "LibrarianEngineeringResearchPaperAdvancedRecipe"],
            "level": ["RequiresSkill(typeof(BasicEngineeringSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 2)"],
            "displayName": [
                "Engineering Research Paper Advanced",
                "Librarian Engineering Research Paper Advanced",
            ],
            "skill": ["BasicEngineeringSkill", "LibrarianSkill"],
        },
        r"Recipe\CulinaryResearchPaperAdvancedMeat.cs": {
            "class": ["CulinaryResearchPaperAdvancedMeatRecipe", "LibrarianCulinaryResearchPaperAdvancedMeatRecipe"],
            "level": ["RequiresSkill(typeof(BakingSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 2)"],
            "displayName": [
                "Culinary Research Paper Advanced Meat",
                "Librarian Culinary Research Paper Advanced Meat",
            ],
            "skill": ["BakingSkill", "LibrarianSkill"],
        },
        r"Item\GeologyResearchPaperModern.cs": {
            "item": ["REMOVE-CLASS", "class GeologyResearchPaperModernItem"],
            "class": ["GeologyResearchPaperModernRecipe", "LibrarianGeologyResearchPaperModernRecipe"],
            "level": ["RequiresSkill(typeof(PotterySkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 3)"],
            "displayName": [
                "Geology Research Paper Modern",
                "Librarian Geology Research Paper Modern",
            ],
            "skill": ["PotterySkill", "LibrarianSkill"],
        },
        r"Item\CulinaryResearchPaperModern.cs": {
            "item": ["REMOVE-CLASS", "class CulinaryResearchPaperModernItem"],
            "class": ["CulinaryResearchPaperModernRecipe", "LibrarianCulinaryResearchPaperModernRecipe"],
            "level": ["RequiresSkill(typeof(AdvancedCookingSkill), 2)", "RequiresSkill(typeof(LibrarianSkill), 3)"],
            "displayName": [
                "Culinary Research Paper Modern",
                "Librarian Culinary Research Paper Modern",
            ],
            "skill": ["AdvancedCookingSkill", "LibrarianSkill"],
        },
        r"Item\DendrologyResearchPaperModern.cs": {
            "item": ["REMOVE-CLASS", "class DendrologyResearchPaperModernItem"],
            "class": ["DendrologyResearchPaperModernRecipe", "LibrarianDendrologyResearchPaperModernRecipe"],
            "level": ["RequiresSkill(typeof(CarpentrySkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 3)"],
            "displayName": [
                "Dendrology Research Paper Modern",
                "Librarian Dendrology Research Paper Modern",
            ],
            "skill": ["CarpentrySkill", "LibrarianSkill"],
        },
        r"Item\MetallurgyResearchPaperModern.cs": {
            "item": ["REMOVE-CLASS", "class MetallurgyResearchPaperModernItem"],
            "class": ["MetallurgyResearchPaperModernRecipe", "LibrarianMetallurgyResearchPaperModernRecipe"],
            "level": ["RequiresSkill(typeof(AdvancedSmeltingSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 3)"],
            "displayName": [
                "Metallurgy Research Paper Modern",
                "Librarian Metallurgy Research Paper Modern",
            ],
            "skill": ["AdvancedSmeltingSkill", "LibrarianSkill"],
        },
        r"Item\AgricultureResearchPaperModern.cs": {
            "item": ["REMOVE-CLASS", "class AgricultureResearchPaperModernItem"],
            "class": ["AgricultureResearchPaperModernRecipe", "LibrarianAgricultureResearchPaperModernRecipe"],
            "level": ["RequiresSkill(typeof(FertilizersSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 3)"],
            "displayName": [
                "Agriculture Research Paper Modern",
                "Librarian Agriculture Research Paper Modern",
            ],
            "skill": ["FertilizersSkill", "LibrarianSkill"],
        },
        r"Item\EngineeringResearchPaperModern.cs": {
            "item": ["REMOVE-CLASS", "class EngineeringResearchPaperModernItem"],
            "class": ["EngineeringResearchPaperModernRecipe", "LibrarianEngineeringResearchPaperModernRecipe"],
            "level": ["RequiresSkill(typeof(MechanicsSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 3)"],
            "displayName": [
                "Engineering Research Paper Modern",
                "Librarian Engineering Research Paper Modern",
            ],
            "skill": ["MechanicsSkill", "LibrarianSkill"],
        },
        r"Recipe\GeologyResearchPaperModernGlass.cs": {
            "class": ["GeologyResearchPaperModernGlassRecipe", "LibrarianGeologyResearchPaperModernGlassRecipe"],
            "level": ["RequiresSkill(typeof(GlassworkingSkill), 1)", "RequiresSkill(typeof(LibrarianSkill), 3)"],
            "displayName": [
                "Geology Research Paper Modern Glass",
                "Librarian Geology Research Paper Modern Glass",
            ],
            "skill": ["GlassworkingSkill", "LibrarianSkill"],
        },
    }

    util.process_recipes(recipe_changes, BUNWULF_EDUCATIONAL_PATH)


@invoke.task
def bunwulf_construction(_: invoke.Context):
    recipes_changes = {
        r"Block\Brick.cs": {
            "level": ["RequiresSkill(typeof(PotterySkill), 1)", "RequiresSkill(typeof(ConstructionSkill), 2)"],
            "displayName": ['Brick")', 'Builder Grade Brick")'],
            "class": ["BrickRecipe", "ConstructionBrickRecipe"],
            "skill": ["PotterySkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class BrickItem"],
            "block": ["REMOVE-CLASS", "public partial class BrickBlock"],
            "constructable": ["REMOVE-CONSTRUCTABLE", '[Tag("Constructable")]'],
        },
        r"Block\CopperPipe.cs": {
            "level": ["RequiresSkill(typeof(SmeltingSkill), 2)", "RequiresSkill(typeof(ConstructionSkill), 3)"],
            "displayName": ['Copper Pipe")', 'Builder Grade Copper Pipe")'],
            "class": ["CopperPipeRecipe", "ConstructionCopperPipeRecipe"],
            "skill": ["SmeltingSkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class CopperPipeItem"],
            "block": ["REMOVE-CLASS", "public partial class CopperPipeBlock"],
        },
        r"Item\Dowel.cs": {
            "level": ["RequiresSkill(typeof(LoggingSkill), 1)", "RequiresSkill(typeof(ConstructionSkill), 1)"],
            "displayName": ['Dowel")', 'Builder Grade Dowel")'],
            "class": ["DowelRecipe", "ConstructionDowelRecipe"],
            "skill": ["LoggingSkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class DowelItem"],
        },
        r"Block\Glass.cs": {
            "level": ["RequiresSkill(typeof(GlassworkingSkill), 1)", "RequiresSkill(typeof(ConstructionSkill), 2)"],
            "displayName": ['Glass")', 'Builder Grade Glass")'],
            "class": ["GlassRecipe", "ConstructionGlassRecipe"],
            "skill": ["GlassworkingSkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class GlassItem"],
            "block": ["REMOVE-CLASS", "public partial class GlassBlock"],
            "constructable": ["REMOVE-CONSTRUCTABLE", '[Tag("Constructable")]'],
        },
        r"Block\HewnLog.cs": {
            "level": ["RequiresSkill(typeof(LoggingSkill), 1)", "RequiresSkill(typeof(ConstructionSkill), 1)"],
            "displayName": ['Hewn Log")', 'Builder Grade Hewn Log")'],
            "class": ["HewnLogRecipe", "ConstructionHewnLogRecipe"],
            "skill": ["LoggingSkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class HewnLogItem"],
            "block": ["REMOVE-CLASS", "public partial class HewnLogBlock"],
            "constructable": ["REMOVE-CONSTRUCTABLE", '[Tag("HewnLog")]'],
        },
        r"Block\IronPipe.cs": {
            "level": ["RequiresSkill(typeof(SmeltingSkill), 1)", "RequiresSkill(typeof(ConstructionSkill), 3)"],
            "displayName": ['Iron Pipe")', 'Builder Grade Iron Pipe")'],
            "class": ["IronPipeRecipe", "ConstructionIronPipeRecipe"],
            "skill": ["SmeltingSkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class IronPipeItem"],
            "block": ["REMOVE-CLASS", "public partial class IronPipeBlock"],
        },
        r"Block\Lumber.cs": {
            "level": ["RequiresSkill(typeof(CarpentrySkill), 1)", "RequiresSkill(typeof(ConstructionSkill), 2)"],
            "displayName": ['Lumber")', 'Builder Grade Lumber")'],
            "class": ["LumberRecipe", "ConstructionLumberRecipe"],
            "skill": ["CarpentrySkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class LumberItem"],
            "block": ["REMOVE-CLASS", "public partial class LumberBlock"],
            "constructable": ["REMOVE-CONSTRUCTABLE", '[Tag("Lumber")]'],
        },
        r"Block\MortaredStone.cs": {
            "level": ["RequiresSkill(typeof(MasonrySkill), 1)", "RequiresSkill(typeof(ConstructionSkill), 1)"],
            "displayName": ['Mortared Stone")', 'Builder Grade Mortared Stone")'],
            "class": ["MortaredStoneRecipe", "ConstructionMortaredStoneRecipe"],
            "skill": ["MasonrySkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class MortaredStoneItem"],
            "block": ["REMOVE-CLASS", "public partial class MortaredStoneBlock"],
            "constructable": ["REMOVE-CONSTRUCTABLE", '[Tag("MortaredStone")]'],
        },
        r"Item\WetBrick.cs": {
            "level": ["RequiresSkill(typeof(PotterySkill), 1)", "RequiresSkill(typeof(ConstructionSkill), 2)"],
            "displayName": ['Wet Brick")', 'Builder Grade Wet Brick")'],
            "class": ["WetBrickRecipe", "ConstructionWetBrickRecipe"],
            "skill": ["PotterySkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class WetBrickItem"],
        },
    }

    util.process_recipes(recipes_changes, BUNWULF_CONSTRUCTION_PATH)
