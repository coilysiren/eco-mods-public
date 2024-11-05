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


def handleRemoveReadonly(func, path, _):
    if not os.access(path, os.W_OK):
        os.chmod(path, stat.S_IWUSR)
        func(path)
    else:
        raise Exception("could not handle path")


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


@invoke.task
def bunwulf_construction(_: invoke.Context):
    recipes_changes = {
        r"Block\Brick.cs": {
            "displayName": ['Localizer.DoStr("Brick")', 'Localizer.DoStr("Builder Grade Brick")'],
            "class": ["BrickRecipe", "ConstructionBrickRecipe"],
            "skill": ["MasonrySkill", "ConstructionSkill"],
        },
        r"Block\CopperPipe.cs": {
            "displayName": ['Localizer.DoStr("Copper Pipe")', 'Localizer.DoStr("Builder Grade Copper Pipe")'],
            "class": ["CopperPipeRecipe", "ConstructionCopperPipeRecipe"],
            "skill": ["SmeltingSkill", "ConstructionSkill"],
        },
        r"Item\Dowel.cs": {
            "displayName": ['Localizer.DoStr("Dowel")', 'Localizer.DoStr("Builder Grade Dowel")'],
            "class": ["DowelRecipe", "ConstructionDowelRecipe"],
            "skill": ["LoggingSkill", "ConstructionSkill"],
        },
        r"Block\Glass.cs": {
            "displayName": ['Localizer.DoStr("Glass")', 'Localizer.DoStr("Builder Grade Glass")'],
            "class": ["GlassRecipe", "ConstructionGlassRecipe"],
            "skill": ["GlassworkingSkill", "ConstructionSkill"],
        },
        r"Block\HewnLog.cs": {
            "displayName": ['Localizer.DoStr("Hewn Log")', 'Localizer.DoStr("Builder Grade Hewn Log")'],
            "class": ["HewnLogRecipe", "ConstructionHewnLogRecipe"],
            "skill": ["LoggingSkill", "ConstructionSkill"],
        },
        r"Block\IronPipe.cs": {
            "displayName": ['Localizer.DoStr("Iron Pipe")', 'Localizer.DoStr("Builder Grade Iron Pipe")'],
            "class": ["IronPipeRecipe", "ConstructionIronPipeRecipe"],
            "skill": ["SmeltingSkill", "ConstructionSkill"],
        },
        r"Block\Lumber.cs": {
            "displayName": ['Localizer.DoStr("Lumber")', 'Localizer.DoStr("Builder Grade Lumber")'],
            "class": ["LumberRecipe", "ConstructionLumberRecipe"],
            "skill": ["LoggingSkill", "ConstructionSkill"],
        },
        r"Block\MortaredStone.cs": {
            "displayName": ['Localizer.DoStr("Mortared Stone")', 'Localizer.DoStr("Builder Grade Mortared Stone")'],
            "class": ["MortaredStoneRecipe", "ConstructionMortaredStoneRecipe"],
            "skill": ["MasonrySkill", "ConstructionSkill"],
        },
        r"Item\WetBrick.cs": {
            "displayName": ['Localizer.DoStr("Wet Brick")', 'Localizer.DoStr("Builder Grade Wet Brick")'],
            "class": ["WetBrickRecipe", "ConstructionWetBrickRecipe"],
            "skill": ["MasonrySkill", "ConstructionSkill"],
        },
    }

    for file, changes in recipes_changes.items():
        print(f"Reading {file}")
        with open(os.path.join(AUTOGEN_PATH, file), "r", encoding="utf-8") as f:
            recipe = f.read()

        for key, values in changes.items():
            print(f"\tReplacing {key}")
            recipe = recipe.replace(values[0], values[1])

        print(f"Writing {file}")
        folder = file.split("\\", maxsplit=1)[0]
        os.makedirs(os.path.join(BUNWULF_CONSTRUCTION_PATH, folder), exist_ok=True)
        with open(os.path.join(BUNWULF_CONSTRUCTION_PATH, file), "w", encoding="utf-8") as f:
            f.write(recipe)
