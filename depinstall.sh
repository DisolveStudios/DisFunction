#!/bin/bash
ASSETS_DIR="$(pwd)/Disfunction/Assets/Dependencies/"
PROP_MODELS_HTTP="https://github.com/DisolveStudios/PropModels.git"
PROP_MODELS_DIR="PropModels"

if [ -d "$ASSETS_DIR/$PROP_MODELS_DIR" ]; then
    echo "Directory $PROP_MODELS_DIR already exists. Pulling latest changes."
    cd "$PROP_MODELS_DIR" && git pull
else
    mkdir "$ASSETS_DIR"
    echo "Cloning repository..."
    git clone "$PROP_MODELS_HTTP"
    mv "$PROP_MODELS_DIR" "$ASSETS_DIR"
fi
