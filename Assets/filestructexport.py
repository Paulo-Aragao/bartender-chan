#!/usr/bin/env python3
import os

# Conjuntos de arquivos e extensões para ignorar
IGNORED_EXTENSIONS = {'.meta'}
IGNORED_FILES = {'.DS_Store', 'Thumbs.db'}
IGNORED_PREFIXES = {"~"}  # Arquivos de backup que começam com "~"

def should_ignore(entry):
    """
    Retorna True se o arquivo/diretório deve ser ignorado.
    """
    _, ext = os.path.splitext(entry)
    if ext in IGNORED_EXTENSIONS:
        return True
    if entry in IGNORED_FILES:
        return True
    for prefix in IGNORED_PREFIXES:
        if entry.startswith(prefix):
            return True
    return False

def generate_full_tree(dir_path, prefix=""):
    """
    Gera uma string com a estrutura completa de árvore (pastas e arquivos),
    ignorando itens irrelevantes.
    """
    tree_str = ""
    entries = sorted(os.listdir(dir_path))
    entries = [entry for entry in entries if not should_ignore(entry)]
    count = len(entries)
    for i, entry in enumerate(entries):
        path = os.path.join(dir_path, entry)
        connector = "├── " if i < count - 1 else "└── "
        if os.path.isdir(path):
            tree_str += f"{prefix}{connector}{entry}/\n"
            extension = "│   " if i < count - 1 else "    "
            tree_str += generate_full_tree(path, prefix + extension)
        else:
            tree_str += f"{prefix}{connector}{entry}\n"
    return tree_str

def generate_directory_tree(dir_path, prefix=""):
    """
    Gera uma string com a estrutura de árvore apenas das pastas,
    ignorando arquivos irrelevantes.
    """
    tree_str = ""
    entries = sorted(os.listdir(dir_path))
    # Filtra apenas os diretórios que não devem ser ignorados
    entries = [entry for entry in entries if not should_ignore(entry) and os.path.isdir(os.path.join(dir_path, entry))]
    count = len(entries)
    for i, entry in enumerate(entries):
        path = os.path.join(dir_path, entry)
        connector = "├── " if i < count - 1 else "└── "
        tree_str += f"{prefix}{connector}{entry}/\n"
        extension = "│   " if i < count - 1 else "    "
        tree_str += generate_directory_tree(path, prefix + extension)
    return tree_str

def main():
    # Diretório atual (raiz da árvore)
    root_dir = os.getcwd()
    root_name = os.path.basename(root_dir)
    
    full_tree = f"{root_name}/\n" + generate_full_tree(root_dir)
    dir_tree = f"{root_name}/\n" + generate_directory_tree(root_dir)
    
    # Define os arquivos de saída
    output_file_full = "directory_tree_full.txt"
    output_file_dirs = "directory_tree_dirs.txt"
    
    with open(output_file_full, "w", encoding="utf-8") as file:
        file.write(full_tree)
    
    with open(output_file_dirs, "w", encoding="utf-8") as file:
        file.write(dir_tree)
    
    print(f"Arquivos gerados com sucesso:\n- {output_file_full}\n- {output_file_dirs}")

if __name__ == "__main__":
    main()
