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


@invoke.task
def bunwulf_construction(_: invoke.Context):
    recipes_changes = {
        r"Block\Brick.cs": {
            "displayName": ['Localizer.DoStr("Brick")', 'Localizer.DoStr("Builder Grade Brick")'],
            "class": ["BrickRecipe", "ConstructionBrickRecipe"],
            "skill": ["MasonrySkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class BrickItem"],
            "block": ["REMOVE-CLASS", "public partial class BrickBlock"],
            "constructable": ["REMOVE-CONSTRUCTABLE", '[Tag("Constructable")]'],
        },
        r"Block\CopperPipe.cs": {
            "displayName": ['Localizer.DoStr("Copper Pipe")', 'Localizer.DoStr("Builder Grade Copper Pipe")'],
            "class": ["CopperPipeRecipe", "ConstructionCopperPipeRecipe"],
            "skill": ["SmeltingSkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class CopperPipeItem"],
            "block": ["REMOVE-CLASS", "public partial class CopperPipeBlock"],
        },
        r"Item\Dowel.cs": {
            "displayName": ['Localizer.DoStr("Dowel")', 'Localizer.DoStr("Builder Grade Dowel")'],
            "class": ["DowelRecipe", "ConstructionDowelRecipe"],
            "skill": ["LoggingSkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class DowelItem"],
        },
        r"Block\Glass.cs": {
            "displayName": ['Localizer.DoStr("Glass")', 'Localizer.DoStr("Builder Grade Glass")'],
            "class": ["GlassRecipe", "ConstructionGlassRecipe"],
            "skill": ["GlassworkingSkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class GlassItem"],
            "block": ["REMOVE-CLASS", "public partial class GlassBlock"],
            "constructable": ["REMOVE-CONSTRUCTABLE", '[Tag("Constructable")]'],
        },
        r"Block\HewnLog.cs": {
            "displayName": ['Localizer.DoStr("Hewn Log")', 'Localizer.DoStr("Builder Grade Hewn Log")'],
            "class": ["HewnLogRecipe", "ConstructionHewnLogRecipe"],
            "skill": ["LoggingSkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class HewnLogItem"],
            "block": ["REMOVE-CLASS", "public partial class HewnLogBlock"],
            "constructable": ["REMOVE-CONSTRUCTABLE", '[Tag("HewnLog")]'],
        },
        r"Block\IronPipe.cs": {
            "displayName": ['Localizer.DoStr("Iron Pipe")', 'Localizer.DoStr("Builder Grade Iron Pipe")'],
            "class": ["IronPipeRecipe", "ConstructionIronPipeRecipe"],
            "skill": ["SmeltingSkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class IronPipeItem"],
            "block": ["REMOVE-CLASS", "public partial class IronPipeBlock"],
        },
        r"Block\Lumber.cs": {
            "displayName": ['Localizer.DoStr("Lumber")', 'Localizer.DoStr("Builder Grade Lumber")'],
            "class": ["LumberRecipe", "ConstructionLumberRecipe"],
            "skill": ["LoggingSkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class LumberItem"],
            "block": ["REMOVE-CLASS", "public partial class LumberBlock"],
            "constructable": ["REMOVE-CONSTRUCTABLE", '[Tag("Lumber")]'],
        },
        r"Block\MortaredStone.cs": {
            "displayName": ['Localizer.DoStr("Mortared Stone")', 'Localizer.DoStr("Builder Grade Mortared Stone")'],
            "class": ["MortaredStoneRecipe", "ConstructionMortaredStoneRecipe"],
            "skill": ["MasonrySkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class MortaredStoneItem"],
            "block": ["REMOVE-CLASS", "public partial class MortaredStoneBlock"],
            "constructable": ["REMOVE-CONSTRUCTABLE", '[Tag("MortaredStone")]'],
        },
        r"Item\WetBrick.cs": {
            "displayName": ['Localizer.DoStr("Wet Brick")', 'Localizer.DoStr("Builder Grade Wet Brick")'],
            "class": ["WetBrickRecipe", "ConstructionWetBrickRecipe"],
            "skill": ["MasonrySkill", "ConstructionSkill"],
            "item": ["REMOVE-CLASS", "public partial class WetBrickItem"],
        },
    }

    for file, changes in recipes_changes.items():
        print(f"Reading {file}")
        with open(os.path.join(AUTOGEN_PATH, file), "r", encoding="utf-8") as f:
            recipe_file = f.read()

        for key, values in changes.items():
            recipe_lines = recipe_file.split("\n")

            # This is a special case where we need to remove the item from the recipe
            # It's a little complex, so read it one step at a time.
            if values[0] == "REMOVE-CLASS":
                print(f'\tRemoving "{values[1]}" from recipe')

                # Step 1: Identify the line where the item starts.
                class_line = 0
                for line, value in enumerate(recipe_lines):
                    if values[1] in value:
                        class_line = line
                        break
                if class_line == 0:
                    raise TextProcessingException(f"\t\tCouldn't find {values[1]} in {file}")
                print(f'\t\tFound "{values[1]}" at line {class_line}')

                # Step 2: Find all of the lines with the serialized attribute.
                serialized_lines = []
                for line, value in enumerate(recipe_lines[:class_line]):
                    if value.strip().startswith("[Serialized]"):
                        serialized_lines.append(line)
                # This should never happen, but just in case.
                if not serialized_lines:
                    raise TextProcessingException(f"\t\tCouldn't find [Serialized] in {file}")
                serialized_line = serialized_lines[-1]
                print(f"\t\t[Serialized] attribute line: {serialized_line}")

                # Step 3: Find the item start column by looking for the first { that
                # occurs after the serialized attribute.
                item_start_line = 0
                for line in range(serialized_line, len(recipe_lines)):
                    if "{" in recipe_lines[line]:
                        item_start_line = line
                        break
                if item_start_line == 0:
                    raise TextProcessingException(f"\t\tCouldn't find item start in {file}")
                print(f"\t\tClass body starts at line {item_start_line}")

                # Step 4: Find the line where the item ends, includes
                # Step 4a, 4b, and 4c.

                # Step 4b:
                # If there's a { on our class's line, then our item end line is aligned
                # with the start our class's line.
                if recipe_lines[class_line].strip().endswith("{"):
                    item_start_column = recipe_lines[class_line].index(values[1])

                # Step 4a:
                # Otherwise, the item end line is the first line that has a } at the same
                # column as the first { in our class's line.
                else:
                    item_end_line = 0
                    item_column_line = recipe_lines[item_start_line]
                    item_start_column = item_column_line.index("{")

                # Step 4c:
                # Find the line where the item ends.
                for line in range(class_line, len(recipe_lines)):
                    if recipe_lines[line].strip().startswith("}"):
                        if recipe_lines[line].index("}") == item_start_column:
                            item_end_line = line
                            break
                if item_end_line == 0:
                    raise TextProcessingException(f"\t\tCouldn't find item end in {file}")
                print(f"\t\tItem ends at line {item_end_line}")

                # Step 5: Remove the lines from item_start_line to item_end_line.
                print(f"\t\tRemoving lines {serialized_line} to {item_end_line}")
                del recipe_lines[serialized_line : item_end_line + 1]

                # Step -1: Join the lines back together to get a single string.
                recipe_file = "\n".join(recipe_lines)

            # This is a special case where we need to remove the item from the recipe
            # It's a little complex, so read it one step at a time.
            if values[0] == "REMOVE-CONSTRUCTABLE":
                print(f"\tRemoving {values[1]} from recipe")

                # Step 1: Identify the first Tag line.
                constructable_line = 0
                for line, value in enumerate(recipe_lines):
                    if values[1] in value:
                        constructable_line = line
                        break
                if constructable_line == 0:
                    raise TextProcessingException(f"\t\tCouldn't find {values[1]} in {file}")

                # Step 2: This is the end of the file, so remove the lines from constructable_line to the end.
                print(f"\t\tRemoving lines {constructable_line} to {len(recipe_lines) - 2}")
                del recipe_lines[constructable_line : len(recipe_lines) - 2]

                # Step -1: Join the lines back together to get a single string.
                recipe_file = "\n".join(recipe_lines)

            else:
                print(f"\tReplacing {key}")
                recipe_file = recipe_file.replace(values[0], values[1])

        print(f"\tWriting {file}")
        folder = file.split("\\", maxsplit=1)[0]
        os.makedirs(os.path.join(BUNWULF_CONSTRUCTION_PATH, folder), exist_ok=True)
        with open(os.path.join(BUNWULF_CONSTRUCTION_PATH, file), "w", encoding="utf-8") as f:
            f.write(recipe_file)
