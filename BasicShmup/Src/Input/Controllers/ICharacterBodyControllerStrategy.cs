using BasicShmup.Dynamics;
using Godot;

namespace BasicShmup.Input.Controllers;

public interface ICharacterBodyControllerStrategy
{
    void Move(CharacterBody2D body, Speed speed);
}