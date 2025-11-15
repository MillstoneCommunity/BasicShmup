FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

ARG GODOT_VERSION="4.5.1"

RUN apt update

FROM base AS godot-download

RUN apt install -y unzip

# Download godot
RUN curl -L -o godot.zip https://github.com/godotengine/godot/releases/download/${GODOT_VERSION}-stable/Godot_v${GODOT_VERSION}-stable_mono_linux_x86_64.zip
RUN unzip godot.zip

# Move godot executable to /godot/godot
RUN mv Godot_v${GODOT_VERSION}-stable_mono_linux_x86_64/ /godot
RUN mv /godot/Godot_v${GODOT_VERSION}-stable_mono_linux.x86_64 /godot/godot

# Downlaod export templates to /godot/templates
RUN curl -L -o templates.tpz https://github.com/godotengine/godot/releases/download/${GODOT_VERSION}-stable/Godot_v${GODOT_VERSION}-stable_mono_export_templates.tpz
RUN mkdir -p /godot/export_templates/${GODOT_VERSION}.stable.mono/
RUN unzip -j -d /godot/export_templates/${GODOT_VERSION}.stable.mono/ templates.tpz

FROM base AS builder

RUN apt install -y \
    libx11-dev \
    libfreetype6-dev \
    pkg-config

COPY --from=godot-download ["godot/", "/godot/"]

ENV PATH="/godot:${PATH}"
