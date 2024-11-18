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
def bunwulf_educational(ctx: invoke.Context):
    # tiny redirection layer for when I forget that I rewrote this script in c#
    ctx.run("dotnet run -- BunWulfEducational", echo=True)


@invoke.task
def bunwulf_structural(_: invoke.Context):
    with open("recipes.yml", "r", encoding="utf-8") as recipes:
        recipe_data = yaml.safe_load(recipes)["BunWulfStructural"]

    util.process_recipes(recipe_data, os.path.join(USERCODE_PATH, "BunWulfStructural", "Recipes"))
