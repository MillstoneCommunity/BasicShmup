# Contribution guide

## Requirements

- [Dotnet 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Godot 4.5.X (dotnet)](https://godotengine.org/download)

  *It is recommented the same Godot version as the [build image](./Infrastructure/Building/Dockerfile).
  Currently, that is Godot 4.5.1.*

## Principles

At the moment I do not know how many people will want to contribute, nor what the background of a potential contributor
will be.
For this reason the following two principles should be in focus when developing this project.

1. **Editor integration:** The code should integrate with the Godot editor, such that configuration can be done in the
   editor, without have to know how do program.
2. **Durability:**
   The code should be written in away, that makes it hard to break the game, though mis-configuration.
   Changes in the godot project, that may break game behaviour, should be caught at game startup, *not* when the
   mis-configured entity is spawned.

*Note*: The current integration with the Godot input system does not follow the Durability principle.
It is a quick implementation, that should be replaced later.

## Issues/Tasks

The project uses [Github issues](https://github.com/MillstoneCommunity/BasicShmup/issues) to tack tasks.
If you wish to contribute grab for at
[`task`](https://github.com/MillstoneCommunity/BasicShmup/issues?q=is%3Aissue%20state%3Aopen%20type%3ATask), and start
developing/designing.
