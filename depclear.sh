#!/bin/bash
DEP_DIR="Dependencies"
ASSETS_DIR="$(pwd)/Disfunction/Assets"
DEST_DIR="$ASSETS_DIR/$DEP_DIR"

HAS_META="false"

if [ -d "$DEST_DIR" ]; then
    sudo rm -rf "$DEST_DIR"/*
    echo "Cleared all dependencies ..."
else
    echo "Nothing to clear ..."
fi
