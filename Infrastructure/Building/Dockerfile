FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

ARG GODOT_VERSION="4.4.1"

RUN apt update

FROM base AS godot-download

RUN apt install -y unzip

# Download godot
RUN curl -L -o godot.zip https://github.com/godotengine/godot/releases/download/${GODOT_VERSION}-stable/Godot_v${GODOT_VERSION}-stable_mono_linux_x86_64.zip
RUN unzip godot.zip

# Move godot executable to /godot/godot
RUN mv Godot_v${GODOT_VERSION}-stable_mono_linux_x86_64/ godot
RUN mv godot/Godot_v${GODOT_VERSION}-stable_mono_linux.x86_64 godot/godot

# Downlaod export templates to /templates
RUN curl -L -o templates.tpz https://github.com/godotengine/godot/releases/download/${GODOT_VERSION}-stable/Godot_v${GODOT_VERSION}-stable_mono_export_templates.tpz
RUN unzip templates.tpz

FROM base AS builder

COPY --from=godot-download ["godot/", "/usr/local/bin/godot/"]
COPY --from=godot-download ["templates/*", "/root/.local/share/godot/export_templates/${GODOT_VERSION}.stable.mono/"]

RUN apt install -y \
    libx11-dev \
    libfreetype6-dev \
    pkg-config

ENV PATH="/usr/local/bin/godot/:${PATH}"

FROM builder AS test-build
# todo move this stage to github action

WORKDIR /src
COPY . .

RUN dotnet test

RUN mkdir -p /build/linux
RUN mkdir -p /build/windows

RUN godot --headless --export-release Linux /build/linux/game.x86_64 ./BasicShmup/project.godot
RUN godot --headless --export-release Windows /build/windows/games.exe ./BasicShmup/project.godot
