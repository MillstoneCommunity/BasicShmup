using BasicShmup.Dynamics;
using Godot;

namespace BasicShmup.Entities.Controllers;

public interface ICharacterBodyControllerStrategy
{
    void Move(CharacterBody2D body, Speed speed);
}