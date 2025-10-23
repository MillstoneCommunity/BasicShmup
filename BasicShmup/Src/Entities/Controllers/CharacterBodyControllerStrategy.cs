using BasicShmup.Dynamics;
using BasicShmup.Input;
using Godot;

namespace BasicShmup.Entities.Controllers;

public class CharacterBodyControllerStrategy : ICharacterBodyControllerStrategy
{
    public void Move(CharacterBody2D body, Speed speed)
    {
        var movement = speed * InputActions.GetClampedMovement();
        body.Velocity = movement;
        body.MoveAndSlide();
    }
}