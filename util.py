import re

import os
import jinja2


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


class TextProcessingException(Exception):
    pass


def process_recipes(recipes_changes, target_path):
    item_pattern = r"\s+(public partial class \w+Item)"

    for file, changes in recipes_changes.items():
        print(f"Reading {file}")
        with open(os.path.join(AUTOGEN_PATH, file), "r", encoding=",utf-8") as f:
            recipe_data = f.read()

        # Remove items, we never need to duplicate them.
        recipes_lines = recipe_data.split("\n")
        for line in recipes_lines:
            if match := re.match(item_pattern, line):
                recipe_data = remove_class(recipe_data, file, line.strip(), match.group(1))

        for key, configs in changes.items():
            if configs[0] == "REMOVE-CONSTRUCTABLE":
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


def remove_class(recipe_data, file, key, class_name):
    recipe_lines = recipe_data.split("\n")
    print(f'\tRemoving "{class_name}" from recipe')

    # Step 1: Identify the line where the class starts.
    class_start_line = 0
    for line, value in enumerate(recipe_lines):
        if class_name in value:
            class_start_line = line
            break
    if class_start_line == 0:
        raise TextProcessingException(f"\t\tCouldn't find {class_name} in {file}:{key}")
    # print(f'\t\tFound "{class_name}" at line {class_start_line}')

    # Step 2: Move class start line upwards if the class has any annotations.
    for index in range(class_start_line, 0, -1):
        line = recipe_lines[index].strip()
        if line.startswith("["):
            class_start_line = index
        elif class_name in line:
            continue
        else:
            break
    # print(f"\t\tMoved class start line to {class_start_line} due to annotations")

    # Step 3: Count brackets to find the end of the class.
    bracket_count = 0
    class_end_index = 0
    recipe_data = "\n".join(recipe_lines)
    class_start_index = recipe_data.find(class_name)
    backets_started = False
    for index in range(class_start_index, len(recipe_data)):
        char = recipe_data[index]
        if char == "{":
            bracket_count += 1
            backets_started = True
        elif char == "}":
            bracket_count -= 1
        elif backets_started and bracket_count == 0:
            class_end_index = index
            break
    if bracket_count != 0 or class_end_index == 0:
        raise TextProcessingException(f"\t\tCouldn't find end of class in {file}:{key}")
    class_end_line = recipe_data[:class_end_index].count("\n") + 1
    # print(f"\t\tclass end bracket found at line {class_end_line}")

    # Step 4: Translate the class end index to a line number, remove the lines.
    # print(f"\t\tRemoving lines {class_start_line} to {class_end_line}")
    recipe_lines = recipe_data.split("\n")
    del recipe_lines[class_start_line:class_end_line]

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


def template_recipes(recipes_templates, template, target_path):
    env = jinja2.Environment(loader=jinja2.FileSystemLoader("templates/"))
    print(f"Rendering templates for {template}")

    for path, values in recipes_templates.items():
        template = env.get_template(template)
        content = template.render(**values)

        with open(os.path.join(target_path, path), mode="w", encoding="utf-8") as recipe:
            recipe.write(content)
            print(f"\t {path}")
